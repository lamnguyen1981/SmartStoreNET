using Autofac;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;

namespace CC.Plugins.Core
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            
        }

        public int Order => 1;
    }
}
