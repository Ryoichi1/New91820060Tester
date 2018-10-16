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
        
        //P65 COLD AD値
        private string _ColdAd;
        public string ColdAd { get { return _ColdAd; } set { SetProperty(ref _ColdAd, value); } }

        private Brush _ColColdAd;
        public Brush ColColdAd { get { return _ColColdAd; } set { SetProperty(ref _ColColdAd, value); } }
        
        //P65 HOT AD値
        private string _HotAd;
        public string HotAd { get { return _HotAd; } set { SetProperty(ref _HotAd, value); } }

        private Brush _ColHotAd;
        public Brush ColHotAd { get { return _ColHotAd; } set { SetProperty(ref _ColHotAd, value); } }
        


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
        private string _Cn1_P60;
        public string Cn1_P60 { get { return _Cn1_P60; } set { SetProperty(ref _Cn1_P60, value); } }

        private Brush _ColCn1_P60;
        public Brush ColCn1_P60 { get { return _ColCn1_P60; } set { SetProperty(ref _ColCn1_P60, value); } }

        private string _Cn1_P20;
        public string Cn1_P20 { get { return _Cn1_P20; } set { SetProperty(ref _Cn1_P20, value); } }

        private Brush _ColCn1_P20;
        public Brush ColCn1_P20 { get { return _ColCn1_P20; } set { SetProperty(ref _ColCn1_P20, value); } }

        private string _Cn1_M20;
        public string Cn1_M20 { get { return _Cn1_M20; } set { SetProperty(ref _Cn1_M20, value); } }

        private Brush _ColCn1_M20;
        public Brush ColCn1_M20 { get { return _ColCn1_M20; } set { SetProperty(ref _ColCn1_M20, value); } }


        //アナログ検査 CN2
        private string _Cn2_P60;
        public string Cn2_P60 { get { return _Cn2_P60; } set { SetProperty(ref _Cn2_P60, value); } }

        private Brush _ColCn2_P60;
        public Brush ColCn2_P60 { get { return _ColCn2_P60; } set { SetProperty(ref _ColCn2_P60, value); } }

        private string _Cn2_P20;
        public string Cn2_P20 { get { return _Cn2_P20; } set { SetProperty(ref _Cn2_P20, value); } }

        private Brush _ColCn2_P20;
        public Brush ColCn2_P20 { get { return _ColCn2_P20; } set { SetProperty(ref _ColCn2_P20, value); } }

        private string _Cn2_M20;
        public string Cn2_M20 { get { return _Cn2_M20; } set { SetProperty(ref _Cn2_M20, value); } }

        private Brush _ColCn2_M20;
        public Brush ColCn2_M20 { get { return _ColCn2_M20; } set { SetProperty(ref _ColCn2_M20, value); } }


        //アナログ検査 CN3
        private string _Cn3_P60;
        public string Cn3_P60 { get { return _Cn3_P60; } set { SetProperty(ref _Cn3_P60, value); } }

        private Brush _ColCn3_P60;
        public Brush ColCn3_P60 { get { return _ColCn3_P60; } set { SetProperty(ref _ColCn3_P60, value); } }

        private string _Cn3_P20;
        public string Cn3_P20 { get { return _Cn3_P20; } set { SetProperty(ref _Cn3_P20, value); } }

        private Brush _ColCn3_P20;
        public Brush ColCn3_P20 { get { return _ColCn3_P20; } set { SetProperty(ref _ColCn3_P20, value); } }

        private string _Cn3_M20;
        public string Cn3_M20 { get { return _Cn3_M20; } set { SetProperty(ref _Cn3_M20, value); } }

        private Brush _ColCn3_M20;
        public Brush ColCn3_M20 { get { return _ColCn3_M20; } set { SetProperty(ref _ColCn3_M20, value); } }

        //アナログ検査 CN4
        private string _Cn4_P60;
        public string Cn4_P60 { get { return _Cn4_P60; } set { SetProperty(ref _Cn4_P60, value); } }

        private Brush _ColCn4_P60;
        public Brush ColCn4_P60 { get { return _ColCn4_P60; } set { SetProperty(ref _ColCn4_P60, value); } }

        private string _Cn4_P20;
        public string Cn4_P20 { get { return _Cn4_P20; } set { SetProperty(ref _Cn4_P20, value); } }

        private Brush _ColCn4_P20;
        public Brush ColCn4_P20 { get { return _ColCn4_P20; } set { SetProperty(ref _ColCn4_P20, value); } }

        private string _Cn4_M20;
        public string Cn4_M20 { get { return _Cn4_M20; } set { SetProperty(ref _Cn4_M20, value); } }

        private Brush _ColCn4_M20;
        public Brush ColCn4_M20 { get { return _ColCn4_M20; } set { SetProperty(ref _ColCn4_M20, value); } }


        //アナログ検査 CN5
        private string _Cn5_P60;
        public string Cn5_P60 { get { return _Cn5_P60; } set { SetProperty(ref _Cn5_P60, value); } }

        private Brush _ColCn5_P60;
        public Brush ColCn5_P60 { get { return _ColCn5_P60; } set { SetProperty(ref _ColCn5_P60, value); } }

        private string _Cn5_P20;
        public string Cn5_P20 { get { return _Cn5_P20; } set { SetProperty(ref _Cn5_P20, value); } }

        private Brush _ColCn5_P20;
        public Brush ColCn5_P20 { get { return _ColCn5_P20; } set { SetProperty(ref _ColCn5_P20, value); } }

        private string _Cn5_M20;
        public string Cn5_M20 { get { return _Cn5_M20; } set { SetProperty(ref _Cn5_M20, value); } }

        private Brush _ColCn5_M20;
        public Brush ColCn5_M20 { get { return _ColCn5_M20; } set { SetProperty(ref _ColCn5_M20, value); } }


        //アナログ検査 CN6
        private string _Cn6_P60;
        public string Cn6_P60 { get { return _Cn6_P60; } set { SetProperty(ref _Cn6_P60, value); } }

        private Brush _ColCn6_P60;
        public Brush ColCn6_P60 { get { return _ColCn6_P60; } set { SetProperty(ref _ColCn6_P60, value); } }

        private string _Cn6_P20;
        public string Cn6_P20 { get { return _Cn6_P20; } set { SetProperty(ref _Cn6_P20, value); } }

        private Brush _ColCn6_P20;
        public Brush ColCn6_P20 { get { return _ColCn6_P20; } set { SetProperty(ref _ColCn6_P20, value); } }

        private string _Cn6_M20;
        public string Cn6_M20 { get { return _Cn6_M20; } set { SetProperty(ref _Cn6_M20, value); } }

        private Brush _ColCn6_M20;
        public Brush ColCn6_M20 { get { return _ColCn6_M20; } set { SetProperty(ref _ColCn6_M20, value); } }


        //アナログ検査 CN7
        private string _Cn7_P60;
        public string Cn7_P60 { get { return _Cn7_P60; } set { SetProperty(ref _Cn7_P60, value); } }

        private Brush _ColCn7_P60;
        public Brush ColCn7_P60 { get { return _ColCn7_P60; } set { SetProperty(ref _ColCn7_P60, value); } }

        private string _Cn7_P20;
        public string Cn7_P20 { get { return _Cn7_P20; } set { SetProperty(ref _Cn7_P20, value); } }

        private Brush _ColCn7_P20;
        public Brush ColCn7_P20 { get { return _ColCn7_P20; } set { SetProperty(ref _ColCn7_P20, value); } }

        private string _Cn7_M20;
        public string Cn7_M20 { get { return _Cn7_M20; } set { SetProperty(ref _Cn7_M20, value); } }

        private Brush _ColCn7_M20;
        public Brush ColCn7_M20 { get { return _ColCn7_M20; } set { SetProperty(ref _ColCn7_M20, value); } }


        //アナログ検査 CN8
        private string _Cn8_P60;
        public string Cn8_P60 { get { return _Cn8_P60; } set { SetProperty(ref _Cn8_P60, value); } }

        private Brush _ColCn8_P60;
        public Brush ColCn8_P60 { get { return _ColCn8_P60; } set { SetProperty(ref _ColCn8_P60, value); } }

        private string _Cn8_P20;
        public string Cn8_P20 { get { return _Cn8_P20; } set { SetProperty(ref _Cn8_P20, value); } }

        private Brush _ColCn8_P20;
        public Brush ColCn8_P20 { get { return _ColCn8_P20; } set { SetProperty(ref _ColCn8_P20, value); } }

        private string _Cn8_M20;
        public string Cn8_M20 { get { return _Cn8_M20; } set { SetProperty(ref _Cn8_M20, value); } }

        private Brush _ColCn8_M20;
        public Brush ColCn8_M20 { get { return _ColCn8_M20; } set { SetProperty(ref _ColCn8_M20, value); } }


        //アナログ検査 CN9
        private string _Cn9_P60;
        public string Cn9_P60 { get { return _Cn9_P60; } set { SetProperty(ref _Cn9_P60, value); } }

        private Brush _ColCn9_P60;
        public Brush ColCn9_P60 { get { return _ColCn9_P60; } set { SetProperty(ref _ColCn9_P60, value); } }

        private string _Cn9_P20;
        public string Cn9_P20 { get { return _Cn9_P20; } set { SetProperty(ref _Cn9_P20, value); } }

        private Brush _ColCn9_P20;
        public Brush ColCn9_P20 { get { return _ColCn9_P20; } set { SetProperty(ref _ColCn9_P20, value); } }

        private string _Cn9_M20;
        public string Cn9_M20 { get { return _Cn9_M20; } set { SetProperty(ref _Cn9_M20, value); } }

        private Brush _ColCn9_M20;
        public Brush ColCn9_M20 { get { return _ColCn9_M20; } set { SetProperty(ref _ColCn9_M20, value); } }


        //アナログ検査 CN10
        private string _Cn10_P60;
        public string Cn10_P60 { get { return _Cn10_P60; } set { SetProperty(ref _Cn10_P60, value); } }

        private Brush _ColCn10_P60;
        public Brush ColCn10_P60 { get { return _ColCn10_P60; } set { SetProperty(ref _ColCn10_P60, value); } }

        private string _Cn10_P20;
        public string Cn10_P20 { get { return _Cn10_P20; } set { SetProperty(ref _Cn10_P20, value); } }

        private Brush _ColCn10_P20;
        public Brush ColCn10_P20 { get { return _ColCn10_P20; } set { SetProperty(ref _ColCn10_P20, value); } }

        private string _Cn10_M20;
        public string Cn10_M20 { get { return _Cn10_M20; } set { SetProperty(ref _Cn10_M20, value); } }

        private Brush _ColCn10_M20;
        public Brush ColCn10_M20 { get { return _ColCn10_M20; } set { SetProperty(ref _ColCn10_M20, value); } }

        //圧力入力 入力電圧
        private string _Cn11_Vol1;
        public string Cn11_Vol1 { get { return _Cn11_Vol1; } set { SetProperty(ref _Cn11_Vol1, value); } }

        private Brush _ColCn11_Vol1;
        public Brush ColCn11_Vol1 { get { return _ColCn11_Vol1; } set { SetProperty(ref _ColCn11_Vol1, value); } }

        private string _Cn11_Vol2;
        public string Cn11_Vol2 { get { return _Cn11_Vol2; } set { SetProperty(ref _Cn11_Vol2, value); } }

        private Brush _ColCn11_Vol2;
        public Brush ColCn11_Vol2 { get { return _ColCn11_Vol2; } set { SetProperty(ref _ColCn11_Vol2, value); } }

        private string _Cn12_Vol1;
        public string Cn12_Vol1 { get { return _Cn12_Vol1; } set { SetProperty(ref _Cn12_Vol1, value); } }

        private Brush _ColCn12_Vol1;
        public Brush ColCn12_Vol1 { get { return _ColCn12_Vol1; } set { SetProperty(ref _ColCn12_Vol1, value); } }

        private string _Cn12_Vol2;
        public string Cn12_Vol2 { get { return _Cn12_Vol2; } set { SetProperty(ref _Cn12_Vol2, value); } }

        private Brush _ColCn12_Vol2;
        public Brush ColCn12_Vol2 { get { return _ColCn12_Vol2; } set { SetProperty(ref _ColCn12_Vol2, value); } }

        //圧力入力 認識圧力値
        private string _Cn11_In1;
        public string Cn11_In1 { get { return _Cn11_In1; } set { SetProperty(ref _Cn11_In1, value); } }

        private Brush _ColCn11_In1;
        public Brush ColCn11_In1 { get { return _ColCn11_In1; } set { SetProperty(ref _ColCn11_In1, value); } }

        private string _Cn11_In2;
        public string Cn11_In2 { get { return _Cn11_In2; } set { SetProperty(ref _Cn11_In2, value); } }

        private Brush _ColCn11_In2;
        public Brush ColCn11_In2 { get { return _ColCn11_In2; } set { SetProperty(ref _ColCn11_In2, value); } }

        private string _Cn12_In1;
        public string Cn12_In1 { get { return _Cn12_In1; } set { SetProperty(ref _Cn12_In1, value); } }

        private Brush _ColCn12_In1;
        public Brush ColCn12_In1 { get { return _ColCn12_In1; } set { SetProperty(ref _ColCn12_In1, value); } }

        private string _Cn12_In2;
        public string Cn12_In2 { get { return _Cn12_In2; } set { SetProperty(ref _Cn12_In2, value); } }

        private Brush _ColCn12_In2;
        public Brush ColCn12_In2 { get { return _ColCn12_In2; } set { SetProperty(ref _ColCn12_In2, value); } }








        //PT100
        private string _Non_Cal_100;
        public string Non_Cal_100 { get { return _Non_Cal_100; } set { SetProperty(ref _Non_Cal_100, value); } }

        private Brush _ColNon_Cal_100;
        public Brush ColNon_Cal_100 { get { return _ColNon_Cal_100; } set { SetProperty(ref _ColNon_Cal_100, value); } }

        private string _Non_Cal_130;
        public string Non_Cal_130 { get { return _Non_Cal_130; } set { SetProperty(ref _Non_Cal_130, value); } }

        private Brush _ColNon_Cal_130;
        public Brush ColNon_Cal_130 { get { return _ColNon_Cal_130; } set { SetProperty(ref _ColNon_Cal_130, value); } }


        //PT100
        private string _Cal_100;
        public string Cal_100 { get { return _Cal_100; } set { SetProperty(ref _Cal_100, value); } }

        private Brush _ColCal_100;
        public Brush ColCal_100 { get { return _ColCal_100; } set { SetProperty(ref _ColCal_100, value); } }

        private string _Cal_130;
        public string Cal_130 { get { return _Cal_130; } set { SetProperty(ref _Cal_130, value); } }

        private Brush _ColCal_130;
        public Brush ColCal_130 { get { return _ColCal_130; } set { SetProperty(ref _ColCal_130, value); } }










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

        //デジタル入力 期待値
        private Brush _ExpU21e;
        public Brush ExpU21e { get { return _ExpU21e; } set { SetProperty(ref _ExpU21e, value); } }

        private Brush _ExpU21f;
        public Brush ExpU21f { get { return _ExpU21f; } set { SetProperty(ref _ExpU21f, value); } }

        //
        private Brush _ExpU22a;
        public Brush ExpU22a { get { return _ExpU22a; } set { SetProperty(ref _ExpU22a, value); } }

        private Brush _ExpU22b;
        public Brush ExpU22b { get { return _ExpU22b; } set { SetProperty(ref _ExpU22b, value); } }

        private Brush _ExpU22c;
        public Brush ExpU22c { get { return _ExpU22c; } set { SetProperty(ref _ExpU22c, value); } }

        private Brush _ExpU22d;
        public Brush ExpU22d { get { return _ExpU22d; } set { SetProperty(ref _ExpU22d, value); } }

        private Brush _ExpU22e;
        public Brush ExpU22e { get { return _ExpU22e; } set { SetProperty(ref _ExpU22e, value); } }

        private Brush _ExpU22f;
        public Brush ExpU22f { get { return _ExpU22f; } set { SetProperty(ref _ExpU22f, value); } }

        //
        private Brush _ExpU23a;
        public Brush ExpU23a { get { return _ExpU23a; } set { SetProperty(ref _ExpU23a, value); } }

        private Brush _ExpU23b;
        public Brush ExpU23b { get { return _ExpU23b; } set { SetProperty(ref _ExpU23b, value); } }

        private Brush _ExpU23c;
        public Brush ExpU23c { get { return _ExpU23c; } set { SetProperty(ref _ExpU23c, value); } }

        private Brush _ExpU23d;
        public Brush ExpU23d { get { return _ExpU23d; } set { SetProperty(ref _ExpU23d, value); } }

        private Brush _ExpU23e;
        public Brush ExpU23e { get { return _ExpU23e; } set { SetProperty(ref _ExpU23e, value); } }

        private Brush _ExpU23f;
        public Brush ExpU23f { get { return _ExpU23f; } set { SetProperty(ref _ExpU23f, value); } }

        //
        private Brush _ExpPC1;
        public Brush ExpPC1 { get { return _ExpPC1; } set { SetProperty(ref _ExpPC1, value); } }

        private Brush _ExpPC2;
        public Brush ExpPC2 { get { return _ExpPC2; } set { SetProperty(ref _ExpPC2, value); } }

        private Brush _ExpPC3;
        public Brush ExpPC3 { get { return _ExpPC3; } set { SetProperty(ref _ExpPC3, value); } }

        private Brush _ExpPC4;
        public Brush ExpPC4 { get { return _ExpPC4; } set { SetProperty(ref _ExpPC4, value); } }

        private Brush _ExpPC5;
        public Brush ExpPC5 { get { return _ExpPC5; } set { SetProperty(ref _ExpPC5, value); } }

        private Brush _ExpPC6;
        public Brush ExpPC6 { get { return _ExpPC6; } set { SetProperty(ref _ExpPC6, value); } }

        private Brush _ExpPC7;
        public Brush ExpPC7 { get { return _ExpPC7; } set { SetProperty(ref _ExpPC7, value); } }


        //デジタル入力 認識値
        private Brush _InputU21e;
        public Brush InputU21e { get { return _InputU21e; } set { SetProperty(ref _InputU21e, value); } }

        private Brush _InputU21f;
        public Brush InputU21f { get { return _InputU21f; } set { SetProperty(ref _InputU21f, value); } }

        //
        private Brush _InputU22a;
        public Brush InputU22a { get { return _InputU22a; } set { SetProperty(ref _InputU22a, value); } }

        private Brush _InputU22b;
        public Brush InputU22b { get { return _InputU22b; } set { SetProperty(ref _InputU22b, value); } }

        private Brush _InputU22c;
        public Brush InputU22c { get { return _InputU22c; } set { SetProperty(ref _InputU22c, value); } }

        private Brush _InputU22d;
        public Brush InputU22d { get { return _InputU22d; } set { SetProperty(ref _InputU22d, value); } }

        private Brush _InputU22e;
        public Brush InputU22e { get { return _InputU22e; } set { SetProperty(ref _InputU22e, value); } }

        private Brush _InputU22f;
        public Brush InputU22f { get { return _InputU22f; } set { SetProperty(ref _InputU22f, value); } }

        //
        private Brush _InputU23a;
        public Brush InputU23a { get { return _InputU23a; } set { SetProperty(ref _InputU23a, value); } }

        private Brush _InputU23b;
        public Brush InputU23b { get { return _InputU23b; } set { SetProperty(ref _InputU23b, value); } }

        private Brush _InputU23c;
        public Brush InputU23c { get { return _InputU23c; } set { SetProperty(ref _InputU23c, value); } }

        private Brush _InputU23d;
        public Brush InputU23d { get { return _InputU23d; } set { SetProperty(ref _InputU23d, value); } }

        private Brush _InputU23e;
        public Brush InputU23e { get { return _InputU23e; } set { SetProperty(ref _InputU23e, value); } }

        private Brush _InputU23f;
        public Brush InputU23f { get { return _InputU23f; } set { SetProperty(ref _InputU23f, value); } }

        //
        private Brush _InputPC1;
        public Brush InputPC1 { get { return _InputPC1; } set { SetProperty(ref _InputPC1, value); } }

        private Brush _InputPC2;
        public Brush InputPC2 { get { return _InputPC2; } set { SetProperty(ref _InputPC2, value); } }

        private Brush _InputPC3;
        public Brush InputPC3 { get { return _InputPC3; } set { SetProperty(ref _InputPC3, value); } }

        private Brush _InputPC4;
        public Brush InputPC4 { get { return _InputPC4; } set { SetProperty(ref _InputPC4, value); } }

        private Brush _InputPC5;
        public Brush InputPC5 { get { return _InputPC5; } set { SetProperty(ref _InputPC5, value); } }

        private Brush _InputPC6;
        public Brush InputPC6 { get { return _InputPC6; } set { SetProperty(ref _InputPC6, value); } }

        private Brush _InputPC7;
        public Brush InputPC7 { get { return _InputPC7; } set { SetProperty(ref _InputPC7, value); } }




    }

}








