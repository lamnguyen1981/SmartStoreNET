using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace CC.Plugins.Subscription
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("CC.Plugins.SubscriptionCardList",
                 "Plugin/Subscription/{action}",
                 new { controller = "Subscription", action = "CardList" },
                 new[] { "CC.Plugins.SubscriptionControllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.SubscriptionConfigure",
                "Plugin/Subscription/{action}",
                new { controller = "Subscription", action = "Configure" },
                new[] { "CC.Plugins.SubscriptionControllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.SubscriptionCardListDetail",
               "Plugin/Subscription/{action}",
               new { controller = "Subscription", action = "CardListDetail" },
               new[] { "CC.Plugins.SubscriptionControllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("CC.Plugins.SubscriptionCardMenuItem",
               "Plugin/Subscription/{action}",
               new { controller = "Subscription", action = "CardMenuItem" },
               new[] { "CC.Plugins.SubscriptionControllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;


            routes.MapRoute("SmartStore.CustomerSubscription.Indix",
                 "Plugin/CustomerSubscription/{action}",
                 new { controller = "CustomerSubscription", action = "LoadCustomers" },
                 new[] { "CC.Plugins.SubscriptionControllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            routes.MapRoute("SmartStore.CustomerSubscription.Charge",
                "Plugin/CustomerSubscription/{action}/{id}",
                new { controller = "CustomerSubscription", action = "Charge" },
                new[] { "CC.Plugins.SubscriptionControllers" }
           )
           .DataTokens["area"] = Plugin.SystemName;

           

            routes.MapRoute("SmartStore.CustomerSubscription.DeleteCardid",
               "Plugin/CustomerSubscription/{action}/{id}",
               new { controller = "CustomerSubscription", action = "DeleteCard" },
               new[] { "CC.Plugins.SubscriptionControllers" }
          )
          .DataTokens["area"] = Plugin.SystemName;
        }




        public int Priority => 0;
    }
}
