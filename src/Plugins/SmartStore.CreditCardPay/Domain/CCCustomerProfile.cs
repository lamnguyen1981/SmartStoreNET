using SmartStore.Core;
using System;
using System.Collections.Generic;

namespace SmartStore.CreditCardPay.Domain
{
    public class CCCustomerProfile:  BaseEntity
    {        

        public int CustomerId { get; set; } // Customer win Web site       

        public string HlCustomerProfileId { get; set; } // CUstomer id saved in Heartland

        public DateTime CreatedOnUtc { get; set; }

        public int CreateByUser { get; set; }

       // public IList<CCCustomerPaymentProfile> CCCustomerPaymentProfiles { get; set; }


    }
}