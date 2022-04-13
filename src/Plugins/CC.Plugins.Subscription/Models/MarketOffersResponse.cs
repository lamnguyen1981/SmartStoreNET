using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class MarketOffersResponse
    {
        public string Vehicle { get; set; }

        public string OfferCode { get; set; }

        public string OfferDescription { get; set; }

        public string DefaultAmount { get; set; }

        public string MarketRecommendation { get; set; }
    }
}