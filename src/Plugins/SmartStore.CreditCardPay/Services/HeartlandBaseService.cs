using GlobalPayments.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public abstract class HeartlandBaseService
    {
        public  HeartlandBaseService(CreditCardPaySettings settings)
        {
            ServicesContainer.ConfigureService(new PorticoConfig
            {
                SecretApiKey = settings.SecretKey
            }); ;
        }
        
    }
}