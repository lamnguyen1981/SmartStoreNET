using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class Event
    {
        public string id { get; set; }

        public string title { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public int level { get; set; }

        public decimal total { get; set; }
    }
}