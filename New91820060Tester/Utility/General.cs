using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;
using static System.Threading.Thread;

namespace New91820060Tester
{


    public static class General
    {
        public static EPX64S io1;
        public static EPX64S io2;


        //インスタンス変数の宣言
        public static SoundPlayer player = null;
        public static SoundPlayer soundPass = null;
        public static SoundPlayer soundPassLong = null;
        public static SoundPlayer soundFail = null;
        public static SoundPlayer soundAlarm = null;
        public static SoundPlayer soundSuccess = null;
        public static SoundPlayer soundNotice = null;
        public static SoundPlayer soundCutin = null;
        public static SoundPlayer soundSwCheck = null;


        public static SolidColorBrush DialogOnBrush = new SolidColorBrush();
        public static SolidColorBrush OnBrush = new SolidColorBrush();
        public static SolidColorBrush OffBrush = new SolidColorBrush();
        public static SolidColorBrush NgBrush = new SolidColorBrush();

        static General()
        {
            //オーディオリソースを取り出す
            General.soundPass = new SoundPlayer(@"Resources\Wav\VictoryLong.wav");
            //General.soundPass = new SoundPlayer(@"Resources\Wav\Victory.wav");
            //General.soundPass = new SoundPlayer(@"Resources\Wav\Pass.wav");
            General.soundPassLong = new SoundPlayer(@"Resources\Wav\PassLong.wav");
            General.soundFail = new SoundPlayer(@"Resources\Wav\Fail.wav");
            General.soundAlarm = new SoundPlayer(@"Resources\Wav\Alarm.wav");
            General.soundSuccess = new SoundPlayer(@"Resources\Wav\Success.wav");
            General.soundNotice = new SoundPlayer(@"Resources\Wav\Notice.wav");
            General.soundCutin = new SoundPlayer(@"Resources\Wav\CutIn.wav");
            General.soundSwCheck = new SoundPlayer(@"Resources\Wav\SwCheck.wav");

            //カラーテンプレートいろいろ
            OffBrush.Color = Colors.Transparent;

            DialogOnBrush.Color = Colors.DodgerBlue;
            DialogOnBrush.Opacity = 0.6;

            OnBrush.Color = Colors.DodgerBlue;
            OnBrush.Opacity = 0.4;

            NgBrush.Color = Colors.HotPink;
            NgBrush.Opacity = 0.4;
        }
        public static void Show()
        {
            var T = 0.3;
            var t = 0.005;

            State.Setting.OpacityTheme = State.VmMainWindow.ThemeOpacity;
            //10msec刻みでT秒で元のOpacityに戻す
            int times = (int)(T / t);

            State.VmMainWindow.ThemeOpacity = 0;
            Task.Run(() =>
            {
                while (true)
                {

                    State.VmMainWindow.ThemeOpacity += State.Setting.OpacityTheme / (double)times;
                    Thread.Sleep((int)(t * 1000));
                    if (State.VmMainWindow.ThemeOpacity >= State.Setting.OpacityTheme) return;

                }
            });
        }

        public static void ResetIo()
        {
            io1.OutByte(EPX64S.PORT.P0, 0);
            io1.OutByte(EPX64S.PORT.P1, 0);
            io1.OutByte(EPX64S.PORT.P2, 0);
            io1.OutByte(EPX64S.PORT.P4, 0b0100_0000);
            Flags.PowOn = false;
        }


        private static List<string> MakePassTestData()//TODO:
        {
            var ListData = new List<string>
            {
                "AssemblyVer " + State.AssemblyInfo,
                "TestSpecVer " + State.TestSpec.TestSpecVer,
                System.DateTime.Now.ToString("yyyy年MM月dd日(ddd) HH:mm:ss"),
                State.VmMainWindow.Operator,
                //State._item == ITEM._002? "91821199-002/R" : "91821199-003/R",
                //State._item == ITEM._002? State.TestSpec._002_Ver : State.TestSpec._003_Ver,
                //State._item == ITEM._002? State.TestSpec._002_Sum : State.TestSpec._003_Sum,

            };

            return ListData;
        }

