using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class SubscriptionCalendarView
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IList<Event> Events { get; set; }
    }
}