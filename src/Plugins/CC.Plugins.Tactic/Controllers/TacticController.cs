using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Plugins.Core.Domain;
using CC.Plugins.Core.Utilities;
using CC.Plugins.Tactic.Models;
using SmartStore.Core;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Security;
using SmartStore.Linq;
using SmartStore.Services;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Theming;
using Telerik.Web.Mvc;

namespace CC.Plugins.Tactic.Controllers
{
    public class TacticController : PluginControllerBase
    {
        // GET: Tactic
        
        private readonly ICommonServices _services;
        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly IRepository<CCTactic> _proRepository;
        private readonly IWorkContext _workContext;
        private readonly IRepository<tbTacticID> _protbTactic;

        public TacticController(ICommonServices services, AdminAreaSettings adminAreaSettings, 
            IRepository<CCTactic> proRepository, IWorkContext workContext,
            IRepository<tbTacticID> protbTactic)
        {
            _services = services;
            _adminAreaSettings = adminAreaSettings;
            _proRepository = proRepository;
            _workContext = workContext;
            _protbTactic = protbTactic;
        }
        

        [AdminAuthorize]
        [AdminThemed]
        public ActionResult Index()
        {
           
            var model = new TacticViewModel
            {
                PageSize = _adminAreaSettings.GridPageSize,
                PageIndex = 1
            };

            ViewBag.tbTacticList = LoadtbTactic();
            ViewBag.VehicleList = LoadVehicleListItem();
            return View(model);
        }

        [AdminAuthorize]
        [AdminThemed]
        [Permission(Permissions.Catalog.Category.Create)]
        public ActionResult Create()
        {
            var model = new TacticViewModel();

            ViewBag.tbTacticList = LoadtbTactic(true);
            ViewBag.VehicleList = LoadVehicleListItem(true);
            return View("AddEditTactic", model);
        }

        [AdminAuthorize]
        [AdminThemed]
        [Permission(Permissions.Catalog.Category.Read)]
        public ActionResult Edit(int id)
        {
            ViewBag.tbTacticList = LoadtbTactic(true);
            ViewBag.VehicleList = LoadVehicleListItem(true);
            var model = new TacticViewModel();
            if (id != 0)
            {
                model = QueryTactics(id.ToString()).FirstOrDefault();
                model.StartWeek = string.Format("{0} - {1}", WeeksOfYearHelper.GetFridayDateByYearWeek(model.StartYW.ToString()).ToString("MM-dd-yyyy"),
                                                               WeeksOfYearHelper.GetFridayDateByYearWeek(model.StartYW.ToString()).AddDays(7).ToString("MM-dd-yyyy"));

                model.EndWeek = string.Format("{0} - {1}", WeeksOfYearHelper.GetFridayDateByYearWeek(model.EndYW.ToString()).ToString("MM-dd-yyyy"),
                                                                WeeksOfYearHelper.GetFridayDateByYearWeek(model.EndYW.ToString()).AddDays(7).ToString("MM-dd-yyyy"));
            }

            //ViewBag.tbVehicleList = LoadtbVehicle(true);
            return View("AddEditTactic", model);
        }

        [HttpPost]
        
