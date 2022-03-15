using SmartStore.Core.Configuration;

namespace SmartStore.CreditCardPay
{
    public class CreditCardPaySettings : ISettings
    {
        public CreditCardPaySettings()
        {
            SecretKey = "skapi_cert_MTyMAQBiHVEAewvIzXVFcmUd2UcyBge_eCpaASUp0A";
        }

        public string PublicKey { get; set; }
        public string SecretKey { get; set; }        
    }
}
