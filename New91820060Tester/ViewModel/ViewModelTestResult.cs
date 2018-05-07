using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Media;


namespace New91820060Tester
{
    public class ViewModelTestResult : BindableBase
    {
        //電源電圧 5V
        private string _Vol5v;
        public string Vol5v { get { return _Vol5v; } set { SetProperty(ref _Vol5v, value); } }

        private Brush _ColVol5v;
        public Brush ColVol5v { get { return _ColVol5v; } set { SetProperty(ref _ColVol5v, value); } }


        private string _VolDaout50_116;
        public string VolDaout50_116 { get { return _VolDaout50_116; } set { SetProperty(ref _VolDaout50_116, value); } }

        private Brush _ColVolDaout50_116;
        public Brush ColVolDaout50_116 { get { return _ColVolDaout50_116; } set { SetProperty(ref _ColVolDaout50_116, value); } }

        private string _VolDaout100_116;
        public string VolDaout100_116 { get { return _VolDaout100_116; } set { SetProperty(ref _VolDaout100_116, value); } }

        private Brush _ColVolDaout100_116;
        public Brush ColVolDaout100_116 { get { return _ColVolDaout100_116; } set { SetProperty(ref _ColVolDaout100_116, value); } }


        private string _VolDaout50_117;
        public string VolDaout50_117 { get { return _VolDaout50_117; } set { SetProperty(ref _VolDaout50_117, value); } }

        private Brush _ColVolDaout50_117;
        public Brush ColVolDaout50_117 { get { return _ColVolDaout50_117; } set { SetProperty(ref _ColVolDaout50_117, value); } }

        private string _VolDaout100_117;
        public string VolDaout100_117 { get { return _VolDaout100_117; } set { SetProperty(ref _VolDaout100_117, value); } }

        private Brush _ColVolDaout100_117;
        public Brush ColVolDaout100_117 { get { return _ColVolDaout100_117; } set { SetProperty(ref _ColVolDaout100_117, value); } }





        //アナログ検査 CN1
        private string _AnCn1_P60;
        public string AnCn1_P60 { get { return _AnCn1_P60; } set { SetProperty(ref _AnCn1_P60, value); } }

        private Brush _ColAnCn1_P60;
        public Brush ColANCn1_P60 { get { return _ColAnCn1_P60; } set { SetProperty(ref _ColAnCn1_P60, value); } }

        private string _AnCn1_P20;
        public string AnCn1_P20 { get { return _AnCn1_P20; } set { SetProperty(ref _AnCn1_P20, value); } }

        private Brush _ColAnCn1_P20;
        public Brush ColANCn1_P20 { get { return _ColAnCn1_P20; } set { SetProperty(ref _ColAnCn1_P20, value); } }

        private string _AnCn1_M20;
        public string AnCn1_M20 { get { return _AnCn1_M20; } set { SetProperty(ref _AnCn1_M20, value); } }

        private Brush _ColAnCn1_M20;
        public Brush ColANCn1_M20 { get { return _ColAnCn1_M20; } set { SetProperty(ref _ColAnCn1_M20, value); } }

        //アナログ検査 CN2
        private string _AnCn2_P60;
        public string AnCn2_P60 { get { return _AnCn2_P60; } set { SetProperty(ref _AnCn2_P60, value); } }

        private Brush _ColAnCn2_P60;
        public Brush ColANCn2_P60 { get { return _ColAnCn2_P60; } set { SetProperty(ref _ColAnCn2_P60, value); } }

        private string _AnCn2_P20;
        public string AnCn2_P20 { get { return _AnCn2_P20; } set { SetProperty(ref _AnCn2_P20, value); } }

        private Brush _ColAnCn2_P20;
        public Brush ColANCn2_P20 { get { return _ColAnCn2_P20; } set { SetProperty(ref _ColAnCn2_P20, value); } }

        private string _AnCn2_M20;
        public string AnCn2_M20 { get { return _AnCn2_M20; } set { SetProperty(ref _AnCn2_M20, value); } }

        private Brush _ColAnCn2_M20;
        public Brush ColANCn2_M20 { get { return _ColAnCn2_M20; } set { SetProperty(ref _ColAnCn2_M20, value); } }


        //アナログ検査 CN3
        private string _AnCn3_P60;
        public string AnCn3_P60 { get { return _AnCn3_P60; } set { SetProperty(ref _AnCn3_P60, value); } }

