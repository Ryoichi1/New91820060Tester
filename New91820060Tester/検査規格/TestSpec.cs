
namespace New91820060Tester
{
    public class TestSpec
    {
        //テストスペックVER
        public string TestSpecVer { get; set; }

        public double Vol5vMax { get; set; }   // +5Vの上限の規格値
        public double Vol5vMin { get; set; }   // +5Vの下限の規格値

        public double PowOnResetAdMax { get; set; }
        public double PowOnResetAdMin { get; set; }

        //サーミスタ入力
        public double TempP60Max { get; set; }   
        public double TempP60Min { get; set; }   

        public double TempP20Max { get; set; }   
        public double TempP20Min { get; set; }   

        public double TempM20Max { get; set; }   
        public double TempM20Min { get; set; }   

        //圧力入力
        public double Press_50_Max { get; set; }   
        public double Press_50_Min { get; set; }   

        public double Press_100_Max { get; set; }   
        public double Press_100_Min { get; set; }   



    }
}
