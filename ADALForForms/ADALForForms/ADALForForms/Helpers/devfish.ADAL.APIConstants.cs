using System;
using System.Globalization;
using System.Text;

namespace devfish.ADAL
{
    public static class APIConstants
    {
        public static string Tenant { get; } = "M365x947151.onmicrosoft.com";
        public static string AADInstance { get; } = @"https://login.microsoftonline.com/{0}";
        public static string Authority { get; } =
            string.Format(CultureInfo.InvariantCulture, AADInstance, Tenant);
        public static string ClientId { get; } = @"8f580ff3-2ab3-469b-bd43-158c581757da";

        //Graph URI 
        public static string GraphResourceUri { get; } = @"https://graph.windows.net";
        public static string GraphApiVersion { get; } = @"2013-11-08";

        public static string Dump()
        {
            StringBuilder sb = new StringBuilder(1024);
            // sb.AppendLine($"{nameof()}: {}");
            sb.AppendLine($"{nameof(AADInstance)}: {AADInstance}");
            sb.AppendLine($"{nameof(Authority)}: {Authority}");
            sb.AppendLine("RedirectUri: PLEASE USE THE IADALHelper dependency service to get RedirUrl strings");
            sb.AppendLine($"{nameof(Tenant)}: {Tenant}");
            sb.AppendLine($"{nameof(GraphResourceUri)}: {GraphResourceUri}");
            sb.AppendLine($"{nameof(GraphApiVersion)}: {GraphApiVersion}");
            return sb.ToString();
        }
    }
}
