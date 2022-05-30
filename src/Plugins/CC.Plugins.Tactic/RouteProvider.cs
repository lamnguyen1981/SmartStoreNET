using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace CC.Plugins.Tactic
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("CC.Plugins.Tactic.Index",
                 "Plugin/Tactic/{action}",
                 new { controller = "Tactic", action = "Index" },
                 new[] { "CC.Plugins.Tactic.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Tactic.ListDetail",
                "Plugin/Tactic/{action}",
                new { controller = "Tactic", action = "ListDetail" },
                new[] { "CC.Plugins.Tactic.Controllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Tactic.EditTactic",
               "Plugin/Tactic/{action}/{id}",
               new { controller = "Tactic", action = "EditTactic" },
               new[] { "CC.Plugins.Tactic.Controllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;
        }




        public int Priority => 0;
    }
}
