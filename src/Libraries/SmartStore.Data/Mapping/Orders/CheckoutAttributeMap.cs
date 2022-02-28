using System.Data.Entity.ModelConfiguration;
using SmartStore.Core.Domain.Orders;

namespace SmartStore.Data.Mapping.Orders
{
    public partial class CheckoutAttributeMap : EntityTypeConfiguration<CheckoutAttribute>
    {
        public CheckoutAttributeMap()
        {
            this.ToTable("SSCheckoutAttribute");
            this.HasKey(ca => ca.Id);
            this.Property(ca => ca.Name).IsRequired().HasMaxLength(400);

            this.Ignore(pva => pva.AttributeControlType);
        }
    }
}