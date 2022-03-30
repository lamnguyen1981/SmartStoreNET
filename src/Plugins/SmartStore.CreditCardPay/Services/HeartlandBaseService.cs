using GlobalPayments.Api;
using SmartStore.Services;

namespace SmartStore.CreditCardPay.Services
{
    public abstract class HeartlandBaseService
    { 
        protected CreditCardPaySettings Settings { get; set; }

        public  HeartlandBaseService(
            ICommonServices _services)
                                    
        {
            var setting = _services.Settings.LoadSetting<CreditCardPaySettings>(_services.StoreContext.CurrentStore.Id);
            ServicesContainer.ConfigureService(new PorticoConfig
            {
                SecretApiKey = setting.HearlandSecretKey
            });
        }


        public  void SetupConfiguration(CreditCardPaySettings settings)
        {

        }
        
    }
}