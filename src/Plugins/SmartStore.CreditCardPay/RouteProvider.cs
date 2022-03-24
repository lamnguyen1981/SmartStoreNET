using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.CreditCardPay
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.CreditCardPay.CardList",
                 "Plugin/SmartStore.CreditCardPay/{action}",
                 new { controller = "CreditCardPay", action = "CardList" },
                 new[] { "SmartStore.CreditCardPay.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("SmartStore.CreditCardPay.Configure",
                "Plugin/SmartStore.CreditCardPay/{action}",
                new { controller = "CreditCardPay", action = "Configure" },
                new[] { "SmartStore.CreditCardPay.Controllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("SmartStore.CreditCardPay.CardListDetail",
               "Plugin/CreditCardPay/{action}",
               new { controller = "CreditCardPay", action = "CardListDetail" },
               new[] { "SmartStore.CreditCardPay.Controllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;
        }




        public int Priority => 0;
    }
}
