using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using static New91820060Tester.General;
using static System.Threading.Thread;

namespace New91820060Tester
{
    public static class デジタル出力チェック
    {
        public enum NAME
        {
            U9_1, U9_2, U9_3, U9_4, U9_5, U9_6, U9_7, U9_8,
            U10_1, U10_2, U10_3, U10_4, U10_5, U10_6, U10_7, U10_8,
            Q1, Q2, /*Q3,*/
            U11_3, U11_4, U11_5, U11_6, U11_7, U11_8,
            U209_1, U209_2, U209_3, U209_4, U209_5, U209_6, U209_7, U209_8,
            U213a1, U213a2, U213a3, U213b1, U213b2, U213b3, U213c1, U213c2, U213c3, U213d1, U213d2, U213d3, U213e1, U213e2, U213e3, U213f1, U213f2, U213f3,
            U214a1, U214a2, U214a3, U214b1, U214b2, U214b3, U214c, U214d, U214e, U214f,
            U215a, U215b, U215c, U215d,
        }
        public class DoutTestSpec
        {
            public NAME name;
            public bool Output;
        }

        public static List<DoutTestSpec> ListSpecs;

        private static void InitList()
        {
            ListSpecs = new List<DoutTestSpec>();
            foreach (var n in Enum.GetValues(typeof(NAME)))
            {
                ListSpecs.Add(new DoutTestSpec { name = (NAME)n });
            }
        }

        public static void SetAllOff()
        {
            Target.SendData("W,ALL,00,00");
        }

