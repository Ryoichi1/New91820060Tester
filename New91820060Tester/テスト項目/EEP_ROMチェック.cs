using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Threading.Thread;

namespace New91820060Tester
{

    public static class EEPROMチェック
    {

        public static async Task<bool> TestEep()
        {
            try
            {
                return await Task<bool>.Run(() =>
                {
                    try
                    {
                        Target.SendData("T,EEP,00,00", 15000);
                        return Target.RecieveData == "1";
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            finally
            {
            }
        }

        public static async Task<bool> WriteFactorySetting()
        {
            try
            {
                return await Task<bool>.Run(() =>
                {
                    try
                    {
                        Target.SendData("W,EEP,00,00", 15000);
                        return Target.RecieveData == "1";
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            finally
            {
            }
        }

        /// <summary>
        /// EEPROMの出荷設定をチェックするため、記録電源OFF状態でメソッドをコールすること
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CheckFactorySetting()
        {
            try
            {
                return await Task<bool>.Run(() =>
                {
                    try
                    {
                        Sleep(3000);
                        General.PowSupply(true);
                        Sleep(2000);

                        Target.SendData("R,EEP,00,00", 15000);
                        return Target.RecieveData == "1";
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            finally
            {
            }
        }

        public static async Task<bool> CheckPt100Setting()
        {
            int t_c = 0;
            int t_d = 0;

            bool resultT_C_100;
            bool resultT_D_130;

            try
            {
                return await Task<bool>.Run(() =>
                {
                    try
                    {
                        Sleep(3000);
                        General.PowSupply(true);
                        Sleep(2000);

                        Target.SendData("R,EEP,01,60", Wait2:250);//T_C 100Ω補正値
                        Int32.TryParse(Target.RecieveData, out t_c);
                        resultT_C_100 = t_c == State.T_C_100;

                    Target.SendData("R,EEP,01,61", Wait2:250);//T_D 130Ω補正値
                        Int32.TryParse(Target.RecieveData, out t_d);
                        resultT_D_130 = t_d == State.T_D_130;

                        return resultT_C_100 && resultT_D_130;

                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            finally
            {
            }
        }

    }
}
