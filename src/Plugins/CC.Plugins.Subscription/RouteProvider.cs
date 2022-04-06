using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace CC.Plugins.Subscription
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("CC.Plugins.Subscription.Index",
                 "Plugin/Subscription/{action}",
                 new { controller = "Subscription", action = "Index" },
                 new[] { "CC.Plugins.Subscription.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Subscription.ListDetail",
                "Plugin/Subscription/{action}",
                new { controller = "Subscription", action = "ListDetail" },
                new[] { "CC.Plugins.Subscription.Controllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;
        }




        public int Priority => 0;
    }
}
