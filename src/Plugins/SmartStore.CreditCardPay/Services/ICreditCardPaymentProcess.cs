using SmartStore.CreditCardPay.Models;

namespace SmartStore.CreditCardPay.Services
{
    public interface ICreditCardPaymentProcess
    {
        int ProcessPayment(CreditCardChargeDetail order, int clientCustomerId);
    }
}