        [ValidateAntiForgeryToken]
        [Permission(Permissions.Configuration.Country.Create)]
        [Permission(Permissions.Configuration.Country.Update)]
        public ActionResult AddEditTactic(TacticViewModel model, FormCollection form)
        {

            ViewBag.tbTacticList = LoadtbTactic(true);
            ViewBag.VehicleList = LoadVehicleListItem(true);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = Utils.Map<TacticViewModel, CCTactic>(model);
            entity.StartYW = ParseYYYYWW(model.StartWeek);
            entity.EndYW = ParseYYYYWW(model.EndWeek);

            if (entity.StartYW == 0 || entity.EndYW == 0)
            {
                ModelState.AddModelError("1", "Start Week or End Week is invalid");
                return View(model);
            }

            if (model.Id > 0)
            {
                entity.UpdatedByUser = _workContext.CurrentCustomer.Id;
                _proRepository.Update(entity);
                NotifySuccess("Vehicle has been updated successfully");
            }
            else
            {
                entity.CreatedByUser = _workContext.CurrentCustomer.Id;
                _proRepository.Insert(entity);
                NotifySuccess("Vehicle has been added successfully");
            }

            return RedirectToAction("Index");
        }

        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Permission(Permissions.Configuration.Currency.Delete)]
        public ActionResult DeleteConfirmed(int id)
        {           

            try
            {
                var pro = _proRepository.GetById(id);
                if (pro != null)
                {
                    pro.Deleted = true;
                    _proRepository.Update(pro);
                    NotifySuccess("Tactic has been deleted successfully");
                }                               
                
            }
            catch (Exception ex)
            {
                NotifyError(ex);
            }

            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        [AdminThemed]
        [HttpPost, GridAction(EnableCustomBinding = true)]
        [Permission(Permissions.Customer.Read)]
        public ActionResult LoadTactics(GridCommand command, TacticViewModel model)
        {
            // We use own own binder for searchCustomerRoleIds property.
            var gridModel = new GridModel<TacticViewModel>();

            var filter = new TacticViewModel
            {
                PageIndex = command.Page - 1,
                PageSize = command.PageSize,
                Deleted = model.Deleted,
                TacticType = model.TacticType,
                TacticCode = model.TacticCode,
                TacticDescription = model.TacticDescription,
                tbTacticId = model.tbTacticId,
                VehicleId = model.VehicleId
            };
           

            var query = QueryTactics();

            var predicate = GenericFilter.BuildFilterQuery<TacticViewModel, TacticViewModel>(filter);
            query = query.Where(predicate);

            var result = query.ToList();

            foreach (var item in result)
            {
                item.StartWeek = string.Format("{0} - {1}", WeeksOfYearHelper.GetFridayDateByYearWeek(item.StartYW.ToString()).ToString("MM-dd-yyyy"),
                                                               WeeksOfYearHelper.GetFridayDateByYearWeek(item.StartYW.ToString()).AddDays(7).ToString("MM-dd-yyyy"));

                item.EndWeek = string.Format("{0} - {1}", WeeksOfYearHelper.GetFridayDateByYearWeek(item.EndYW.ToString()).ToString("MM-dd-yyyy"),
                                                                WeeksOfYearHelper.GetFridayDateByYearWeek(item.EndYW.ToString()).AddDays(7).ToString("MM-dd-yyyy"));
            }

            gridModel.Total = result.Count();
            gridModel.Data = result.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize);                           

            return new JsonResult
            {
                Data = gridModel
            };
        }

        private IEnumerable<TacticViewModel> QueryTactics(string id ="")
        {
            string sqlQuery = @"Select cc.* , tb.Tactic as tbTacticName, v.[Name] as VehicleName
                                From CCTactic cc inner join tbTacticID tb on tb.TacticID = cc.tbTacticId
                                inner join CCVehicle v on v.Id = cc.VehicleId
                                where (cc.Deleted is null or cc.Deleted = 0)";
            if (id != "")
                sqlQuery = sqlQuery + " and cc.Id=" + id;

            return _services.DbContext.SqlQuery<TacticViewModel>(sqlQuery);
        }

        private IList<SelectListItem> LoadtbTactic(bool isEdit = false)
        {
            string sqlQuery = @"Select * from tbTacticID";

            var query = _services.DbContext.SqlQuery<tbTacticViewModel>(sqlQuery).ToList();
            if (!isEdit)
            {
                query.Insert(0, (new tbTacticViewModel
                {
                    Id = 0,
                    Tactic = "All"
                }));
            }

            return query
                .Select(x => new SelectListItem
                {
                    Text = x.Tactic,
                    Value = x.TacticID.ToString()
                })
                .ToList();
        }

        private IList<SelectListItem> LoadVehicleListItem(bool isEdit = false)
        {
            string sqlQuery = @"Select * from CCVehicle";

            var query = _services.DbContext.SqlQuery<CCVehicle>(sqlQuery).ToList();
            if (!isEdit)
            {
                query.Insert(0, (new CCVehicle
                {
                    Id = 0,
                    Name = "All"
                }));
            }

            return query
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        private int ParseYYYYWW(string weekRange)
        {
            if (string.IsNullOrEmpty(weekRange)) return 0;
            var arr = weekRange.Split(new string[] { " - " }, StringSplitOptions.None);
            if (arr == null || arr.Count() < 2) return 0;
            return WeeksOfYearHelper.GetWeekOfYearByDateTime(DateTime.Parse(arr[0]));
        }
    }
}