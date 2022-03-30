using GlobalPayments.Api.Entities;
using SmartStore.CreditCardPay.Models;
using System.Collections.Generic;

namespace SmartStore.CreditCardPay.Services
{
    public interface IHeartlandRecurrService
    {
        Customer AddCustomer(CustomerInfo holder);

        IList<PaymentMethodResponse> GetAllPaymentMethods(string CustomerId);

        string AddPaymentMethod(string CustomerId, PaymentMethodInfo card);

        string DeletePaymentMethod(string paymentProfileId);

        string AddPaymentMethod(CustomerInfo customer, PaymentMethodInfo card);

        IList<Customer> FindAll();

        HlServiceResponse Charge(CreditCardChargeDetailRequest cardCharge);

        string EditPaymentMethod(PaymentMethodInfo payment);


    }
}