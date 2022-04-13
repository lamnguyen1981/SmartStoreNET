using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class OffersResponse
    {
        public string Vehicle { get; set; }

        public string OfferCode { get; set; }

        public string OfferDescription { get; set; }

        public decimal MarketRecommendation { get; set; }

        public decimal SalonOverride { get; set; }
    }
}