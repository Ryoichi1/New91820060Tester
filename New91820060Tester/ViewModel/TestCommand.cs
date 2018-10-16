
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Effects;
using static New91820060Tester.General;

namespace New91820060Tester
{
    public class TestCommand
    {

        //デリゲートの宣言
        public Action RefreshDataContext;//Test.Xaml内でテスト結果をクリアするために使用すする
        public Action SbRingLoad;
        public Action SbPass;
        public Action SbFail;
        public Action StopButtonBlinkOn;
        public Action StopButtonBlinkOff;

        private bool FlagTestTime;

        DropShadowEffect effect判定表示PASS;
        DropShadowEffect effect判定表示FAIL;

        public TestCommand()
        {
            effect判定表示PASS = new DropShadowEffect();
            effect判定表示PASS.Color = Colors.Aqua;
            effect判定表示PASS.Direction = 0;
            effect判定表示PASS.ShadowDepth = 0;
            effect判定表示PASS.Opacity = 1.0;
            effect判定表示PASS.BlurRadius = 80;

            effect判定表示FAIL = new DropShadowEffect();
            effect判定表示FAIL.Color = Colors.HotPink; ;
            effect判定表示FAIL.Direction = 0;
            effect判定表示FAIL.ShadowDepth = 0;
            effect判定表示FAIL.Opacity = 1.0;
            effect判定表示FAIL.BlurRadius = 40;

        }

        public async Task StartCheck()
        {
            var dis = App.Current.Dispatcher;
            while (true)
            {
                await Task.Run(() =>
                {
                    RETRY:
                    while (true)
                    {
                        if (Flags.OtherPage) break;
                        //Thread.Sleep(200);

                        //作業者名、工番が正しく入力されているかの判定
                        if (!Flags.SetOperator)
                        {
                            State.VmTestStatus.Message = Constants.MessOperator;
                            Flags.EnableTestStart = false;
                            continue;
                        }

                        if (!Flags.SetOpecode)
                        {
                            State.VmTestStatus.Message = Constants.MessOpecode;
                            Flags.EnableTestStart = false;
                            continue;
                        }

                        if (!Flags.SetModel)
                        {
                            State.VmTestStatus.Message = Constants.MessModel;
                            Flags.EnableTestStart = false;
                            continue;
                        }

                        General.CheckAll周辺機器フラグ();
                        if (!Flags.AllOk周辺機器接続)
                        {
                            State.VmTestStatus.Message = Constants.MessCheckConnectMachine;
                            Flags.EnableTestStart = false;
                            continue;
                        }


                        dis.BeginInvoke(StopButtonBlinkOn);

                        if (Flags.State日常点検)
                        {
                            State.VmTestStatus.Message = Constants.MessSet;
                            Flags.日常点検中 = false;//通常試験モード
                        }
                        else
                        {
                            State.VmTestStatus.Message = Constants.MessDailyCheck;
                            Flags.日常点検中 = true;//日常点検モード
                        }

                        Flags.EnableTestStart = true;
                        Flags.Click確認Button = false;

                        while (true)
                        {
                            if (Flags.OtherPage || Flags.Click確認Button)
                            {
                                dis.BeginInvoke(StopButtonBlinkOff);
                                return;
                            }

                            if (!Flags.SetOperator || !Flags.SetOpecode)
                            {
                                dis.BeginInvoke(StopButtonBlinkOff);
                                State.VmMainWindow.Flyout = false;
                                goto RETRY;
                            }
                        }
                    }

                });

                if (Flags.OtherPage)//待機中に他のページに遷移したらメソッドを抜ける
                {
                    return;
                }

                //プレスのレバーが下がっているかの確認
                if (!General.CheckPress())
                {
                    MessageBox.Show("プレスのレバーをさげてください");
                    continue;
                }
                //非常停止スイッチが解除されているかの確認
                if (!General.CheckEmergencyReset())
                {
                    MessageBox.Show("非常停止スイッチを解除してください");
                    continue;
                }

                State.VmMainWindow.EnableOtherButton = false;
                State.VmTestStatus.StartButtonContent = Constants.停止;
                State.VmTestStatus.TestSettingEnable = false;
                State.VmMainWindow.OperatorEnable = false;

                await Test();//メインルーチンへ

                //試験合格後、ラベル貼り付けページを表示する場合は下記のステップを追加すること
                //if (Flags.ShowLabelPage) return;

                //日常点検合格、一項目試験合格、試験NGの場合は、Whileループを繰り返す
                //通常試験合格の場合は、ラベル貼り付けフォームがロードされた時点で、一旦StartCheckメソッドを終了します
                //その後、ラベル貼り付けフォームが閉じられた後に、Test.xamlがリロードされ、そのフォームロードイベントでStartCheckメソッドがコールされます

            }

        }

