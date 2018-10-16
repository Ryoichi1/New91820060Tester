using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Threading.Thread;
using static New91820060Tester.General;


namespace New91820060Tester
{
    public static class TestDaOut
    {


        public enum DA_OUT_CH { CN116, CN117 }
        public enum DA_OUT_VAL { _50, _100 }

        public static async Task<bool> CheckDaOut(DA_OUT_CH ch, DA_OUT_VAL val)
        {
            (bool result, double data) ResultFromMbed = (false, 0);
            double volData = 0;
            var result = false;
            double max = 0.0;
            double min = 0.0;
            string outval;

            try
            {
                return await Task<bool>.Run(() =>
                {
                    try
                    {
                        if (ch == DA_OUT_CH.CN116)
                        {
                            max = val == DA_OUT_VAL._50 ? State.TestSpec.Dout116_50_Max : State.TestSpec.Dout116_100_Max;
                            min = val == DA_OUT_VAL._50 ? State.TestSpec.Dout116_50_Min : State.TestSpec.Dout116_100_Min;
                        }
                        else
                        {
                            max = val == DA_OUT_VAL._50 ? State.TestSpec.Dout117_50_Max : State.TestSpec.Dout117_100_Max;
                            min = val == DA_OUT_VAL._50 ? State.TestSpec.Dout117_50_Min : State.TestSpec.Dout117_100_Min;
                        }
                        //検査スペックの選択
                        outval = (val == DA_OUT_VAL._50) ? "50" : "100";

                        General.ResetAdInputForMed();
                        Sleep(250);

                        //DA出力開始
                        if (ch == DA_OUT_CH.CN116)
                        {
                            Target.SendData($"W,U207,00,{outval}");
                        }
                        else
                        {
                            Target.SendData($"W,U208,00,{outval}");
                        }

                        //mbedのアナログポートに接続する処理
                        if (ch == DA_OUT_CH.CN116)
                            General.SetRL2(true);
                        else
                            General.SetRL3(true);

                        Sleep(3000);

                        ResultFromMbed = LPC1768.MeasVol();
                        if (!ResultFromMbed.result)//LPC1768との通信こけたらダメ
                            return result = false;

                        volData = ResultFromMbed.data * 2;//試験機回路で入力電圧を1/2にしてmbedに取り込みます（mbedへの入力が3.3VMaxのため）

                        return result = (min < volData && volData < max);
                    }
                    catch
                    {
                        return result = false;
                    }
                });
            }
            finally
            {
                General.ResetAdInputForMed();
                //毎回出力をOFFする
                Target.SendData("W,U207,00,00");
                Target.SendData("W,U208,00,00");
                //ビューモデルの更新
                if (ch == DA_OUT_CH.CN116)
                {
                    if (val == DA_OUT_VAL._50)
                    {
                        State.VmTestResults.VolDaout50_116 = $"{volData.ToString("F2")}V";
                        State.VmTestResults.ColVolDaout50_116 = result ? OffBrush : NgBrush;
                    }
                    else
                    {
                        State.VmTestResults.VolDaout100_116 = $"{volData.ToString("F2")}V";
                        State.VmTestResults.ColVolDaout100_116 = result ? OffBrush : NgBrush;
                    }
                }
                else
                {
                    if (val == DA_OUT_VAL._50)
                    {
                        State.VmTestResults.VolDaout50_117 = $"{volData.ToString("F2")}V";
                        State.VmTestResults.ColVolDaout50_117 = result ? OffBrush : NgBrush;
                    }
                    else
                    {
                        State.VmTestResults.VolDaout100_117 = $"{volData.ToString("F2")}V";
                        State.VmTestResults.ColVolDaout100_117 = result ? OffBrush : NgBrush;
                    }
                }

                if (!result)
                {
                    State.VmTestStatus.Spec = $"規格値 : {min.ToString("F2")} ～ {max.ToString("F2")}V";
                    State.VmTestStatus.MeasValue = $"計測値 : {volData.ToString("F2")}V";
                }
            }
        }






    }
}
