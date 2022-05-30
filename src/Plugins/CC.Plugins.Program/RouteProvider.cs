using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace CC.Plugins.Program
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("CC.Plugins.Program.Index",
                 "Plugin/Program/{action}",
                 new { controller = "Program", action = "Index" },
                 new[] { "CC.Plugins.Program.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Program.ListDetail",
                "Plugin/Program/{action}",
                new { controller = "Program", action = "ListDetail" },
                new[] { "CC.Plugins.Program.Controllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Program.EditProgram",
               "Plugin/Program/{action}/{id}",
               new { controller = "Program", action = "EditProgram" },
               new[] { "CC.Plugins.Program.Controllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;
        }




        public int Priority => 0;
    }
}
