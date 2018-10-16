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
    public static class サーミスタ入力チェック
    {
        public enum CH
        {
            CN1, CN2, CN3, CN4, CN5, CN6, CN7, CN8, CN9, CN10
        }
        public enum TEMP
        {
            P60, P20, M20
        }
        public class TestSpec
        {
            public CH ch;
            public double temp;
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

        private static void SetTh(TEMP tmp)
        {
            switch (tmp)
            {
                case TEMP.P60:
                    General.SetTh(TH.TEMP_P60);
                    break;
                case TEMP.P20:
                    General.SetTh(TH.TEMP_P20);
                    break;
                case TEMP.M20:
                    General.SetTh(TH.TEMP_M20);
                    break;
            }
        }

        public static async Task<bool> CheckTh(TEMP tmp)
        {
            bool result = false;
            double max = 0;
            double min = 0;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    try
                    {
                        switch (tmp)
                        {
                            case TEMP.P60:
                                max = State.TestSpec.TempP60Max;
                                min = State.TestSpec.TempP60Min;
                                break;
                            case TEMP.P20:
                                max = State.TestSpec.TempP20Max;
                                min = State.TestSpec.TempP20Min;
                                break;
                            case TEMP.M20:
                                max = State.TestSpec.TempM20Max;
                                min = State.TestSpec.TempM20Min;
                                break;
                        }

                        ResetViewModel(tmp);

                        InitList();//テストスペック毎回初期化

                        Target.SendData("W,ORION,00,01");

                        Sleep(300);
                        SetTh(tmp);
                        Sleep(6000);
                        Target.SendData("R,ALL,00,00");
                        var buffArray = Target.RecieveData.Split(',').Take(10);

                        int i = 0;
                        foreach (var buff in buffArray)
                        {
                            Double.TryParse(buff, out var val);
                            ListSpecs[i++].temp = val / 100.0;
                        }

                        ListSpecs.ForEach(l => l.result = min <= l.temp && l.temp <= max);
                        SetViewModel(tmp);

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
                Target.SendData("W,ORION,00,00");//オリオンLibを非activにする
                State.VmTestStatus.Spec = $"規格値 : {min.ToString("F1")} ～ {max.ToString("F1")}℃";
                State.VmTestStatus.MeasValue = "計測値 : ---";
            }
        }

        private static void SetViewModel(TEMP tmp)
        {
            switch (tmp)
            {
                case TEMP.P60:
                    State.VmTestResults.Cn1_P60 = $"{ListSpecs[0].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn2_P60 = $"{ListSpecs[1].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn3_P60 = $"{ListSpecs[2].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn4_P60 = $"{ListSpecs[3].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn5_P60 = $"{ListSpecs[4].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn6_P60 = $"{ListSpecs[5].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn7_P60 = $"{ListSpecs[6].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn8_P60 = $"{ListSpecs[7].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn9_P60 = $"{ListSpecs[8].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn10_P60 = $"{ListSpecs[9].temp.ToString("F2")}℃";

                    State.VmTestResults.ColCn1_P60 = ListSpecs[0].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn2_P60 = ListSpecs[1].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn3_P60 = ListSpecs[2].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn4_P60 = ListSpecs[3].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn5_P60 = ListSpecs[4].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn6_P60 = ListSpecs[5].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn7_P60 = ListSpecs[6].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn8_P60 = ListSpecs[7].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn9_P60 = ListSpecs[8].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn10_P60 = ListSpecs[9].result ? OffBrush : NgBrush;
                    break;

                case TEMP.P20:
                    State.VmTestResults.Cn1_P20 = $"{ListSpecs[0].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn2_P20 = $"{ListSpecs[1].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn3_P20 = $"{ListSpecs[2].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn4_P20 = $"{ListSpecs[3].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn5_P20 = $"{ListSpecs[4].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn6_P20 = $"{ListSpecs[5].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn7_P20 = $"{ListSpecs[6].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn8_P20 = $"{ListSpecs[7].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn9_P20 = $"{ListSpecs[8].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn10_P20 = $"{ListSpecs[9].temp.ToString("F2")}℃";

                    State.VmTestResults.ColCn1_P20 = ListSpecs[0].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn2_P20 = ListSpecs[1].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn3_P20 = ListSpecs[2].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn4_P20 = ListSpecs[3].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn5_P20 = ListSpecs[4].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn6_P20 = ListSpecs[5].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn7_P20 = ListSpecs[6].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn8_P20 = ListSpecs[7].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn9_P20 = ListSpecs[8].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn10_P20 = ListSpecs[9].result ? OffBrush : NgBrush;
                    break;

                case TEMP.M20:
                    State.VmTestResults.Cn1_M20 = $"{ListSpecs[0].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn2_M20 = $"{ListSpecs[1].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn3_M20 = $"{ListSpecs[2].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn4_M20 = $"{ListSpecs[3].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn5_M20 = $"{ListSpecs[4].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn6_M20 = $"{ListSpecs[5].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn7_M20 = $"{ListSpecs[6].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn8_M20 = $"{ListSpecs[7].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn9_M20 = $"{ListSpecs[8].temp.ToString("F2")}℃";
                    State.VmTestResults.Cn10_M20 = $"{ListSpecs[9].temp.ToString("F2")}℃";

                    State.VmTestResults.ColCn1_M20 = ListSpecs[0].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn2_M20 = ListSpecs[1].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn3_M20 = ListSpecs[2].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn4_M20 = ListSpecs[3].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn5_M20 = ListSpecs[4].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn6_M20 = ListSpecs[5].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn7_M20 = ListSpecs[6].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn8_M20 = ListSpecs[7].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn9_M20 = ListSpecs[8].result ? OffBrush : NgBrush;
                    State.VmTestResults.ColCn10_M20 = ListSpecs[9].result ? OffBrush : NgBrush;
                    break;

            }
        }

        private static void ResetViewModel(TEMP tmp)
        {
            switch (tmp)
            {
                case TEMP.P60:
                    State.VmTestResults.Cn1_P60 = "";
                    State.VmTestResults.Cn2_P60 = "";
                    State.VmTestResults.Cn3_P60 = "";
                    State.VmTestResults.Cn4_P60 = "";
                    State.VmTestResults.Cn5_P60 = "";
                    State.VmTestResults.Cn6_P60 = "";
                    State.VmTestResults.Cn7_P60 = "";
                    State.VmTestResults.Cn8_P60 = "";
                    State.VmTestResults.Cn9_P60 = "";
                    State.VmTestResults.Cn10_P60 = "";

                    State.VmTestResults.ColCn1_P60 = OffBrush;
                    State.VmTestResults.ColCn2_P60 = OffBrush;
                    State.VmTestResults.ColCn3_P60 = OffBrush;
                    State.VmTestResults.ColCn4_P60 = OffBrush;
                    State.VmTestResults.ColCn5_P60 = OffBrush;
                    State.VmTestResults.ColCn6_P60 = OffBrush;
                    State.VmTestResults.ColCn7_P60 = OffBrush;
                    State.VmTestResults.ColCn8_P60 = OffBrush;
                    State.VmTestResults.ColCn9_P60 = OffBrush;
                    State.VmTestResults.ColCn10_P60 = OffBrush;
                    break;

                case TEMP.P20:
                    State.VmTestResults.Cn1_P20 = "";
                    State.VmTestResults.Cn2_P20 = "";
                    State.VmTestResults.Cn3_P20 = "";
                    State.VmTestResults.Cn4_P20 = "";
                    State.VmTestResults.Cn5_P20 = "";
                    State.VmTestResults.Cn6_P20 = "";
                    State.VmTestResults.Cn7_P20 = "";
                    State.VmTestResults.Cn8_P20 = "";
                    State.VmTestResults.Cn9_P20 = "";
                    State.VmTestResults.Cn10_P20 = "";

                    State.VmTestResults.ColCn1_P20 = OffBrush;
                    State.VmTestResults.ColCn2_P20 = OffBrush;
                    State.VmTestResults.ColCn3_P20 = OffBrush;
                    State.VmTestResults.ColCn4_P20 = OffBrush;
                    State.VmTestResults.ColCn5_P20 = OffBrush;
                    State.VmTestResults.ColCn6_P20 = OffBrush;
                    State.VmTestResults.ColCn7_P20 = OffBrush;
                    State.VmTestResults.ColCn8_P20 = OffBrush;
                    State.VmTestResults.ColCn9_P20 = OffBrush;
                    State.VmTestResults.ColCn10_P20 = OffBrush;
                    break;

                case TEMP.M20:
                    State.VmTestResults.Cn1_M20 = "";
                    State.VmTestResults.Cn2_M20 = "";
                    State.VmTestResults.Cn3_M20 = "";
                    State.VmTestResults.Cn4_M20 = "";
                    State.VmTestResults.Cn5_M20 = "";
                    State.VmTestResults.Cn6_M20 = "";
                    State.VmTestResults.Cn7_M20 = "";
                    State.VmTestResults.Cn8_M20 = "";
                    State.VmTestResults.Cn9_M20 = "";
                    State.VmTestResults.Cn10_M20 = "";

                    State.VmTestResults.ColCn1_M20 = OffBrush;
                    State.VmTestResults.ColCn2_M20 = OffBrush;
                    State.VmTestResults.ColCn3_M20 = OffBrush;
                    State.VmTestResults.ColCn4_M20 = OffBrush;
                    State.VmTestResults.ColCn5_M20 = OffBrush;
                    State.VmTestResults.ColCn6_M20 = OffBrush;
                    State.VmTestResults.ColCn7_M20 = OffBrush;
                    State.VmTestResults.ColCn8_M20 = OffBrush;
                    State.VmTestResults.ColCn9_M20 = OffBrush;
                    State.VmTestResults.ColCn10_M20 = OffBrush;
                    break;
            }
        }



    }
}