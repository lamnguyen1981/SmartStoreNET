using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class Order
    {
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status  { get; set; }
    }
}