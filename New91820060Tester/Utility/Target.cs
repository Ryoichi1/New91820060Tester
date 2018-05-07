using System.IO.Ports;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace New91820060Tester
{
    public static class Target
    {
        public enum MODE { RS232, RS422 }

        private const string ComName232 = "USB Serial Port (";
        private const string ComName422 = "CONTEC";

        private static SerialPort port232;
        private static SerialPort port422;

        public static string RecieveData { get; private set; }//披検査基板から受信した生データ(232C,422 共通で使用する)
        public static MODE CurrentMode { get; set; }

        static Target()
        {
            port232 = new SerialPort();
            port422 = new SerialPort();

            State.VmComm.ColRs232c = General.OnBrush;
            State.VmComm.ColRs422 = General.OffBrush;
        }

        public static bool InitPort232()
        {
            try
            {
                var comNum = FindSerialPort.GetComNo(ComName232);
                if (comNum == null) return false;

                //Comポートの取得
                if (!port232.IsOpen)
                {
                    //シリアルポート設定
                    port232.PortName = comNum;
                    port232.BaudRate = 38400;
                    port232.DataBits = 8;
                    port232.Parity = System.IO.Ports.Parity.None;
                    port232.StopBits = System.IO.Ports.StopBits.One;
                    port232.RtsEnable = false;
                    port232.DtrEnable = true;
                    port232.NewLine = "\r\n";
                    port232.ReadTimeout = 1000;
                    //ポートオープン
                    port232.Open();
                }
                return true;
            }
            catch
            {
                if (port232.IsOpen) port232.Close();
                return false;
            }
        }

        public static bool InitPort422()
        {
            try
            {
                var comNum = FindSerialPort.GetComNo(ComName422);
                if (comNum == null) return false;

                //Comポートの取得
                if (!port422.IsOpen)
                {
                    //シリアルポート設定
                    port422.PortName = comNum;
                    port422.BaudRate = 38400;
                    port422.DataBits = 8;
                    port422.Parity = System.IO.Ports.Parity.None;
                    port422.StopBits = System.IO.Ports.StopBits.One;
                    port422.RtsEnable = true;
                    port422.NewLine = "\r\n";
                    port422.ReadTimeout = 1000;
                    //ポートオープン
                    port422.Open();
                }
                return true;
            }
            catch
            {
                if (port422.IsOpen) port422.Close();
                return false;
            }
        }

        public static void Close232()
        {
            if (port232.IsOpen) port232.Close();
        }

        public static void Close422()
        {
            if (port422.IsOpen) port422.Close();
        }


        public static void ChangeMode(MODE mode)
        {
            if (mode == MODE.RS232)
            {
                General.io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b7, EPX64S.OUT.L);
                port232.RtsEnable = true;
                State.VmComm.ColRs232c = General.OnBrush;
                State.VmComm.ColRs422 = General.OffBrush;
                CurrentMode = MODE.RS232;
            }
            else
            {
                General.io1.OutBit(EPX64S.PORT.P0, EPX64S.BIT.b7, EPX64S.OUT.H);
                port232.RtsEnable = false;
                State.VmComm.ColRs232c = General.OffBrush;
                State.VmComm.ColRs422 = General.OnBrush;
                CurrentMode = MODE.RS422;
            }
        }

        //**************************************************************************
        //ターゲットにコマンドを送る
        //**************************************************************************
        public static bool SendData(string Data, int Wait = 3000, bool setLog = true, bool DoAnalysis = true)
        {
            var selectedPort = new SerialPort();
            selectedPort = CurrentMode == MODE.RS232 ? port232 : port422;

            //送信処理
            try
            {
                State.VmComm.TX = "";
                State.VmComm.RX = "";

                ClearBuff();//受信バッファのクリア

                selectedPort.WriteLine(Data);
                if (setLog)
                    State.VmComm.TX = Data;
                if (!DoAnalysis)
                    return true;

            }
            catch
            {
                State.VmComm.TX = "TX_Error";
                return false;
            }

            //受信処理
            try
            {
                RecieveData = "";//初期化
                selectedPort.ReadTimeout = Wait;
                RecieveData = selectedPort.ReadLine();
                if (setLog)
                    State.VmComm.RX = RecieveData;
                return true;
            }
            catch
            {
                State.VmComm.RX = "TimeoutErr";
                return false;
            }
            finally
            {
                Thread.Sleep(100);
            }
        }



        //**************************************************************************
        //受信バッファをクリアする
        //**************************************************************************
        private static void ClearBuff()
        {
            if (port232.IsOpen)
                port232.DiscardInBuffer();
            if (port422.IsOpen)
                port422.DiscardInBuffer();
        }
    }
}