        private Brush _ColAnCn3_P60;
        public Brush ColANCn3_P60 { get { return _ColAnCn3_P60; } set { SetProperty(ref _ColAnCn3_P60, value); } }

        private string _AnCn3_P20;
        public string AnCn3_P20 { get { return _AnCn3_P20; } set { SetProperty(ref _AnCn3_P20, value); } }

        private Brush _ColAnCn3_P20;
        public Brush ColANCn3_P20 { get { return _ColAnCn3_P20; } set { SetProperty(ref _ColAnCn3_P20, value); } }

        private string _AnCn3_M20;
        public string AnCn3_M20 { get { return _AnCn3_M20; } set { SetProperty(ref _AnCn3_M20, value); } }

        private Brush _ColAnCn3_M20;
        public Brush ColANCn3_M20 { get { return _ColAnCn3_M20; } set { SetProperty(ref _ColAnCn3_M20, value); } }



        //デジタル出力 期待値
        private Brush _ExpU9_1;
        public Brush ExpU9_1 { get { return _ExpU9_1; } set { SetProperty(ref _ExpU9_1, value); } }

        private Brush _ExpU9_2;
        public Brush ExpU9_2 { get { return _ExpU9_2; } set { SetProperty(ref _ExpU9_2, value); } }

        private Brush _ExpU9_3;
        public Brush ExpU9_3 { get { return _ExpU9_3; } set { SetProperty(ref _ExpU9_3, value); } }

        private Brush _ExpU9_4;
        public Brush ExpU9_4 { get { return _ExpU9_4; } set { SetProperty(ref _ExpU9_4, value); } }

        private Brush _ExpU9_5;
        public Brush ExpU9_5 { get { return _ExpU9_5; } set { SetProperty(ref _ExpU9_5, value); } }

        private Brush _ExpU9_6;
        public Brush ExpU9_6 { get { return _ExpU9_6; } set { SetProperty(ref _ExpU9_6, value); } }

        private Brush _ExpU9_7;
        public Brush ExpU9_7 { get { return _ExpU9_7; } set { SetProperty(ref _ExpU9_7, value); } }

        private Brush _ExpU9_8;
        public Brush ExpU9_8 { get { return _ExpU9_8; } set { SetProperty(ref _ExpU9_8, value); } }

    /// ///////
        private Brush _ExpU10_1;
        public Brush ExpU10_1 { get { return _ExpU10_1; } set { SetProperty(ref _ExpU10_1, value); } }

        private Brush _ExpU10_2;
        public Brush ExpU10_2 { get { return _ExpU10_2; } set { SetProperty(ref _ExpU10_2, value); } }

        private Brush _ExpU10_3;
        public Brush ExpU10_3 { get { return _ExpU10_3; } set { SetProperty(ref _ExpU10_3, value); } }

        private Brush _ExpU10_4;
        public Brush ExpU10_4 { get { return _ExpU10_4; } set { SetProperty(ref _ExpU10_4, value); } }

        private Brush _ExpU10_5;
        public Brush ExpU10_5 { get { return _ExpU10_5; } set { SetProperty(ref _ExpU10_5, value); } }

        private Brush _ExpU10_6;
        public Brush ExpU10_6 { get { return _ExpU10_6; } set { SetProperty(ref _ExpU10_6, value); } }

        private Brush _ExpU10_7;
        public Brush ExpU10_7 { get { return _ExpU10_7; } set { SetProperty(ref _ExpU10_7, value); } }

        private Brush _ExpU10_8;
        public Brush ExpU10_8 { get { return _ExpU10_8; } set { SetProperty(ref _ExpU10_8, value); } }

        ////
        private Brush _ExpQ1;
        public Brush ExpQ1 { get { return _ExpQ1; } set { SetProperty(ref _ExpQ1, value); } }

        private Brush _ExpQ2;
        public Brush ExpQ2 { get { return _ExpQ2; } set { SetProperty(ref _ExpQ2, value); } }

        private Brush _ExpQ3;
        public Brush ExpQ3 { get { return _ExpQ3; } set { SetProperty(ref _ExpQ3, value); } }

       
        private Brush _ExpU11_3;
        public Brush ExpU11_3 { get { return _ExpU11_3; } set { SetProperty(ref _ExpU11_3, value); } }

