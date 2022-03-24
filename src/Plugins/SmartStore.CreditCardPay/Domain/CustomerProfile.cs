﻿using SmartStore.Core;
using System;

namespace SmartStore.CreditCardPay.Domain
{
    public class CustomerProfile:  BaseEntity
    {
        public CustomerProfile()
        {

        }

        public int CustomerProfileId { get; set; } // Customer win Web site       

        public string HlCustomerProfileId { get; set; } // CUstomer id saved in Heartland

        public DateTime CreateDate { get; set; }

        public int  CreateByUser { get; set; }
       
    }
}