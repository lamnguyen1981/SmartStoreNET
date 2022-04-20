using System;
using System.Data.Entity;
using SmartStore.Data;

namespace CC.Plugins.Location.Data
{

    /// <summary>
    /// Object context
    /// </summary>
    public class LocationContext : ObjectContextBase
    {
        public const string ALIASKEY = "sm_object_context_Location";

        

        /// <summary>
        /// For tooling support, e.g. EF Migrations
        /// </summary>
        public LocationContext()
            : base()
        {
        }

       
       // public DbSet<CustomerPaymentProfile> CustomerPaymentProfiles { get; set; }

        public LocationContext(string nameOrConnectionString)
            : base(nameOrConnectionString, ALIASKEY)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          //  modelBuilder.Entity<CustomerAddress>().ToTable("CCCustomerAddress");
            
           //  modelBuilder.Configurations.Add(new CustomerPaymentMapping());
           


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
            // Database.SetInitializer<LocationContext>(null);
            base.OnModelCreating(modelBuilder);
        }

    }
}
