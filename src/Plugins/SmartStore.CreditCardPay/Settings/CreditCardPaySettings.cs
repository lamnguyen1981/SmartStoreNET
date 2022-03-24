using SmartStore.Core.Configuration;

namespace SmartStore.CreditCardPay
{
    public class CreditCardPaySettings : ISettings
    {
        public CreditCardPaySettings()
        {
            // SecretKey = "skapi_cert_MW7dAQBF1V4AQaSsSwVpMQBOFZLA7ub2oSxnslAFKg";
            HearlandSecretKey = "skapi_cert_MW7dAQBF1V4AQaSsSwVpMQBOFZLA7ub2oSxnslAFKg";
            
        }

        public string HearlandPublicKey { get; set; }
        public string HearlandSecretKey { get; set; }        
    }
}
