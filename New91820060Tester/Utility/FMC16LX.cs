using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using static System.Threading.Thread;
using System.Threading.Tasks;
using static New91820060Tester.General;

namespace New91820060Tester
{
    //＜注意事項＞
    //①このクラスはＦＤＴをシンプルインターフェイスで立ち上げることを前提に作ってあります

    public static class FMC16LX
    {
        //********************************************************************************************************
        // 外部プロセスのメイン・ウィンドウを起動するためのWin32 API
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hWnd, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        // ShowWindowAsync関数のパラメータに渡す定義値
        private const int SW_RESTORE = 9;  // 画面を元の大きさに戻す

        //********************************************************************************************************


        //静的メンバの宣言

        private static System.Timers.Timer Tm;
        private static Process Fmp;

        //フラグ
        private static bool FlagTimer;
        public static bool FlagWrite { get; private set; }


        //コンストラクタ
        static FMC16LX()
        {
            //タイマー（ウィンドウハンドル取得用）の設定
            Tm = new System.Timers.Timer();
            Tm.Enabled = false;
            Tm.Interval = 5000;
            Tm.Elapsed += (object s, ElapsedEventArgs e) =>
            {
                Tm.Stop();
                FlagTimer = false;//タイムアウト
            };
        }

        public static void SetAdapter(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P4, EPX64S.BIT.b7, EPX64S.OUT.H);
        }

        private static void SetAdapterButton(bool sw)
        {
            io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b1, EPX64S.OUT.H);
            Sleep(500);
            io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b1, EPX64S.OUT.L);
        }



        public static async Task<bool> WriteFirmware(string path)
        {
            //フラグの初期化
            FlagWrite = false;

            //ローカル関数の定義
            bool foo(string title, string mess, double time)
            {
                FlagTimer = true;
                Tm.Interval = time;
                Tm.Start();
                while (true)
                {
                    if (!FlagTimer)
                        return false;
                    var hWnd_完了画面 = FindWindow("#32770", title);
                    if (hWnd_完了画面 == IntPtr.Zero) continue;
                    var hWnd_ResultText = FindWindowEx(hWnd_完了画面, IntPtr.Zero, "Static", mess);
                    if (hWnd_ResultText != IntPtr.Zero)
                    {
                        Sleep(500);
                        return true;
                    }
                }
            }

            return await Task<bool>.Run(() =>
            {
                try
                {
                    SetAdapter(true);
                    Sleep(500);
                    PowSupply(true);
                    Sleep(1000);
                    SetAdapterButton(true);
                    Sleep(700);
                    SetAdapterButton(false);
                    Sleep(1000);

                    //プロセスを作成してFDTを立ち上げる
                    IntPtr hWnd = IntPtr.Zero;
                    Fmp = new Process();
                    Fmp.StartInfo.FileName = Constants.ExePath_FMC16;
                    Fmp.Start();
                    Fmp.WaitForInputIdle();//指定されたプロセスで未処理の入力が存在せず、ユーザーからの入力を待っている状態になるまで、またはタイムアウト時間が経過するまで待機します。

                    //FDTのウィンドウハンドル取得
                    FlagTimer = true;
                    Tm.Start();
                    while (hWnd == IntPtr.Zero)
                    {
                        Application.DoEvents();
                        if (FlagTimer == false)
                        {
                            Fmp.Kill();
                            Fmp.Close();
                            Fmp.Dispose();
                            return false;
                        }

                        hWnd = FindWindow(null, "FUJITSU FLASH MCU Programmer");
                    }

                    //FDTを最前面に表示してアクティブにする（センドキー送るため）
                    SetForegroundWindow(hWnd);
                    Sleep(1000);

                    SendKeys.SendWait("{TAB}");
                    Sleep(500);
                    SendKeys.SendWait("{TAB}");
                    Sleep(500);
                    SendKeys.SendWait("{ENTER}");
                    Sleep(500);

                    //ファームウェアのファイルパスを入力
                    SendKeys.SendWait(path);
                    Sleep(400);
                    SendKeys.SendWait("{ENTER}");
                    Sleep(400);
                    SendKeys.SendWait("{TAB}");
                    Sleep(300);
                    SendKeys.SendWait("{TAB}");
                    Sleep(300);//ここでDownLoadボタンにいる
                    SendKeys.SendWait("{ENTER}");
                    Sleep(500);
                    SendKeys.SendWait("{ENTER}");

                    if (!foo("8 / 8", "Download OK!", 7000))
                        return false;
                    Sleep(100);
                    SendKeys.SendWait("{ENTER}");//OKボタンを押す

                    Sleep(100);
                    SendKeys.SendWait("{TAB}");
                    Sleep(300);//ここでEraseボタンにいる
                    SendKeys.SendWait("{ENTER}");

                    if (!foo("5 / 5", "Erase OK!", 12000))
                        return false;
                    Sleep(100);
                    SendKeys.SendWait("{ENTER}");//OKボタンを押す
                    Sleep(100);
                    SendKeys.SendWait("+{TAB}");
                    Sleep(100);
                    SendKeys.SendWait("+{TAB}");

                    SetAdapterButton(true);
                    Sleep(1000);
                    SetAdapterButton(false);
                    Sleep(1000);

                    Sleep(100);//ここでFull Operationボタンにいる
                    SendKeys.SendWait("{ENTER}");
                    Sleep(300);//ここでFull Operationボタンにいる
                    SendKeys.SendWait("{ENTER}");
                    return foo("512 / 512", "Full Operation OK!", 50000);

                }
                catch
                {
                    return false;
                }
                finally
                {
                    Sleep(500);
                    Fmp.Kill();
                    Fmp.Close();
                    Fmp.Dispose();

                    Sleep(600);
                    PowSupply(false);
                    Sleep(1000);
                    SetAdapter(false);
                    Sleep(500);
                }
            });
        }






    }
}