        private Brush _ExpU11_4;
        public Brush ExpU11_4 { get { return _ExpU11_4; } set { SetProperty(ref _ExpU11_4, value); } }

        private Brush _ExpU11_5;
        public Brush ExpU11_5 { get { return _ExpU11_5; } set { SetProperty(ref _ExpU11_5, value); } }

        private Brush _ExpU11_6;
        public Brush ExpU11_6 { get { return _ExpU11_6; } set { SetProperty(ref _ExpU11_6, value); } }

        private Brush _ExpU11_7;
        public Brush ExpU11_7 { get { return _ExpU11_7; } set { SetProperty(ref _ExpU11_7, value); } }

        private Brush _ExpU11_8;
        public Brush ExpU11_8 { get { return _ExpU11_8; } set { SetProperty(ref _ExpU11_8, value); } }


        private Brush _ExpU209_1;
        public Brush ExpU209_1 { get { return _ExpU209_1; } set { SetProperty(ref _ExpU209_1, value); } }

        private Brush _ExpU209_2;
        public Brush ExpU209_2 { get { return _ExpU209_2; } set { SetProperty(ref _ExpU209_2, value); } }

        private Brush _ExpU209_3;
        public Brush ExpU209_3 { get { return _ExpU209_3; } set { SetProperty(ref _ExpU209_3, value); } }

        private Brush _ExpU209_4;
        public Brush ExpU209_4 { get { return _ExpU209_4; } set { SetProperty(ref _ExpU209_4, value); } }

        private Brush _ExpU209_5;
        public Brush ExpU209_5 { get { return _ExpU209_5; } set { SetProperty(ref _ExpU209_5, value); } }

        private Brush _ExpU209_6;
        public Brush ExpU209_6 { get { return _ExpU209_6; } set { SetProperty(ref _ExpU209_6, value); } }

        private Brush _ExpU209_7;
        public Brush ExpU209_7 { get { return _ExpU209_7; } set { SetProperty(ref _ExpU209_7, value); } }

        private Brush _ExpU209_8;
        public Brush ExpU209_8 { get { return _ExpU209_8; } set { SetProperty(ref _ExpU209_8, value); } }
        /// 
        private Brush _ExpU213a1;
        public Brush ExpU213a1 { get { return _ExpU213a1; } set { SetProperty(ref _ExpU213a1, value); } }

        private Brush _ExpU213a2;
        public Brush ExpU213a2 { get { return _ExpU213a2; } set { SetProperty(ref _ExpU213a2, value); } }

        private Brush _ExpU213a3;
        public Brush ExpU213a3 { get { return _ExpU213a3; } set { SetProperty(ref _ExpU213a3, value); } }

        private Brush _ExpU213b1;
        public Brush ExpU213b1 { get { return _ExpU213b1; } set { SetProperty(ref _ExpU213b1, value); } }

        private Brush _ExpU213b2;
        public Brush ExpU213b2 { get { return _ExpU213b2; } set { SetProperty(ref _ExpU213b2, value); } }

        private Brush _ExpU213b3;
        public Brush ExpU213b3 { get { return _ExpU213b3; } set { SetProperty(ref _ExpU213b3, value); } }

        private Brush _ExpU213c1;
        public Brush ExpU213c1 { get { return _ExpU213c1; } set { SetProperty(ref _ExpU213c1, value); } }

        private Brush _ExpU213c2;
        public Brush ExpU213c2 { get { return _ExpU213c2; } set { SetProperty(ref _ExpU213c2, value); } }

        private Brush _ExpU213c3;
        public Brush ExpU213c3 { get { return _ExpU213c3; } set { SetProperty(ref _ExpU213c3, value); } }

        private Brush _ExpU213d1;
        public Brush ExpU213d1 { get { return _ExpU213d1; } set { SetProperty(ref _ExpU213d1, value); } }

        private Brush _ExpU213d2;
        public Brush ExpU213d2 { get { return _ExpU213d2; } set { SetProperty(ref _ExpU213d2, value); } }

        private Brush _ExpU213d3;
        public Brush ExpU213d3 { get { return _ExpU213d3; } set { SetProperty(ref _ExpU213d3, value); } }

        private Brush _ExpU213e1;
        public Brush ExpU213e1 { get { return _ExpU213e1; } set { SetProperty(ref _ExpU213e1, value); } }

        private Brush _ExpU213e2;
        public Brush ExpU213e2 { get { return _ExpU213e2; } set { SetProperty(ref _ExpU213e2, value); } }

