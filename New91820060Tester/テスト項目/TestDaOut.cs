using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Threading.Thread;


namespace New91820060Tester
{
    public static class TestDaOut
    {


        public enum DA_OUT_CH { CN116, CN117 }
        public enum DA_OUT_VAL { _50, _100 }

        public static async Task<bool> CheckDaOut(DA_OUT_CH ch, DA_OUT_VAL val)
        {
            (bool result, int adVal) ResultFromMbed = (false, 0);
            double volData = 0;
            var result = false;

            //検査スペックの選択
            double Max = val == DA_OUT_VAL._50 ? State.TestSpec.Press_50_Max : State.TestSpec.Press_100_Max;
            double Min = val == DA_OUT_VAL._50 ? State.TestSpec.Press_50_Min : State.TestSpec.Press_100_Min;

            try
            {
                return await Task<bool>.Run(() =>
                {
                    try
                    {
                        General.ResetAdInputForMed();
                        Sleep(250);

                        //DA出力開始
                        var outval = (val == DA_OUT_VAL._50) ? "50" : "100";
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

                        Sleep(5000);

                        ResultFromMbed = LPC1768.MeasVol();
                        if (!ResultFromMbed.result)//LPC1768との通信こけたらダメ
                            return result = false;
                        volData = ResultFromMbed.adVal * (3.3 / Math.Pow(2, 12)) * 4;//試験機回路で入力電圧を1/4にしてmbedに取り込みます（mbedへの入力が3.3VMaxのため）
                        return result = (Min < volData && volData < Max);
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
                //ビューモデルの更新
                if (ch == DA_OUT_CH.CN116)
                {
                    if (val == DA_OUT_VAL._50)
                    {
                        State.VmTestResults.VolDaout50_116 = $"{volData.ToString("F2")}V";
                        if (!result)
                            State.VmTestResults.ColVolDaout50_116 = General.NgBrush;
                    }
                    else
                    {
                        State.VmTestResults.VolDaout100_116 = $"{volData.ToString("F2")}V";
                        if (!result)
                            State.VmTestResults.ColVolDaout100_116 = General.NgBrush;
                    }
                }
                else
                {
                    if (val == DA_OUT_VAL._50)
                    {
                        State.VmTestResults.VolDaout50_117 = $"{volData.ToString("F2")}V";
                        if (!result)
                            State.VmTestResults.ColVolDaout50_117 = General.NgBrush;
                    }
                    else
                    {
                        State.VmTestResults.VolDaout100_117 = $"{volData.ToString("F2")}V";
                        if (!result)
                            State.VmTestResults.ColVolDaout100_117 = General.NgBrush;
                    }
                }
            }
        }






    }
}
