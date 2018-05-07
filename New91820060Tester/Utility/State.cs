using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace New91820060Tester
{
    public class TestSpecs
    {
        public int Key;
        public string Value;
        public bool PowSw;

        public TestSpecs(int key, string value, bool powSW = true)
        {
            this.Key = key;
            this.Value = value;
            this.PowSw = powSW;

        }
    }

    public static class State
    {
        //データソース（バインディング用）
        public static ViewModelMainWindow VmMainWindow = new ViewModelMainWindow();
        public static ViewModelTestStatus VmTestStatus = new ViewModelTestStatus();
        public static ViewModelTestResult VmTestResults = new ViewModelTestResult();
        public static ViewModelCommunication VmComm = new ViewModelCommunication();
        public static TestCommand testCommand = new TestCommand();

        //パブリックメンバ
        public static Configuration Setting { get; set; }
        public static TestSpec TestSpec { get; set; }

        public static List<FirmwareInfo> ItemInfoLists { get; set; }

        public static string ProductFwPath { get; set; }

        public static string CurrDir { get; set; }

        public static string AssemblyInfo { get; set; }

        public static double CurrentThemeOpacity { get; set; }

        public static Uri uriOtherInfoPage { get; set; }


        public static List<TestSpecs> テスト項目 = new List<TestSpecs>()
        {
            new TestSpecs(100, "コネクタ実装チェック", true),

            new TestSpecs(200, "電源電圧チェック +5V", true),

            new TestSpecs(300, "検査ソフト書き込み", false),

            new TestSpecs(400, "デジタル出力チェック", true),

            new TestSpecs(500, "デジタル入力チェック", true),

            new TestSpecs(600, "DA_OUT CN116 50", true),
            //new TestSpecs(301, "DA_OUT CN116 100", false),

            new TestSpecs(1000, "製品プログラム書き込み", false),

        };

        //個別設定のロード
        public static void LoadConfigData()
        {
            //Configファイルのロード
            Setting = Deserialize<Configuration>(Constants.filePath_Configuration);


            VmMainWindow.ListOperator = Setting.作業者リスト;
            VmMainWindow.Theme = Setting.PathTheme;
            VmMainWindow.ThemeOpacity = Setting.OpacityTheme;

            if (Setting.日付 != DateTime.Now.ToString("yyyyMMdd"))
            {
                Setting.日付 = DateTime.Now.ToString("yyyyMMdd");
                Setting.TodayOkCount = 0;
                Setting.TodayNgCount = 0;
            }

            VmTestStatus.OkCount = Setting.TodayOkCount.ToString() + "台";
            VmTestStatus.NgCount = Setting.TodayNgCount.ToString() + "台";

            //TestSpecファイルのロード
            TestSpec = Deserialize<TestSpec>(Constants.filePath_TestSpec);

            //全アイテムのConfファイルのロード
            ItemInfoLists = new List<FirmwareInfo>();
            var confPathList = SearchConfFilePath();
            confPathList.ForEach(c =>
            {
                ItemInfoLists.Add(Deserialize<FirmwareInfo>(c));
            });
        }


        public static List<string> SearchConfFilePath()
        {
            string dirPath = $@"{Constants.RootPath}\ConfigData"; // 検索するディレクトリ

            // 対象ファイルを検索する
            return Directory.GetFileSystemEntries(dirPath, @"Item*.config").ToList();
        }



        //インスタンスをXMLデータに変換する
        public static bool Serialization<T>(T obj, string xmlFilePath)
        {
            try
            {
                //XmlSerializerオブジェクトを作成
                //オブジェクトの型を指定する
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(T));
                //書き込むファイルを開く（UTF-8 BOM無し）
                System.IO.StreamWriter sw = new System.IO.StreamWriter(xmlFilePath, false, new System.Text.UTF8Encoding(false));
                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(sw, obj);
                //ファイルを閉じる
                sw.Close();

                return true;

            }
            catch
            {
                return false;
            }

        }

        //XMLデータからインスタンスを生成する
        public static T Deserialize<T>(string xmlFilePath)
        {
            System.Xml.Serialization.XmlSerializer serializer;
            using (var sr = new System.IO.StreamReader(xmlFilePath, new System.Text.UTF8Encoding(false)))
            {
                serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        //********************************************************
        //個別設定データの保存
        //********************************************************
        public static bool Save個別データ()
        {
            try
            {
                //Configファイルの保存
                Setting.作業者リスト = VmMainWindow.ListOperator;
                Setting.PathTheme = VmMainWindow.Theme;
                Setting.OpacityTheme = VmMainWindow.ThemeOpacity;

                Serialization<Configuration>(Setting, Constants.filePath_Configuration);

                return true;
            }
            catch
            {
                return false;

            }

        }




    }

}
