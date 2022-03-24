using SmartStore.CreditCardPay.Domain;
using System.Data.Entity.ModelConfiguration;

namespace SmartStore.Shipping.Mapping
{
    public partial class CustomerPaymentMapping : EntityTypeConfiguration<CustomerPaymentProfile>
    {
        public CustomerPaymentMapping()
        {
            this.ToTable("CCCustomerPaymentProfile");
            this.HasKey(x => x.Id);
            this.Property(x => x.CustomerPaymentProfileAlias).IsOptional().HasMaxLength(100);
            this.Property(x => x.CustomerPaymentProfileId).IsRequired().HasMaxLength(100);
            this.Property(x => x.CreateDate).IsOptional();
            this.Property(x => x.CreateByUser).IsOptional();
            this.Property(x => x.HlCustomerProfileId).IsOptional().HasMaxLength(100);
            // Property(c => c.TransactionId).IsRequired();
            //HasRequired(pc => pc.CustomerProfile)
            //    .WithMany()
            //    .HasForeignKey(pc => pc.CustomerProfileId);

            //this.HasM
        }
    }
}