using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static New91820060Tester.General;

namespace New91820060Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class Init
    {
        private SolidColorBrush ButtonOnBrush = new SolidColorBrush();
        private SolidColorBrush ButtonOffBrush = new SolidColorBrush();
        private const double ButtonOpacity = 0.4;

        public Init()
        {
            InitializeComponent();

            ButtonOnBrush.Color = Colors.DodgerBlue;
            ButtonOffBrush.Color = Colors.Transparent;
            ButtonOnBrush.Opacity = ButtonOpacity;
            ButtonOffBrush.Opacity = ButtonOpacity;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            labelMess.Content = "";
            buttonInit.Background = Brushes.Transparent;
            General.PowSupply(false);
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            General.PowSupply(false);
        }


        private async void buttonInit_Click(object sender, RoutedEventArgs e)
        {
            buttonInit.Background = ButtonOnBrush;

            MessageBox.Show("ＳＷ１ と　ＳＷ６　を押しながら\r\nＯＫボタンを押してください");
            General.PowSupply(true);
            await Task.Delay(2000);
            labelMess.Content = "手を離しても大丈夫です\r\n初期化中。。。。";
            await Task.Delay(10000);
            labelMess.Content = "";
            General.PowSupply(false);
            MessageBox.Show("初期化完了しました");
            buttonInit.Background = ButtonOffBrush;
        }

    }

}
