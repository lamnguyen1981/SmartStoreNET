using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Models
{
    public class PaymentMethod
    {
        public string CardHolderName { get; set; }

        public string ExpireDate { get; set; }

        public string CardType { get; set; }

        public string CardMask { get; set; }
    }
}