
namespace New91820060Tester
{
    public class TestSpec
    {
        //テストスペックVER
        public string TestSpecVer { get; set; }

        //電源電圧
        public double Vol5vMax { get; set; }   // +5Vの上限の規格値
        public double Vol5vMin { get; set; }   // +5Vの下限の規格値

        //P65 AD値チェック
        public double PowOnResetAdMax { get; set; }
        public double PowOnResetAdMin { get; set; }
        public double WdtResetAd { get; set; }

        //DA出力
        public double Dout116_50_Max { get; set; }   
        public double Dout116_50_Min { get; set; }   

        public double Dout116_100_Max { get; set; }   
        public double Dout116_100_Min { get; set; }   

        public double Dout117_50_Max { get; set; }   
        public double Dout117_50_Min { get; set; }   

        public double Dout117_100_Max { get; set; }   
        public double Dout117_100_Min { get; set; }   

        //サーミスタ入力
        public double TempP60Max { get; set; }   
        public double TempP60Min { get; set; }   

        public double TempP20Max { get; set; }   
        public double TempP20Min { get; set; }   

        public double TempM20Max { get; set; }   
        public double TempM20Min { get; set; }   

        //圧力入力
        public double Vol_Press_L_Max { get; set; }   
        public double Vol_Press_L_Min { get; set; }   

        public double Vol_Press_H_Max { get; set; }   
        public double Vol_Press_H_Min { get; set; }   


        public int Press_L_Max { get; set; }   
        public int Press_L_Min { get; set; }   

        public int Press_H_Max { get; set; }   
        public int Press_H_Min { get; set; }   

        //PT100調整
        public int Adjustment_100Ω_Max { get; set; }   
        public int Adjustment_100Ω_Min { get; set; }   
        public int Adjustment_130Ω_Max { get; set; }   
        public int Adjustment_130Ω_Min { get; set; }

        //PT100確認
        public double Temp0_Max { get; set; }   
        public double Temp0_Min { get; set; }   

        public double Temp77_66_Max { get; set; }   
        public double Temp77_66_Min { get; set; }   


    }
}
