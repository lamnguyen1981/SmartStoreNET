using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using SmartStore.CreditCardPay.Domain;
using SmartStore.Data;
using SmartStore.Data.Setup;
using SmartStore.Shipping.Mapping;

namespace SmartStore.CreditCardPay.Data
{

    /// <summary>
    /// Object context
    /// </summary>
    public class CreditCardPayContext : ObjectContextBase
    {
        public const string ALIASKEY = "sm_object_context_creditcardpay";

        

        /// <summary>
        /// For tooling support, e.g. EF Migrations
        /// </summary>
        public CreditCardPayContext()
            : base()
        {
        }

       
       // public DbSet<CustomerPaymentProfile> CustomerPaymentProfiles { get; set; }

        public CreditCardPayContext(string nameOrConnectionString)
            : base(nameOrConnectionString, ALIASKEY)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          //  modelBuilder.Entity<CustomerAddress>().ToTable("CCCustomerAddress");
            
           //  modelBuilder.Configurations.Add(new CustomerPaymentMapping());
            modelBuilder.Configurations.Add(new CCCustomerPaymentMapping());
            modelBuilder.Configurations.Add(new CCCustomerProfileMapping());


            //var typesToRegister = from t in Assembly.GetExecutingAssembly().GetTypes()
            //                      where t.Namespace.HasValue() &&
            //                            t.BaseType != null &&
            //                            t.BaseType.IsGenericType
            //                      let genericType = t.BaseType.GetGenericTypeDefinition()
            //                      where genericType == typeof(EntityTypeConfiguration<>) || genericType == typeof(ComplexTypeConfiguration<>)
            //                      select t;

            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.Configurations.Add(configurationInstance);
            //}

            //disable EdmMetadata generation
            // modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            // Database.SetInitializer<CreditCardPayContext>(null);
            base.OnModelCreating(modelBuilder);
        }

    }
}
