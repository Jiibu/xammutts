using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ZXingPlay
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // jhealy: this MUST be a nav page, watch out
            // MainPage = new ZXingPlay.MainPage();

            MainPage = new NavigationPage(new ZXingPlay.MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
