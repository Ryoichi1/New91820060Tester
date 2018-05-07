using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Media;

namespace New91820060Tester
{

    public class ViewModelCommunication : BindableBase
    {
        //LPC1768
        private string _TX;
        public string TX
        {
            get { return _TX; }
            set { SetProperty(ref _TX, value); }
        }

        private string _RX;
        public string RX
        {
            get { return _RX; }
            set { SetProperty(ref _RX, value); }
        }

        private Brush _ColRs232c;
        public Brush ColRs232c { get { return _ColRs232c; } set { SetProperty(ref _ColRs232c, value); } }
        
        private Brush _ColRs422;
        public Brush ColRs422 { get { return _ColRs422; } set { SetProperty(ref _ColRs422, value); } }
    }
}
