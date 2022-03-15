using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;

namespace SmartStore.CreditCardPay.Models
{
    public class ConfigurationModel : ModelBase
    {
        [SmartResourceDisplayName("Plugins.Hearland.PublicKey")]
        public string HearlandPublicKey { get; set; }

        [SmartResourceDisplayName("Plugins.Hearland.SecretKey")]
        public string HearlandSecretKey { get; set; }
       
    }
}