        public static async Task<bool> CheckDout()
        {
            bool resultOn = false;
            bool resultOff = false;


            try
            {
                return await Task<bool>.Run(() =>
                {
                    ResetViewModelExp();
                    ResetViewModelOut();

                    SetG7SA(true);//CN14よりAC200Vを入力する
                    Sleep(300);

                    Flags.AddDecision = false;
                    InitList();//テストスペック毎回初期化

                    SetAllOff();//出力初期化
                    Sleep(300);

                    return ListSpecs.All(L =>
                    {
                        resultOn = false;
                        resultOff = false;

                        if (Flags.ClickStopButton) return false;

                        //テストログの更新
                        State.VmTestStatus.TestLog += "\r\n" + L.name.ToString() + " ONチェック";

                        //ONチェック
                        var cmd = GetOutputCmd(L.name);
                        if (!Target.SendData($"W,{cmd},01")) return false;
                        wait(L.name);
                        AnalysisData(L.name);

                        var targetState = ListSpecs.FirstOrDefault(l => l.name == L.name);
                        switch (L.name)
                        {
                            case NAME.U213a1:
                            case NAME.U213a2:
                            case NAME.U213a3:
                            case NAME.U213b1:
                            case NAME.U213b2:
                            case NAME.U213b3:
                            case NAME.U213c1:
                            case NAME.U213c2:
                            case NAME.U213c3:
                            case NAME.U213d1:
                            case NAME.U213d2:
                            case NAME.U213d3:
                            case NAME.U213e1:
                            case NAME.U213e2:
                            case NAME.U213e3:
                            case NAME.U213f1:
                            case NAME.U213f2:
                            case NAME.U213f3:
                            case NAME.U214a1:
                            case NAME.U214a2:
                            case NAME.U214a3:
                            case NAME.U214b1:
                            case NAME.U214b2:
                            case NAME.U214b3:
                                resultOn = (targetState.Output && ListSpecs.Where(l => l.Output).Count() == 3);
                                break;
                            default:
                                resultOn = (targetState.Output && ListSpecs.Where(l => l.Output).Count() == 1);
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
                        State.VmTestStatus.TestLog += "\r\n" + L.name.ToString() + " OFFチェック";
                        if (!Target.SendData($"W,{cmd},00")) return false;
                        wait(L.name);
                        AnalysisData(L.name, true);

                        resultOff = ListSpecs.All(list =>
                        {
                            return !list.Output;
                        });

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
                });
            }
            finally
            {
                SetG7SA(false);//CN14へのAC200V入力をOFFする
                State.VmTestStatus.TestLog += "\r\n";

                if (!resultOn || !resultOff)
                {
                    State.VmTestStatus.Spec = "規格値 : ---";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }
        }

        private static void wait(NAME name)
        {
            switch (name)
            {
                case NAME.U11_5:
                case NAME.U11_6:
                case NAME.U11_7:
                case NAME.U11_8:
                case NAME.U209_2:
                case NAME.U209_3:
                case NAME.U209_4:
                case NAME.U209_5:
                case NAME.U209_6:
                case NAME.U209_7:
                case NAME.U209_8:

                    Sleep(400);
                    break;
                default:
                    Sleep(100);
                    break;
            }
        }


        private static string GetOutputCmd(NAME name)
        {
            var cmd = "";
            switch (name)
            {
                case NAME.U9_1: cmd = "U9,01"; break;
                case NAME.U9_2: cmd = "U9,02"; break;
                case NAME.U9_3: cmd = "U9,03"; break;
                case NAME.U9_4: cmd = "U9,04"; break;
                case NAME.U9_5: cmd = "U9,05"; break;
                case NAME.U9_6: cmd = "U9,06"; break;
                case NAME.U9_7: cmd = "U9,07"; break;
                case NAME.U9_8: cmd = "U9,08"; break;

                case NAME.U10_1: cmd = "U10,01"; break;
                case NAME.U10_2: cmd = "U10,02"; break;
                case NAME.U10_3: cmd = "U10,03"; break;
                case NAME.U10_4: cmd = "U10,04"; break;
                case NAME.U10_5: cmd = "U10,05"; break;
                case NAME.U10_6: cmd = "U10,06"; break;
                case NAME.U10_7: cmd = "U10,07"; break;
                case NAME.U10_8: cmd = "U10,08"; break;

                case NAME.Q1: cmd = "Q1,00"; break;
                case NAME.Q2: cmd = "Q2,00"; break;
                //case NAME.Q3:    cmd = "Q3,00"; break;

                case NAME.U11_3: cmd = "U11,03"; break;
                case NAME.U11_4: cmd = "U11,04"; break;
                case NAME.U11_5: cmd = "U11,05"; break;
                case NAME.U11_6: cmd = "U11,06"; break;
                case NAME.U11_7: cmd = "U11,07"; break;
                case NAME.U11_8: cmd = "U11,08"; break;

                case NAME.U209_1: cmd = "U209,01"; break;
                case NAME.U209_2: cmd = "U209,02"; break;
                case NAME.U209_3: cmd = "U209,03"; break;
                case NAME.U209_4: cmd = "U209,04"; break;
                case NAME.U209_5: cmd = "U209,05"; break;
                case NAME.U209_6: cmd = "U209,06"; break;
                case NAME.U209_7: cmd = "U209,07"; break;
                case NAME.U209_8: cmd = "U209,08"; break;

                case NAME.U213a1:
                case NAME.U213a2:
                case NAME.U213a3:
                    cmd = "U213,02"; break;
                case NAME.U213b1:
                case NAME.U213b2:
                case NAME.U213b3:
                    cmd = "U213,04"; break;
                case NAME.U213c1:
                case NAME.U213c2:
                case NAME.U213c3:
                    cmd = "U213,06"; break;
                case NAME.U213d1:
                case NAME.U213d2:
                case NAME.U213d3:
                    cmd = "U213,10"; break;
                case NAME.U213e1:
                case NAME.U213e2:
                case NAME.U213e3:
                    cmd = "U213,12"; break;
                case NAME.U213f1:
                case NAME.U213f2:
                case NAME.U213f3:
                    cmd = "U213,15"; break;


                case NAME.U214a1:
                case NAME.U214a2:
                case NAME.U214a3:
                    cmd = "U214,02"; break;
                case NAME.U214b1:
                case NAME.U214b2:
                case NAME.U214b3:
                    cmd = "U214,04"; break;

                case NAME.U214c: cmd = "U214,06"; break;
                case NAME.U214d: cmd = "U214,10"; break;
                case NAME.U214e: cmd = "U214,12"; break;
                case NAME.U214f: cmd = "U214,15"; break;

                case NAME.U215a: cmd = "U215,02"; break;
                case NAME.U215b: cmd = "U215,04"; break;
                case NAME.U215c: cmd = "U215,06"; break;
                case NAME.U215d: cmd = "U215,10"; break;
            }
            return cmd;
        }
        private static void AnalysisData(NAME onName, bool ExpAllOff = false)
        {
            ReadAndSetListSpecs();
            //ビューモデルの更新

            //期待値のセット
            if (ExpAllOff)
                ResetViewModelExp();
            else
                SetViewModelExp(onName);

            //出力値セット
            SetViewModelOut();
        }
        private static void ReadAndSetListSpecs()
        {

            io2.ReadInputData(EPX64S.PORT.P0);
            io2.ReadInputData(EPX64S.PORT.P1);
            io2.ReadInputData(EPX64S.PORT.P2);
            io2.ReadInputData(EPX64S.PORT.P3);
            io2.ReadInputData(EPX64S.PORT.P4);
            io2.ReadInputData(EPX64S.PORT.P5);
            io2.ReadInputData(EPX64S.PORT.P7);

            io1.ReadInputData(EPX64S.PORT.P3);
            io1.ReadInputData(EPX64S.PORT.P7);

            var io2P0Data = io2.P0InputData;
            var io2P1Data = io2.P1InputData;
            var io2P2Data = io2.P2InputData;
            var io2P3Data = io2.P3InputData;
            var io2P4Data = io2.P4InputData;
            var io2P5Data = io2.P5InputData;
            var io2P6Data = io2.P6InputData;
            var io2P7Data = io2.P7InputData;

            var io1P3Data = io1.P3InputData;
            var io1P7Data = io1.P7InputData;

            ListSpecs.FirstOrDefault(L => L.name == NAME.U9_1).Output = (io2P0Data & 0b1000_0000) == 0;//IO2 P07
            ListSpecs.FirstOrDefault(L => L.name == NAME.U9_2).Output = (io2P0Data & 0b0100_0000) == 0;//IO2 P06
            ListSpecs.FirstOrDefault(L => L.name == NAME.U9_3).Output = (io2P0Data & 0b0010_0000) == 0;//IO2 P05
            ListSpecs.FirstOrDefault(L => L.name == NAME.U9_4).Output = (io2P0Data & 0b0001_0000) == 0;//IO2 P04
            ListSpecs.FirstOrDefault(L => L.name == NAME.U9_5).Output = (io2P0Data & 0b0000_1000) == 0;//IO2 P03
            ListSpecs.FirstOrDefault(L => L.name == NAME.U9_6).Output = (io2P0Data & 0b0000_0100) == 0;//IO2 P02
            ListSpecs.FirstOrDefault(L => L.name == NAME.U9_7).Output = (io2P0Data & 0b0000_0010) == 0;//IO2 P01
            ListSpecs.FirstOrDefault(L => L.name == NAME.U9_8).Output = (io2P0Data & 0b0000_0001) == 0;//IO2 P00

            ListSpecs.FirstOrDefault(L => L.name == NAME.U10_1).Output = (io2P7Data & 0b1000_0000) == 0;//IO2 P77
            ListSpecs.FirstOrDefault(L => L.name == NAME.U10_2).Output = (io2P3Data & 0b0100_0000) == 0;//IO2 P36
            ListSpecs.FirstOrDefault(L => L.name == NAME.U10_3).Output = (io2P3Data & 0b0010_0000) == 0;//IO2 P35
            ListSpecs.FirstOrDefault(L => L.name == NAME.U10_4).Output = (io2P3Data & 0b0001_0000) == 0;//IO2 P34
            ListSpecs.FirstOrDefault(L => L.name == NAME.U10_5).Output = (io2P3Data & 0b0000_1000) == 0;//IO2 P33
            ListSpecs.FirstOrDefault(L => L.name == NAME.U10_6).Output = (io2P3Data & 0b0000_0100) == 0;//IO2 P32
            ListSpecs.FirstOrDefault(L => L.name == NAME.U10_7).Output = (io2P3Data & 0b0000_0010) == 0;//IO2 P31
            ListSpecs.FirstOrDefault(L => L.name == NAME.U10_8).Output = (io2P3Data & 0b0000_0001) == 0;//IO2 P30

            ListSpecs.FirstOrDefault(L => L.name == NAME.U11_3).Output = (io1P7Data & 0b0000_0010) == 0;//IO1 P71
            ListSpecs.FirstOrDefault(L => L.name == NAME.U11_4).Output = (io1P7Data & 0b0000_0001) == 0;//IO1 P70
            ListSpecs.FirstOrDefault(L => L.name == NAME.U11_5).Output = (io2P7Data & 0b0000_1000) == 0;//IO2 P73
            ListSpecs.FirstOrDefault(L => L.name == NAME.U11_6).Output = (io2P7Data & 0b0000_0100) == 0;//IO2 P72
            ListSpecs.FirstOrDefault(L => L.name == NAME.U11_7).Output = (io2P7Data & 0b0000_0010) == 0;//IO2 P71
            ListSpecs.FirstOrDefault(L => L.name == NAME.U11_8).Output = (io2P7Data & 0b0000_0001) == 0;//IO2 P70

            ListSpecs.FirstOrDefault(L => L.name == NAME.Q1).Output = (io1P7Data & 0b0000_1000) == 0;//IO1 P73
            ListSpecs.FirstOrDefault(L => L.name == NAME.Q2).Output = (io1P7Data & 0b0000_0100) == 0;//IO1 P72
            //ListSpecs.FirstOrDefault(L => L.name == NAME.Q3).Output = (io2P7Data & 0b0001_0000) == 0;//IO2 P74

            ListSpecs.FirstOrDefault(L => L.name == NAME.U209_1).Output = (io2P7Data & 0b0100_0000) == 0;//IO2 P76
            ListSpecs.FirstOrDefault(L => L.name == NAME.U209_2).Output = (io1P3Data & 0b0100_0000) == 0;//IO1 P36
            ListSpecs.FirstOrDefault(L => L.name == NAME.U209_3).Output = (io1P3Data & 0b0010_0000) == 0;//IO1 P35
            ListSpecs.FirstOrDefault(L => L.name == NAME.U209_4).Output = (io1P3Data & 0b0001_0000) == 0;//IO1 P34
            ListSpecs.FirstOrDefault(L => L.name == NAME.U209_5).Output = (io1P3Data & 0b0000_1000) == 0;//IO1 P33
            ListSpecs.FirstOrDefault(L => L.name == NAME.U209_6).Output = (io1P3Data & 0b0000_0100) == 0;//IO1 P32
            ListSpecs.FirstOrDefault(L => L.name == NAME.U209_7).Output = (io1P3Data & 0b0000_0010) == 0;//IO1 P31
            ListSpecs.FirstOrDefault(L => L.name == NAME.U209_8).Output = (io1P3Data & 0b0000_0001) == 0;//IO1 P30

            ListSpecs.FirstOrDefault(L => L.name == NAME.U213a1).Output = (io2P1Data & 0b0000_0001) != 0;//IO2 P10
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213b1).Output = (io2P1Data & 0b0000_0010) != 0;//IO2 P11
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213c1).Output = (io2P1Data & 0b0000_0100) != 0;//IO2 P12
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213d1).Output = (io2P1Data & 0b0000_1000) != 0;//IO2 P13
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213e1).Output = (io2P1Data & 0b0001_0000) != 0;//IO2 P14
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213f1).Output = (io2P1Data & 0b0010_0000) != 0;//IO2 P15
            ListSpecs.FirstOrDefault(L => L.name == NAME.U214a1).Output = (io2P1Data & 0b0100_0000) != 0;//IO2 P16
            ListSpecs.FirstOrDefault(L => L.name == NAME.U214b1).Output = (io2P1Data & 0b1000_0000) != 0;//IO2 P17

