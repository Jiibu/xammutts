//
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associateddocumentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY
//

using Xamarin.Forms;
using DevFish.Utils;

namespace DeviceInfoXamPCL
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
            IDeviceInfo deviceInfo = DependencyService.Get<IDeviceInfo>();
            Label_HW_Id.Text = deviceInfo.DeviceUniqueHardwareId;
            Label_FriendlyName.Text = deviceInfo.FriendlyName;
            Label_Manuf.Text = deviceInfo.Manuf;
            Label_Model.Text = deviceInfo.Model;
            Label_OS.Text = deviceInfo.OSName;
			Label_OSVersion.Text = deviceInfo.OSVersion;
        }
    }
}
