namespace SmartStore.CreditCardPay.Models
{
    public class PaymentMethodInfo
    {
        public string Cvv { get; set; }

        public int ExpMonth { get; set; }

        public int ExpYear { get; set; }

        public string Number { get; set; }

        public string Token { get; set; }

        public string CardAlias { get; set; }

        public string CardHolderName { get; set; }

        public string PaymentProfileId { get; set; }
    }
}