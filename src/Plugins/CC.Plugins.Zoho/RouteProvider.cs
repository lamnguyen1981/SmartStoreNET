using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace CC.Plugins.Zoho
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("CC.Plugins.Zoho.Index",
                 "Plugin/Zoho/{action}",
                 new { controller = "Zoho", action = "Index" },
                 new[] { "CC.Plugins.Zoho.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Zoho.ListDetail",
                "Plugin/Zoho/{action}",
                new { controller = "Zoho", action = "ListDetail" },
                new[] { "CC.Plugins.Zoho.Controllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.Zoho.EditZoho",
               "Plugin/Zoho/{action}/{id}",
               new { controller = "Zoho", action = "EditZoho" },
               new[] { "CC.Plugins.Zoho.Controllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;
        }




        public int Priority => 0;
    }
}