        public static bool SaveTestData()
        {
            try
            {
                var dataList = new List<string>();
                dataList = MakePassTestData();

                var OkDataFilePath = Constants.PassDataFolderPath + State.VmMainWindow.Opecode + ".csv";

                if (!System.IO.File.Exists(OkDataFilePath))
                {
                    //既存検査データがなければ新規作成
                    File.Copy(Constants.PassDataFolderPath + "Format.csv", OkDataFilePath);
                }

                // リストデータをすべてカンマ区切りで連結する
                string stCsvData = string.Join(",", dataList);

                // appendをtrueにすると，既存のファイルに追記
                // falseにすると，ファイルを新規作成する
                var append = true;

                // 出力用のファイルを開く
                using (var sw = new System.IO.StreamWriter(OkDataFilePath, append, Encoding.GetEncoding("Shift_JIS")))
                {
                    sw.WriteLine(stCsvData);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        //**************************************************************************
        //検査データの保存　　　　
        //引数：なし
        //戻値：なし
        //**************************************************************************

        public static bool SaveNgData(List<string> dataList)
        {
            try
            {
                var NgDataFilePath = Constants.FailDataFolderPath + State.VmMainWindow.Opecode + ".csv";
                if (!File.Exists(NgDataFilePath))
                {
                    //既存検査データがなければ新規作成
                    File.Copy(Constants.FailDataFolderPath + "FormatNg.csv", NgDataFilePath);
                }

                var stArrayData = dataList.ToArray();
                // リストデータをすべてカンマ区切りで連結する
                string stCsvData = string.Join(",", stArrayData);

                // appendをtrueにすると，既存のファイルに追記
                //         falseにすると，ファイルを新規作成する
                var append = true;

                // 出力用のファイルを開く
                using (var sw = new System.IO.StreamWriter(NgDataFilePath, append, Encoding.GetEncoding("Shift_JIS")))
                {
                    sw.WriteLine(stCsvData);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }



        public static void PowSupply(bool sw)
        {
            if (Flags.PowOn == sw) return;

            io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b0, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
            Flags.PowOn = sw;
        }


        //**************************************************************************
        //WAVEファイルを再生する
        //引数：なし
        //戻値：なし
        //**************************************************************************  

        //WAVEファイルを再生する（非同期で再生）
        public static void PlaySound(SoundPlayer p)
        {
            //再生されているときは止める
            if (player != null)
                player.Stop();

            //waveファイルを読み込む
            player = p;
            //最後まで再生し終えるまで待機する
            player.Play();
        }
        //WAVEファイルを再生する（同期で再生）
        public static void PlaySound2(SoundPlayer p)
        {
            //再生されているときは止める
            if (player != null)
                player.Stop();

            //waveファイルを読み込む
            player = p;
            //最後まで再生し終えるまで待機する
            player.PlaySync();

        }

        public static void PlaySoundLoop(SoundPlayer p)
        {
            //再生されているときは止める
            if (player != null)
                player.Stop();

            //waveファイルを読み込む
            player = p;
            //最後まで再生し終えるまで待機する
            player.PlayLooping();
        }

        //再生されているWAVEファイルを止める
        public static void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }



        public static void ResetViewModel()//TODO:
        {
            //ViewModel OK台数、NG台数、Total台数の更新
            State.VmTestStatus.OkCount = State.Setting.TodayOkCount.ToString() + "台";
            State.VmTestStatus.NgCount = State.Setting.TodayNgCount.ToString() + "台";
            State.VmTestStatus.Message = Constants.MessSet;


            State.VmTestStatus.DecisionVisibility = System.Windows.Visibility.Hidden;
            State.VmTestStatus.ErrInfoVisibility = System.Windows.Visibility.Hidden;
            State.VmTestStatus.RingVisibility = System.Windows.Visibility.Visible;

            State.VmTestStatus.TestTime = "00:00";
            State.VmTestStatus.進捗度 = 0;
            State.VmTestStatus.TestLog = "";

            State.VmTestStatus.FailInfo = "";
            State.VmTestStatus.Spec = "";
            State.VmTestStatus.MeasValue = "";


            //試験結果のクリア
            State.VmTestResults = new ViewModelTestResult();

            //通信ログのクリア
            //TODO:
            State.VmComm.TX = "";
            State.VmComm.RX = "";

            //他ページへの遷移を許可する
            State.VmMainWindow.EnableOtherButton = true;

            //各種フラグの初期化
            Flags.PowOn = false;
            Flags.ClickStopButton = false;
            Flags.Testing = false;

            State.VmComm.ColRs232c = General.OnBrush;
            State.VmComm.ColRs422 = General.OffBrush;


            State.VmTestStatus.RetryLabelVis = System.Windows.Visibility.Hidden;
            State.VmTestStatus.TestSettingEnable = true;
            State.VmMainWindow.OperatorEnable = true;

            //コネクタチェックでエラーになると表示されたままになるので隠す（誤動作防止！！！）
            State.VmTestStatus.EnableButtonErrInfo = System.Windows.Visibility.Hidden;

            //次回試験時にチェックしたままだと、ソフト書き忘れてNGになるため、毎回初期化する
            State.VmTestStatus.CheckWriteTestFwPass = false;

        }


        public static void CheckAll周辺機器フラグ()
        {
            Flags.AllOk周辺機器接続 = Flags.StateMbed && Flags.StateEpx64_1 && Flags.StateEpx64_2 && Flags.State232C && Flags.State422;
        }


        public static void Init周辺機器()//TODO:
        {
            Flags.Initializing周辺機器 = true;

            //RS232C通信ポートの初期化
            bool Stop232 = false;
            Task.Run(() =>
            {
                while (true)
                {
                    if (Flags.StopInit周辺機器)
                    {
                        break;
                    }

                    Flags.State232C = Target.InitPort232();
                    if (Flags.State232C)
                        break;

                    Thread.Sleep(500);
                }
                Stop232 = true;
            });

            //RS422通信ポートの初期化
            bool Stop422 = false;
            Task.Run(() =>
            {
                while (true)
                {
                    if (Flags.StopInit周辺機器)
                    {
                        break;
                    }

                    Flags.State422 = Target.InitPort422();
                    if (Flags.State422)
                        break;

                    Thread.Sleep(500);
                }
                Stop422 = true;
            });

            //LPC1768の初期化d
            bool StopMbed = false;
            Task.Run(() =>
            {
                while (true)
                {
                    if (Flags.StopInit周辺機器)
                    {
                        break;
                    }

                    Flags.StateMbed = LPC1768.Init();
                    if (Flags.StateMbed)
                        break;

                    Thread.Sleep(500);
                }
                StopMbed = true;
            });

            //EPX64S_1の初期化
            bool StopIo1 = false;
            Task.Run(() =>
            {
                General.io1 = new EPX64S();
                while (true)
                {
                    if (Flags.StopInit周辺機器)
                    {
                        break;
                    }
                               
                    Flags.StateEpx64_1 =  General.io1.InitEpx64S(0b0001_0111, Constants.SerialEpx64s_1);//
                    if (Flags.StateEpx64_1)
                    {
                        ResetIo();
                        break;
                    }

                    Thread.Sleep(500);
                }
                StopIo1 = true;
            });

            //EPX64S_2の初期化
            bool StopIo2 = false;
            Task.Run(() =>
            {
                General.io2 = new EPX64S();
                while (true)
                {
                    if (Flags.StopInit周辺機器)
                    {
                        break;
                    }
                               
                    Flags.StateEpx64_2 =  General.io2.InitEpx64S(0b0000_0000, Constants.SerialEpx64s_2);//全ポート入力設定
                    if (Flags.StateEpx64_2)
                        break;

                    Thread.Sleep(500);
                }
                StopIo2 = true;
            });


            Task.Run(() =>
            {
                while (true)
                {
                    CheckAll周辺機器フラグ();

                    //EPX64Sの初期化の中で、K100、K101の溶着チェックを行っているが、これがNGだとしてもInit周辺機器()は終了する
                    var IsAllStopped = Stop232 && Stop422 && StopMbed && StopIo1 && StopIo2;

                    if (Flags.AllOk周辺機器接続 || IsAllStopped) break;
                    Sleep(400);

                }
                Flags.Initializing周辺機器 = false;
            });

        }

        public enum TH { TEMP_P60, TEMP_P20, TEMP_M20 }

        public static void SetTh(TH th)
        {
            switch (th)
            {
                case TH.TEMP_P60:
                    io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b0, EPX64S.OUT.L);
                    io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b1, EPX64S.OUT.L);
                    break;
                case TH.TEMP_P20:
                    io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b0, EPX64S.OUT.H);
                    io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b1, EPX64S.OUT.L);
                    break;
                case TH.TEMP_M20:
                    io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b0, EPX64S.OUT.L);
                    io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b1, EPX64S.OUT.H);
                    break;
            }
        }

        public static async Task PushStamp()
        {
            io1.OutBit(EPX64S.PORT.P4, EPX64S.BIT.b6, EPX64S.OUT.L);
            await Task.Delay(500);
            io1.OutBit(EPX64S.PORT.P4, EPX64S.BIT.b6, EPX64S.OUT.H);
        }

        //強制ガイドリレー フィードバック信号取得
        //強制ガイドリレーOFF状態にて確認すること
        public static bool GetG7saFb()
        {
            io2.ReadInputData(EPX64S.PORT.P7);
            return (io2.P7InputData & 0b0010_0000) == 0;//強制ガイドリレーが正常にOFFしていれば、 P75が0になります
        }


        public static void ResetAdInputForMed()
        {
            SetRL1(false);
            SetRL2(false);
            SetRL3(false);
            SetK4(false);
        }


        //試験機リレー制御
        public static void SetK4(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b2, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetRL1(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b3, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetRL2(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b4, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetRL3(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b5, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetRL4(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b6, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetRY100(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b2, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetRY101(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b3, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetRY102(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b4, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetRY103(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b5, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetRY104(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b6, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }

        public static void SetG7SA(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P1, EPX64S.BIT.b7, sw ? EPX64S.OUT.H : EPX64S.OUT.L);
        }


    }

}

