//
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associateddocumentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY
//

using Android.Bluetooth;
using Android.OS;
using Android.Telephony;
using System;
using Xamarin.Forms;
using D = System.Diagnostics.Debug;

using DevFish.Utils;

[assembly: Dependency(typeof(DevFish.Utils.Droid.DeviceInfo))]
namespace DevFish.Utils.Droid
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

        // unfortunately this requires bluetooth capabilities
		private string GetFriendlyName()
		{
            string retval;
            try
            { 
                BluetoothAdapter myDevice = BluetoothAdapter.DefaultAdapter;
                string deviceName = myDevice.Name;
                retval = deviceName;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                retval = "bluetooth error getting FriendlyName";
            }
            return retval;
        }

		private string GetId()
		{
            if (isEmulator())
                return "emulator";
            else
                return Android.OS.Build.Serial;
        }

		private string GetManuf()
		{
            return Android.OS.Build.Manufacturer;
        }
		private string GetModel()
		{
            return Android.OS.Build.Model;
        }

		private string GetOSName()
		{
            return Android.OS.Build.Brand;
        }
		private string GetOSVersion()
		{
            return $"{Android.OS.Build.VERSION.Release} SDK{Android.OS.Build.VERSION.Sdk}";
        }

        // http://stackoverflow.com/questions/37283184/xamarin-android-detect-emulator
        public bool isEmulator()
        {
            string fing = Build.Fingerprint;
            bool isEmulator = false;

            D.WriteLine($"!!! Build.Fingerprint={fing}");

            if (fing != null)
            {
                isEmulator = fing.Contains("vbox") || fing.Contains("generic") || fing.Contains("vsemu");
            }

            return isEmulator;
        }
    }
}
