﻿using System.Windows.Media;

namespace New91820060Tester
{
    public static class Flags
    {
        public static bool OtherPage { get; set; }

        //試験開始時に初期化が必要なフラグ
        public static bool StopInit周辺機器 { get; set; }
        public static bool Initializing周辺機器 { get; set; }
        public static bool EnableTestStart { get; set; }
        public static bool Testing { get; set; }
        public static bool PowOn { get; set; }//メイン電源ON/OFF

        public static bool Ac200On { get; set; }

        public static bool 日常点検中 { get; set; }//日常点検中はTrue、通常試験はFalse

        public static bool ShowErrInfo { get; set; }
        public static bool AddDecision { get; set; }

        public static bool DialogReturn { get; set; }

        public static bool ClickStopButton { get; set; }
        public static bool Click確認Button { get; set; }
        public static bool AllOk周辺機器接続 { get; set; }

        public static bool MustCheckQ3Out { get; set; }

        private static SolidColorBrush RetryPanelBrush = new SolidColorBrush();
        private static SolidColorBrush StatePanelOkBrush = new SolidColorBrush();
        private static SolidColorBrush StatePanelNgBrush = new SolidColorBrush();
        private const double StatePanelOpacity = 0.3;

        static Flags()//コンストラクタ
        {
            RetryPanelBrush.Color = Colors.DodgerBlue;
            RetryPanelBrush.Opacity = StatePanelOpacity;

            StatePanelOkBrush.Color = Colors.DodgerBlue;
            StatePanelOkBrush.Opacity = StatePanelOpacity;

            StatePanelNgBrush.Color = Colors.DeepPink;
            StatePanelNgBrush.Opacity = StatePanelOpacity;
        }

        private static bool _SetAdapterForAc100Input;
        public static bool SetAdapterForAc100Input
        {
            get { return _SetAdapterForAc100Input; }
            set
            {
                _SetAdapterForAc100Input = value;
                if (_SetAdapterForAc100Input)
                {
                    State.VmTestStatus.ColAc100 = General.OnBrush;
                    State.VmTestStatus.ColAc200 = General.OffBrush;
                }
                else
                {
                    State.VmTestStatus.ColAc100 = General.OffBrush;
                    State.VmTestStatus.ColAc200 = General.OnBrush;
                }
            }
        }

        private static bool _State日常点検;
        public static bool State日常点検
        {
            get { return _State日常点検; }
            set
            {
                _State日常点検 = value;
                State.VmTestStatus.ColDailyCheck = value ? General.OnBrush : General.NgBrush;
            }
        }


        //周辺機器ステータス
        private static bool _StateG7sa;
        public static bool StateG7sa
        {
            get { return _StateG7sa; }
            set
            {
                _StateG7sa = value;
                State.VmTestStatus.ColorG7sa = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }

        private static bool _StateMbed;
        public static bool StateMbed
        {
            get { return _StateMbed; }
            set
            {
                _StateMbed = value;
                State.VmTestStatus.ColorMbed = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }


        private static bool _StateEpx64_1;
        public static bool StateEpx64_1
        {
            get { return _StateEpx64_1; }
            set
            {
                _StateEpx64_1 = value;
                State.VmTestStatus.ColorEpx64_1 = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }

        private static bool _StateEpx64_2;
        public static bool StateEpx64_2
        {
            get { return _StateEpx64_2; }
            set
            {
                _StateEpx64_2 = value;
                State.VmTestStatus.ColorEpx64_2 = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }

        private static bool _State232C;
        public static bool State232C
        {
            get { return _State232C; }
            set
            {
                _State232C = value;
                State.VmTestStatus.Color232C = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }

        private static bool _State422;
        public static bool State422
        {
            get { return _State422; }
            set
            {
                _State422 = value;
                State.VmTestStatus.Color422 = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }


        private static bool _Retry;
        public static bool Retry
        {
            get { return _Retry; }
            set
            {
                _Retry = value;
                State.VmTestStatus.RetryLabelVis = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            }
        }



        //フラグ
        private static bool _SetOperator;
        public static bool SetOperator
        {
            get { return _SetOperator; }
            set
            {
                _SetOperator = value;
                if (value)
                {
                    if (State.VmMainWindow.Operator == "畔上")
                    {
                        State.VmTestStatus.UnitTestEnable = true;
                    }
                    else
                    {
                        //一般作業者には、単体テスト選択できないようにする
                        State.VmTestStatus.UnitTestEnable = false;
                        State.VmTestStatus.CheckUnitTest = false;
                    }

                    if (SetOpecode)
                        return;

                    //工番入力許可する
                    State.VmMainWindow.ReadOnlyOpecode = false;
                }
                else
                {
                    State.VmMainWindow.Operator = "";
                    State.VmTestStatus.UnitTestEnable = false;
                    State.VmTestStatus.CheckUnitTest = false;
                    State.VmMainWindow.SelectIndex = -1;
                }
            }
        }


        private static bool _SetOpecode;
        public static bool SetOpecode
        {
            get { return _SetOpecode; }

            set
            {
                _SetOpecode = value;

                if (value)
                {
                    State.VmMainWindow.ReadOnlyOpecode = true;//工番が確定したので、編集不可とする
                    State.VmMainWindow.ReadOnlyModel = false;//型番入力を許可する
                }
                else
                {
                    State.VmMainWindow.ReadOnlyOpecode = false;
                    State.VmMainWindow.Opecode = "";
                }

            }
        }

        private static bool _SetModel;
        public static bool SetModel
        {
            get { return _SetModel; }

            set
            {
                _SetModel = value;

                if (value)
                {
                    State.VmMainWindow.ReadOnlyModel = true;
                }
                else
                {
                    State.VmMainWindow.ReadOnlyModel = false;
                    State.VmMainWindow.Model = "";
                }

            }
        }



    }
}
