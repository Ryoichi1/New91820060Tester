using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Threading.Thread;
using static New91820060Tester.General;


namespace New91820060Tester
{
    /// <summary>
    /// 
    /// </summary>
    public static class PT100チェック
    {
        const int WAIT = 20000;

        public enum PT_MODE { PT62, PT82 }

        public enum TH { _100, _130, }

        public static PT_MODE PtMode { get; set; }


        private static void SetTh(TH th)
        {
            if (th == TH._100)
            {
                var dialog = new DialogPic("100Ωにセットしてください", DialogPic.PT_NAME.PT100);
                dialog.ShowDialog();
            }
            else
            {
                var dialog = new DialogPic("130Ωにセットしてください", DialogPic.PT_NAME.PT130);
                dialog.ShowDialog();
            }
        }


        //アイテムによりR125の定数が異なる
        //ITEM002 62Ω
        //それ以外 82Ω
        //この定数の違いにより、オリオンライブラリのコールする関数が変わる
        //R125 = 82Ω → Pt_Read82() ;	// i にA/D変換し82Ωテーブルから温度に変換後40回の移動平均を１回した結果が入ります。
        //R125 = 62Ω → Pt_Read62() ;	// i にA/D変換し82Ωテーブルから温度に変換後40回の移動平均を１回した結果が入ります。

        //＜吉田Bの手法＞
        //PCを使わないセルフテストのため、検査する製品がどのアイテムか判別できないため、自力でＲ125の定数を調べるため
        //下記の方法をとっていた。
        //ダイヤル抵抗器を100Ωにセットする
        //TEST = 1で補正無しに設定
        //Mode = pt62に設定する
        //オリオンLibをactivにする
        //15秒待ち
        //-5000＜PT100_AD＜-3500 ならmode=pt82に変更する

        //＜畔上の手法＞
        //リニューアル後の試験機は、PCを使ったインタラクティブな試験のため
        //バーコードリーダーで製品型番を読み込み、アイテムの判別ができる
        //よって吉田Bの手法は使用しないこととする


        public static async Task<bool> CalPt100(TH th)
        {
            //ダイヤル抵抗器を100Ω or 130Ωにセットする
            //TEST = 1で補正無しに設定
            //mode変数により、Modeを変更する
            //オリオンLibをactivにする
            //15秒待ち
            //PT100_AD/100.0 を保存しておく
            var result = false;
            var max = 0;
            var min = 0;

            var pt100Ad = 0;




            SetTh(th);//作業者に抵抗器の切り替え指示を出す

            return await Task<bool>.Run(() =>
            {
                try
                {
                    if (th == TH._100)
                    {
                        max = State.TestSpec.Adjustment_100Ω_Max;
                        min = State.TestSpec.Adjustment_100Ω_Min;
                    }
                    else
                    {
                        max = State.TestSpec.Adjustment_130Ω_Max;
                        min = State.TestSpec.Adjustment_130Ω_Min;
                    }

                    General.PowSupply(true);
                    Sleep(3000);

                    Target.SendData("W,TB1,00,01", Wait2: 350);//補正無し

                    if (PtMode == PT_MODE.PT62)
                    {
                        Target.SendData("W,TB1,05,01", Wait2: 350);//pt62モードに設定
                        State.VmTestStatus.ColPt62 = OnBrush;
                        State.VmTestStatus.ColPt82 = OffBrush;
                    }
                    else
                    {
                        Target.SendData("W,TB1,05,00", Wait2: 350);//pt82モードに設定
                        State.VmTestStatus.ColPt62 = OffBrush;
                        State.VmTestStatus.ColPt82 = OnBrush;
                    }

                    Target.SendData("W,ORION,00,01");//オリオンLibをactivにする
                    Sleep(WAIT);
                    Target.SendData("R,ALL,00,00", Wait2: 350);

                    Int32.TryParse(Target.RecieveData.Split(',')[12], out pt100Ad);

                    if (th == TH._100)
                        State.T_C_100 = pt100Ad;
                    else
                        State.T_D_130 = pt100Ad;

                    result = min <= pt100Ad && pt100Ad <= max;

                    if (th == TH._100)
                    {
                        State.VmTestResults.Non_Cal_100 = pt100Ad.ToString();
                        State.VmTestResults.ColNon_Cal_100 = result ? OffBrush : NgBrush;
                    }
                    else
                    {
                        State.VmTestResults.Non_Cal_130 = pt100Ad.ToString();
                        State.VmTestResults.ColNon_Cal_130 = result ? OffBrush : NgBrush;
                    }

                    return result;

                }
                catch
                {
                    return false;
                }
                finally
                {
                    Target.SendData("W,ORION,00,00", Wait2: 350);//オリオンLibを非activにする
                    if (!result)
                    {
                        State.VmTestStatus.Spec = $"規格値 : {min.ToString()} ～ {max.ToString()}";
                        State.VmTestStatus.MeasValue = $"計測値 : {pt100Ad.ToString()}";
                    }
                }
            });
        }

