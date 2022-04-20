
using CC.Plugins.Location.Models;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Security;
using SmartStore.Services;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Theming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace CC.Plugins.Location.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location

        private readonly ICommonServices _services;
        private readonly AdminAreaSettings _adminAreaSettings;

        public LocationController(ICommonServices services, AdminAreaSettings adminAreaSettings)
        {
            _services = services;
            _adminAreaSettings = adminAreaSettings;

        }

        [AdminAuthorize]
        [AdminThemed]
        public ActionResult EditLocation(int locationId)
        {
            string sqlQuery = @"Select LocationID, LocationName, 'Salon' as LocationType from tbLocation
                                where LocationID = {0}
                                Union
                                Select MarketID, MarketName, 'Market' as LocationType from tbMarket
                                where MarketID = {0}";

            var result = _services.DbContext.SqlQuery<LocationModel>(sqlQuery, locationId);
            

            return View(result.FirstOrDefault());
        }

        [AdminAuthorize]
        [AdminThemed]
        public ActionResult LoadLocations()
        {
            var model = new LocationModel
            {
                PageSize = _adminAreaSettings.GridPageSize,
                PageIndex = 1
            };
           
            return View(model);
        }

        [AdminAuthorize]
        [AdminThemed]
        [HttpPost, GridAction(EnableCustomBinding = true)]
        [Permission(Permissions.Customer.Read)]
        public ActionResult LoadLocations(GridCommand command, LocationModel model)
        {
            // We use own own binder for searchCustomerRoleIds property.
            var gridModel = new GridModel<LocationModel>();

            var filter = new LocationModel
            {                
                PageIndex = command.Page - 1,
                PageSize = command.PageSize
            };

            string sqlQuery = @"Select LocationID, LocationName, 'Salon' as LocationType from tbLocation
                                Union
                                Select MarketID, MarketName, 'Market' as LocationType from tbMarket";
            var result = _services.DbContext.SqlQuery<LocationModel>(sqlQuery).ToList();
                                                    
            gridModel.Total = result.Count();
            gridModel.Data = result.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize);
            

            return new JsonResult
            {
                Data = gridModel
            };
        }

        [AdminAuthorize]
        [ChildActionOnly]        
        public ActionResult Configure()
        {
           

            return View();
        }

        public ActionResult List()
        {
          
            return View();
        }

      

    }
}