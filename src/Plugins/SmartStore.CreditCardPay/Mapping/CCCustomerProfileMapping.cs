using SmartStore.CreditCardPay.Domain;
using System.Data.Entity.ModelConfiguration;

namespace SmartStore.Shipping.Mapping
{
    public partial class CCCustomerProfileMapping : EntityTypeConfiguration<CCCustomerProfile>
    {
        public CCCustomerProfileMapping()
        {
            this.ToTable("CCCustomerProfile");
            this.HasKey(x => x.Id);
            this.Property(x => x.CustomerId).IsRequired();
            this.Property(x => x.HlCustomerProfileId).IsRequired().HasMaxLength(100);
            this.Property(x => x.CreatedOnUtc).IsOptional();
            this.Property(x => x.CreateByUser).IsOptional();
           
            //HasOptional(p => p.MediaFile)
            //   .WithMany()
            //   .HasForeignKey(p => p.MediaFileId)
            //   .WillCascadeOnDelete(false);

        }
    }
}