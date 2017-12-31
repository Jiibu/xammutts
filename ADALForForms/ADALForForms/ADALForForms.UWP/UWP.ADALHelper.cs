using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

using MacysRoom;
using System.Security.Claims;

[assembly: Xamarin.Forms.Dependency(typeof(MacysRoom.UWP.ADALHelper))]
namespace MacysRoom.UWP
{
    public class ADALHelper : devfish.ADAL.IADALHelper
    {
        public async Task<AuthenticationResult> AuthenticateAsync(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var uri = new System.Uri(returnUri);
            
            var platformParams = GetPlatformParameters();

            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
            return authResult;
        }

        private string m_ADALRedirectUrl = string.Empty;
        public string GetADALRedirUrl()
        {
            if (string.IsNullOrWhiteSpace(m_ADALRedirectUrl))
            {
                m_ADALRedirectUrl = Windows.Security.Authentication.Web.WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();
            }
            return m_ADALRedirectUrl;
        }

        public void Logout( string authority )
        {
            AuthenticationContext authContext = new AuthenticationContext(authority);
            var filter = new HttpBaseProtocolFilter();
            filter.ClearAuthenticationCache();
            authContext.TokenCache.Clear();
            HttpCookieCollection myCookies = filter.CookieManager.GetCookies(new System.Uri(authority));
            foreach (HttpCookie cookie in myCookies)
            {
                filter.CookieManager.DeleteCookie(cookie);
            }
        }

        public IPlatformParameters GetPlatformParameters()
        {
            return new PlatformParameters(PromptBehavior.Auto, false);
        }
    }
}
