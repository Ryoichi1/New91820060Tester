﻿using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;


namespace New91820060Tester
{
    public class ViewModelTestStatus : BindableBase
    {

        //ファームウェア情報 ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        private string _FwVer;
        public string FwVer
        {
            get { return _FwVer; }
            set { SetProperty(ref _FwVer, value); }
        }


        private string _FwSum;
        public string FwSum
        {
            get { return _FwSum; }
            set { SetProperty(ref _FwSum, value); }
        }

        //エラー詳細表示ボタンの可視切り替え
        private Visibility _EnableButtonErrInfo = Visibility.Hidden;
        public Visibility EnableButtonErrInfo
        {
            get { return _EnableButtonErrInfo; }

            set
            {
                SetProperty(ref _EnableButtonErrInfo, value);
            }
        }


        //強制停止ボタンのプロパティ（PWA試験時に使用）////////////////////////////////////////////////////////////////////////////////////////
        private bool _StopButtonEnable;
        public bool StopButtonEnable
        {

            get { return _StopButtonEnable; }
            set { SetProperty(ref _StopButtonEnable, value); }

        }

        private double _StopButtonVis;
        public double StopButtonVis
        {
            get { return _StopButtonVis; }
            set { SetProperty(ref _StopButtonVis, value); }
        }



        //スタートボタンのプロパティ(完成体検査時に使用)/////////////////////////////////////////////////////////////////////////////////////

        private bool _StartButtonEnable;
        public bool StartButtonEnable
        {

            get { return _StartButtonEnable; }
            set { SetProperty(ref _StartButtonEnable, value); }

        }

        private string _StartButtonContent;
        public string StartButtonContent
        {
            get { return _StartButtonContent; }
            set { SetProperty(ref _StartButtonContent, value); }
        }


        //テストログ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        private string _TestLog;
        public string TestLog
        {
            get { return _TestLog; }
            set { SetProperty(ref _TestLog, value); }
        }

        //EEPROMチェック、熱電対補正値取得時のリング表示
        private bool _IsActiveRing;
        public bool IsActiveRing { get { return _IsActiveRing; } set { SetProperty(ref _IsActiveRing, value); } }

        private Visibility _RetryLabelVis;
        public Visibility RetryLabelVis
        {
            get { return _RetryLabelVis; }
            set { SetProperty(ref _RetryLabelVis, value); }
        }

        //テスト時間
        private string _TestTime;
        public string TestTime
        {
            get { return _TestTime; }
            set { SetProperty(ref _TestTime, value); }
        }

        //判定表示、進捗表示プログレスリング■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

        //判定表示　PASS or FAIL
        private string _Decision;
        public string Decision
        {
            get { return _Decision; }
            set { SetProperty(ref _Decision, value); }
        }

        //label判定Color
        private Brush _Colorlabel判定;
        public Brush Colorlabel判定
        {
            get { return _Colorlabel判定; }
            set { SetProperty(ref _Colorlabel判定, value); }
        }

        //FAIL判定時に表示するエラー情報
        private string _FailInfo;
        public string FailInfo
        {
            get { return _FailInfo; }
            set { SetProperty(ref _FailInfo, value); }
        }

        //FAIL判定時に表示する試験スペック
        private string _Spec;
        public string Spec
        {
            get { return _Spec; }
            set { SetProperty(ref _Spec, value); }
        }

        //FAIL判定時に表示する計測値
        private string _MeasValue;
        public string MeasValue
        {
            get { return _MeasValue; }
            set { SetProperty(ref _MeasValue, value); }
        }

        //判定表示の可視性
        private Visibility _DecisionVisibility;
        public Visibility DecisionVisibility
        {
            get { return _DecisionVisibility; }
            set { SetProperty(ref _DecisionVisibility, value); }
        }

        //規格値、計測値の可視性
        private Visibility _ErrInfoVisibility;
        public Visibility ErrInfoVisibility
        {
            get { return _ErrInfoVisibility; }
            set { SetProperty(ref _ErrInfoVisibility, value); }
        }



