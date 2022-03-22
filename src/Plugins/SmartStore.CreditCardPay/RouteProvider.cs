﻿using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.CreditCardPay
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("CC.Plugins.ProductSubscription",
                 "Plugins/ProductSubscription/{action}",
                 new { controller = "ProductSubscription", action = "List" },
                 new[] { "CC.Plugins.ProductSubscription.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("SmartStore.CreditCardPay.ByTotal",
                 "Plugins/ShippingByTotal/{action}",
                 new { controller = "ByTotal", action = "Configure" },
                 new[] { "SmartStore.CreditCardPay.Controllers" }
            )
            .DataTokens["area"] = "SmartStore.CreditCardPay";

            routes.MapRoute("SmartStore.CreditCardPay.FixedRate",
                 "Plugins/FixedRate/{action}",
                 new { controller = "FixedRate", action = "Configure" },
                 new[] { "SmartStore.CreditCardPay.Controllers" }
            )
            .DataTokens["area"] = "SmartStore.CreditCardPay";
        }

        public int Priority => 0;
    }
}
