using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;

namespace devfish.ADAL
{
    public class AuthenticationHelper
    {
        public static async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            // var uri = new Uri(returnUri);
            System.Uri uri = new System.Uri(returnUri);

            // var platformParams = new PlatformParameters();
            var platformParams = devfish.ADAL.StateBag.ADALHelper.GetPlatformParameters();

            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
            return authResult;
        }


        public async static void Login( string authority, string graphResourceUri, string clientId, string redirUrl )
        {
            AuthenticationResult data = await devfish.ADAL.AuthenticationHelper.Authenticate(
                authority,
                graphResourceUri,
                clientId,
                redirUrl);
        }
    }
}
