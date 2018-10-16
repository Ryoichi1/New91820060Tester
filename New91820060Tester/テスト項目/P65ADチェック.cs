using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static New91820060Tester.General;
using static System.Threading.Thread;

namespace New91820060Tester
{
    /// <summary>
    ///
    /// </summary>
    public static class P65ADチェック
    {
        public static async Task<bool> CheckP65()
        {
            bool resultCold = false;
            bool resultHot = false;

            int coldAd = 0;
            int hotAd = 0;

            return await Task<bool>.Run(() =>
            {
                try
                {
                    Flags.AddDecision = false;

                    Sleep(3000);
                    General.PowSupply(true);
                    Sleep(1000);

                    //テストログの更新
                    State.VmTestStatus.TestLog += "\r\n コールドスタートチェック";
                    Target.SendData("R,AD,00,00");
                    Int32.TryParse(Target.RecieveData, out coldAd);
                    resultCold = State.TestSpec.PowOnResetAdMin < coldAd && coldAd < State.TestSpec.PowOnResetAdMax;
                    State.VmTestResults.ColdAd = coldAd.ToString();
                    State.VmTestResults.ColColdAd = resultCold ? OffBrush : NgBrush;

                    if (resultCold)
                    {
                        //テストログの更新
                        State.VmTestStatus.TestLog += "---PASS";
                    }
                    else
                    {
                        //テストログの更新
                        State.VmTestStatus.TestLog += "---FAIL";
                        State.VmTestStatus.Spec = $"規格値 : {State.TestSpec.PowOnResetAdMin.ToString()} ～ {State.TestSpec.PowOnResetAdMax.ToString()}";
                        State.VmTestStatus.MeasValue = $"計測値 : {coldAd.ToString()}";
                        return false;
                    }


                    //テストログの更新
                    State.VmTestStatus.TestLog += "\r\n ホットスタートチェック";
                    Sleep(4000);
                    Target.SendData("W,WDT,00,00");
                    Target.SendData("R,AD,00,00");
                    Int32.TryParse(Target.RecieveData, out hotAd);
                    resultHot = hotAd == State.TestSpec.WdtResetAd;
                    State.VmTestResults.HotAd = hotAd.ToString();
                    State.VmTestResults.ColHotAd = resultHot ? OffBrush : NgBrush;

                    if (resultHot)
                    {
                        //テストログの更新
                        State.VmTestStatus.TestLog += "---PASS\r\n";
                        return true;
                    }
                    else
                    {
                        //テストログの更新
                        State.VmTestStatus.TestLog += "---FAIL\r\n";
                        State.VmTestStatus.Spec = $"規格値 : {State.TestSpec.WdtResetAd.ToString()}";
                        State.VmTestStatus.MeasValue = $"計測値 : {hotAd.ToString()}";
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

    }
}