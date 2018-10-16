using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static New91820060Tester.General;
using static System.Threading.Thread;

namespace New91820060Tester
{
    /// <summary>
    /// デジタル入力検査
    /// U21、U22、U23、PC1～PC7、DSW1、DSW2
    /// </summary>
    public static class デジタル入力チェック
    {
        public enum IC_NAME
        {
            U23a, U23b, U23c, U23d, U23e, U23f,
            U21e, U21f,
            U22a, U22b, U22c, U22d, U22e, U22f,
            PC7,
            PC1, PC2, PC3, PC4, PC5, PC6,
        }
        public class DinTestSpec
        {
            public IC_NAME name;
            public bool Input;
        }

        public static List<DinTestSpec> ListDinSpecs;

        private static void InitList()
        {
            ListDinSpecs = new List<DinTestSpec>();
            foreach (var n in Enum.GetValues(typeof(IC_NAME)))
            {
                ListDinSpecs.Add(new DinTestSpec { name = (IC_NAME)n });
            }
        }

        public static void SetAllOff()
        {
            io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b6, EPX64S.OUT.L);
            io1.OutByte(EPX64S.PORT.P2, 0x00);
            io1.OutByte(EPX64S.PORT.P4, 0b0100_0000);//p46は合格印スタンプの制御 逆論理（Low入力でソレノイドがONする）なので注意
            SetG7SA(false);
        }

