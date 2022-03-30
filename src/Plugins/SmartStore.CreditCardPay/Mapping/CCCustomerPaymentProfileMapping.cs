using SmartStore.CreditCardPay.Domain;
using System.Data.Entity.ModelConfiguration;

namespace SmartStore.Shipping.Mapping
{
    public partial class CCCustomerPaymentMapping : EntityTypeConfiguration<CCCustomerPaymentProfile>
    {
        public CCCustomerPaymentMapping()
        {
            this.ToTable("CCCustomerPaymentProfile");
            this.HasKey(x => x.Id);
            this.Property(x => x.CustomerPaymentProfileAlias).IsOptional().HasMaxLength(100);
            this.Property(x => x.CustomerPaymentProfileId).IsRequired().HasMaxLength(100);
            this.Property(x => x.CreatedByUser).IsOptional();
            this.Property(x => x.CreatedOnUtc).IsOptional();

            HasRequired(p => p.CCCustomerProfile)
              .WithMany()
              .HasForeignKey(p => p.CCustomerProfileId)
              .WillCascadeOnDelete(false);

           // HasRequired(pc => pc.CCCustomerProfile
                
                
               // .HasForeignKey(pc => pc.CustomerProfileId);

            //this.HasM
        }
    }
}