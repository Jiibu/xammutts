using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ZXingPlay
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Scan_Clicked(object sender, EventArgs e)
        {
            // Label_ScanResult.Text = $"{DateTime.UtcNow.ToLocalTime().ToString()}";
            await DoScan();
        }

        private async Task DoScan()
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();

            if (result != null)
            {
                Label_ScanResult.Text = result.Text;
            }
            else
            {
                Label_ScanResult.Text = "no results returned";
            }
        }
    }
}