        public static async Task<bool> CheckPt100(TH th)
        {
            //オリオンlibで定義されているT_C、T_Dにさきほど保存した補正値をセットする
            //オリオンlibで定義されているT_C、T_Dにさきほど保存した補正値をセットする
            //TEST = 0 で補正有りに設定
            //オリオンLibをactivにする
            //100Ω、130Ωを接続して温度値が規格ないであることの確認

            var result = false;
            var max = 0.0;
            var min = 0.0;

            var pt100Ad = 0;
            var temp = 0.0;

            if (th == TH._100 || Flags.Retry || Flags.日常点検中)
            {
                SetTh(th);//作業者に抵抗器の切り替え指示を出す
                //調整 100Ω → 130Ωの順に接続しているので、確認のときは抵抗の切り替え回数を減らすため
                //130Ω → 100Ωの順に行う
                //よって、130Ωは切り替え指示を出さない（リトライ時は無条件で指示を出す）
            }

            return await Task<bool>.Run(() =>
            {
                try
                {
                    if (th == TH._100)
                    {
                        max = State.TestSpec.Temp0_Max;
                        min = State.TestSpec.Temp0_Min;
                    }
                    else
                    {
                        max = State.TestSpec.Temp77_66_Max;
                        min = State.TestSpec.Temp77_66_Min;
                    }

                    General.PowSupply(true);
                    Sleep(3000);

                    //オリオンLibのT_C、T_Dに補正値をセットする
                    if (!SetAdjustmentToOrionLib()) return false;

                    Target.SendData("W,TB1,00,00", Wait2: 350);//補正有り

                    if (PtMode == PT_MODE.PT62)
                        Target.SendData("W,TB1,05,01", Wait2: 350);//pt62モードに設定
                    else
                        Target.SendData("W,TB1,05,00", Wait2: 350);//pt82モードに設定

                    Target.SendData("W,ORION,00,01");//オリオンLibをactivにする
                    Sleep(WAIT);
                    Target.SendData("R,ALL,00,00", Wait2: 350);

                    Int32.TryParse(Target.RecieveData.Split(',')[12], out pt100Ad);
                    temp = pt100Ad / 100.0;
                    result = min <= temp && temp <= max;

                    if (th == TH._100)
                    {
                        State.VmTestResults.Cal_100 = $"{temp.ToString("F2")}℃";
                        State.VmTestResults.ColCal_100 = result ? OffBrush : NgBrush;
                    }
                    else
                    {
                        State.VmTestResults.Cal_130 = $"{ temp.ToString("F2")}℃";
                        State.VmTestResults.ColCal_130 = result ? OffBrush : NgBrush;
                    }

                    return result;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    Target.SendData("W,ORION,00,00", Wait2: 350);//オリオンLibを非activにする
                    if (!result)
                    {
                        State.VmTestStatus.Spec = $"規格値 : {min.ToString("F2")} ～ {max.ToString("F2")}℃";
                        State.VmTestStatus.MeasValue = $"計測値 : {temp.ToString("F2")}℃";
                    }
                }
            });
        }



        private static bool SetAdjustmentToOrionLib()
        {
            try
            {
                //EEPROMに保存されている補正値を読み出す
                var resultAdj100 = ReadAdjustmentFromEep(TH._100);
                if (!resultAdj100.result) return false;

                var resultAdj130 = ReadAdjustmentFromEep(TH._130);
                if (!resultAdj130.result) return false;

                //T_C 100の書き込み
                Target.SendData($"W,TB1,01,{resultAdj100.adj.ToString()}", Wait2: 350);

                //T_D 130の書き込み
                Target.SendData($"W,TB1,02,{resultAdj130.adj.ToString()}", Wait2: 350);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> WriteAdjustmentToEep()
        {

            return await Task<bool>.Run(() =>
           {
               try
               {
                   //T_C 100の書き込み
                   Target.SendData($"W,TB1,03,{State.T_C_100.ToString()}", Wait2: 350);

                   //T_D 130の書き込み
                   Target.SendData($"W,TB1,04,{State.T_D_130.ToString()}", Wait2: 350);

                   return true;
               }
               catch
               {
                   return false;
               }
               finally
               {
                   General.PowSupply(false);
                   Sleep(3500);
               }
           });
        }

        /// <summary>
        /// 補正値を読み出してアプリ側で記録している値を同じかどうか確認する
        /// </summary>
        public static async Task<bool> CompareAdjustment(TH th)
        {
            return await Task<bool>.Run(() =>
           {
               try
               {
                   if (th == TH._100)
                   {
                       //T_C 100の読み出し
                       Target.SendData("R,EEP,01,60", Wait2: 350);
                   }
                   else
                   {
                       //T_D 130の読み出し
                       Target.SendData("R,EEP,01,61", Wait2: 350);
                   }

                   Int32.TryParse(Target.RecieveData, out var readData);

                   if (th == TH._100)
                   {
                       return readData == State.T_C_100;
                   }
                   else
                   {
                       return readData == State.T_D_130;
                   }
               }
               catch
               {
                   return false;
               }
           });
        }

        private static (bool result, int adj) ReadAdjustmentFromEep(TH th)
        {
            try
            {
                if (th == TH._100)
                {
                    //T_C 100の読み出し
                    Target.SendData("R,EEP,01,60", Wait2: 350);
                }
                else
                {
                    //T_D 130の読み出し
                    Target.SendData("R,EEP,01,61", Wait2: 350);
                }

                var result = Int32.TryParse(Target.RecieveData, out var readData);

                return (result, readData);
            }
            catch
            {
                return (false, 0);
            }
        }


    }
}