using SmartStore.CreditCardPay.Models;

namespace SmartStore.CreditCardPay.Services
{
    public interface IHeartlandCreditService
    {
        HlServiceResponse VerifyCard(PaymentMethodInfo card);

        HlServiceResponse VerifyCard(PaymentMethodInfo card, CustomerInfo cardHolder);

        HlServiceResponse Charge(PaymentMethodInfo card, string currency, decimal amount);

       // HlServiceResponse Charge(CreditCardChargeDetailRequest cardCharge);

        HlServiceResponse Refund(string transactionId, decimal amount, string currency);

        HlServiceResponse ReverseTransaction(string transactionId, decimal amount, string currency);

        HlServiceResponse VoildTransaction(string transactionId);

        HlServiceResponse EditTransaction(string transactionId, decimal newAmount, string currency);

    }
}