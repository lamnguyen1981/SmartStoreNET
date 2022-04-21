using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class MarketFutureOffersResponse
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Print { get; set; }
        public int App { get; set; }
        public int Email { get; set; }
        public decimal Estimated { get; set; }


    }
}