        private Brush _ExpU213e3;
        public Brush ExpU213e3 { get { return _ExpU213e3; } set { SetProperty(ref _ExpU213e3, value); } }

        private Brush _ExpU213f1;
        public Brush ExpU213f1 { get { return _ExpU213f1; } set { SetProperty(ref _ExpU213f1, value); } }

        private Brush _ExpU213f2;
        public Brush ExpU213f2 { get { return _ExpU213f2; } set { SetProperty(ref _ExpU213f2, value); } }

        private Brush _ExpU213f3;
        public Brush ExpU213f3 { get { return _ExpU213f3; } set { SetProperty(ref _ExpU213f3, value); } }


        /// 
        private Brush _ExpU214a1;
        public Brush ExpU214a1 { get { return _ExpU214a1; } set { SetProperty(ref _ExpU214a1, value); } }

        private Brush _ExpU214a2;
        public Brush ExpU214a2 { get { return _ExpU214a2; } set { SetProperty(ref _ExpU214a2, value); } }

        private Brush _ExpU214a3;
        public Brush ExpU214a3 { get { return _ExpU214a3; } set { SetProperty(ref _ExpU214a3, value); } }

        private Brush _ExpU214b1;
        public Brush ExpU214b1 { get { return _ExpU214b1; } set { SetProperty(ref _ExpU214b1, value); } }

        private Brush _ExpU214b2;
        public Brush ExpU214b2 { get { return _ExpU214b2; } set { SetProperty(ref _ExpU214b2, value); } }

        private Brush _ExpU214b3;
        public Brush ExpU214b3 { get { return _ExpU214b3; } set { SetProperty(ref _ExpU214b3, value); } }

        private Brush _ExpU214c;
        public Brush ExpU214c { get { return _ExpU214c; } set { SetProperty(ref _ExpU214c, value); } }

        private Brush _ExpU214d;
        public Brush ExpU214d { get { return _ExpU214d; } set { SetProperty(ref _ExpU214d, value); } }

        private Brush _ExpU214e;
        public Brush ExpU214e { get { return _ExpU214e; } set { SetProperty(ref _ExpU214e, value); } }

        private Brush _ExpU214f;
        public Brush ExpU214f { get { return _ExpU214f; } set { SetProperty(ref _ExpU214f, value); } }

        private Brush _ExpU215a;
        public Brush ExpU215a { get { return _ExpU215a; } set { SetProperty(ref _ExpU215a, value); } }

        private Brush _ExpU215b;
        public Brush ExpU215b { get { return _ExpU215b; } set { SetProperty(ref _ExpU215b, value); } }

        private Brush _ExpU215c;
        public Brush ExpU215c { get { return _ExpU215c; } set { SetProperty(ref _ExpU215c, value); } }

        private Brush _ExpU215d;
        public Brush ExpU215d { get { return _ExpU215d; } set { SetProperty(ref _ExpU215d, value); } }


        //デジタル出力 期待値
        private Brush _OutU9_1;
        public Brush OutU9_1 { get { return _OutU9_1; } set { SetProperty(ref _OutU9_1, value); } }

        private Brush _OutU9_2;
        public Brush OutU9_2 { get { return _OutU9_2; } set { SetProperty(ref _OutU9_2, value); } }

        private Brush _OutU9_3;
        public Brush OutU9_3 { get { return _OutU9_3; } set { SetProperty(ref _OutU9_3, value); } }

        private Brush _OutU9_4;
        public Brush OutU9_4 { get { return _OutU9_4; } set { SetProperty(ref _OutU9_4, value); } }

        private Brush _OutU9_5;
        public Brush OutU9_5 { get { return _OutU9_5; } set { SetProperty(ref _OutU9_5, value); } }

        private Brush _OutU9_6;
        public Brush OutU9_6 { get { return _OutU9_6; } set { SetProperty(ref _OutU9_6, value); } }

        private Brush _OutU9_7;
        public Brush OutU9_7 { get { return _OutU9_7; } set { SetProperty(ref _OutU9_7, value); } }

        private Brush _OutU9_8;
        public Brush OutU9_8 { get { return _OutU9_8; } set { SetProperty(ref _OutU9_8, value); } }

    /// ///////
        private Brush _OutU10_1;
        public Brush OutU10_1 { get { return _OutU10_1; } set { SetProperty(ref _OutU10_1, value); } }

