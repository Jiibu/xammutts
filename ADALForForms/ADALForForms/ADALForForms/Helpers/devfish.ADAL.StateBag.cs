using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

namespace devfish.ADAL
{
    public static class StateBag
    {
        public static AuthenticationResult AuthResult { get; set; } = null;

        private static IADALHelper m_ADALHelper = null;
        public static IADALHelper ADALHelper
        {
            get
            {
                if (m_ADALHelper == null)
                {
                    m_ADALHelper = DependencyService.Get<IADALHelper>();
                }
                return m_ADALHelper;
            }
        }
    }
}
