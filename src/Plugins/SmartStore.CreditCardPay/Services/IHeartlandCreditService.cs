using SmartStore.CreditCardPay.Models;

namespace SmartStore.CreditCardPay.Services
{
    public interface IHeartlandCreditService
    {
        HlResponse VerifyCard(CreditCard card);

        HlResponse VerifyCard(CreditCard card, CardHolder cardHolder);

        HlResponse Charge(CreditCard card, string currency, decimal amount);

       // HlResponse Charge(CreditCardChargeDetail cardCharge);

        HlResponse Refund(string transactionId, decimal amount, string currency);

        HlResponse ReverseTransaction(string transactionId, decimal amount, string currency);

        HlResponse VoildTransaction(string transactionId);

        HlResponse EditTransaction(string transactionId, decimal newAmount, string currency);

    }
}