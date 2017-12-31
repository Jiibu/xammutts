using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using Xamarin.Forms;

using devfish.ADAL;

namespace ADALForForms
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
            Button_Login.Clicked += Button_Login_Clicked;
            Button_Logout.Clicked += Button_Logout_Clicked;
            Button_Cls.Clicked += Button_Cls_Clicked;
            msg("ADAL API Constants: " + APIConstants.Dump());
            msg("RedirUri: " + StateBag.ADALHelper.GetADALRedirUrl());
        }

        private void Button_Cls_Clicked(object sender, EventArgs e)
        {
            msg_cls();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Button_Login.Clicked -= Button_Login_Clicked;
            Button_Logout.Clicked -= Button_Logout_Clicked;
            Button_Cls.Clicked -= Button_Cls_Clicked;
        }

        private async void Button_Login_Clicked(object sender, EventArgs e)
        {
            try
            {
                // msg("chirp");
                AuthenticationResult authResult = await
                    StateBag.ADALHelper.AuthenticateAsync(
                    APIConstants.Authority,
                    APIConstants.GraphResourceUri,
                    APIConstants.ClientId,
                    StateBag.ADALHelper.GetADALRedirUrl()
                    );

                StateBag.AuthResult = authResult;
                msg(authResult.ToDebugString());
            }
            catch ( Exception ex )
            {
                string exstuff = ex.ToString();
                if (exstuff.Contains("authentication_ui_failed"))
                {
                    await DisplayAlert("authentication_ui_failed", "try changing your SSL/TLS Implementation from Default to Managed TLS 1.1 under Android Options > Advanced > SSL/TLS Implementation", "eeek");
                }
                System.Diagnostics.Debug.WriteLine("!!! " + ex.ToString());
                msg("exception on login: " + ex.ToString());
            }
        }
        private void Button_Logout_Clicked(object sender, EventArgs e)
        {
            StateBag.ADALHelper.Logout(APIConstants.Authority);
            StateBag.AuthResult = null;
            msg("logged out");
        }

        private void msg( string msg, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrEmpty(msg)) return;

            Label_Results.Text += $"{Environment.NewLine}{memberName}: {msg}";
        }
        private void msg_cls()
        {
            Label_Results.Text = "... cleared and ready ...";
        }
    }
}
