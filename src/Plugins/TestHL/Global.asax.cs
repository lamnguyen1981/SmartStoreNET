using Autofac;
using Autofac.Integration.Mvc;
using SmartStore.Core.Infrastructure;
using SmartStore.CreditCardPay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestHL.Controllers;
using Autofac.Core;
using SmartStore.Core.Data;

using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.CreditCardPay;
using SmartStore.CreditCardPay.Data;
using SmartStore.CreditCardPay.Domain;

using SmartStore.Data;
using System.Data.Entity;

namespace TestHL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IContainer container { get; set; }
        protected void Application_Start()
        {
           // Database.SetInitializer<CustomerDBContext>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // var engine = EngineContext.Initialize(false);
            var builder = new ContainerBuilder();
            builder.RegisterType<CreditCardPaymentProcess>().As<ICreditCardPaymentProcess>().InstancePerRequest();
            builder.RegisterType<HeartlandReportService>().As<IHeartlandReportService>().InstancePerRequest();
            builder.RegisterType<CreditCardPaySettings>().As<CreditCardPaySettings>().InstancePerRequest();
            builder.RegisterType<HeartlandRecurrService>().As<IHeartlandRecurrService>().InstancePerRequest();
            builder.RegisterType<HeartlandCreditService>().As<IHeartlandCreditService>().InstancePerRequest();


            //data layer
            //register named context
          
          //  var cnnStr  = @" Data Source=localhost\SQLEXPRESS;Initial Catalog=smartstore_newtable1;Integrated Security=True;Persist Security Info=False;Enlist=False;Pooling=True;Min Pool Size=1;Max Pool Size=100;Connect Timeout=15;User Instance=False";
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

             builder.RegisterType<EfRepository<CustomerPaymentProfile>>()
                .As<IRepository<CustomerPaymentProfile>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CreditCardPayContext.ALIASKEY))
                .InstancePerRequest();
            //builder.RegisterInstance(HeartlandReportService)
            //    .As<IHeartlandReportService>()
            //    .SingleInstance();

            builder.RegisterType<HomeController>();
            container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
