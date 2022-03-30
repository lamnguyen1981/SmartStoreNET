using Autofac;
using Autofac.Core;
using SmartStore.Core.Data;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Data;
using SmartStore.CreditCardPay.Data;
using SmartStore.CreditCardPay.Domain;
using SmartStore.CreditCardPay.Services;
using SmartStore.Services;

namespace SmartStore.CreditCardPay
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            builder.RegisterType<HeartlandReportService>().As<IHeartlandReportService>().InstancePerRequest();
            builder.RegisterType<CreditCardManagementService>().As<ICreditCardManagementService>().InstancePerRequest();
            builder.RegisterType<CreditCardPaySettings>().As<CreditCardPaySettings>().InstancePerRequest();
            builder.RegisterType<HeartlandRecurrService>().As<IHeartlandRecurrService>().InstancePerRequest();
            builder.RegisterType<CommonServices>().As<ICommonServices>().InstancePerRequest();

            //data layer
            //register named context
            builder.Register<IDbContext>(c => new CreditCardPayContext(DataSettings.Current.DataConnectionString))
                .Named<IDbContext>(CreditCardPayContext.ALIASKEY)
                .InstancePerRequest();

            builder.Register<CreditCardPayContext>(c => new CreditCardPayContext(DataSettings.Current.DataConnectionString))
                .InstancePerRequest();

            //override required repository with our custom context
            //builder.RegisterType<EfRepository<CustomerAddress>>()
            //    .As<IRepository<CustomerAddress>>()
            //    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CreditCardPayContext.ALIASKEY))
            //    .InstancePerRequest();

            //builder.RegisterType<EfRepository<CustomerProfile>>()
            //    .As<IRepository<CustomerProfile>>()
            //    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CreditCardPayContext.ALIASKEY))
            //    .InstancePerRequest();

            builder.RegisterType<EfRepository<CCCustomerPaymentProfile>>()
               .As<IRepository<CCCustomerPaymentProfile>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CreditCardPayContext.ALIASKEY))
               .InstancePerRequest();

            builder.RegisterType<EfRepository<CCCustomerProfile>>()
               .As<IRepository<CCCustomerProfile>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CreditCardPayContext.ALIASKEY))
               .InstancePerRequest();
        }

        public int Order => 1;
    }
}