        public static async Task<bool> CheckDin()
        {
            bool result = false;
            bool resultOn = false;
            bool resultOff = false;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    try
                    {
                        ResetViewModelExp();
                        ResetViewModelIn();

                        Flags.AddDecision = false;
                        InitList();//テストスペック毎回初期化

                        SetAllOff();//出力初期化
                        Sleep(300);

                        return ListDinSpecs.All(L =>
                        {
                            resultOn = false;
                            resultOff = false;

                            if (Flags.ClickStopButton) return false;

                            //テストログの更新
                            State.VmTestStatus.TestLog += "\r\n" + L.name.ToString() + " 入力ONチェック";

                            //ON入力
                            SetInput(L.name, true);
                            AnalysisData(L.name);

                            var targetState = ListDinSpecs.FirstOrDefault(l => l.name == L.name);
                            switch (L.name)
                            {
                                case IC_NAME.PC1:
                                case IC_NAME.PC2:
                                    resultOn = (targetState.Input && ListDinSpecs.Where(l => l.Input).Count() == 2);
                                    break;
                                case IC_NAME.PC3:
                                case IC_NAME.PC4:
                                case IC_NAME.PC5:
                                case IC_NAME.PC6:
                                    resultOn = (targetState.Input && ListDinSpecs.Where(l => l.Input).Count() == 3);
                                    break;
                                default:
                                    resultOn = (targetState.Input && ListDinSpecs.Where(l => l.Input).Count() == 1);
                                    break;
                            }

                            if (resultOn)
                            {
                                //テストログの更新
                                State.VmTestStatus.TestLog += "---PASS";
                            }
                            else
                            {
                                //テストログの更新
                                State.VmTestStatus.TestLog += "---FAIL";
                                return false;
                            }

                            //OFFチェック
                            State.VmTestStatus.TestLog += "\r\n" + L.name.ToString() + " 入力OFFチェック";

                            //OFF入力
                            SetInput(L.name, false);
                            if (L.name == IC_NAME.PC3 || L.name == IC_NAME.PC4 || L.name == IC_NAME.PC5 || L.name == IC_NAME.PC6)
                            {
                                AnalysisData(L.name, ExpAllOff: true, sw_pc1_2_on: true);
                            }
                            else
                            {
                                AnalysisData(L.name, ExpAllOff: true);
                            }
                            targetState = ListDinSpecs.FirstOrDefault(l => l.name == L.name);

                            switch (L.name)
                            {
                                case IC_NAME.PC3:
                                case IC_NAME.PC4:
                                case IC_NAME.PC5:
                                case IC_NAME.PC6:
                                    resultOff = (!targetState.Input && ListDinSpecs.Where(l => l.Input).Count() == 2);
                                    break;
                                default:
                                    resultOff = ListDinSpecs.All(l => !l.Input);
                                    break;
                            }

                            if (resultOff)
                            {
                                //テストログの更新
                                State.VmTestStatus.TestLog += "---PASS";
                                return true;
                            }
                            else
                            {
                                //テストログの更新
                                State.VmTestStatus.TestLog += "---FAIL";
                                return false;
                            }
                        });
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            finally
            {
                if (result)
                {
                    ResetViewModelExp();
                    ResetViewModelIn();
                }
                SetG7SA(false);//CN14へのAC200V入力をOFFする
                State.VmTestStatus.TestLog += "\r\n";

                if (!resultOn || !resultOff)
                {
                    State.VmTestStatus.Spec = "規格値 : ---";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }
        }

        private static void SetInput(IC_NAME name, bool sw)
        {
            switch (name)
            {
                case IC_NAME.U21e:
                    io1.OutBit(EPX64S.PORT.P2, EPX64S.BIT.b7, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U21f:
                    io1.OutBit(EPX64S.PORT.P2, EPX64S.BIT.b6, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U22a:
                    io1.OutBit(EPX64S.PORT.P4, EPX64S.BIT.b0, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U22b:
                    io1.OutBit(EPX64S.PORT.P4, EPX64S.BIT.b1, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U22c:
                    io1.OutBit(EPX64S.PORT.P4, EPX64S.BIT.b2, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U22d:
                    io1.OutBit(EPX64S.PORT.P4, EPX64S.BIT.b3, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U22e:
                    io1.OutBit(EPX64S.PORT.P4, EPX64S.BIT.b4, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U22f:
                    io1.OutBit(EPX64S.PORT.P4, EPX64S.BIT.b5, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U23a:
                    io1.OutBit(EPX64S.PORT.P2, EPX64S.BIT.b0, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U23b:
                    io1.OutBit(EPX64S.PORT.P2, EPX64S.BIT.b1, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U23c:
                    io1.OutBit(EPX64S.PORT.P2, EPX64S.BIT.b2, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U23d:
                    io1.OutBit(EPX64S.PORT.P2, EPX64S.BIT.b3, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U23e:
                    io1.OutBit(EPX64S.PORT.P2, EPX64S.BIT.b4, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.U23f:
                    io1.OutBit(EPX64S.PORT.P2, EPX64S.BIT.b5, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
                    break;
                case IC_NAME.PC1:
                    SetG7SA(sw ? true : false);
                    break;
                case IC_NAME.PC2:
                    SetG7SA(sw ? true : false);
                    break;
                case IC_NAME.PC3:
                    if (!Flags.Ac200On)
                        SetG7SA(true);//G7SAのフラグをチェックして、ONしていなければONさせる処理
                    SetRY100(sw ? true : false);
                    Sleep(150);
                    break;
                case IC_NAME.PC4:
                    if (!Flags.Ac200On)
                        SetG7SA(true);//G7SAのフラグをチェックして、ONしていなければONさせる処理
                    SetRY101(sw ? true : false);
                    Sleep(150);
                    break;
                case IC_NAME.PC5:
                    if (!Flags.Ac200On)
                        SetG7SA(true);//G7SAのフラグをチェックして、ONしていなければONさせる処理
                    SetRY102(sw ? true : false);
                    Sleep(150);
                    break;
                case IC_NAME.PC6:
                    if (!Flags.Ac200On)
                        SetG7SA(true);//G7SAのフラグをチェックして、ONしていなければONさせる処理
                    SetRY103(sw ? true : false);
                    Sleep(150);
                    break;
                case IC_NAME.PC7:
                    SetRL4(sw ? true : false);
                    break;
            }
            Sleep(25);
        }

        private static void AnalysisData(IC_NAME onName, bool ExpAllOff = false, bool sw_pc1_2_on = false)
        {
            ReadAndSetListSpecs();
            //ビューモデルの更新

            //期待値のセット
            if (ExpAllOff)
                ResetViewModelExp(sw_pc1_2_on);
            else
                SetViewModelExp(onName);

            //出力値セット
            SetViewModelIn();
        }
        private static void ReadAndSetListSpecs()
        {
            Target.SendData("R,U21,00,00");
            var buffU21 = Convert.ToInt32(Target.RecieveData, 16);

            Target.SendData("R,U6,00,00");
            var buffU22 = Convert.ToInt32(Target.RecieveData, 16);

            Target.SendData("R,U23,00,00");
            var buffU23 = Convert.ToInt32(Target.RecieveData, 16);

            Target.SendData("R,U7,00,00");
            var buffU7 = Convert.ToInt32(Target.RecieveData, 16);

            Target.SendData("R,PC1_6,00,00");
            var buffPC1_6 = Convert.ToInt32(Target.RecieveData, 16);


            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U21e).Input = (buffU21 & 0b0000_0001) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U21f).Input = (buffU21 & 0b0000_0010) != 0;

            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23a).Input = (buffU23 & 0b0000_0001) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23b).Input = (buffU23 & 0b0000_0010) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23c).Input = (buffU23 & 0b0000_0100) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23d).Input = (buffU23 & 0b0000_1000) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23e).Input = (buffU23 & 0b0001_0000) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23f).Input = (buffU23 & 0b0010_0000) != 0;

            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22a).Input = (buffU22 & 0b0000_0001) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22b).Input = (buffU22 & 0b0000_0010) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22c).Input = (buffU22 & 0b0000_0100) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22d).Input = (buffU22 & 0b0000_1000) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22e).Input = (buffU22 & 0b0001_0000) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22f).Input = (buffU22 & 0b0010_0000) != 0;

            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC1).Input = (buffPC1_6 & 0b0000_0001) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC2).Input = (buffPC1_6 & 0b0000_0010) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC3).Input = (buffPC1_6 & 0b0000_0100) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC4).Input = (buffPC1_6 & 0b0000_1000) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC5).Input = (buffPC1_6 & 0b0001_0000) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC6).Input = (buffPC1_6 & 0b0010_0000) != 0;
            ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC7).Input = (buffU7 & 0b0001_0000) != 0;

        }
        private static void ResetViewModelExp(bool sw_pc1_2_on = false)
        {
            //期待値
            State.VmTestResults.ExpU21e = OffBrush;
            State.VmTestResults.ExpU21f = OffBrush;

            State.VmTestResults.ExpU22a = OffBrush;
            State.VmTestResults.ExpU22b = OffBrush;
            State.VmTestResults.ExpU22c = OffBrush;
            State.VmTestResults.ExpU22d = OffBrush;
            State.VmTestResults.ExpU22e = OffBrush;
            State.VmTestResults.ExpU22f = OffBrush;

            State.VmTestResults.ExpU23a = OffBrush;
            State.VmTestResults.ExpU23b = OffBrush;
            State.VmTestResults.ExpU23c = OffBrush;
            State.VmTestResults.ExpU23d = OffBrush;
            State.VmTestResults.ExpU23e = OffBrush;
            State.VmTestResults.ExpU23f = OffBrush;

            State.VmTestResults.ExpPC1 = sw_pc1_2_on ? OnBrush : OffBrush;
            State.VmTestResults.ExpPC2 = sw_pc1_2_on ? OnBrush : OffBrush;
            State.VmTestResults.ExpPC3 = OffBrush;
            State.VmTestResults.ExpPC4 = OffBrush;
            State.VmTestResults.ExpPC5 = OffBrush;
            State.VmTestResults.ExpPC6 = OffBrush;
            State.VmTestResults.ExpPC7 = OffBrush;
        }
        private static void ResetViewModelIn()
        {
            //認識値
            State.VmTestResults.InputU21e = OffBrush;
            State.VmTestResults.InputU21f = OffBrush;

            State.VmTestResults.InputU22a = OffBrush;
            State.VmTestResults.InputU22b = OffBrush;
            State.VmTestResults.InputU22c = OffBrush;
            State.VmTestResults.InputU22d = OffBrush;
            State.VmTestResults.InputU22e = OffBrush;
            State.VmTestResults.InputU22f = OffBrush;

            State.VmTestResults.InputU23a = OffBrush;
            State.VmTestResults.InputU23b = OffBrush;
            State.VmTestResults.InputU23c = OffBrush;
            State.VmTestResults.InputU23d = OffBrush;
            State.VmTestResults.InputU23e = OffBrush;
            State.VmTestResults.InputU23f = OffBrush;

            State.VmTestResults.InputPC1 = OffBrush;
            State.VmTestResults.InputPC2 = OffBrush;
            State.VmTestResults.InputPC3 = OffBrush;
            State.VmTestResults.InputPC4 = OffBrush;
            State.VmTestResults.InputPC5 = OffBrush;
            State.VmTestResults.InputPC6 = OffBrush;
            State.VmTestResults.InputPC7 = OffBrush;

        }
        private static void SetViewModelIn()
        {
            State.VmTestResults.InputU21e = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U21e).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU21f = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U21f).Input ? OnBrush : OffBrush;

            State.VmTestResults.InputU22a = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22a).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU22b = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22b).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU22c = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22c).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU22d = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22d).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU22e = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22e).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU22f = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U22f).Input ? OnBrush : OffBrush;

            State.VmTestResults.InputU23a = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23a).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU23b = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23b).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU23c = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23c).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU23d = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23d).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU23e = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23e).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputU23f = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.U23f).Input ? OnBrush : OffBrush;

            State.VmTestResults.InputPC1 = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC1).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputPC2 = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC2).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputPC3 = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC3).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputPC4 = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC4).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputPC5 = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC5).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputPC6 = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC6).Input ? OnBrush : OffBrush;
            State.VmTestResults.InputPC7 = ListDinSpecs.FirstOrDefault(L => L.name == IC_NAME.PC7).Input ? OnBrush : OffBrush;
        }
        private static void SetViewModelExp(IC_NAME onName)
        {
            State.VmTestResults.ExpU21e = onName == IC_NAME.U21e ? OnBrush : OffBrush;
            State.VmTestResults.ExpU21f = onName == IC_NAME.U21f ? OnBrush : OffBrush;

            State.VmTestResults.ExpU22a = onName == IC_NAME.U22a ? OnBrush : OffBrush;
            State.VmTestResults.ExpU22b = onName == IC_NAME.U22b ? OnBrush : OffBrush;
            State.VmTestResults.ExpU22c = onName == IC_NAME.U22c ? OnBrush : OffBrush;
            State.VmTestResults.ExpU22d = onName == IC_NAME.U22d ? OnBrush : OffBrush;
            State.VmTestResults.ExpU22e = onName == IC_NAME.U22e ? OnBrush : OffBrush;
            State.VmTestResults.ExpU22f = onName == IC_NAME.U22f ? OnBrush : OffBrush;

            State.VmTestResults.ExpU23a = onName == IC_NAME.U23a ? OnBrush : OffBrush;
            State.VmTestResults.ExpU23b = onName == IC_NAME.U23b ? OnBrush : OffBrush;
            State.VmTestResults.ExpU23c = onName == IC_NAME.U23c ? OnBrush : OffBrush;
            State.VmTestResults.ExpU23d = onName == IC_NAME.U23d ? OnBrush : OffBrush;
            State.VmTestResults.ExpU23e = onName == IC_NAME.U23e ? OnBrush : OffBrush;
            State.VmTestResults.ExpU23f = onName == IC_NAME.U23f ? OnBrush : OffBrush;

            State.VmTestResults.ExpPC1 = (onName == IC_NAME.PC1 || onName == IC_NAME.PC2 || onName == IC_NAME.PC3 || onName == IC_NAME.PC4 || onName == IC_NAME.PC5 || onName == IC_NAME.PC6) ? OnBrush : OffBrush;
            State.VmTestResults.ExpPC2 = (onName == IC_NAME.PC1 || onName == IC_NAME.PC2 || onName == IC_NAME.PC3 || onName == IC_NAME.PC4 || onName == IC_NAME.PC5 || onName == IC_NAME.PC6) ? OnBrush : OffBrush;
            State.VmTestResults.ExpPC3 = onName == IC_NAME.PC3 ? OnBrush : OffBrush;
            State.VmTestResults.ExpPC4 = onName == IC_NAME.PC4 ? OnBrush : OffBrush;
            State.VmTestResults.ExpPC5 = onName == IC_NAME.PC5 ? OnBrush : OffBrush;
            State.VmTestResults.ExpPC6 = onName == IC_NAME.PC6 ? OnBrush : OffBrush;
            State.VmTestResults.ExpPC7 = onName == IC_NAME.PC7 ? OnBrush : OffBrush;
        }

    }
}