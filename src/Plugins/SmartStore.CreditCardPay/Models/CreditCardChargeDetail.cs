using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Models
{
    public class CreditCardChargeDetail
    {
        public CardHolder Holder { get; set; }

        public CreditCard Card { get; set; }

        public string OrderId { get; set; }

        public string HlCustomerId { get; set; }

        public bool isSaveCard { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public decimal WithConvenienceAmt { get; set; }

        public decimal WithShippingAmt { get; set; }

        public decimal WithSurchargeAmount { get; set; }

        public string PaymentLinkId { get; set; }        

    }
}