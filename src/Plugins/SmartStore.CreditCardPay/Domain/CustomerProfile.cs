using SmartStore.Core;

namespace SmartStore.CreditCardPay.Domain
{
    public class CustomerProfile : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        
       // [ForeignKey("CustomerAddress")]
        public int AddressId { get; set; }

        public  CustomerAddress CustomerAddress { get; set; }

       // public ICollection<CustomerPayment> CustomerPayments { get; set; }
    }
}