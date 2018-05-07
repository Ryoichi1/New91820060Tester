using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace New91820060Tester
{
    public static class コネクタチェック
    {
        public enum NAME { CN13_15, CN16_22, CN24_30, CN23, CN1_10, CN101_107, CN108_115, CN11_12, CN109_117, CN26, CN110_114 }

        public static List<CnSpec> ListCnSpec;

        public class CnSpec
        {
            public NAME name;
            public bool result;
        }



        private static void InitList()
        {
            ListCnSpec = new List<CnSpec>();
            foreach (var n in Enum.GetValues(typeof(NAME)))
            {
                ListCnSpec.Add(new CnSpec { name = (NAME)n });
            }
        }


        public static async Task<bool> CheckCn()
        {
            bool result = false;

            try
            {
                return await Task<bool>.Run(() =>
                {
                    try
                    {
                        InitList();

                        General.io1.ReadInputData(EPX64S.PORT.P7);
                        var io1P7Data = General.io1.P7InputData;

                        General.io2.ReadInputData(EPX64S.PORT.P6);
                        var io2P6Data = General.io2.P6InputData;

                        ListCnSpec.ForEach(l =>
                        {
                            bool re = false;
                            switch (l.name)
                            {
                                case NAME.CN13_15:
                                    re = (io2P6Data & 0b0000_0001) == 0x00;
                                    break;
                                case NAME.CN16_22:
                                    re = (io2P6Data & 0b0000_0010) == 0x00;
                                    break;
                                case NAME.CN24_30:
                                    re = (io2P6Data & 0b0000_0100) == 0x00;
                                    break;
                                case NAME.CN23:
                                    re = (io2P6Data & 0b0000_1000) == 0x00;
                                    break;
                                case NAME.CN1_10:
                                    re = (io2P6Data & 0b0001_0000) == 0x00;
                                    break;
                                case NAME.CN101_107:
                                    re = (io2P6Data & 0b0010_0000) == 0x00;
                                    break;
                                case NAME.CN108_115:
                                    re = (io2P6Data & 0b0100_0000) == 0x00;
                                    break;
                                case NAME.CN11_12:
                                    re = (io1P7Data & 0b0001_0000) == 0x00;
                                    break;
                                case NAME.CN109_117:
                                    re = (io1P7Data & 0b0010_0000) == 0x00;
                                    break;
                                case NAME.CN26:
                                    re = (io1P7Data & 0b0100_0000) == 0x00;
                                    break;
                                case NAME.CN110_114:
                                    re = (io1P7Data & 0b1000_0000) == 0x00;
                                    break;
                            }

                            l.result = re;
                        });

                        return result = ListCnSpec.All(l => l.result);

                    }
                    catch
                    {
                        return result = false;
                    }

                });
            }
            finally
            {
                if (!result)
                {
                    State.uriOtherInfoPage = new Uri("Page/ErrInfo/ErrInfoCnCheck.xaml", UriKind.Relative);
                    State.VmTestStatus.EnableButtonErrInfo = System.Windows.Visibility.Visible;
                }
            }
        }















    }
}
