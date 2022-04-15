﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class Order
    {
        public DateTime Date { get; set; }
        public int Level { get; set; }
        public decimal Price { get; set; }
        public string Offer  { get; set; }
    }
}