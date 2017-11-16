using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HttpPlay
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new HttpPlay.MainPage();
            MainPage = new HttpPlay.DelegateHandlerPage();
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
