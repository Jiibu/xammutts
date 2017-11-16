//
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associateddocumentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY
//

using System;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;
using Xamarin.Forms;

[assembly: Dependency(typeof(DevFish.Utils.UWP.DeviceInfo))]
namespace DevFish.Utils.UWP
{
    public class DeviceInfo : IDeviceInfo
    {
        private const string EAS_NOT_PROVISIONED = "Exchange Active Sync Client not provisioned";
        private EasClientDeviceInformation MyEasClientInfo { get; set; } = new EasClientDeviceInformation();

        public string DeviceUniqueHardwareId { get; private set;}
        public string FriendlyName { get; private set; }
        public string Manuf { get; private set; }
        public string Model { get; private set; }
        public string OSName { get; private set; }
        public string OSVersion { get; private set; }


        // static constructor
        public DeviceInfo()
        {
            DeviceUniqueHardwareId = GetId();
            var deviceInformation = new EasClientDeviceInformation();
            Model = deviceInformation.SystemProductName;
            Manuf = deviceInformation.SystemManufacturer;
            FriendlyName = deviceInformation.FriendlyName;
            OSName = deviceInformation.OperatingSystem;
            OSVersion = GetOSVersion();
        }

        // credit to peter torr - http://stackoverflow.com/questions/31746613/how-do-i-get-a-unique-identifier-for-a-device-within-windows-10-universal
        private static string GetId()
        {
            string retval = string.Empty;
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

            return retval;
        }

        private string GetOSVersion()
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
    }
}
