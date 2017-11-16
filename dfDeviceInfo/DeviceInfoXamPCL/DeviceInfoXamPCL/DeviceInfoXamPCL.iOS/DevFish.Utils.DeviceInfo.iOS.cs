//
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associateddocumentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY
//


using System;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DevFish.Utils.iOS.DeviceInfo))]
namespace DevFish.Utils.iOS
{
    public class DeviceInfo : IDeviceInfo
    {
        public string DeviceUniqueHardwareId { get; private set; }
        public string FriendlyName { get; private set; }
        public string Manuf { get; private set; }
        public string Model { get; private set; }
        public string OSName { get; private set; }
        public string OSVersion { get; private set; }

        // static constructor
        public DeviceInfo()
        {
			DeviceUniqueHardwareId = GetId();
			Model = GetModel();
			Manuf = GetManuf();
			FriendlyName = GetFriendlyName();
			OSName = GetOSName();
			OSVersion = GetOSVersion();
        }

		private string GetFriendlyName()
		{
			return UIDevice.CurrentDevice.Name.ToString();
		}
		private string GetId()
		{
			return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
		}
		private string GetManuf()
		{
			return "Apple";
		}
		private string GetModel()
		{
			return UIDevice.CurrentDevice.Model.ToString();
		}

		private string GetOSName()
		{
			return UIDevice.CurrentDevice.SystemName.ToString();
		}
		private string GetOSVersion()
		{
			return UIDevice.CurrentDevice.SystemVersion.ToString();
		}
    }
}
