using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace CC.Plugins.Location
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("CC.Plugins.Location.Index",
                 "Plugin/Location/{action}",
                 new { controller = "Location", action = "Index" },
                 new[] { "CC.Plugins.Location.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Location.ListDetail",
                "Plugin/Location/{action}",
                new { controller = "Location", action = "ListDetail" },
                new[] { "CC.Plugins.Location.Controllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;
        }




        public int Priority => 0;
    }
}
