using SmartStore.Core.Configuration;

namespace CC.Plugins.Subscription
{
    public class SubscriptionSettings : ISettings
    {
        public SubscriptionSettings()
        {
           
            
        }

        public string HearlandPublicKey { get; set; }
        public string HearlandSecretKey { get; set; }        
    }
}
