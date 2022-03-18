using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Domain
{
    public class CustomerAddress : BaseEntity
    {
        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public CustomerProfile CustomerProfile { get; set; }
    }
}