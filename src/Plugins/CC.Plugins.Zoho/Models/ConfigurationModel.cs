using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;

namespace CC.Plugins.Zoho.Models
{
    public class ConfigurationModel : ModelBase
    {
        

        [SmartResourceDisplayName("Plugins.Zoho.ClientId")]
        public string ClientId { get; set; }

        [SmartResourceDisplayName("Plugins.Zoho.ClientSecret")]
        public string ClientSecret { get; set; }

        [SmartResourceDisplayName("Plugins.Zoho.RefreshToken")]
        public string RefreshToken { get; set; }

        [SmartResourceDisplayName("Plugins.Zoho.OauthToken")]
        public string OauthToken { get; set; }

        [SmartResourceDisplayName("Plugins.Zoho.OrganizationId")]
        public string OrganizationId { get; set; }

        [SmartResourceDisplayName("Plugins.Zoho.AuthToken")]
        public string AuthToken { get; set; }

    }
}