//
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associateddocumentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY
//

using System;

using D = System.Diagnostics.Debug;

#if WINDOWS_UWP
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;
#elif __IOS__
using UIKit;
#elif __DROID__
using Android.Bluetooth;
using Android.OS;
#else
#endif

// Conditional compilation constants
// ios > __IOS__
// droid > none defined by default.  
//         consuming project must add __DROID__ to "all configurations" compilation constant
// UWP > WINDOWS_UWP

namespace DevFish.Utils.Shared
{
    public class DeviceInfo
    {

        private string PlatformConditional { get; set; }

#if WINDOWS_UWP
        private const string EAS_NOT_PROVISIONED = "Exchange Active Sync Client not provisioned";
        private EasClientDeviceInformation MyEasClientInfo { get; set; } = new EasClientDeviceInformation();
#elif __IOS__
#elif __DROID__
#else
#endif

        public DeviceInfo()
        {
#if WINDOWS_UWP
            PlatformConditional = "WINDOWS_UWP";
#elif __IOS__
            PlatformConditional = "IOS";
#elif __DROID__
            PlatformConditional = "DROID";
#else
            PlatformConditional = "UNDEFINED PLATFORM CONDITIONAL";
#endif
            this.DeviceUniqueHardwareId = this.GetDeviceUniqueHardwareId();
            this.FriendlyName = this.GetFriendlyName();
            this.Manuf = this.GetManuf();
            this.Model = this.GetModel();
            this.OSName = this.GetOSName();
            this.OSVersion = this.GetOSVersion();
        }
        public string DeviceUniqueHardwareId { get; private set; }
        public string FriendlyName { get; private set; }
        public string Manuf { get; private set; }
        public string Model { get; private set; }
        public string OSName { get; private set; }
        public string OSVersion { get; private set; }

        private string GetDeviceUniqueHardwareId()
        {
            string retval = string.Empty;
            retval = $"UniqueID for {PlatformConditional}";
#if WINDOWS_UWP
            try
            { 
                // apparently this is already in xamarin forms
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.System.Profile.HardwareIdentification"))
                {
                    var token = HardwareIdentification.GetPackageSpecificToken(null);
                    var hardwareId = token.Id;
                    var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

                    byte[] bytes = new byte[hardwareId.Length];
                    dataReader.ReadBytes(bytes);

                    return BitConverter.ToString(bytes).Replace("-", "");
                }
                else
                {
                    retval = "Windows.System.Profile.HardwareIdentification API not present";
                }
            }
            catch ( Exception ex )
            {
                retval = ex.ToString();
            }
#elif __IOS__
            retval = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
#elif __DROID__
            if (IsEmulator())
                retval = "android emulator - no unique id";
            else
                retval = Android.OS.Build.Serial;
#endif
            return retval;
        }

        private string GetFriendlyName()
        {
            string retval = string.Empty;
            retval = $"FriendlyName for {PlatformConditional}";
#if WINDOWS_UWP
            retval = MyEasClientInfo.FriendlyName;
#elif __IOS__
            retval = UIDevice.CurrentDevice.Name.ToString();
#elif __DROID__
            try
            {
                BluetoothAdapter myDevice = BluetoothAdapter.DefaultAdapter;
                string deviceName = myDevice.Name;
                retval = deviceName;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                retval = "bluetooth error getting FriendlyName";
            }
#endif
            return retval;
        }

        private string GetManuf()
        {
            string retVal = string.Empty;
            retVal = $"Manuf for {PlatformConditional}";
#if WINDOWS_UWP
            retVal = MyEasClientInfo.SystemManufacturer;
#elif __IOS__
            retVal = "Apple";
#elif __DROID__
            retVal = Android.OS.Build.Manufacturer;
#endif
            return retVal;
        }

        private string GetModel()
        {
            string retval = string.Empty;
            retval = $"Model for {PlatformConditional}";
#if WINDOWS_UWP
            retval = MyEasClientInfo.SystemProductName;
#elif __IOS__
            retval = UIDevice.CurrentDevice.Model.ToString();
#elif __DROID__
            retval = Android.OS.Build.Model;
#endif
            return retval;
        }

        private string GetOSName()
        {
            string retVal = string.Empty;
            retVal = $"OSName for {PlatformConditional}";
#if WINDOWS_UWP
            retVal = MyEasClientInfo.OperatingSystem;
#elif __IOS__
            retVal = UIDevice.CurrentDevice.SystemName.ToString();
#elif __DROID__
            retVal = Android.OS.Build.Brand;
#endif
            return retVal;
        }
        private string GetOSVersion()
        {
            string retVal = string.Empty;
            retVal = $"OSVersion for {PlatformConditional}";
#if WINDOWS_UWP
            retVal = GetOSVersionUWP();
#elif __IOS__
            retVal = UIDevice.CurrentDevice.SystemVersion.ToString();
#elif __DROID__
            retVal = $"{Android.OS.Build.VERSION.Release} SDK{Android.OS.Build.VERSION.Sdk}";
#endif
            return retVal;
        }

#if __DROID__
        // http://stackoverflow.com/questions/37283184/xamarin-android-detect-emulator
        public bool IsEmulator()
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
#endif

#if WINDOWS_UWP
        private string GetOSVersionUWP()
        {
            string deviceFamilyVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            ulong version = ulong.Parse(deviceFamilyVersion);
            ulong major = (version & 0xFFFF000000000000L) >> 48;
            ulong minor = (version & 0x0000FFFF00000000L) >> 32;
            ulong build = (version & 0x00000000FFFF0000L) >> 16;
            ulong revision = (version & 0x000000000000FFFFL);
            string osVersion = $"{major}.{minor}.{build}.{revision}";
            return osVersion;
        }
#endif
    }
}
