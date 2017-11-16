using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HttpPlay
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DelegateHandlerPage : ContentPage
    {
        public DelegateHandlerPage()
        {
            InitializeComponent();
        }

        private HttpClient m_myHttpClient;
        private HttpClient MyHttpClient
        {
            get
            {
                if ( m_myHttpClient==null)
                {
                    m_myHttpClient = new HttpClient(new MyTraceHandler());
                }
                return m_myHttpClient;
            }
        }
        private async void Button_DoIt_Clicked(object sender, EventArgs e)
        {
            string data;
            try
            {
                data = await MyHttpClient.GetStringAsync(Entry_URL.Text);
            }
            catch ( Exception ex )
            {
                await this.DisplayAlert("Exception calling httpclient", ex.ToString(), "ow");
                data = ex.ToString();
            }
            
            Label_Result.Text = String.IsNullOrWhiteSpace(data) ? $"{DateTime.UtcNow.ToString()} - no data returned" : data.Trim();
        }
    }
}