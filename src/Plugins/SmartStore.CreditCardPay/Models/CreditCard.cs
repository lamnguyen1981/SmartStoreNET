﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Models
{
    public class CreditCard
    {
        public string Cvv { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Number { get; set; }
    }
}