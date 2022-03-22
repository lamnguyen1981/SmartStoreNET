using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.CreditCardPay
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("CC.Plugins.ProductSubscription",
                 "Plugin/CreditCardPay/{action}",
                 new { controller = "CreditCardPay", action = "CardList" },
                 new[] { "SmartStore.CreditCardPay.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;            
        }

        public int Priority => 0;
    }
}
