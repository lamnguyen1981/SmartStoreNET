using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Models
{
    public class PaymentTransaction
    {
        public decimal? AdjustmentAmount { get; set; }

        public string AdjustmentCurrency { get; set; }
        public string AdjustmentReason { get; set; }       

        /// <summary>
        /// The originally requested authorization amount.
        /// </summary>
        /// 
        public decimal? Amount { get; set; }

        public string CardHolderFirstName { get; set; }

        public string CardHolderLastName { get; set; }

        public string CardHolderName { get; set; }

        public string CardType { get; set; }

        public string Currency { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerId { get; set; }

        public string CustomerLastName { get; set; }

        public string OrderId { get; set; }

        public DateTime? TransactionDate { get; set; }

        public string TransactionId { get; set; }
    }
}