

using SmartStore.Core.Configuration;

namespace CC.Plugins.Zoho
{
    public class ZohoSettings : ISettings
    {
        public ZohoSettings()
        {
           
            
        }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RefreshToken { get; set; }

        public string OauthToken { get; set; }

        public string OrganizationId { get; set; }

        public string AuthToken { get; set; }
    }
}
