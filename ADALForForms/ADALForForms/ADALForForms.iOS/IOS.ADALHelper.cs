using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using ADALForForms;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ADALForForms.iOS.ADALHelper))]
namespace ADALForForms.iOS
{
    public class ADALHelper : devfish.ADAL.IADALHelper
    {
        public async Task<AuthenticationResult> AuthenticateAsync(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            // jhealy: maybe platformparams here is what's wrong with other one?
            var platformParams = GetPlatformParameters();

            var authResult = await authContext.AcquireTokenAsync(
                resource, 
                clientId,
                new System.Uri(returnUri), 
                platformParams);

            return authResult;
        }

        public IPlatformParameters GetPlatformParameters()
        {
            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            return new PlatformParameters(controller);
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
            authContext.TokenCache.Clear();
            Foundation.NSHttpCookieStorage cookieStorage = Foundation.NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in cookieStorage.Cookies)
            {
                cookieStorage.DeleteCookie(cookie);
            }
        }
    }
}
