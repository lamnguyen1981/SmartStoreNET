namespace SmartStore.CreditCardPay.Models
{
    public class CreditCardChargeDetailRequest : HeartlandRequestBase
    {       

        public string OrderId { get; set; }         
           
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public decimal WithConvenienceAmt { get; set; }

        public decimal WithShippingAmt { get; set; }

        public decimal WithSurchargeAmount { get; set; }
                     

    }
}