using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Domain
{
    public class CustomerPayment:  BaseEntity
    {
       
        public int CustomerProfileId { get; set; }

        public int TransactionId { get; set; }

       
    }
}