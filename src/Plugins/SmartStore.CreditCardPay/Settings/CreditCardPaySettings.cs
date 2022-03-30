using SmartStore.Core.Configuration;

namespace SmartStore.CreditCardPay
{
    public class CreditCardPaySettings : ISettings
    {
        public CreditCardPaySettings()
        {
           
            
        }

        public string HearlandPublicKey { get; set; }
        public string HearlandSecretKey { get; set; }        
    }
}
