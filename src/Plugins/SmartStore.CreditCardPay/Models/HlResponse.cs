﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Models
{
    public class HlResponse
    {
        public string AuthorizationCode { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseText { get; set; }

        public string TransactionId { get; set; }

        public Token Token { get; set; }
    }
}