            ListSpecs.FirstOrDefault(L => L.name == NAME.U213a2).Output = (io2P2Data & 0b0000_0001) == 0;//IO2 P20
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213b2).Output = (io2P2Data & 0b0000_0010) == 0;//IO2 P21
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213c2).Output = (io2P2Data & 0b0000_0100) == 0;//IO2 P22
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213d2).Output = (io2P2Data & 0b0000_1000) == 0;//IO2 P23
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213e2).Output = (io2P4Data & 0b0000_0001) == 0;//IO2 P40
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213f2).Output = (io2P4Data & 0b0000_0010) == 0;//IO2 P41
            ListSpecs.FirstOrDefault(L => L.name == NAME.U214a2).Output = (io2P4Data & 0b0000_0100) == 0;//IO2 P42
            ListSpecs.FirstOrDefault(L => L.name == NAME.U214b2).Output = (io2P4Data & 0b0000_1000) == 0;//IO2 P43

            ListSpecs.FirstOrDefault(L => L.name == NAME.U213a3).Output = (io2P2Data & 0b0001_0000) == 0;//IO2 P24
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213b3).Output = (io2P2Data & 0b0010_0000) == 0;//IO2 P25
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213c3).Output = (io2P2Data & 0b0100_0000) == 0;//IO2 P26
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213d3).Output = (io2P2Data & 0b1000_0000) == 0;//IO2 P27
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213e3).Output = (io2P4Data & 0b0001_0000) == 0;//IO2 P44
            ListSpecs.FirstOrDefault(L => L.name == NAME.U213f3).Output = (io2P4Data & 0b0010_0000) == 0;//IO2 P45
            ListSpecs.FirstOrDefault(L => L.name == NAME.U214a3).Output = (io2P4Data & 0b0100_0000) == 0;//IO2 P46
            ListSpecs.FirstOrDefault(L => L.name == NAME.U214b3).Output = (io2P4Data & 0b1000_0000) == 0;//IO2 P47

            ListSpecs.FirstOrDefault(L => L.name == NAME.U214c).Output = (io2P5Data & 0b0000_0001) == 0;//IO2 P50
            ListSpecs.FirstOrDefault(L => L.name == NAME.U214d).Output = (io2P5Data & 0b0000_0010) == 0;//IO2 P51
            ListSpecs.FirstOrDefault(L => L.name == NAME.U214e).Output = (io2P5Data & 0b0000_0100) == 0;//IO2 P52
            ListSpecs.FirstOrDefault(L => L.name == NAME.U214f).Output = (io2P5Data & 0b0000_1000) == 0;//IO2 P53
            ListSpecs.FirstOrDefault(L => L.name == NAME.U215a).Output = (io2P5Data & 0b0001_0000) == 0;//IO2 P54
            ListSpecs.FirstOrDefault(L => L.name == NAME.U215b).Output = (io2P5Data & 0b0010_0000) == 0;//IO2 P55
            ListSpecs.FirstOrDefault(L => L.name == NAME.U215c).Output = (io2P5Data & 0b0100_0000) == 0;//IO2 P56
            ListSpecs.FirstOrDefault(L => L.name == NAME.U215d).Output = (io2P5Data & 0b1000_0000) == 0;//IO2 P57
        }
        private static void ResetViewModelExp()
        {
            //期待値
            State.VmTestResults.ExpU9_1 = OffBrush;
            State.VmTestResults.ExpU9_2 = OffBrush;
            State.VmTestResults.ExpU9_3 = OffBrush;
            State.VmTestResults.ExpU9_4 = OffBrush;
            State.VmTestResults.ExpU9_5 = OffBrush;
            State.VmTestResults.ExpU9_6 = OffBrush;
            State.VmTestResults.ExpU9_7 = OffBrush;
            State.VmTestResults.ExpU9_8 = OffBrush;

            State.VmTestResults.ExpU10_1 = OffBrush;
            State.VmTestResults.ExpU10_2 = OffBrush;
            State.VmTestResults.ExpU10_3 = OffBrush;
            State.VmTestResults.ExpU10_4 = OffBrush;
            State.VmTestResults.ExpU10_5 = OffBrush;
            State.VmTestResults.ExpU10_6 = OffBrush;
            State.VmTestResults.ExpU10_7 = OffBrush;
            State.VmTestResults.ExpU10_8 = OffBrush;

            State.VmTestResults.ExpQ1 = OffBrush;
            State.VmTestResults.ExpQ2 = OffBrush;
            State.VmTestResults.ExpQ3 = OffBrush;

            State.VmTestResults.ExpU11_3 = OffBrush;
            State.VmTestResults.ExpU11_4 = OffBrush;
            State.VmTestResults.ExpU11_5 = OffBrush;
            State.VmTestResults.ExpU11_6 = OffBrush;
            State.VmTestResults.ExpU11_7 = OffBrush;
            State.VmTestResults.ExpU11_8 = OffBrush;

            State.VmTestResults.ExpU209_1 = OffBrush;
            State.VmTestResults.ExpU209_2 = OffBrush;
            State.VmTestResults.ExpU209_3 = OffBrush;
            State.VmTestResults.ExpU209_4 = OffBrush;
            State.VmTestResults.ExpU209_5 = OffBrush;
            State.VmTestResults.ExpU209_6 = OffBrush;
            State.VmTestResults.ExpU209_7 = OffBrush;
            State.VmTestResults.ExpU209_8 = OffBrush;

            State.VmTestResults.ExpU213a1 = OffBrush;
            State.VmTestResults.ExpU213b1 = OffBrush;
            State.VmTestResults.ExpU213c1 = OffBrush;
            State.VmTestResults.ExpU213d1 = OffBrush;
            State.VmTestResults.ExpU213e1 = OffBrush;
            State.VmTestResults.ExpU213f1 = OffBrush;
            State.VmTestResults.ExpU213a2 = OffBrush;
            State.VmTestResults.ExpU213b2 = OffBrush;
            State.VmTestResults.ExpU213c2 = OffBrush;
            State.VmTestResults.ExpU213d2 = OffBrush;
            State.VmTestResults.ExpU213e2 = OffBrush;
            State.VmTestResults.ExpU213f2 = OffBrush;
            State.VmTestResults.ExpU213a3 = OffBrush;
            State.VmTestResults.ExpU213b3 = OffBrush;
            State.VmTestResults.ExpU213c3 = OffBrush;
            State.VmTestResults.ExpU213d3 = OffBrush;
            State.VmTestResults.ExpU213e3 = OffBrush;
            State.VmTestResults.ExpU213f3 = OffBrush;


            State.VmTestResults.ExpU214a1 = OffBrush;
            State.VmTestResults.ExpU214a2 = OffBrush;
            State.VmTestResults.ExpU214a3 = OffBrush;
            State.VmTestResults.ExpU214b1 = OffBrush;
            State.VmTestResults.ExpU214b2 = OffBrush;
            State.VmTestResults.ExpU214b3 = OffBrush;
            State.VmTestResults.ExpU214c = OffBrush;
            State.VmTestResults.ExpU214d = OffBrush;
            State.VmTestResults.ExpU214e = OffBrush;
            State.VmTestResults.ExpU214f = OffBrush;

            State.VmTestResults.ExpU215a = OffBrush;
            State.VmTestResults.ExpU215b = OffBrush;
            State.VmTestResults.ExpU215c = OffBrush;
            State.VmTestResults.ExpU215d = OffBrush;

        }
        private static void ResetViewModelOut()
        {
            //出力値
            State.VmTestResults.OutU9_1 = OffBrush;
            State.VmTestResults.OutU9_2 = OffBrush;
            State.VmTestResults.OutU9_3 = OffBrush;
            State.VmTestResults.OutU9_4 = OffBrush;
            State.VmTestResults.OutU9_5 = OffBrush;
            State.VmTestResults.OutU9_6 = OffBrush;
            State.VmTestResults.OutU9_7 = OffBrush;
            State.VmTestResults.OutU9_8 = OffBrush;

            State.VmTestResults.OutU10_1 = OffBrush;
            State.VmTestResults.OutU10_2 = OffBrush;
            State.VmTestResults.OutU10_3 = OffBrush;
            State.VmTestResults.OutU10_4 = OffBrush;
            State.VmTestResults.OutU10_5 = OffBrush;
            State.VmTestResults.OutU10_6 = OffBrush;
            State.VmTestResults.OutU10_7 = OffBrush;
            State.VmTestResults.OutU10_8 = OffBrush;

            State.VmTestResults.OutQ1 = OffBrush;
            State.VmTestResults.OutQ2 = OffBrush;
            State.VmTestResults.OutQ3 = OffBrush;

            State.VmTestResults.OutU11_3 = OffBrush;
            State.VmTestResults.OutU11_4 = OffBrush;
            State.VmTestResults.OutU11_5 = OffBrush;
            State.VmTestResults.OutU11_6 = OffBrush;
            State.VmTestResults.OutU11_7 = OffBrush;
            State.VmTestResults.OutU11_8 = OffBrush;

            State.VmTestResults.OutU209_1 = OffBrush;
            State.VmTestResults.OutU209_2 = OffBrush;
            State.VmTestResults.OutU209_3 = OffBrush;
            State.VmTestResults.OutU209_4 = OffBrush;
            State.VmTestResults.OutU209_5 = OffBrush;
            State.VmTestResults.OutU209_6 = OffBrush;
            State.VmTestResults.OutU209_7 = OffBrush;
            State.VmTestResults.OutU209_8 = OffBrush;

            State.VmTestResults.OutU213a1 = OffBrush;
            State.VmTestResults.OutU213b1 = OffBrush;
            State.VmTestResults.OutU213c1 = OffBrush;
            State.VmTestResults.OutU213d1 = OffBrush;
            State.VmTestResults.OutU213e1 = OffBrush;
            State.VmTestResults.OutU213f1 = OffBrush;
            State.VmTestResults.OutU213a2 = OffBrush;
            State.VmTestResults.OutU213b2 = OffBrush;
            State.VmTestResults.OutU213c2 = OffBrush;
            State.VmTestResults.OutU213d2 = OffBrush;
            State.VmTestResults.OutU213e2 = OffBrush;
            State.VmTestResults.OutU213f2 = OffBrush;
            State.VmTestResults.OutU213a3 = OffBrush;
            State.VmTestResults.OutU213b3 = OffBrush;
            State.VmTestResults.OutU213c3 = OffBrush;
            State.VmTestResults.OutU213d3 = OffBrush;
            State.VmTestResults.OutU213e3 = OffBrush;
            State.VmTestResults.OutU213f3 = OffBrush;


            State.VmTestResults.OutU214a1 = OffBrush;
            State.VmTestResults.OutU214a2 = OffBrush;
            State.VmTestResults.OutU214a3 = OffBrush;
            State.VmTestResults.OutU214b1 = OffBrush;
            State.VmTestResults.OutU214b2 = OffBrush;
            State.VmTestResults.OutU214b3 = OffBrush;
            State.VmTestResults.OutU214c = OffBrush;
            State.VmTestResults.OutU214d = OffBrush;
            State.VmTestResults.OutU214e = OffBrush;
            State.VmTestResults.OutU214f = OffBrush;

            State.VmTestResults.OutU215a = OffBrush;
            State.VmTestResults.OutU215b = OffBrush;
            State.VmTestResults.OutU215c = OffBrush;
            State.VmTestResults.OutU215d = OffBrush;
        }
        private static void SetViewModelOut()
        {
            State.VmTestResults.OutU9_1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U9_1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU9_2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U9_2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU9_3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U9_3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU9_4 = ListSpecs.FirstOrDefault(L => L.name == NAME.U9_4).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU9_5 = ListSpecs.FirstOrDefault(L => L.name == NAME.U9_5).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU9_6 = ListSpecs.FirstOrDefault(L => L.name == NAME.U9_6).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU9_7 = ListSpecs.FirstOrDefault(L => L.name == NAME.U9_7).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU9_8 = ListSpecs.FirstOrDefault(L => L.name == NAME.U9_8).Output ? OnBrush : OffBrush;

            State.VmTestResults.OutU10_1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U10_1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU10_2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U10_2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU10_3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U10_3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU10_4 = ListSpecs.FirstOrDefault(L => L.name == NAME.U10_4).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU10_5 = ListSpecs.FirstOrDefault(L => L.name == NAME.U10_5).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU10_6 = ListSpecs.FirstOrDefault(L => L.name == NAME.U10_6).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU10_7 = ListSpecs.FirstOrDefault(L => L.name == NAME.U10_7).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU10_8 = ListSpecs.FirstOrDefault(L => L.name == NAME.U10_8).Output ? OnBrush : OffBrush;

            State.VmTestResults.OutQ1 = ListSpecs.FirstOrDefault(L => L.name == NAME.Q1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutQ2 = ListSpecs.FirstOrDefault(L => L.name == NAME.Q2).Output ? OnBrush : OffBrush;
            //State.VmTestResults.OutQ3 = ListSpecs.FirstOrDefault(L => L.name == NAME.Q3).Output ? OnBrush : OffBrush;

            State.VmTestResults.OutU11_3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U11_3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU11_4 = ListSpecs.FirstOrDefault(L => L.name == NAME.U11_4).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU11_5 = ListSpecs.FirstOrDefault(L => L.name == NAME.U11_5).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU11_6 = ListSpecs.FirstOrDefault(L => L.name == NAME.U11_6).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU11_7 = ListSpecs.FirstOrDefault(L => L.name == NAME.U11_7).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU11_8 = ListSpecs.FirstOrDefault(L => L.name == NAME.U11_8).Output ? OnBrush : OffBrush;

            State.VmTestResults.OutU209_1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U209_1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU209_2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U209_2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU209_3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U209_3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU209_4 = ListSpecs.FirstOrDefault(L => L.name == NAME.U209_4).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU209_5 = ListSpecs.FirstOrDefault(L => L.name == NAME.U209_5).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU209_6 = ListSpecs.FirstOrDefault(L => L.name == NAME.U209_6).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU209_7 = ListSpecs.FirstOrDefault(L => L.name == NAME.U209_7).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU209_8 = ListSpecs.FirstOrDefault(L => L.name == NAME.U209_8).Output ? OnBrush : OffBrush;

            State.VmTestResults.OutU213a1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213a1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213a2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213a2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213a3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213a3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213b1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213b1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213b2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213b2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213b3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213b3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213c1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213c1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213c2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213c2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213c3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213c3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213d1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213d1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213d2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213d2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213d3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213d3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213e1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213e1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213e2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213e2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213e3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213e3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213f1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213f1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213f2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213f2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU213f3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U213f3).Output ? OnBrush : OffBrush;

            State.VmTestResults.OutU214a1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U214a1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU214a2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U214a2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU214a3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U214a3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU214b1 = ListSpecs.FirstOrDefault(L => L.name == NAME.U214b1).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU214b2 = ListSpecs.FirstOrDefault(L => L.name == NAME.U214b2).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU214b3 = ListSpecs.FirstOrDefault(L => L.name == NAME.U214b3).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU214c = ListSpecs.FirstOrDefault(L => L.name == NAME.U214c).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU214d = ListSpecs.FirstOrDefault(L => L.name == NAME.U214d).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU214e = ListSpecs.FirstOrDefault(L => L.name == NAME.U214e).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU214f = ListSpecs.FirstOrDefault(L => L.name == NAME.U214f).Output ? OnBrush : OffBrush;

            State.VmTestResults.OutU215a = ListSpecs.FirstOrDefault(L => L.name == NAME.U215a).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU215b = ListSpecs.FirstOrDefault(L => L.name == NAME.U215b).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU215c = ListSpecs.FirstOrDefault(L => L.name == NAME.U215c).Output ? OnBrush : OffBrush;
            State.VmTestResults.OutU215d = ListSpecs.FirstOrDefault(L => L.name == NAME.U215d).Output ? OnBrush : OffBrush;

        }
        private static void SetViewModelExp(NAME onName)
        {
            State.VmTestResults.ExpU9_1 = onName == NAME.U9_1 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU9_2 = onName == NAME.U9_2 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU9_3 = onName == NAME.U9_3 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU9_4 = onName == NAME.U9_4 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU9_5 = onName == NAME.U9_5 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU9_6 = onName == NAME.U9_6 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU9_7 = onName == NAME.U9_7 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU9_8 = onName == NAME.U9_8 ? OnBrush : OffBrush;

            State.VmTestResults.ExpU10_1 = onName == NAME.U10_1 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU10_2 = onName == NAME.U10_2 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU10_3 = onName == NAME.U10_3 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU10_4 = onName == NAME.U10_4 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU10_5 = onName == NAME.U10_5 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU10_6 = onName == NAME.U10_6 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU10_7 = onName == NAME.U10_7 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU10_8 = onName == NAME.U10_8 ? OnBrush : OffBrush;

            State.VmTestResults.ExpQ1 = onName == NAME.Q1 ? OnBrush : OffBrush;
            State.VmTestResults.ExpQ2 = onName == NAME.Q2 ? OnBrush : OffBrush;
            //State.VmTestResults.ExpQ3 = onName == NAME.Q3 ? OnBrush : OffBrush;

            State.VmTestResults.ExpU11_3 = onName == NAME.U11_3 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU11_4 = onName == NAME.U11_4 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU11_5 = onName == NAME.U11_5 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU11_6 = onName == NAME.U11_6 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU11_7 = onName == NAME.U11_7 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU11_8 = onName == NAME.U11_8 ? OnBrush : OffBrush;

            State.VmTestResults.ExpU209_1 = onName == NAME.U209_1 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU209_2 = onName == NAME.U209_2 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU209_3 = onName == NAME.U209_3 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU209_4 = onName == NAME.U209_4 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU209_5 = onName == NAME.U209_5 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU209_6 = onName == NAME.U209_6 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU209_7 = onName == NAME.U209_7 ? OnBrush : OffBrush;
            State.VmTestResults.ExpU209_8 = onName == NAME.U209_8 ? OnBrush : OffBrush;

            State.VmTestResults.ExpU213a1 = (onName == NAME.U213a1 || onName == NAME.U213a2 || onName == NAME.U213a3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213a2 = (onName == NAME.U213a1 || onName == NAME.U213a2 || onName == NAME.U213a3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213a3 = (onName == NAME.U213a1 || onName == NAME.U213a2 || onName == NAME.U213a3) ? OnBrush : OffBrush;

            State.VmTestResults.ExpU213b1 = (onName == NAME.U213b1 || onName == NAME.U213b2 || onName == NAME.U213b3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213b2 = (onName == NAME.U213b1 || onName == NAME.U213b2 || onName == NAME.U213b3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213b3 = (onName == NAME.U213b1 || onName == NAME.U213b2 || onName == NAME.U213b3) ? OnBrush : OffBrush;

            State.VmTestResults.ExpU213c1 = (onName == NAME.U213c1 || onName == NAME.U213c2 || onName == NAME.U213c3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213c2 = (onName == NAME.U213c1 || onName == NAME.U213c2 || onName == NAME.U213c3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213c3 = (onName == NAME.U213c1 || onName == NAME.U213c2 || onName == NAME.U213c3) ? OnBrush : OffBrush;

            State.VmTestResults.ExpU213d1 = (onName == NAME.U213d1 || onName == NAME.U213d2 || onName == NAME.U213d3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213d2 = (onName == NAME.U213d1 || onName == NAME.U213d2 || onName == NAME.U213d3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213d3 = (onName == NAME.U213d1 || onName == NAME.U213d2 || onName == NAME.U213d3) ? OnBrush : OffBrush;

            State.VmTestResults.ExpU213e1 = (onName == NAME.U213e1 || onName == NAME.U213e2 || onName == NAME.U213e3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213e2 = (onName == NAME.U213e1 || onName == NAME.U213e2 || onName == NAME.U213e3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213e3 = (onName == NAME.U213e1 || onName == NAME.U213e2 || onName == NAME.U213e3) ? OnBrush : OffBrush;

            State.VmTestResults.ExpU213f1 = (onName == NAME.U213f1 || onName == NAME.U213f2 || onName == NAME.U213f3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213f2 = (onName == NAME.U213f1 || onName == NAME.U213f2 || onName == NAME.U213f3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU213f3 = (onName == NAME.U213f1 || onName == NAME.U213f2 || onName == NAME.U213f3) ? OnBrush : OffBrush;

            State.VmTestResults.ExpU214a1 = (onName == NAME.U214a1 || onName == NAME.U214a2 || onName == NAME.U214a3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU214a2 = (onName == NAME.U214a1 || onName == NAME.U214a2 || onName == NAME.U214a3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU214a3 = (onName == NAME.U214a1 || onName == NAME.U214a2 || onName == NAME.U214a3) ? OnBrush : OffBrush;

            State.VmTestResults.ExpU214b1 = (onName == NAME.U214b1 || onName == NAME.U214b2 || onName == NAME.U214b3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU214b2 = (onName == NAME.U214b1 || onName == NAME.U214b2 || onName == NAME.U214b3) ? OnBrush : OffBrush;
            State.VmTestResults.ExpU214b3 = (onName == NAME.U214b1 || onName == NAME.U214b2 || onName == NAME.U214b3) ? OnBrush : OffBrush;

            State.VmTestResults.ExpU214c = onName == NAME.U214c ? OnBrush : OffBrush;
            State.VmTestResults.ExpU214d = onName == NAME.U214d ? OnBrush : OffBrush;
            State.VmTestResults.ExpU214e = onName == NAME.U214e ? OnBrush : OffBrush;
            State.VmTestResults.ExpU214f = onName == NAME.U214f ? OnBrush : OffBrush;

            State.VmTestResults.ExpU215a = onName == NAME.U215a ? OnBrush : OffBrush;
            State.VmTestResults.ExpU215b = onName == NAME.U215b ? OnBrush : OffBrush;
            State.VmTestResults.ExpU215c = onName == NAME.U215c ? OnBrush : OffBrush;
            State.VmTestResults.ExpU215d = onName == NAME.U215d ? OnBrush : OffBrush;
        }

    }
}