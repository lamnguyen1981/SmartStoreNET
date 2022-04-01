using SmartStore.CreditCardPay.Models;
using System.Collections.Generic;

namespace SmartStore.CreditCardPay.Services
{
    public interface ICreditCardManagementService
    {
        int Charge(CreditCardChargeDetailRequest order);

        string AddPaymentMethod(HeartlandRequestBase request);

        string DeletePaymentMethod(string paymentProfileId);

        IList<PaymentMethodResponse> GetAllPaymentMethods(int customerId);

        IList<PaymentMethodResponse> SearchPaymentMethod(PaymentMethodSearchCondition criterial);
    }
}