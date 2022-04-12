using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class SalonSubscriptionResponse
    {
        public int Id { get; set; }
        public string SalonCode { get; set; }
        public string SalonName { get; set; }
        public string Email { get; set; }
        public int? P2N { get; set; }
        public int? N2B { get; set; }
        public int? B2G { get; set; }
    }
}