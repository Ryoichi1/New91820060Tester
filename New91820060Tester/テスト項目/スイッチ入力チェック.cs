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
    public static class スイッチ入力チェック
    {
        public enum DSW1_NAME
        {
            BIT_0, BIT_1, BIT_2, BIT_3, BIT_4, BIT_5, BIT_6, BIT_7, BIT_8,
        }
        public enum DSW2_NAME
        {
            BIT_0, BIT_1, BIT_2, BIT_3, BIT_4, BIT_5, BIT_6, BIT_7, BIT_8, BIT_9, BIT_A, BIT_B, BIT_C, BIT_D, BIT_E, BIT_F,
        }
        public class Dsw1TestSpec
        {
            public DSW1_NAME name;
            public byte Exp;
        }

        public class Dsw2TestSpec
        {
            public DSW2_NAME name;
            public byte Exp;
        }

        public static List<Dsw1TestSpec> ListDsw1Specs;
        public static List<Dsw2TestSpec> ListDsw2Specs;

        private static void InitList()
        {
            ListDsw1Specs = new List<Dsw1TestSpec>();
            foreach (var n in Enum.GetValues(typeof(DSW1_NAME)))
            {
                ListDsw1Specs.Add(new Dsw1TestSpec { name = (DSW1_NAME)n });

            }
            ListDsw1Specs[0].Exp = 0b1111_1110;
            ListDsw1Specs[1].Exp = 0b1111_1100;
            ListDsw1Specs[2].Exp = 0b1111_1000;
            ListDsw1Specs[3].Exp = 0b1111_0000;
            ListDsw1Specs[4].Exp = 0b1110_0000;
            ListDsw1Specs[5].Exp = 0b1100_0000;
            ListDsw1Specs[6].Exp = 0b1000_0000;
            ListDsw1Specs[7].Exp = 0b0000_0000;


            ListDsw2Specs = new List<Dsw2TestSpec>();
            foreach (var n in Enum.GetValues(typeof(DSW2_NAME)))
            {
                ListDsw2Specs.Add(new Dsw2TestSpec { name = (DSW2_NAME)n });
            }
            ListDsw2Specs[0].Exp = 0b0000_1111;
            ListDsw2Specs[1].Exp = 0b0000_1110;
            ListDsw2Specs[2].Exp = 0b0000_1101;
            ListDsw2Specs[3].Exp = 0b0000_1100;
            ListDsw2Specs[4].Exp = 0b0000_1011;
            ListDsw2Specs[5].Exp = 0b0000_1010;
            ListDsw2Specs[6].Exp = 0b0000_1001;
            ListDsw2Specs[7].Exp = 0b0000_1000;
            ListDsw2Specs[8].Exp = 0b0000_0111;
            ListDsw2Specs[9].Exp = 0b0000_0110;
            ListDsw2Specs[10].Exp = 0b0000_0101;
            ListDsw2Specs[11].Exp = 0b0000_0100;
            ListDsw2Specs[12].Exp = 0b0000_0011;
            ListDsw2Specs[13].Exp = 0b0000_0010;
            ListDsw2Specs[14].Exp = 0b0000_0001;
            ListDsw2Specs[15].Exp = 0b0000_0000;
        }


        public static async Task<bool> CheckDsw1()
        {
            bool result = false;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    try
                    {
                        Target.ChangeMode(Target.MODE.RS422);
                        Sleep(400);

                        General.PlaySoundSync(General.soundNotice);
                        InitList();//テストスペック毎回初期化
                        Sleep(300);

                        int i = 1;
                        foreach (var l in ListDsw1Specs)
                        {
                            State.VmTestStatus.Message = $"DSW1のビット{i++.ToString()}をON↑してください";
                            while (true)
                            {
                                if (Flags.ClickStopButton)
                                    return false;
                                Target.SendData("R,U8,00,00", setLog: false);
                                if (Convert.ToInt32(Target.RecieveData, 16) == l.Exp)
                                {
                                    General.PlaySoundAsync(General.soundSwCheck);
                                    break;
                                }
                            }
                        }

                        General.PlaySoundAsync(General.soundNotice);
                        State.VmTestStatus.Message = "DSW1をすべてOFFしてください";
                        while (true)
                        {
                            if (Flags.ClickStopButton)
                                return false;
                            Target.SendData("R,U8,00,00", setLog: false);
                            if (Convert.ToInt32(Target.RecieveData, 16) == 0xFF)
                            {
                                General.PlaySoundAsync(General.soundSwCheck);
                                break;
                            }
                        }

                        State.VmTestStatus.Message = $"DSW1を出荷設定（{State.Dsw1DefaultMess}）にしてください";
                        while (true)
                        {
                            if (Flags.ClickStopButton)
                                return false;
                            Target.SendData("R,U8,00,00", setLog: false);
                            if (Convert.ToInt32(Target.RecieveData, 16) == State.Dsw1DefaultVal)
                            {
                                General.PlaySoundSync(General.soundSuccess);
                                break;
                            }
                        }

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            finally
            {
                State.VmTestStatus.Message = Constants.MessWait;
                Target.ChangeMode(Target.MODE.RS232);
                await Task.Delay(400);
            }
        }

        public static async Task<bool> CheckDsw2()
        {
            bool result = false;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    try
                    {
                        General.PlaySoundSync(General.soundNotice);
                        InitList();//テストスペック毎回初期化
                        Sleep(300);

                        int i = 0;
                        foreach (var l in ListDsw2Specs)
                        {
                            State.VmTestStatus.Message = $"DSW2を{i++.ToString("X")}にしてください";
                            while (true)
                            {
                                if (Flags.ClickStopButton)
                                    return false;
                                Target.SendData("R,U7,00,00", setLog: false);
                                if ((Convert.ToInt32(Target.RecieveData, 16) & 0x0F) == l.Exp)
                                {
                                    General.PlaySoundAsync(General.soundSwCheck);
                                    break;
                                }
                            }
                        }

                        RETRY:
                        State.VmTestStatus.Message = $"DSW2を出荷設定（{State.Dsw2DefaultMess}）にしてください";
                        while (true)
                        {
                            if (Flags.ClickStopButton)
                                return false;
                            Target.SendData("R,U7,00,00", setLog: false);
                            if ((Convert.ToInt32(Target.RecieveData, 16) & 0x0F) == ListDsw2Specs[State.Dsw2DefaultVal].Exp)
                            {
                                General.PlaySoundSync(General.soundSuccess);
                                break;
                            }
                        }

                        Sleep(2000);

                        Target.SendData("R,U7,00,00", setLog: false);
                        if ((Convert.ToInt32(Target.RecieveData, 16) & 0x0F) != ListDsw2Specs[State.Dsw2DefaultVal].Exp)
                            goto RETRY;

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            finally
            {
                State.VmTestStatus.Message = Constants.MessWait;
            }
        }

    }
}