        //判定表示の色
        private DropShadowEffect _ColorDecision;
        public DropShadowEffect ColorDecision
        {
            get { return _ColorDecision; }
            set { SetProperty(ref _ColorDecision, value); }
        }


        //プログレスリングのEndAngle
        private int _進捗度;
        public int 進捗度
        {
            get { return _進捗度; }
            set { SetProperty(ref _進捗度, value); }
        }

        //プログレスリングの可視性
        private Visibility _RingVisibility;
        public Visibility RingVisibility
        {
            get { return _RingVisibility; }
            set { SetProperty(ref _RingVisibility, value); }
        }

        //テストオプション（単体試験選択）■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

        //試験中はテスト設定パネルを操作できないようにするためのプロパティ
        private bool _TestSettingEnable = true;
        public bool TestSettingEnable
        {

            get { return _TestSettingEnable; }
            set { SetProperty(ref _TestSettingEnable, value); }
        }


        //単体試験チェックボックスとコンボボックスの可視切り替え
        //これ重要！！！ 
        //EnableUnitTestをhiddenにした時点で、CheckUnitTestは必ずfalseになる
        //畔上以外の作業者を選択時は、EnableUnitTestがhiddenになるため、
        //絶対に一項目試験はできなくなり、通しで試験をするようになる

        private bool _UnitTestEnable;
        public bool UnitTestEnable
        {
            get { return _UnitTestEnable; }

            set
            {
                SetProperty(ref _UnitTestEnable, value);
            }
        }

        //単体試験チェックボックスがチェックされているかどうかの判定
        private bool? _CheckUnitTest = false;
        public bool? CheckUnitTest
        {
            get { return _CheckUnitTest; }
            set { SetProperty(ref _CheckUnitTest, value); }
        }

        //単体試験コンボボックスのアイテムソース
        private List<string> _UnitTestItems;
        public List<string> UnitTestItems
        {

            get { return _UnitTestItems; }
            set { SetProperty(ref _UnitTestItems, value); }

        }

        //単体試験コンボボックスの選択されたアイテム
        private string _UnitTestName;
        public string UnitTestName
        {

            get { return _UnitTestName; }
            set { SetProperty(ref _UnitTestName, value); }

        }

        //テストFW書き込みパス チェックボックスがチェックされているかどうかの判定
        private bool? _CheckWriteTestFwPass = false;
        public bool? CheckWriteTestFwPass
        {
            get { return _CheckWriteTestFwPass; }
            set { SetProperty(ref _CheckWriteTestFwPass, value); }
        }


        //作業者へのメッセージ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }



        //ステータス表示部■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

        private string _OkCount;
        public string OkCount
        {
            get { return _OkCount; }
            set { SetProperty(ref _OkCount, value); }
        }

        private string _NgCount;
        public string NgCount
        {
            get { return _NgCount; }
            set { SetProperty(ref _NgCount, value); }
        }


        private Brush _ColorMbed;
        public Brush ColorMbed
        {
            get { return _ColorMbed; }
            set { SetProperty(ref _ColorMbed, value); }
        }

        private Brush _ColorEpx64_1;
        public Brush ColorEpx64_1
        {
            get { return _ColorEpx64_1; }
            set { SetProperty(ref _ColorEpx64_1, value); }
        }

        private Brush _ColorEpx64_2;
        public Brush ColorEpx64_2
        {
            get { return _ColorEpx64_2; }
            set { SetProperty(ref _ColorEpx64_2, value); }
        }

        private Brush _Color232C;
        public Brush Color232C
        {
            get { return _Color232C; }
            set { SetProperty(ref _Color232C, value); }
        }

        private Brush _Color422;
        public Brush Color422
        {
            get { return _Color422; }
            set { SetProperty(ref _Color422, value); }
        }

    }
}
