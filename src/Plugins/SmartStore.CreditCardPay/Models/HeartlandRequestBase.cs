using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Models
{
    public class HeartlandRequestBase
    {
        public int customerId { get; set; }

        public CustomerInfo CardHolder { get; set; }

        public PaymentMethodInfo Card { get; set; }        

        public bool IsSaveCard { get; set; }
    }
}