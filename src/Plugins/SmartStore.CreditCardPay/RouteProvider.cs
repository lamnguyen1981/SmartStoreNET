﻿using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.CreditCardPay
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.CreditCardPay.CardList",
                 "Plugin/CreditCardPay/{action}",
                 new { controller = "CreditCardPay", action = "CardList" },
                 new[] { "SmartStore.CreditCardPay.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("SmartStore.CreditCardPay.Configure",
                "Plugin/CreditCardPay/{action}",
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

            routes.MapRoute("SmartStore.CreditCardPay.CardMenuItem",
               "Plugin/CreditCardPay/{action}",
               new { controller = "CreditCardPay", action = "CardMenuItem" },
               new[] { "SmartStore.CreditCardPay.Controllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;


            routes.MapRoute("SmartStore.CustomerCreditCard.Indix",
                 "Plugin/CustomerCreditCard/{action}",
                 new { controller = "CustomerCreditCard", action = "LoadCustomers" },
                 new[] { "SmartStore.CreditCardPay.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("SmartStore.CustomerCreditCard.Charge",
                "Plugin/CustomerCreditCard/{action}/{id}",
                new { controller = "CustomerCreditCard", action = "Charge" },
                new[] { "SmartStore.CreditCardPay.Controllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;

           

            routes.MapRoute("SmartStore.CustomerCreditCard.DeleteCardid",
               "Plugin/CustomerCreditCard/{action}/{id}",
               new { controller = "CustomerCreditCard", action = "DeleteCard" },
               new[] { "SmartStore.CreditCardPay.Controllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;
        }




        public int Priority => 0;
    }
}
