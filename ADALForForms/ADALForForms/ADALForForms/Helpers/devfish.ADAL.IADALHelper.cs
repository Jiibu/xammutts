using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace devfish.ADAL
{
    public interface IADALHelper
    {
        Task<AuthenticationResult> AuthenticateAsync(string authority, string resource, string clientId, string returnUri);
        string GetADALRedirUrl();
        void Logout(string authority);
        Microsoft.IdentityModel.Clients.ActiveDirectory.IPlatformParameters GetPlatformParameters();
    }
}