        private void Timer()
        {
            var t = Task.Run(() =>
            {
                //Stopwatchオブジェクトを作成する
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                while (FlagTestTime)
                {
                    Thread.Sleep(200);
                    State.VmTestStatus.TestTime = sw.Elapsed.ToString().Substring(3, 5);
                }
                sw.Stop();
            });
        }

        //メインルーチン
        public async Task Test()
        {

            Flags.Testing = true;

            State.VmTestStatus.Message = Constants.MessWait;

            await Task.Delay(500);

            FlagTestTime = true;
            Timer();

            var FailStepNo = 0;
            var RetryCnt = 0;//リトライ用に使用する
            var FailTitle = "";

            Target.ChangeMode(Target.MODE.RS232);
            await Task.Delay(200);

            var テスト項目最新 = new List<TestSpecs>();
            if (State.VmTestStatus.CheckUnitTest == true)
            {
                //チェックしてある項目の百の桁の解析
                var re = Int32.Parse(State.VmTestStatus.UnitTestName.Split('_').ToArray()[0]);
                int 上位桁 = Int32.Parse(State.VmTestStatus.UnitTestName.Substring(0, (re >= 1000) ? 2 : 1));
                var 抽出データ = State.テスト項目.Where(p => (p.Key / 100) == 上位桁);
                foreach (var p in 抽出データ)
                {
                    テスト項目最新.Add(new TestSpecs(p.Key, p.Value, p.PowSw));
                }
            }
            else
            {
                if (Flags.日常点検中)
                {
                    テスト項目最新 = State.テスト日常点検;
                }
                else
                {
                    テスト項目最新 = State.テスト項目;
                }
            }



            try
            {
                //IO初期化
                General.ResetIo();
                Thread.Sleep(400);


                foreach (var d in テスト項目最新.Select((s, i) => new { i, s }))
                {
                    Retry:
                    State.VmTestStatus.Spec = "規格値 : ---";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                    Flags.AddDecision = true;

                    SetTestLog(d.s.Key.ToString() + "_" + d.s.Value);

                    if (d.s.PowSw)
                    {
                        if (!Flags.PowOn)
                        {
                            PowSupply(true);
                            await Task.Delay(2000);
                        }
                    }
                    else
                    {
                        PowSupply(false);
                        await Task.Delay(1000);
                    }

                    switch (d.s.Key)
                    {
                        case 100://コネクタ実装チェック
                            if (await コネクタチェック.CheckCn()) break;
                            goto case 5000;

                        case 200://電源5Vチェック
                            if (await MeasVoltage.Check5v()) break;
                            goto case 5000;

                        case 300://検査ソフト書き込み
                            if (State.VmTestStatus.CheckWriteTestFwPass == true) break;
                            if (await 書き込み.WriteFw(Constants.FolderPath_TestFw)) break;
                            goto case 5000;

                        case 400://P65 ADチェック
                            if (await P65ADチェック.CheckP65()) break;
                            goto case 5000;

                        case 500://デジタル出力チェック
                            if (await デジタル出力チェック.CheckDout()) break;
                            goto case 5000;

                        case 600://デジタル入力チェック
                            if (await デジタル入力チェック.CheckDin()) break;
                            goto case 5000;

                        case 700://DA OUT CN116 50
                            if (await TestDaOut.CheckDaOut(TestDaOut.DA_OUT_CH.CN116, TestDaOut.DA_OUT_VAL._50)) break;
                            goto case 5000;
                        case 701://DA OUT CN116 100
                            if (await TestDaOut.CheckDaOut(TestDaOut.DA_OUT_CH.CN116, TestDaOut.DA_OUT_VAL._100)) break;
                            goto case 5000;
                        case 702://DA OUT CN117 50
                            if (await TestDaOut.CheckDaOut(TestDaOut.DA_OUT_CH.CN117, TestDaOut.DA_OUT_VAL._50)) break;
                            goto case 5000;
                        case 703://DA OUT CN117 100
                            if (await TestDaOut.CheckDaOut(TestDaOut.DA_OUT_CH.CN117, TestDaOut.DA_OUT_VAL._100)) break;
                            goto case 5000;

                        case 800://サーミスタ入力チェック +60℃
                            if (await サーミスタ入力チェック.CheckTh(サーミスタ入力チェック.TEMP.P60)) break;
                            goto case 5000;
                        case 801://サーミスタ入力チェック +60℃
                            if (await サーミスタ入力チェック.CheckTh(サーミスタ入力チェック.TEMP.P20)) break;
                            goto case 5000;
                        case 802://サーミスタ入力チェック +60℃
                            if (await サーミスタ入力チェック.CheckTh(サーミスタ入力チェック.TEMP.M20)) break;
                            goto case 5000;

                        case 900://圧力入力1
                            if (await 圧力入力チェック.GetLev(圧力入力チェック.CH.CN11, 圧力入力チェック.PRESS.V1_5)) break;
                            goto case 5000;
                        case 901://圧力入力1
                            if (await 圧力入力チェック.GetLev(圧力入力チェック.CH.CN11, 圧力入力チェック.PRESS.V3_5)) break;
                            goto case 5000;
                        case 902://圧力入力1
                            if (await 圧力入力チェック.GetLev(圧力入力チェック.CH.CN12, 圧力入力チェック.PRESS.V1_5)) break;
                            goto case 5000;
                        case 903://圧力入力1
                            if (await 圧力入力チェック.GetLev(圧力入力チェック.CH.CN12, 圧力入力チェック.PRESS.V3_5)) break;
                            goto case 5000;
                        case 904://圧力入力1
                            if (await 圧力入力チェック.CheckPress(圧力入力チェック.PRESS.V1_5)) break;
                            goto case 5000;
                        case 905://圧力入力2
                            if (await 圧力入力チェック.CheckPress(圧力入力チェック.PRESS.V3_5)) break;
                            goto case 5000;

                        case 1000://EEP_ROMテスト
                            if (State.VmTestStatus.CheckTestQk == true) break;

                            await Task.Delay(500);
                            if (await EEPROMチェック.TestEep()) break;
                            goto case 5000;
                        case 1001://出荷設定書き込み
                            if (State.VmTestStatus.CheckTestQk == true) break;

                            if (await EEPROMチェック.WriteFactorySetting()) break;
                            goto case 5000;
                        case 1002://出荷設定チェック
                            if (State.VmTestStatus.CheckTestQk == true) break;

                            if (await EEPROMチェック.CheckFactorySetting()) break;
                            goto case 5000;

                        case 1100://DSW1チェック
                            if (await スイッチ入力チェック.CheckDsw1()) break;
                            goto case 5000;

                        case 1200://DSW2チェック
                            if (await スイッチ入力チェック.CheckDsw2()) break;
                            goto case 5000;

                        case 1300://PT100調整 100Ω
                            if (State.VmTestStatus.CheckTestQk == true) break;

                            if (await PT100チェック.CalPt100(PT100チェック.TH._100)) break;
                            goto case 5000;
                        case 1301://PT100調整 130Ω
                            if (State.VmTestStatus.CheckTestQk == true) break;

                            if (await PT100チェック.CalPt100(PT100チェック.TH._130)) break;
                            goto case 5000;

                        case 1400://補正値 EEPROMへの書き込み
                            if (State.VmTestStatus.CheckTestQk == true) break;

                            if (await PT100チェック.WriteAdjustmentToEep()) break;
                            goto case 5000;
                        case 1401://補正値 読み出してアプリ側で記録している値と同じか比較する T_C 100
                            if (State.VmTestStatus.CheckTestQk == true) break;

                            if (await PT100チェック.CompareAdjustment(PT100チェック.TH._100)) break;
                            goto case 5000;
                        case 1402://補正値 読み出してアプリ側で記録している値を同じがチェックする T_C 130
                            if (State.VmTestStatus.CheckTestQk == true) break;

                            if (await PT100チェック.CompareAdjustment(PT100チェック.TH._130)) break;
                            goto case 5000;

                        case 1500://PT100チェック 130Ω ※130Ω → 100Ωの方が抵抗を切り替える回数が減る！
                            if (await PT100チェック.CheckPt100(PT100チェック.TH._130)) break;
                            goto case 5000;
                        case 1501://PT100チェック 100Ω
                            if (await PT100チェック.CheckPt100(PT100チェック.TH._100)) break;
                            goto case 5000;

                        case 1600://製品ソフト書き込み
                            if (await 書き込み.WriteFw(State.ProductFwPath)) break;
                            goto case 5000;


                        case 5000://NGだっときの処理
                            Flags.Retry = true;
                            if (Flags.AddDecision) SetTestLog("---- FAIL\r\n");
                            FailStepNo = d.s.Key;
                            FailTitle = d.s.Value;

                            ResetIo();
                            //周辺機器の再初期化

                            State.VmTestStatus.IsActiveRing = false;//リング表示してる可能性があるので念のため消す処理

                            if (Flags.ClickStopButton) goto FAIL;

                            if (RetryCnt++ != Constants.RetryCount)
                            {
                                //リトライ履歴リスト更新
                                goto Retry;
                            }

                            General.PlaySoundLoop(General.soundAlarm);
                            var YesNoResult = MessageBox.Show("この項目はＮＧですがリトライしますか？", "", MessageBoxButtons.YesNo);
                            General.StopSound();

                            //何が選択されたか調べる
                            if (YesNoResult == DialogResult.Yes)
                            {
                                RetryCnt = 0;
                                await Task.Delay(1000);
                                goto Retry;
                            }

                            goto FAIL;


                    }
                    //↓↓各ステップが合格した時の処理です↓↓
                    if (Flags.AddDecision) SetTestLog("---- PASS\r\n");

                    State.VmTestStatus.IsActiveRing = false;

                    //リトライステータスをリセットする
                    RetryCnt = 0;
                    Flags.Retry = false;

                    await Task.Run(() =>
                    {
                        var CurrentProgValue = State.VmTestStatus.進捗度;
                        var NextProgValue = (int)(((d.i + 1) / (double)テスト項目最新.Count()) * 100);
                        var 変化量 = NextProgValue - CurrentProgValue;
                        foreach (var p in Enumerable.Range(1, 変化量))
                        {
                            State.VmTestStatus.進捗度 = CurrentProgValue + p;
                            if (State.VmTestStatus.CheckUnitTest == false)
                            {
                                Thread.Sleep(10);
                            }
                        }
                    });
                    if (Flags.ClickStopButton)
                    {
                        ResetIo();
                        goto FAIL;
                    }
                }


                //↓↓すべての項目が合格した時の処理です↓↓
                ResetIo();
                await Task.Delay(500);
                State.VmTestStatus.Message = Constants.MessRemove;

                if (Flags.日常点検中)
                {
                    if (State.VmTestStatus.CheckUnitTest != true) //null or False アプリ立ち上げ時はnullになっている！
                    {
                        using (var file = new System.IO.StreamWriter(Constants.Path_日常点検データ, true, System.Text.Encoding.GetEncoding("Shift_JIS")))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd/ HH:mm:ss"));
                        }
                        Flags.State日常点検 = true;
                    }
                }
                else
                {
                    //通しで試験が合格したときの処理です(検査データを保存して、シリアルナンバーをインクリメントする)
                    if (State.VmTestStatus.CheckUnitTest != true) //null or False アプリ立ち上げ時はnullになっている！
                    {
                        if (!General.SaveTestData())
                        {
                            FailStepNo = 5000;
                            FailTitle = "検査データ保存";
                            goto FAIL_DATA_SAVE;
                        }

                        await General.PushStamp();//合格印押し

                        //当日試験合格数をインクリメント ビューモデルはまだ更新しない
                        State.Setting.TodayOkCount++;
                    }
                }

                FlagTestTime = false;
                State.VmTestStatus.StartButtonContent = Constants.確認;
                State.VmTestStatus.StartButtonEnable = true;
                State.VmTestStatus.Message = Constants.MessRemove;

                State.VmTestStatus.Colorlabel判定 = Brushes.AntiqueWhite;
                State.VmTestStatus.Decision = "PASS";
                State.VmTestStatus.ColorDecision = effect判定表示PASS;

                ResetRing();
                SetDecision();
                SbPass();

                PlaySoundAsync(soundPassLong);

                Flags.Click確認Button = false;
                StopButtonBlinkOn();
                await Task.Run(() =>
                {
                    while (true)
                    {
                        if (Flags.Click確認Button)
                            break;
                        Thread.Sleep(100);
                    }
                });
                StopSound();
                StopButtonBlinkOff();
                await Task.Delay(500);

                return;

                //不合格時の処理
                FAIL:
                await Task.Delay(500);
                FAIL_DATA_SAVE:

                FlagTestTime = false;
                State.VmTestStatus.StartButtonContent = Constants.確認;
                State.VmTestStatus.StartButtonEnable = true;
                State.VmTestStatus.Message = Constants.MessRemove;


                //当日試験不合格数をインクリメント ビューモデルはまだ更新しない
                State.Setting.TodayNgCount++;
                await Task.Delay(100);

                State.VmTestStatus.Colorlabel判定 = Brushes.AliceBlue;
                State.VmTestStatus.Decision = "FAIL";
                State.VmTestStatus.ColorDecision = effect判定表示FAIL;

                SetErrorMessage(FailStepNo, FailTitle);

                var NgDataList = new List<string>()
                                    {
                                        State.VmMainWindow.Opecode,
                                        State.VmMainWindow.Operator,
                                        System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                        State.VmTestStatus.FailInfo,
                                        State.VmTestStatus.Spec,
                                        State.VmTestStatus.MeasValue
                                    };

                General.SaveNgData(NgDataList);


                ResetRing();
                SetDecision();
                SetErrInfo();
                SbFail();

                General.PlaySoundAsync(General.soundFail);

                Flags.Click確認Button = false;
                StopButtonBlinkOn();
                await Task.Run(() =>
                {
                    while (true)
                    {
                        if (Flags.Click確認Button)
                            break;
                        Thread.Sleep(100);
                    }
                });
                StopButtonBlinkOff();
                await Task.Delay(500);

                return;

            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("想定外の例外発生DEATH！！！\r\n申し訳ありませんが再起動してください");
                Environment.Exit(0);

            }
            finally
            {
                CheckSafetyG7sa();//

                ResetIo();
                SbRingLoad();

                ResetViewModel();
                RefreshDataContext();
            }

        }

        //フォームきれいにする処理いろいろ
        private void ClearForm()
        {
            SbRingLoad();
            RefreshDataContext();
        }

        private void SetErrorMessage(int stepNo, string title)
        {
            if (Flags.ClickStopButton)
            {
                State.VmTestStatus.FailInfo = "エラーコード ---     強制停止";
            }
            else
            {
                State.VmTestStatus.FailInfo = "エラーコード " + stepNo.ToString("00") + "   " + title + "異常";
            }
        }

        //テストログの更新
        private void SetTestLog(string addData)
        {
            State.VmTestStatus.TestLog += addData;
        }

        private void ResetRing()
        {
            State.VmTestStatus.RingVisibility = System.Windows.Visibility.Hidden;

        }

        private void SetDecision()
        {
            State.VmTestStatus.DecisionVisibility = System.Windows.Visibility.Visible;
        }

        private void SetErrInfo()
        {
            State.VmTestStatus.ErrInfoVisibility = System.Windows.Visibility.Visible;
        }



    }
}
