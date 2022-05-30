using Autofac;
using Autofac.Core;
using CC.Plugins.Tactic.Data;
using CC.Plugins.Core.Domain;
using SmartStore.Core.Data;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Data;
using SmartStore.Services;

namespace CC.Plugins.Tactic
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            //data layer
            //register named context
            builder.Register<IDbContext>(c => new TacticContext(DataSettings.Current.DataConnectionString))
                .Named<IDbContext>(TacticContext.ALIASKEY)
                
                .InstancePerRequest();

            //builder.Register<CreditCardPayContext>(c => new CreditCardPayContext(DataSettings.Current.DataConnectionString))
            //    .InstancePerRequest();
           

            builder.RegisterType<EfRepository<CCTactic>>()
               .As<IRepository<CCTactic>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>(TacticContext.ALIASKEY))
               .InstancePerRequest();

            builder.RegisterType<EfRepository<tbTacticID>>()
              .As<IRepository<tbTacticID>>()
              .WithParameter(ResolvedParameter.ForNamed<IDbContext>(TacticContext.ALIASKEY))
              .InstancePerRequest();
        }

        public int Order => 1;
    }
}
