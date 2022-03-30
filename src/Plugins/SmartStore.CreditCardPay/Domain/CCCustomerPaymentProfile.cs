using SmartStore.Core;
using System;

namespace SmartStore.CreditCardPay.Domain
{
    public class CCCustomerPaymentProfile : BaseEntity
    {        

        public string CustomerPaymentProfileId { get; set; }

        public string CustomerPaymentProfileAlias { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int CreatedByUser { get; set; }

       
        public int CCustomerProfileId { get; set; }

        public CCCustomerProfile CCCustomerProfile {get ; set;}

    }
}