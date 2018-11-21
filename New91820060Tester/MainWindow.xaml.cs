using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using static New91820060Tester.General;

namespace New91820060Tester
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow
    {

        DispatcherTimer timerTextInput;

        Uri uriTestPage = new Uri("Page/Test/Test.xaml", UriKind.Relative);
        Uri uriConfPage = new Uri("Page/Config/Conf.xaml", UriKind.Relative);
        Uri uriHelpPage = new Uri("Page/Help/Help.xaml", UriKind.Relative);

        public MainWindow()
        {
            InitializeComponent();
            App._naviTest = FrameTest.NavigationService;
            App._naviConf = FrameConf.NavigationService;
            App._naviHelp = FrameHelp.NavigationService;
            App._naviInfo = FrameInfo.NavigationService;

            this.MouseLeftButtonDown += (sender, e) => this.DragMove();//ウィンドウ全体でドラッグ可能にする

            this.DataContext = State.VmMainWindow;



            //タイマーの設定
            timerTextInput = new DispatcherTimer(DispatcherPriority.Normal);
            timerTextInput.Interval = TimeSpan.FromMilliseconds(1000);
            timerTextInput.Tick += (object sender, EventArgs e) =>
            {
                timerTextInput.Stop();
                if (!Flags.SetOpecode)
                    State.VmMainWindow.Opecode = "";
                if (!Flags.SetModel)
                    State.VmMainWindow.Model = "";
            };
            timerTextInput.Start();

            GetInfo();

            //カレントディレクトリの取得
            State.CurrDir = Directory.GetCurrentDirectory();

            //試験用パラメータのロード
            State.LoadConfigData();

            Init周辺機器();//非同期処理です

            //日常点検が実施されているかの確認
            Flags.State日常点検 = General.Check日常点検データ();

            InitMainForm();//メインフォーム初期

        }



        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                while (Flags.Initializing周辺機器) ;

                if (Flags.State232C)
                    Target.Close232();

                if (Flags.State422)
                    Target.Close422();

                if (Flags.StateMbed)
                    LPC1768.ClosePort();

                if (Flags.StateEpx64_1)
                {
                    ResetIo();
                    io1.Close();
                }

                if (Flags.StateEpx64_2)
                    io2.Close();

                if (!State.Save個別データ())
                {
                    MessageBox.Show("個別データの保存に失敗しました");
                }

            }
            catch
            {
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Flags.Testing)
            {
                e.Cancel = true;
            }
            else
            {
                Flags.StopInit周辺機器 = true;
            }
        }


        private void cbOperator_DropDownClosed(object sender, EventArgs e)
        {
            if (cbOperator.SelectedIndex == -1)
                return;
            Flags.SetOperator = true;
            SetFocus();
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            if (Flags.Testing)
                return;

            //FW Infoのクリア
            State.VmTestStatus.FwVer = "";
            State.VmTestStatus.FwSum = "";
            State.ProductFwPath = "";
            State.PathForTestData = "";

            State.VmTestStatus.ColPt82 = OffBrush;
            State.VmTestStatus.ColPt62 = OffBrush;

            State.VmTestStatus.ColAc100 = OffBrush;
            State.VmTestStatus.ColAc200 = OffBrush;

            Flags.SetOpecode = false;
            Flags.SetModel = false;

            //いったん編集禁止にする
            State.VmMainWindow.ReadOnlyOpecode = true;
            State.VmMainWindow.ReadOnlyModel = true;

            if (Flags.SetOperator)
                State.VmMainWindow.ReadOnlyOpecode = false;//工番編集許可する

            SetFocus();
        }

        private void tbOpecode_TextChanged(object sender, TextChangedEventArgs e)
        {
            //１文字入力されるごとに、タイマーを初期化する
            timerTextInput.Stop();
            timerTextInput.Start();

            if (State.VmMainWindow.Opecode.Length != 13) return;
            //以降は工番が正しく入力されているかどうかの判定
            if (System.Text.RegularExpressions.Regex.IsMatch(
                State.VmMainWindow.Opecode, @"^\d-\d\d-\d\d\d\d-\d\d\d$",
                System.Text.RegularExpressions.RegexOptions.ECMAScript))
            {
                timerTextInput.Stop();
                Flags.SetOpecode = true;
                SetFocus();
            }

        }

        //アセンブリ情報の取得
        private void GetInfo()
        {
            //アセンブリバージョンの取得
            var asm = Assembly.GetExecutingAssembly();
            var M = asm.GetName().Version.Major.ToString();
            var N = asm.GetName().Version.Minor.ToString();
            var B = asm.GetName().Version.Build.ToString();
            State.AssemblyInfo = M + "." + N + "." + B;

        }

        //フォームのイニシャライズ
        private void InitMainForm()
        {
            TabInfo.Header = "";//実行時はエラーインフォタブのヘッダを空白にして作業差に見えないようにする
            TabInfo.IsEnabled = false; //作業差がTABを選択できないようにします

            State.VmMainWindow.EnableOtherButton = true;

            State.VmTestStatus.UnitTestEnable = false;
            State.VmMainWindow.OperatorEnable = true;
            State.VmMainWindow.ReadOnlyOpecode = true;
            State.VmMainWindow.ReadOnlyModel = true;

        }

        //フォーカスのセット
        public void SetFocus()
        {
            if (!Flags.SetOperator)
            {

                if (!cbOperator.IsFocused)
                    cbOperator.Focus();
                return;
            }

            if (!Flags.SetOpecode)
            {
                if (!tbOpecode.IsFocused)
                    tbOpecode.Focus();
                return;
            }

            if (!Flags.SetModel)
            {
                if (!tbModel.IsFocused)
                    tbModel.Focus();
                return;
            }

        }


        private async void TabMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = TabMenu.SelectedIndex;
            if (index == 0)
            {
                Flags.OtherPage = false;//フラグを初期化しておく

                App._naviConf.Refresh();
                App._naviHelp.Refresh();
                App._naviTest.Navigate(uriTestPage);
                SetFocus();//テスト画面に移行する際にフォーカスを必須項目入力欄にあてる

                if (Flags.Testing)
                    return;

                //高速にページ切り替えボタンを押すと異常動作する場合があるので、ページが遷移してから500msec間は、他のページに遷移できないようにする
                State.VmMainWindow.EnableOtherButton = false;
                await Task.Delay(500);
                State.VmMainWindow.EnableOtherButton = true;
            }
            else if (index == 1)
            {
                Flags.OtherPage = true;
                App._naviConf.Navigate(uriConfPage);
                App._naviHelp.Refresh();
                //高速にページ切り替えボタンを押すと異常動作する場合があるので、ページが遷移してから500msec間は、他のページに遷移できないようにする
                State.VmMainWindow.EnableOtherButton = false;
                await Task.Delay(500);
                State.VmMainWindow.EnableOtherButton = true;
            }
            else if (index == 2)
            {
                Flags.OtherPage = true;
                App._naviHelp.Navigate(uriHelpPage);
                App._naviConf.Refresh();
                //高速にページ切り替えボタンを押すと異常動作する場合があるので、ページが遷移してから500msec間は、他のページに遷移できないようにする
                State.VmMainWindow.EnableOtherButton = false;
                await Task.Delay(500);
                State.VmMainWindow.EnableOtherButton = true;

            }
            else if (index == 3)//Infoタブ 作業者がこのタブを選択することはない。 TEST画面のエラー詳細ボタンを押した時にこのタブが選択されるようコードビハインドで記述
            {
                //Flags.OtherPage = true;
                App._naviInfo.Navigate(State.uriOtherInfoPage);
                App._naviConf.Refresh();
                App._naviHelp.Refresh();
            }


        }

        private void tbModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            //１文字入力されるごとに、タイマーを初期化する
            timerTextInput.Stop();
            timerTextInput.Start();

            if (!State.VmMainWindow.Model.Contains("/R"))
                return;

            var inputModel = State.VmMainWindow.Model;

            //ITEM004はAC100V入力使用なので、CN13への入力は18Kオームを挿入する必要があります
            Flags.SetAdapterForAc100Input = (inputModel == "91820060-004/R" || 
                                          inputModel == "0A004600XX0/R") ? true : false;

            //ITEM005はQ3周辺回路未実装のため検査をスキップする
            Flags.MustCheckQ3Out = inputModel == "91820060-005/R" ? false : true;

            //ITEM003、それ以外のアイテムで IO基板のコネクタ配置が異なるため、プレス治具のピンボードを交換する必要があります
            var IsSettingPinBoardForItem003 = General.IsPinBoardForItem003();
            switch (inputModel)
            {
                case "91820060-003/R":
                    if (!IsSettingPinBoardForItem003)
                    {
                        State.VmMainWindow.FlyoutMess = "上側の基板のコネクタ配置が異なるため\r\nプレス治具のピンボード交換が必要です";
                        State.VmMainWindow.Flyout = true;
                    }
                    break;
                default:
                    if (IsSettingPinBoardForItem003)
                    {
                        State.VmMainWindow.FlyoutMess = "上側の基板のコネクタ配置が異なるため\r\nプレス治具のピンボード交換が必要です";
                        State.VmMainWindow.Flyout = true;
                    }
                    break;
            }

            //選択されたアイテムのFW Infoを決定する
            foreach (var i in State.ItemInfoLists)
            {
                if (i.Model == inputModel)
                {
                    State.VmTestStatus.FwVer = i.ProductFirmVer;
                    State.VmTestStatus.FwSum = i.ProductFirmSum;
                    State.ProductFwPath = $@"{Constants.RootPath}\FirmWare\{i.Model.TrimEnd('/', 'R')}\{i.ProductFirmName}";
                    State.Dsw1DefaultVal = Convert.ToInt32(i.Dsw1, 16);
                    State.Dsw1DefaultMess = i.Dsw1Mess;
                    State.Dsw2DefaultVal = Int32.Parse(i.Dsw2);
                    State.Dsw2DefaultMess = i.Dsw2Mess;

                    //PT_MODEの選択 重要！！！
                    PT100チェック.PtMode = i.PtMode.Contains("82") ? PT100チェック.PT_MODE.PT82 : PT100チェック.PT_MODE.PT62;
                    if (PT100チェック.PtMode == PT100チェック.PT_MODE.PT82)
                    {
                        State.VmTestStatus.ColPt82 = OnBrush;
                        State.VmTestStatus.ColPt62 = OffBrush;
                    }
                    else
                    {
                        State.VmTestStatus.ColPt82 = OffBrush;
                        State.VmTestStatus.ColPt62 = OnBrush;
                    }



                    if (i.Model.Contains("91820060-001")) State.PathForTestData = Constants.PassDataFolderPath_91820060_001;
                    else if (i.Model.Contains("91820060-002")) State.PathForTestData = Constants.PassDataFolderPath_91820060_002;
                    else if (i.Model.Contains("91820060-003")) State.PathForTestData = Constants.PassDataFolderPath_91820060_003;
                    else if (i.Model.Contains("91820060-004")) State.PathForTestData = Constants.PassDataFolderPath_91820060_004;
                    else if (i.Model.Contains("91820060-005")) State.PathForTestData = Constants.PassDataFolderPath_91820060_005;
                    else if (i.Model.Contains("0A004259XX0")) State.PathForTestData = Constants.PassDataFolderPath_0A004259XX;
                    else if (i.Model.Contains("0A004600XX0")) State.PathForTestData = Constants.PassDataFolderPath_0A004600XX;

                    timerTextInput.Stop();
                    Flags.SetModel = true;
                    SetFocus();
                    return;
                }
            }
        }

        private void ButtonOkFlyout_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);//非常停止ボタンが押されたら強制終了する
        }
    }
}
