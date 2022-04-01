using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Models
{
    public class PaymentMethodSearchCondition
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CardAlias { get; set; }        

        public string CardHolderName { get; set; }       

        public string CardType { get; set; }

        public string CardMask { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}