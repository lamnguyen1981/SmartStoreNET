using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Models
{
    public class Token
    {
        public string TokenCode { get; set; }

        public string TokenMessage { get; set; }

        public string TokenValue { get; set; }
    }
}