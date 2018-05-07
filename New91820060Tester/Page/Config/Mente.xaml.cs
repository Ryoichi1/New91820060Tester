using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static New91820060Tester.General;

namespace New91820060Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class Mente
    {
        private SolidColorBrush ButtonOnBrush = new SolidColorBrush();
        private SolidColorBrush ButtonOffBrush = new SolidColorBrush();
        private const double ButtonOpacity = 0.4;

        public Mente()
        {
            InitializeComponent();

            ButtonOnBrush.Color = Colors.DodgerBlue;
            ButtonOffBrush.Color = Colors.Transparent;
            ButtonOnBrush.Opacity = ButtonOpacity;
            ButtonOffBrush.Opacity = ButtonOpacity;

            CanvasComm.DataContext = State.VmComm;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            buttonPow.Background = Brushes.Transparent;
            General.PowSupply(false);

            //if (!FlagConnect)
            //{
            //    Flags.State232C = Target.InitPort232();
            //}
        }


        bool FlagPow;
        private void buttonPow_Click(object sender, RoutedEventArgs e)
        {
            if (FlagPow)
            {
                PowSupply(false);
                buttonPow.Background = ButtonOffBrush;
            }
            else
            {
                PowSupply(true);
                buttonPow.Background = ButtonOnBrush;
            }

            FlagPow = !FlagPow;
        }

        private async void buttonStamp_Click(object sender, RoutedEventArgs e)
        {
            buttonStamp.Background = ButtonOnBrush;
            await General.PushStamp();
            buttonStamp.Background = ButtonOffBrush;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            State.VmComm.TX = "";
            State.VmComm.RX = "";
            tbCommand.Text = "";

            rbTempP60.IsChecked = true;
            rbRs232c.IsChecked = true;
        }

        bool FlagConnect = true;
        private void buttonComDisconnect_Click(object sender, RoutedEventArgs e)
        {
            FlagConnect = !FlagConnect;

            if (FlagConnect)
            {
                Flags.State422 = Target.InitPort422();
                buttonComDisconnect.Content = "ターゲット通信切断";
            }
            else
            {
                Target.Close422();
                buttonComDisconnect.Content = "ターゲット通信接続";
            }



        }





        private async void buttonWriteTestFw_Click(object sender, RoutedEventArgs e)
        {
            await 書き込み.WriteFw(Constants.FolderPath_TestFw);
        }

        private void rbTempP60_Checked(object sender, RoutedEventArgs e)
        {
            General.SetTh(TH.TEMP_P60);
        }

        private void rbTempP20_Checked(object sender, RoutedEventArgs e)
        {
            General.SetTh(TH.TEMP_P20);
        }

        private void rbTempM20_Checked(object sender, RoutedEventArgs e)
        {
            General.SetTh(TH.TEMP_M20);
        }

        private void buttonSendMain_Click(object sender, RoutedEventArgs e)
        {
            if (tbCommand.Text == "") return;
            Target.SendData(tbCommand.Text);
        }

        private void rbRs232c_Checked(object sender, RoutedEventArgs e)
        {
            Target.ChangeMode(Target.MODE.RS232);
        }

        private void rbRs422_Checked(object sender, RoutedEventArgs e)
        {
            Target.ChangeMode(Target.MODE.RS422);
        }

        private void buttonMeasVol_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonMeasVol_Copy_Click(object sender, RoutedEventArgs e)
        {
            General.SetRL1(true);
        }


        bool FlagG7sa;
        private void buttonSafetyRelay_Click(object sender, RoutedEventArgs e)
        {
            if (FlagG7sa)
            {
                SetG7SA(false);
                buttonSafetyRelay.Background = ButtonOffBrush;
            }
            else
            {
                SetG7SA(true);
                buttonSafetyRelay.Background = ButtonOnBrush;
            }

            FlagG7sa = !FlagG7sa;

        }
    }

}
