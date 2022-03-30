using SmartStore.CreditCardPay.Models;
using System.Collections.Generic;

namespace SmartStore.CreditCardPay.Services
{
    public interface ICreditCardManagementService
    {
       // int ProcessPayment(CreditCardChargeDetailRequest order, int clientCustomerId);

        string AddPaymentMethod(HeartlandRequestBase request);

        string DeletePaymentMethod(string paymentProfileId);

        IList<PaymentMethodResponse> GetAllPaymentMethods(int customerId);
    }
}