using Autofac;
using Autofac.Core;
using CC.Plugins.Zoho.Services;
using SmartStore.Core.Data;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;

using SmartStore.Services;

namespace CC.Plugins.Zoho
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            builder.RegisterType<ZohoService>().As<IZohoService>().InstancePerRequest();
        }

        public int Order => 1;
    }
}
