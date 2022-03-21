namespace SmartStore.CreditCardPay.Models
{
    public class HlResponse
    {
        public string AuthorizationCode { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseText { get; set; }

        public string TransactionId { get; set; }

        public string Token { get; set; }

        public string CardType { get; set; }

        public string HlCustomerId { get; set; } // Customer in Heartland system

        public string PaymentMethodType { get; set; }

        public string PaymentLinkId { get; set; }

    }
}