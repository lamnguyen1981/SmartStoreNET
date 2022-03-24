using SmartStore.Core;
using System;

namespace SmartStore.CreditCardPay.Domain
{
    public class CustomerPaymentProfile :  BaseEntity
    {        

        public int CustomerProfileId { get; set; } // Customer win Web site       

        public string HlCustomerProfileId { get; set; } // CUstomer id saved in Heartland

        public string CustomerPaymentProfileId { get; set; }

        public string CustomerPaymentProfileAlias { get; set; }

        public DateTime CreateDate { get; set; }

        public int  CreateByUser { get; set; }
       
    }
}