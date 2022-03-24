using GlobalPayments.Api;

namespace SmartStore.CreditCardPay.Services
{
    public abstract class HeartlandBaseService
    {
        public  HeartlandBaseService(CreditCardPaySettings settings)
        {
            ServicesContainer.ConfigureService(new PorticoConfig
            {
                SecretApiKey = settings.HearlandSecretKey
            });
        }
        
    }
}