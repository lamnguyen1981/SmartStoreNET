using SmartStore.Core.Logging;
using SmartStore.Core.Plugins;
using SmartStore.Services.Cms;
using SmartStore.Services.Configuration;
using SmartStore.Services.Localization;
using System.Collections.Generic;
using System.Web.Routing;

namespace CC.Plugins.Location
{
    public class Plugin : BasePlugin, IWidget, IConfigurable
    {
        private readonly ILogger _logger;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        
        public const string SystemName = "CC.Plugins.Location";
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// /// <param name="settingService">Settings service</param>
        /// /// <param name="settingService">Localization service</param>
        //public Plugin(ILogger logger,
        //    ISettingService settingService,
        //    ILocalizationService localizationService,
        //    LocationContext objectContext)
        //{
        //    this._logger = logger;
        //    this._settingService = settingService;
        //    this._localizationService = localizationService;
        //    this._objectContext = objectContext;
        //}
        public Plugin()
        {

        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "Location";
            routeValues = new RouteValueDictionary() { { "area", Plugin.SystemName } };
        }

        public void GetDisplayWidgetRoute(string widgetZone, object model, int storeId, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {

            //if widgetZone = "myaccount_menu_after"
            actionName = "Index";
            controllerName = "Location";
            routeValues = new RouteValueDictionary
                {
                    {"Namespaces", "CC.Plugins.Location.Controllers"},
                    {"area", "CC.Plugins.Location"},
                    {"widgetZone", widgetZone}
                    //{"additionalData", routeValues }
                };

            //else //if (widgetZone == "productdetail_action_links_after")
            //{
            //    actionName = "Subscribe";
            //    controllerName = "ProductLocation";
            //    routeValues = new RouteValueDictionary
            //    {
            //        {"Namespaces", "CC.Plugins.ChamberlainProductLocation.Controllers"},
            //        {"area", "CC.Plugins.ChamberlainProductLocation"},
            //        {"widgetZone", widgetZone}
            //        //{"additionalData", routeValues }
            //    };
            //}
            //else 
            //{
            //    //if widgetzone = admin_content_before
            //    ProductModel productModel = model as ProductModel;

            //    actionName = "NotificationPopup";
            //    controllerName = "ProductLocation";
            //    routeValues = new RouteValueDictionary
            //    {
            //        {"Namespaces", "CC.Plugins.ChamberlainProductLocation.Controllers"},
            //        {"area", "CC.Plugins.ChamberlainProductLocation"},
            //        {"widgetZone", widgetZone},
            //        {"productId", productModel != null ? productModel.Id : 0 }
            //        //{"additionalData", routeValues }
            //    };
            //} 

        }

        public IList<string> GetWidgetZones()
        {
            return new List<string>
            {
                "myaccount_menu_after" //, "admin_content_before"//,"productdetail_action_links_after", 
            };
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        public override void Install()
        {
            //var shippingByTotalSettings = new ShippingByTotalSettings()
            //{
            //    LimitMethodsToCreated = false,
            //    SmallQuantityThreshold = 0,
            //    SmallQuantitySurcharge = 0
            //};
            //_settingService.SaveSetting(shippingByTotalSettings);

            //_localizationService.ImportPluginResourcesFromXml(this.PluginDescriptor);

            base.Install();

            //_logger.Info(string.Format("Plugin installed: SystemName: {0}, Version: {1}, Description: '{2}'", PluginDescriptor.SystemName, PluginDescriptor.Version, PluginDescriptor.FriendlyName));
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        public override void Uninstall()
        {
         //   _settingService.DeleteSetting<ShippingByTotalSettings>();

            _localizationService.DeleteLocaleStringResources(PluginDescriptor.ResourceRootKey);

          //  var migrator = new DbMigrator(new Configuration());
          //  migrator.Update(DbMigrator.InitialDatabase);

          //  _localizationService.DeleteLocaleStringResources("Plugins.FriendlyName.Shipping.FixedRateShipping", false);

            base.Uninstall();
        }
    }
}
