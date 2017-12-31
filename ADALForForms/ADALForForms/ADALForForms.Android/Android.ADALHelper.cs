using Android.App;
using Android.Webkit;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

using ADALForForms;

[assembly: Xamarin.Forms.Dependency(typeof(ADALForForms.Droid.ADALHelper))]
namespace ADALForForms.Droid
{
    public class ADALHelper : devfish.ADAL.IADALHelper
    {
        public async Task<AuthenticationResult> AuthenticateAsync(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var platformParams = new PlatformParameters((Activity)Forms.Context);

            var authResult = await authContext.AcquireTokenAsync(
                resource, 
                clientId,
                new System.Uri(returnUri), 
                platformParams);

            return authResult;
        }

        public static MainActivity MyMainActivity { get; set; } = null;
        public static void Init( MainActivity myMainActivity )
        {
            MyMainActivity = myMainActivity;
        }

        public IPlatformParameters GetPlatformParameters()
        {
            if ( MyMainActivity==null )
            {
                throw new Exception("Android.ADALHelper.GetPlatformParameters.  MyMainActivity is null.");
            }
            return new PlatformParameters(MyMainActivity);
        }
        private string m_RedirUrl { get; set; } = @"https://pleasefix";
        public System.Uri GetRedirectUri()
        {;
            return new Uri(m_RedirUrl);
        }

        public string GetADALRedirUrl()
        {
            return m_RedirUrl;
        }

        public string GetWebAPIRedirUrl()
        {
            return m_RedirUrl;
        }

        public void Logout(string authority)
        {
            AuthenticationContext authContext = new AuthenticationContext(authority);
            authContext.TokenCache.Clear();
            CookieManager.Instance.RemoveAllCookie();
        }
    }
}
