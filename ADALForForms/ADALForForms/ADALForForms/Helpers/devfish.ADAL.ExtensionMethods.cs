using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Text;

using devfish.String;

namespace devfish.ADAL
{
    public static class ExtensionMethods
    {
        public static string ToDebugString(this Microsoft.IdentityModel.Clients.ActiveDirectory.UserInfo u)
        {
            if (u == null) return "-------- UserInfo==null ------------";

            //return ($"UserInfo: DisplayableId:{u.DisplayableId} FamilyName:{u.FamilyName} GivenName:{u.GivenName} IdentityProvider:{u.IdentityProvider}");
            StringBuilder sb = new StringBuilder(1024);
            sb.AppendLineIfValNotBlank(nameof(u.DisplayableId),u.DisplayableId);
            sb.AppendLineIfValNotBlank(nameof(u.FamilyName),u.FamilyName);
            sb.AppendLineIfValNotBlank(nameof(u.GivenName),u.GivenName);
            sb.AppendLineIfValNotBlank(nameof(u.IdentityProvider),u.IdentityProvider);
            return sb.ToString();
        }
        public static string ToDebugString(this AuthenticationResult r )
        {
            if ( r==null ) return "-------- AuthenticationResult==null ------------";

            StringBuilder sb = new StringBuilder(1024);
            sb.AppendLine("-------- AuthenticationResult begin ------------");  
            // sb.AppendLineIfValNotBlank(nameof(),);
            sb.AppendLineIfValNotBlank(nameof(r.AccessToken),r.AccessToken.TakeUpTo(30) +"...");
            sb.AppendLineIfValNotBlank(nameof(r.AccessTokenType), r.AccessToken.TakeUpTo(30) + "..." );
            sb.AppendLineIfValNotBlank(nameof(r.ExpiresOn),r.ExpiresOn.ToString());
            sb.AppendLineIfValNotBlank(nameof(r.ExtendedLifeTimeToken), r.ExtendedLifeTimeToken.ToString().TakeUpTo(30) + "...");
            sb.AppendLineIfValNotBlank(nameof(r.IdToken),r.IdToken.TakeUpTo(30) + "...");
            sb.AppendLineIfValNotBlank(nameof(r.TenantId),r.TenantId);
            sb.AppendLine("-------- embedded UserInfo  begin ------------");
            sb.AppendLineIfValNotBlank(nameof(r.UserInfo), r.UserInfo.ToDebugString());
            sb.AppendLine("-------- embedded UserInfo  end ------------");
            sb.AppendLine("-------- AuthenticationResult end ------------");
            return sb.ToString();
        }

        public static void AppendIfValNotBlank( this StringBuilder sb, string key, string val )
        {
            if (string.IsNullOrWhiteSpace(val)) return;
            sb.Append($"{key}=[{val}]");
        }
        public static void AppendLineIfValNotBlank(this StringBuilder sb, string key, string val)
        {
            if (string.IsNullOrWhiteSpace(val)) return;
            sb.AppendLine($"{key}=[{val}]");
        }
    }
}
