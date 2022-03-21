using GlobalPayments.Api.Entities;
using SmartStore.CreditCardPay.Models;
using System.Collections.Generic;

namespace SmartStore.CreditCardPay.Services
{
    public interface IHeartlandRecurrService
    {
        string AddCustomer(CardHolder holder);

        IList<PaymentMethod> GetAllPaymentMethods(string CustomerId);

        string AddPaymentMethod(string CustomerId, CreditCard card);

        IList<Customer> FindAll();


    }
}