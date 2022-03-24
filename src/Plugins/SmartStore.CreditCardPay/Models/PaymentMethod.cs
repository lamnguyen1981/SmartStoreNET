namespace SmartStore.CreditCardPay.Models
{
    public class PaymentMethod
    {
        private string _expireDate;

        public string CardHolderName { get; set; }        

        public string ExpireDate
        {
            get { return string.Format("{0}/{1}", _expireDate.Substring(0, 2), _expireDate.Substring(2)); }
            set { _expireDate = value; }
        }

        public string CardType { get; set; }

        public string CardMask { get; set; }
    }
}