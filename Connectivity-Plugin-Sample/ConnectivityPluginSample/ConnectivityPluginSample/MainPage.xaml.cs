using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConnectivityPluginSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Button_IsReachable.Clicked += Button_IsReachable_Clicked;
            Button_IsRemoteReachable.Clicked += Button_IsRemoteReachable_Clicked;
            Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            UpdateStatus();
        }

        private async void  Button_IsRemoteReachable_Clicked(object sender, EventArgs e)
        {
            Label_IsRemoteReachableStatus.Text = "querying status - 5s timeout";
            bool isRemoteReachable = await Plugin.Connectivity.CrossConnectivity.Current.IsRemoteReachable(Entry_RemoteReachableHost.Text, 5000);
            Label_IsRemoteReachableStatus.Text = isRemoteReachable.ToString();
        }

        private async void Button_IsReachable_Clicked(object sender, EventArgs e)
        {
            Label_IsReachableStatus.Text = "querying status - 5s timeout";
            bool isReachable = await Plugin.Connectivity.CrossConnectivity.Current.IsReachable(Entry_ReachableHost.Text.Trim(), 5000);
            Label_IsReachableStatus.Text = isReachable.ToString();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Button_IsReachable.Clicked -= Button_IsReachable_Clicked;
            Button_IsReachable.Clicked -= Button_IsReachable_Clicked;
            Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged -= Current_ConnectivityChanged;
        }

        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            Plugin.Connectivity.Abstractions.IConnectivity plugin = Plugin.Connectivity.CrossConnectivity.Current;
            Label_IsConnected.Text = plugin.IsConnected.ToString();

            string connectionTypes = string.Empty;
            foreach( Plugin.Connectivity.Abstractions.ConnectionType ct in plugin.ConnectionTypes )
            {
                if (!string.IsNullOrWhiteSpace(connectionTypes)) connectionTypes += ", ";
                connectionTypes += ct.ToString();
            }
            Label_ConnectionTypes.Text = connectionTypes.ToString();

            string bandwidths = string.Empty;
            foreach (ulong bandwidth in plugin.Bandwidths )
            {
                if (!string.IsNullOrWhiteSpace(bandwidths)) bandwidths += ", ";
                bandwidths += bandwidth.ToString();
            }
            Label_Bandwidths.Text = bandwidths.ToString();
        }
    }
}
