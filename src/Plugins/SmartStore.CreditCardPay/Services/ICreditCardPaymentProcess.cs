using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public interface ICreditCardPaymentProcess
    {
        int ProcessPayment(OrderDetail order);
    }
}