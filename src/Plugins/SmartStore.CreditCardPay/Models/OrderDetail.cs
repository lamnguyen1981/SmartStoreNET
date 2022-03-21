namespace SmartStore.CreditCardPay.Models
{
    public class OrderDetail
    {
        public CardHolder cardHolder { get; set; }

        public CreditCard card { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get;  set; }

        public string InvoiceNumber { get; set; }

        public bool isSaveCustomerInfor { get; set; }

        public bool useToken { get; set; }
    }
}