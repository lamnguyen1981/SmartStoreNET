namespace SmartStore.CreditCardPay.Models
{
    public class PaymentMethodResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CardAlias { get; set; }

        private string _expireDate;

        public string CardHolderName { get; set; }        

        public string ExpireDate
        {
            get { return string.Format("{0}/{1}", _expireDate.Substring(0, 2), _expireDate.Substring(2)); }
            set { _expireDate = value; }
        }

        public string CardType { get; set; }

        public string CardMask { get; set; }

        public string PaymentProfileId { get; set; }
    }
}