using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace CC.Plugins.Vehicle
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("CC.Plugins.Vehicle.Index",
                 "Plugin/Vehicle/{action}",
                 new { controller = "Vehicle", action = "Index" },
                 new[] { "CC.Plugins.Vehicle.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Vehicle.ListDetail",
                "Plugin/Vehicle/{action}",
                new { controller = "Vehicle", action = "ListDetail" },
                new[] { "CC.Plugins.Vehicle.Controllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Vehicle.EditVehicle",
               "Plugin/Vehicle/{action}/{id}",
               new { controller = "Vehicle", action = "EditVehicle" },
               new[] { "CC.Plugins.Vehicle.Controllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;
        }




        public int Priority => 0;
    }
}
