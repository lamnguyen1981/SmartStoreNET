using SmartStore.Core;
using System;

namespace SmartStore.CreditCardPay.Domain
{
    public class CustomerPayment:  BaseEntity
    {
        public CustomerPayment()
        {

        }

        public int CustomerProfileId { get; set; } // Customer win Web site

        public string TransactionId { get; set; }

        public string HlCustomerProfileId { get; set; } // CUstomer id saved in Heartland

        public DateTime CreateDate { get; set; }

        public string PaymentMethodType { get; set; }
    }
}