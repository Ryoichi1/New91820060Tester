using System.Windows;
using Microsoft.Practices.Prism.Mvvm;
using System.Linq;

namespace New91820060Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class ErrInfoCnCheck
    {
        public class vm : BindableBase
        {
            private Visibility _VisCN13_15 = Visibility.Hidden;
            public Visibility VisCN13_15 { get { return _VisCN13_15; } set { SetProperty(ref _VisCN13_15, value); } }

            private Visibility _VisCN16_22 = Visibility.Hidden;
            public Visibility VisCN16_22 { get { return _VisCN16_22; } set { SetProperty(ref _VisCN16_22, value); } }

            private Visibility _VisCN24_30 = Visibility.Hidden;
            public Visibility VisCN24_30 { get { return _VisCN24_30; } set { SetProperty(ref _VisCN24_30, value); } }

            private Visibility _VisCN23 = Visibility.Hidden;
            public Visibility VisCN23 { get { return _VisCN23; } set { SetProperty(ref _VisCN23, value); } }

            private Visibility _VisCN1_10 = Visibility.Hidden;
            public Visibility VisCN1_10 { get { return _VisCN1_10; } set { SetProperty(ref _VisCN1_10, value); } }

            private Visibility _VisCN101_107 = Visibility.Hidden;
            public Visibility VisCN101_107 { get { return _VisCN101_107; } set { SetProperty(ref _VisCN101_107, value); } }

            private Visibility _VisCN108_115 = Visibility.Hidden;
            public Visibility VisCN108_115 { get { return _VisCN108_115; } set { SetProperty(ref _VisCN108_115, value); } }

            private Visibility _VisCN11_12 = Visibility.Hidden;
            public Visibility VisCN11_12 { get { return _VisCN11_12; } set { SetProperty(ref _VisCN11_12, value); } }

            private Visibility _VisCN109_117 = Visibility.Hidden;
            public Visibility VisCN109_117 { get { return _VisCN109_117; } set { SetProperty(ref _VisCN109_117, value); } }

            private Visibility _VisCN26 = Visibility.Hidden;
            public Visibility VisCN26 { get { return _VisCN26; } set { SetProperty(ref _VisCN26, value); } }

            private Visibility _VisCN110_114 = Visibility.Hidden;
            public Visibility VisCN110_114 { get { return _VisCN110_114; } set { SetProperty(ref _VisCN110_114, value); } }



            private string _NgList;
            public string NgList { get { return _NgList; } set { SetProperty(ref _NgList, value); } }

        }

        private vm viewmodel;

        public ErrInfoCnCheck()
        {
            InitializeComponent();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            viewmodel = new vm();
            this.DataContext = viewmodel;
            SetErrInfo();
        }
        private void SetErrInfo()
        {
            if (コネクタチェック.ListCnSpec == null) return;
            var NgList = コネクタチェック.ListCnSpec.Where(cn => !cn.result);

            foreach (var cn in NgList)
            {
                switch (cn.name)
                {
                    case コネクタチェック.NAME.CN13_15:
                        viewmodel.VisCN13_15 = Visibility.Visible;
                        viewmodel.NgList += "CN13～15\r\n";
                        break;
                    case コネクタチェック.NAME.CN16_22:
                        viewmodel.VisCN16_22 = Visibility.Visible;
                        viewmodel.NgList += "CN16～22\r\n";
                        break;
                    case コネクタチェック.NAME.CN24_30:
                        viewmodel.VisCN24_30 = Visibility.Visible;
                        viewmodel.NgList += "CN24,25,30\r\n";
                        break;
                    case コネクタチェック.NAME.CN23:
                        viewmodel.VisCN23 = Visibility.Visible;
                        viewmodel.NgList += "CN23\r\n";
                        break;
                    case コネクタチェック.NAME.CN1_10:
                        viewmodel.VisCN1_10 = Visibility.Visible;
                        viewmodel.NgList += "CN1～10\r\n";
                        break;
                    case コネクタチェック.NAME.CN101_107:
                        viewmodel.VisCN101_107 = Visibility.Visible;
                        viewmodel.NgList += "CN101～107\r\n";
                        break;
                    case コネクタチェック.NAME.CN108_115:
                        viewmodel.VisCN108_115 = Visibility.Visible;
                        viewmodel.NgList += "CN108,112,113,115\r\n";
                        break;
                    case コネクタチェック.NAME.CN11_12:
                        viewmodel.VisCN11_12 = Visibility.Visible;
                        viewmodel.NgList += "CN11,12\r\n";
                        break;
                    case コネクタチェック.NAME.CN109_117:
                        viewmodel.VisCN109_117 = Visibility.Visible;
                        viewmodel.NgList += "CN109,116,117\r\n";
                        break;
                    case コネクタチェック.NAME.CN26:
                        viewmodel.VisCN26 = Visibility.Visible;
                        viewmodel.NgList += "CN26\r\n";
                        break;
                    case コネクタチェック.NAME.CN110_114:
                        viewmodel.VisCN110_114 = Visibility.Visible;
                        viewmodel.NgList += "CN110,111,,114\r\n";
                        break;
                }

            }
        }

        private void buttonReturn_Click(object sender, RoutedEventArgs e)
        {
            State.VmMainWindow.TabIndex = 0;

        }


    }
}
