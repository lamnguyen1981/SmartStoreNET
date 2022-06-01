using Autofac;
using Autofac.Core;
using CC.Plugins.Vehicle.Data;
using CC.Plugins.Core.Domain;
using SmartStore.Core.Data;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Data;
using SmartStore.Services;

namespace CC.Plugins.Vehicle
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            //data layer
            //register named context
            builder.Register<IDbContext>(c => new VehicleContext(DataSettings.Current.DataConnectionString))
                .Named<IDbContext>(VehicleContext.ALIASKEY)
                
                .InstancePerRequest();

            //builder.Register<CreditCardPayContext>(c => new CreditCardPayContext(DataSettings.Current.DataConnectionString))
            //    .InstancePerRequest();
           

            builder.RegisterType<EfRepository<CCVehicle>>()
               .As<IRepository<CCVehicle>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>(VehicleContext.ALIASKEY))
               .InstancePerRequest();

            builder.RegisterType<EfRepository<tbVehicleID>>()
              .As<IRepository<tbVehicleID>>()
              .WithParameter(ResolvedParameter.ForNamed<IDbContext>(VehicleContext.ALIASKEY))
              .InstancePerRequest();

            builder.RegisterType<EfRepository<CCProgram>>()
              .As<IRepository<CCProgram>>()
              .WithParameter(ResolvedParameter.ForNamed<IDbContext>(VehicleContext.ALIASKEY))
              .InstancePerRequest();
        }

        public int Order => 1;
    }
}
