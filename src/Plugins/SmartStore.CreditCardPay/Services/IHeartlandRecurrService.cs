using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public interface IHeartlandRecurrService
    {
        string AddCustomer(CardHolder holder);

        IEnumerable<PaymentMethod> GetAllPaymentMethods(string CustomerId);

        string AddPaymentMethod(string CustomerId, CreditCard card);


    }
}