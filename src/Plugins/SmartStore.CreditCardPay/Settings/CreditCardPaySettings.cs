using SmartStore.Core.Configuration;

namespace SmartStore.CreditCardPay
{
    public class CreditCardPaySettings : ISettings
    {
        public CreditCardPaySettings()
        {
            SecretKey = "";
        }

        public string PublicKey { get; set; }
        public string SecretKey { get; set; }        
    }
}
