using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Threading.Thread;


namespace New91820060Tester
{
    public static class MeasVoltage
    {
        public static async Task<bool> Check5v()
        {
            (bool result, double data) Result = (false, 0);
            double volData = 0;
            bool result = false;

            double max = State.TestSpec.Vol5vMax;
            double min = State.TestSpec.Vol5vMin;

            try
            {
                return await Task<bool>.Run(() =>
                {
                    try
                    {
                        General.SetRL1(true);
                        Sleep(3000);
                        Result = LPC1768.MeasVol();
                        if (!Result.result)//LPC1768との通信こけたらダメ
                            return false;
                        volData = Result.data * 2;//試験機側では入力電圧を抵抗で分圧して1/2にしています（mbedへの入力が3.3VMaxのため）
                        return result = (min < volData && volData < max);
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            finally
            {
                General.SetRL1(false);
                //複数回計測して、最後に計測した値を表示する
                State.VmTestResults.Vol5v = $"{volData.ToString("F2")}V";
                if (!result)
                {
                    State.VmTestResults.ColVol5v = General.NgBrush;
                    State.VmTestStatus.Spec = $"規格値 : {min.ToString("F2")} ～ {max.ToString("F2")}V";
                    State.VmTestStatus.MeasValue = $"計測値 : {volData.ToString("F2")}V";
                }
            }
        }





    }
}