        private Brush _OutU10_2;
        public Brush OutU10_2 { get { return _OutU10_2; } set { SetProperty(ref _OutU10_2, value); } }

        private Brush _OutU10_3;
        public Brush OutU10_3 { get { return _OutU10_3; } set { SetProperty(ref _OutU10_3, value); } }

        private Brush _OutU10_4;
        public Brush OutU10_4 { get { return _OutU10_4; } set { SetProperty(ref _OutU10_4, value); } }

        private Brush _OutU10_5;
        public Brush OutU10_5 { get { return _OutU10_5; } set { SetProperty(ref _OutU10_5, value); } }

        private Brush _OutU10_6;
        public Brush OutU10_6 { get { return _OutU10_6; } set { SetProperty(ref _OutU10_6, value); } }

        private Brush _OutU10_7;
        public Brush OutU10_7 { get { return _OutU10_7; } set { SetProperty(ref _OutU10_7, value); } }

        private Brush _OutU10_8;
        public Brush OutU10_8 { get { return _OutU10_8; } set { SetProperty(ref _OutU10_8, value); } }

        ////
        private Brush _OutQ1;
        public Brush OutQ1 { get { return _OutQ1; } set { SetProperty(ref _OutQ1, value); } }

        private Brush _OutQ2;
        public Brush OutQ2 { get { return _OutQ2; } set { SetProperty(ref _OutQ2, value); } }

        private Brush _OutQ3;
        public Brush OutQ3 { get { return _OutQ3; } set { SetProperty(ref _OutQ3, value); } }

       
        private Brush _OutU11_3;
        public Brush OutU11_3 { get { return _OutU11_3; } set { SetProperty(ref _OutU11_3, value); } }

        private Brush _OutU11_4;
        public Brush OutU11_4 { get { return _OutU11_4; } set { SetProperty(ref _OutU11_4, value); } }

        private Brush _OutU11_5;
        public Brush OutU11_5 { get { return _OutU11_5; } set { SetProperty(ref _OutU11_5, value); } }

        private Brush _OutU11_6;
        public Brush OutU11_6 { get { return _OutU11_6; } set { SetProperty(ref _OutU11_6, value); } }

        private Brush _OutU11_7;
        public Brush OutU11_7 { get { return _OutU11_7; } set { SetProperty(ref _OutU11_7, value); } }

        private Brush _OutU11_8;
        public Brush OutU11_8 { get { return _OutU11_8; } set { SetProperty(ref _OutU11_8, value); } }


        private Brush _OutU209_1;
        public Brush OutU209_1 { get { return _OutU209_1; } set { SetProperty(ref _OutU209_1, value); } }

        private Brush _OutU209_2;
        public Brush OutU209_2 { get { return _OutU209_2; } set { SetProperty(ref _OutU209_2, value); } }

        private Brush _OutU209_3;
        public Brush OutU209_3 { get { return _OutU209_3; } set { SetProperty(ref _OutU209_3, value); } }

        private Brush _OutU209_4;
        public Brush OutU209_4 { get { return _OutU209_4; } set { SetProperty(ref _OutU209_4, value); } }

        private Brush _OutU209_5;
        public Brush OutU209_5 { get { return _OutU209_5; } set { SetProperty(ref _OutU209_5, value); } }

        private Brush _OutU209_6;
        public Brush OutU209_6 { get { return _OutU209_6; } set { SetProperty(ref _OutU209_6, value); } }

        private Brush _OutU209_7;
        public Brush OutU209_7 { get { return _OutU209_7; } set { SetProperty(ref _OutU209_7, value); } }

        private Brush _OutU209_8;
        public Brush OutU209_8 { get { return _OutU209_8; } set { SetProperty(ref _OutU209_8, value); } }
        /// 
        private Brush _OutU213a1;
        public Brush OutU213a1 { get { return _OutU213a1; } set { SetProperty(ref _OutU213a1, value); } }

        private Brush _OutU213a2;
        public Brush OutU213a2 { get { return _OutU213a2; } set { SetProperty(ref _OutU213a2, value); } }

        private Brush _OutU213a3;
        public Brush OutU213a3 { get { return _OutU213a3; } set { SetProperty(ref _OutU213a3, value); } }

        private Brush _OutU213b1;
        public Brush OutU213b1 { get { return _OutU213b1; } set { SetProperty(ref _OutU213b1, value); } }

