namespace SmartStore.CreditCardPay.Models
{
    public class CreditCard
    {
        public string Cvv { get; set; }

        public int ExpMonth { get; set; }

        public int ExpYear { get; set; }

        public string Number { get; set; }

        public string Token { get; set; }

        public string CardAlias { get; set; }
    }
}