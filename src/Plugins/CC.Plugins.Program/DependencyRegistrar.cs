using Autofac;
using Autofac.Core;
using CC.Plugins.Program.Data;
using CC.Plugins.Core.Domain;
using SmartStore.Core.Data;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Data;
using SmartStore.Services;

namespace CC.Plugins.Program
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            //data layer
            //register named context
            builder.Register<IDbContext>(c => new ProgramContext(DataSettings.Current.DataConnectionString))
                .Named<IDbContext>(ProgramContext.ALIASKEY)
                
                .InstancePerRequest();

            //builder.Register<CreditCardPayContext>(c => new CreditCardPayContext(DataSettings.Current.DataConnectionString))
            //    .InstancePerRequest();
           

            builder.RegisterType<EfRepository<CCProgram>>()
               .As<IRepository<CCProgram>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ProgramContext.ALIASKEY))
               .InstancePerRequest();

            builder.RegisterType<EfRepository<tbTacticID>>()
              .As<IRepository<tbTacticID>>()
              .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ProgramContext.ALIASKEY))
              .InstancePerRequest();
        }

        public int Order => 1;
    }
}
