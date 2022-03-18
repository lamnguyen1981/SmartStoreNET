using SmartStore.CreditCardPay.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SmartStore.Shipping.Mapping
{
    public partial class CustomerProfileMapping : EntityTypeConfiguration<CustomerProfile>
    {
        public CustomerProfileMapping()
        {
            this.ToTable("CCCustomerProfile");
            this.HasKey(x => x.Id);

            Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            Property(c => c.LastName).IsRequired().HasMaxLength(100);
            Property(c => c.Email).IsRequired().HasMaxLength(50);
            Property(c => c.PhoneNumber).IsRequired().HasMaxLength(50);
            // HasRequired(x => x.CustomerAddress);
            HasRequired(s => s.CustomerAddress)
                .WithRequiredPrincipal(x => x.CustomerProfile);
                
            //this.HasM
        }
    }
}