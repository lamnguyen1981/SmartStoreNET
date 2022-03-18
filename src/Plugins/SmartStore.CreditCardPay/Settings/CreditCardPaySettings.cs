using SmartStore.Core.Configuration;

namespace SmartStore.CreditCardPay
{
    public class CreditCardPaySettings : ISettings
    {
        public CreditCardPaySettings()
        {
            SecretKey = "skapi_cert_MW7dAQBF1V4AQaSsSwVpMQBOFZLA7ub2oSxnslAFKg";
        }

        public string PublicKey { get; set; }
        public string SecretKey { get; set; }        
    }
}
