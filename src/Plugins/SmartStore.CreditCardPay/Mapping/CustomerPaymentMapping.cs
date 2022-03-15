using SmartStore.CreditCardPay.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SmartStore.Shipping.Mapping
{
    public partial class CustomerPaymentMapping : EntityTypeConfiguration<CustomerPayment>
    {
        public CustomerPaymentMapping()
        {
            this.ToTable("CCCustomerPayment");
            this.HasKey(x => x.Id);

            Property(c => c.TransactionId).IsRequired();
            //HasRequired(pc => pc.CustomerProfile)
            //    .WithMany()
            //    .HasForeignKey(pc => pc.CustomerProfileId);

            //this.HasM
        }
    }
}