        private Brush _OutU213b2;
        public Brush OutU213b2 { get { return _OutU213b2; } set { SetProperty(ref _OutU213b2, value); } }

        private Brush _OutU213b3;
        public Brush OutU213b3 { get { return _OutU213b3; } set { SetProperty(ref _OutU213b3, value); } }

        private Brush _OutU213c1;
        public Brush OutU213c1 { get { return _OutU213c1; } set { SetProperty(ref _OutU213c1, value); } }

        private Brush _OutU213c2;
        public Brush OutU213c2 { get { return _OutU213c2; } set { SetProperty(ref _OutU213c2, value); } }

        private Brush _OutU213c3;
        public Brush OutU213c3 { get { return _OutU213c3; } set { SetProperty(ref _OutU213c3, value); } }

        private Brush _OutU213d1;
        public Brush OutU213d1 { get { return _OutU213d1; } set { SetProperty(ref _OutU213d1, value); } }

        private Brush _OutU213d2;
        public Brush OutU213d2 { get { return _OutU213d2; } set { SetProperty(ref _OutU213d2, value); } }

        private Brush _OutU213d3;
        public Brush OutU213d3 { get { return _OutU213d3; } set { SetProperty(ref _OutU213d3, value); } }

        private Brush _OutU213e1;
        public Brush OutU213e1 { get { return _OutU213e1; } set { SetProperty(ref _OutU213e1, value); } }

        private Brush _OutU213e2;
        public Brush OutU213e2 { get { return _OutU213e2; } set { SetProperty(ref _OutU213e2, value); } }

        private Brush _OutU213e3;
        public Brush OutU213e3 { get { return _OutU213e3; } set { SetProperty(ref _OutU213e3, value); } }

        private Brush _OutU213f1;
        public Brush OutU213f1 { get { return _OutU213f1; } set { SetProperty(ref _OutU213f1, value); } }

        private Brush _OutU213f2;
        public Brush OutU213f2 { get { return _OutU213f2; } set { SetProperty(ref _OutU213f2, value); } }

        private Brush _OutU213f3;
        public Brush OutU213f3 { get { return _OutU213f3; } set { SetProperty(ref _OutU213f3, value); } }


        /// 
        private Brush _OutU214a1;
        public Brush OutU214a1 { get { return _OutU214a1; } set { SetProperty(ref _OutU214a1, value); } }

        private Brush _OutU214a2;
        public Brush OutU214a2 { get { return _OutU214a2; } set { SetProperty(ref _OutU214a2, value); } }

        private Brush _OutU214a3;
        public Brush OutU214a3 { get { return _OutU214a3; } set { SetProperty(ref _OutU214a3, value); } }

        private Brush _OutU214b1;
        public Brush OutU214b1 { get { return _OutU214b1; } set { SetProperty(ref _OutU214b1, value); } }

        private Brush _OutU214b2;
        public Brush OutU214b2 { get { return _OutU214b2; } set { SetProperty(ref _OutU214b2, value); } }

        private Brush _OutU214b3;
        public Brush OutU214b3 { get { return _OutU214b3; } set { SetProperty(ref _OutU214b3, value); } }

        private Brush _OutU214c;
        public Brush OutU214c { get { return _OutU214c; } set { SetProperty(ref _OutU214c, value); } }

        private Brush _OutU214d;
        public Brush OutU214d { get { return _OutU214d; } set { SetProperty(ref _OutU214d, value); } }

        private Brush _OutU214e;
        public Brush OutU214e { get { return _OutU214e; } set { SetProperty(ref _OutU214e, value); } }

        private Brush _OutU214f;
        public Brush OutU214f { get { return _OutU214f; } set { SetProperty(ref _OutU214f, value); } }

        private Brush _OutU215a;
        public Brush OutU215a { get { return _OutU215a; } set { SetProperty(ref _OutU215a, value); } }

        private Brush _OutU215b;
        public Brush OutU215b { get { return _OutU215b; } set { SetProperty(ref _OutU215b, value); } }

        private Brush _OutU215c;
        public Brush OutU215c { get { return _OutU215c; } set { SetProperty(ref _OutU215c, value); } }

        private Brush _OutU215d;
        public Brush OutU215d { get { return _OutU215d; } set { SetProperty(ref _OutU215d, value); } }




    }

}








