using SmartStore.CreditCardPay.Domain;
using System.Data.Entity.ModelConfiguration;

namespace SmartStore.Shipping.Mapping
{
    public partial class CustomerAddressMapping : EntityTypeConfiguration<CustomerAddress>
    {
        public CustomerAddressMapping()
        {
            this.ToTable("CCCustomerPaymentAddess");
            this.HasKey(x => x.Id);

            Property(c => c.Address).IsRequired().HasMaxLength(100);
            Property(c => c.City).IsRequired().HasMaxLength(100);
            Property(c => c.State).IsRequired().HasMaxLength(50);
            Property(c => c.Zip).IsRequired().HasMaxLength(50);
            //HasOptional(x => x.CustomerProfile);
            //this.HasM
        }
    }
}