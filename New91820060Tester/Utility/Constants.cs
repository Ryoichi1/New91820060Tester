
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
        public const string MessDailyCheck = "点検用サンプルをセットして開始ボタンを押してください";

        public const string MessRemove = "確認ボタンを押してから、製品を取り外してください";

        public const string FlyoutMess        = "接続を確認してください！";

        public const string MessWait = "検査中！　しばらくお待ちください・・・";
        public const string MessCheckConnectMachine = "周辺機器の接続を確認してください！";

        public const int SerialEpx64s_1 = 217090730;
        public const int SerialEpx64s_2 = 217090731;

        public static readonly string RootPath     = @"C:\91820060";

        public static readonly string ExePath_FMC16          = $@"{RootPath}\FMC16LX\flash.exe";

        public static readonly string filePath_TestSpec      = $@"{RootPath}\ConfigData\TestSpec.config";
        public static readonly string filePath_Configuration = $@"{RootPath}\ConfigData\Configuration.config";
        public static readonly string FolderPath_TestFw      = $@"{RootPath}\FirmWare\試験用ソフト\PapTestFw.mhx";

        public static readonly string Path_Manual = $@"{RootPath}\Manual.pdf";

        public static readonly string Path_日常点検データ = $@"{RootPath}\日常点検\日常点検.txt";

        //検査データフォルダのパス
        public static readonly string PassDataFolderPath_91820060_001 = $@"{RootPath}\検査データ\合格品データ\Item91820060_001\";
        public static readonly string PassDataFolderPath_91820060_002 = $@"{RootPath}\検査データ\合格品データ\Item91820060_002\";
        public static readonly string PassDataFolderPath_91820060_003 = $@"{RootPath}\検査データ\合格品データ\Item91820060_003\";
        public static readonly string PassDataFolderPath_91820060_004 = $@"{RootPath}\検査データ\合格品データ\Item91820060_004\";
        public static readonly string PassDataFolderPath_91820060_005 = $@"{RootPath}\検査データ\合格品データ\Item91820060_005\";
        public static readonly string PassDataFolderPath_0A004259XX   = $@"{RootPath}\検査データ\合格品データ\Item_0A004259XX\";
        public static readonly string PassDataFolderPath_0A004600XX   = $@"{RootPath}\検査データ\合格品データ\Item_0A004600XX\";
        public static readonly string FailDataFolderPath              = $@"{RootPath}\検査データ\不良品データ\";
        public static readonly string PassTestDataFormatPath              = $@"{RootPath}\検査データ\合格品データ\Format.csv";
        public static readonly string FailTestDataFormatPath              = $@"{RootPath}\検査データ\不良品データ\FormatNg.csv";


        //リトライ回数
        public static readonly int RetryCount = 0;












    }
}
