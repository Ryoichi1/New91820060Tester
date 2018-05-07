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
            (bool result, int adVal) Result = (false, 0);
            double volData = 0;

            try
            {
                General.SetRL1(true);
                await Task.Delay(3000);
                return await Task<bool>.Run(() =>
                {
                    //電圧入力していない状態でもノイズ等で5V近くになるときがあったので
                    //念のため何回か計測する
                    foreach (var i in Enumerable.Range(0, 4))
                    {
                        Sleep(200);
                        Result = LPC1768.MeasVol();
                        if (!Result.result)//LPC1768との通信こけたらダメ
                            return false;
                        volData = Result.adVal * (3.3 / Math.Pow(2, 12)) * 2;//試験機側では入力電圧を抵抗で分圧して1/2にしています（mbedへの入力が3.3VMaxのため）
                        if (volData < State.TestSpec.Vol5vMin || volData > State.TestSpec.Vol5vMax)//1回でも範囲ならダメ
                            return false;
                    }
                    return true;
                });
            }
            catch
            {
                return false;
            }
            finally
            {
                General.SetRL1(false);
                //複数回計測して、最後に計測した値を表示する
                State.VmTestResults.Vol5v = $"{volData.ToString("F2")}V";
                if (!Result.result)
                    State.VmTestResults.ColVol5v = General.NgBrush;
            }
        }





    }
}
