
namespace New91820060Tester
{
    public static class Constants
    {
        //スタートボタンの表示
        public const string 開始 = "検査開始";
        public const string 停止 = "強制停止";
        public const string 確認 = "確認";

        //作業者へのメッセージ
        public const string MessModel = "型番を入力してください";
        public const string MessOpecode = "工番を入力してください";
        public const string MessOperator = "作業者名を選択してください";
        public const string MessSet = "製品をセットして開始ボタンを押してください";
        public const string MessRemove = "製品を取り外してください";

        public const string FlyoutMess        = "接続を確認してください！";

        public const string MessWait = "検査中！　しばらくお待ちください・・・";
        public const string MessCheckConnectMachine = "周辺機器の接続を確認してください！";

        public const int SerialEpx64s_1 = 216060599;
        public const int SerialEpx64s_2 = 240129407;

        public static readonly string RootPath     = @"C:\91820060";

        public static readonly string ExePath_FMC16          = $@"{RootPath}\FMC16LX\flash.exe";

        public static readonly string filePath_Connect           = "/Resources/Pic/Connect1.jpg";
        public static readonly string filePath_ConnectForItem004 = "/Resources/Pic/Connect2.jpg";

        public static readonly string filePath_TestSpec      = $@"{RootPath}\ConfigData\TestSpec.config";
        public static readonly string filePath_Configuration = $@"{RootPath}\ConfigData\Configuration.config";
        public static readonly string filePath_Command       = $@"{RootPath}\ConfigData\Command.config";
        public static readonly string FolderPath_TestFw      = $@"{RootPath}\FirmWare\試験用ソフト\PapTestFw.mhx";

        public static readonly string Path_Manual = @"C:\IfBoxTester\Manual.pdf";

        //検査データフォルダのパス
        public static readonly string PassDataFolderPath = @"C:\91821199\検査データ\合格品データ\";
        public static readonly string FailDataFolderPath = @"C:\91821199\検査データ\不良品データ\";


        //リトライ回数
        public static readonly int RetryCount = 0;












    }
}
