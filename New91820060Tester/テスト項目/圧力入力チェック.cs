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
    public static class 圧力入力チェック
    {
        public static int Vol_1_5_CN11 { get; set; }
        public static int Vol_3_5_CN11 { get; set; }
        public static int Vol_1_5_CN12 { get; set; }
        public static int Vol_3_5_CN12 { get; set; }

        public enum CH
        {
            CN11, CN12
        }
        public enum PRESS
        {
            V1_5, V3_5
        }
        public class TestSpec
        {
            public CH ch;
            public double press;
            public bool result;
        }

        public static List<TestSpec> ListSpecs;

        private static void InitList()
        {
            ListSpecs = new List<TestSpec>();
            foreach (var n in Enum.GetValues(typeof(CH)))
            {
                ListSpecs.Add(new TestSpec { ch = (CH)n });
            }
        }

        public static async Task<bool> GetLev(CH ch, PRESS p)
        {
            const int Default_1_5_Cn116 = 36;
            const int Default_1_5_Cn117 = 74;
            const int Default_3_5_Cn116 = 87;
            const int Default_3_5_Cn117 = 176;

            (bool result, double data) ResultFromMbed = (false, 0);
            double volData = 0;
            bool result = false;

            double max = 0.0;
            double min = 0.0;



            int CurrentVal = 0;

            return await Task<bool>.Run(() =>
            {
                try
                {
                    if (p == PRESS.V1_5)
                    {
                        max = State.TestSpec.Vol_Press_L_Max;
                        min = State.TestSpec.Vol_Press_L_Min;
                    }
                    else
                    {
                        max = State.TestSpec.Vol_Press_H_Max;
                        min = State.TestSpec.Vol_Press_H_Min;
                    }

                    General.ResetAdInputForMed();
                    Sleep(250);

                    //DA出力 デフォルト値の設定
                    if (ch == CH.CN11)
                    {
                        if (p == PRESS.V1_5)
                            CurrentVal = Default_1_5_Cn116;
                        else
                            CurrentVal = Default_3_5_Cn116;
                    }
                    else
                    {
                        if (p == PRESS.V1_5)
                            CurrentVal = Default_1_5_Cn117;
                        else
                            CurrentVal = Default_3_5_Cn117;
                    }

                    //DA出力開始
                    if (ch == CH.CN11)
                        Target.SendData($"W,U207,00,{CurrentVal.ToString()}");
                    else
                        Target.SendData($"W,U208,00,{CurrentVal.ToString()}");

                    //mbedのアナログポートに接続する処理
                    if (ch == CH.CN11)
                        General.SetRL2(true);
                    else
                        General.SetRL3(true);

                    Sleep(300);

                    var tm = new GeneralTimer(30000);
                    tm.start();
                    while (true)
                    {
                        if (Flags.ClickStopButton || tm.FlagTimeout)
                            return false;
                        Sleep(300);
                        ResultFromMbed = LPC1768.MeasVol();
                        if (!ResultFromMbed.result)//LPC1768との通信こけたらダメ
                            continue;

                        //電圧値の変換
                        volData = MeasVol(ch, ResultFromMbed.data);

                        //ビューモデルの更新
                        SetVmVol(ch, p, volData);

                        //判定
                        if (min <= volData && volData <= max)
                        {
                            Sleep(1000);
                            //安定後の再計測

                            //電圧値の変換
                            volData = MeasVol(ch, ResultFromMbed.data);

                            //ビューモデルの更新
                            SetVmVol(ch, p, volData);

                            if (min <= volData && volData <= max)
                            {
                                SetInput(ch, p, CurrentVal);
                                return result = true;
                            }
                        }

                        if (volData < min)
                            CurrentVal++;
                        else if (volData > max)
                            CurrentVal--;
                        //DA出力
                        if (ch == CH.CN11)
                            Target.SendData($"W,U207,00,{CurrentVal.ToString()}");
                        else
                            Target.SendData($"W,U208,00,{CurrentVal.ToString()}");
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    PressOff();
                    if (!result)
                    {
                        if (ch == CH.CN11)
                        {
                            if (p == PRESS.V1_5)
                                State.VmTestResults.ColCn11_Vol1 = result ? OffBrush : NgBrush;
                            else
                                State.VmTestResults.ColCn11_Vol2 = result ? OffBrush : NgBrush;
                        }
                        else
                        {
                            if (p == PRESS.V1_5)
                                State.VmTestResults.ColCn12_Vol1 = result ? OffBrush : NgBrush;
                            else
                                State.VmTestResults.ColCn12_Vol2 = result ? OffBrush : NgBrush;
                        }
                    }
                }
            });
        }

        private static void SetInput(CH ch, PRESS p, int data)
        {
            if (ch == CH.CN11)
            {
                if (p == PRESS.V1_5)
                    Vol_1_5_CN11 = data;
                else
                    Vol_3_5_CN11 = data;
            }
            else
            {
                if (p == PRESS.V1_5)
                    Vol_1_5_CN12 = data;
                else
                    Vol_3_5_CN12 = data;
            }
        }


        private static double MeasVol(CH ch, double data)
        {
            var volData = 0.0;
            //電圧値の変換
            volData = data * 2;//試験機回路で入力電圧を1/2にしてmbedに取り込みます（mbedへの入力が3.3VMaxのため）

            return volData;
        }

        private static void SetVmVol(CH ch, PRESS p, double volData)
        {
            //ビューモデルの更新
            if (ch == CH.CN11)
            {
                if (p == PRESS.V1_5)
                    State.VmTestResults.Cn11_Vol1 = $"{volData.ToString("F2")}V";
                else
                    State.VmTestResults.Cn11_Vol2 = $"{volData.ToString("F2")}V";
            }
            else
            {
                if (p == PRESS.V1_5)
                    State.VmTestResults.Cn12_Vol1 = $"{volData.ToString("F2")}V";
                else
                    State.VmTestResults.Cn12_Vol2 = $"{volData.ToString("F2")}V";
            }
        }


        private static void SetPress(PRESS p)
        {
            if (p == PRESS.V1_5)
            {
                Target.SendData($"W,U207,00,{Vol_1_5_CN11.ToString()}", Wait2: 300);
                Target.SendData($"W,U208,00,{Vol_1_5_CN12.ToString()}", Wait2: 300);
            }
            else
            {
                Target.SendData($"W,U207,00,{Vol_3_5_CN11.ToString()}", Wait2: 300);
                Target.SendData($"W,U208,00,{Vol_3_5_CN12.ToString()}", Wait2: 300);
            }
        }

        private static void PressOff()
        {
            Target.SendData($"W,U207,00,00", Wait2: 300);
            Target.SendData($"W,U208,00,00", Wait2: 300);
        }

        public static async Task<bool> CheckPress(PRESS p)
        {
            bool result = false;
            int max = 0;
            int min = 0;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    try
                    {
                        switch (p)

                        {
                            case PRESS.V1_5:
                                max = State.TestSpec.Press_L_Max;
                                min = State.TestSpec.Press_L_Min;
                                break;
                            case PRESS.V3_5:
                                max = State.TestSpec.Press_H_Max;
                                min = State.TestSpec.Press_H_Min;
                                break;
                        }

                        ResetViewModel(p);

                        InitList();//テストスペック毎回初期化

                        Target.SendData("W,ORION,00,01");
                        General.SetK4(true);
                        SetPress(p);
                        Sleep(7000);
                        Target.SendData("R,ALL,00,00");
                        var buffArray = Target.RecieveData.Split(',').Skip(10).Take(2);

                        int i = 0;
                        foreach (var buff in buffArray)
                        {
                            Int32.TryParse(buff, out var val);
                            ListSpecs[i++].press = val;
                        }

                        ListSpecs.ForEach(l => l.result = (min <= l.press && l.press <= max));
                        SetViewModel(p);

                        return ListSpecs.All(l => l.result);
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            finally
            {
                PressOff();
                Target.SendData("W,ORION,00,00");//オリオンLibを非activにする
                State.VmTestStatus.Spec = $"規格値 : {min.ToString()} ～ {max.ToString()}kPa";
                State.VmTestStatus.MeasValue = "計測値 : ---";
            }
        }

        private static void SetViewModel(PRESS p)
        {
            switch (p)
            {
                case PRESS.V1_5:
                    State.VmTestResults.Cn11_In1 = $"{ListSpecs[0].press.ToString()}kPa";
                    State.VmTestResults.Cn12_In1 = $"{ListSpecs[1].press.ToString()}kPa";
                    State.VmTestResults.ColCn11_In1 = ListSpecs[0].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn12_In1 = ListSpecs[1].result ? OffBrush : NgBrush;
                    break;
                case PRESS.V3_5:
                    State.VmTestResults.Cn11_In2 = $"{ListSpecs[0].press.ToString()}kPa";
                    State.VmTestResults.Cn12_In2 = $"{ListSpecs[1].press.ToString()}kPa";
                    State.VmTestResults.ColCn11_In2 = ListSpecs[0].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn12_In2 = ListSpecs[1].result ? OffBrush : NgBrush;
                    break;

            }
        }

        private static void ResetViewModel(PRESS p)
        {
            switch (p)
            {
                case PRESS.V1_5:
                    State.VmTestResults.Cn11_In1 = "";
                    State.VmTestResults.Cn12_In1 = "";
                    State.VmTestResults.ColCn11_In1 = OffBrush;
                    State.VmTestResults.ColCn12_In1 = OffBrush;
                    break;
                case PRESS.V3_5:
                    State.VmTestResults.Cn11_In2 = "";
                    State.VmTestResults.Cn12_In2 = "";
                    State.VmTestResults.ColCn11_In2 = OffBrush;
                    State.VmTestResults.ColCn12_In2 = OffBrush;
                    break;
            }
        }



    }
}