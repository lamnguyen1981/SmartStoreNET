using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Plugins.Core.Domain;
using CC.Plugins.Core.Utilities;
using CC.Plugins.Vehicle.Models;
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

namespace CC.Plugins.Vehicle.Controllers
{
    public class VehicleController : PluginControllerBase
    {
        // GET: Vehicle

        private readonly ICommonServices _services;
        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly IRepository<CCProgram> _proRepository;
        private readonly IRepository<tbVehicleID> _tbvRepository;
        private readonly IRepository<CCVehicle> _vRepository;
        private readonly IWorkContext _workContext;


        public VehicleController(ICommonServices services, AdminAreaSettings adminAreaSettings,
            IRepository<CCProgram> proRepository, IWorkContext workContext,
            IRepository<tbVehicleID> tbvRepository,
            IRepository<CCVehicle> vRepository
           )
        {
            _services = services;
            _adminAreaSettings = adminAreaSettings;
            _proRepository = proRepository;
            _workContext = workContext;
            _tbvRepository = tbvRepository;
            _vRepository = vRepository;
        }

        [AdminAuthorize]
        [AdminThemed]
        [Permission(Permissions.Catalog.Category.Create)]
        public ActionResult Create()
        {
            var model = new VehicleViewModel();

            ViewBag.tbVehicleLists = LoadtbVehicles(true);
            ViewBag.Programs = LoadPrograms(true);
            return View("AddEditVehicle", model);
        }

        [AdminAuthorize]
        [AdminThemed]
        [Permission(Permissions.Catalog.Category.Read)]
        public ActionResult Edit(int id)
        {
            ViewBag.tbVehicleLists = LoadtbVehicles(true);
            ViewBag.Programs = LoadPrograms(true);
            var model = new VehicleViewModel();
            if (id != 0)
            {
                model = QueryStringVehicle(id.ToString()).FirstOrDefault();
                model.StartWeek = string.Format("{0} - {1}", WeeksOfYearHelper.GetFridayDateByYearWeek(model.StartYW.ToString()).ToString("MM-dd-yyyy"),
                                                               WeeksOfYearHelper.GetFridayDateByYearWeek(model.StartYW.ToString()).AddDays(7).ToString("MM-dd-yyyy"));

                model.EndWeek = string.Format("{0} - {1}", WeeksOfYearHelper.GetFridayDateByYearWeek(model.EndYW.ToString()).ToString("MM-dd-yyyy"),
                                                                WeeksOfYearHelper.GetFridayDateByYearWeek(model.EndYW.ToString()).AddDays(7).ToString("MM-dd-yyyy"));
            }

            //ViewBag.tbVehicleList = LoadtbVehicle(true);
            return View("AddEditVehicle", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Permission(Permissions.Catalog.Category.Update)]
        public ActionResult Edit(VehicleViewModel model, bool continueEditing, FormCollection form)
        {


            return View(model);
        }



        [AdminAuthorize]
        [AdminThemed]
        public ActionResult Index()
        {
            var model = new VehicleViewModel
            {
                PageSize = _adminAreaSettings.GridPageSize,
                PageIndex = 1
            };


            ViewBag.tbVehicleLists = LoadtbVehicles();
            ViewBag.Programs = LoadPrograms();
            return View(model);
        }

        
        [HttpPost]
        [AdminAuthorize]
        [AdminThemed]
        [ValidateAntiForgeryToken]
        [Permission(Permissions.Configuration.Country.Create)]
        [Permission(Permissions.Configuration.Country.Update)]
        public ActionResult AddEditVehicle(VehicleViewModel model, FormCollection form)
        {
            ViewBag.tbVehicleLists = LoadtbVehicles(true);
            ViewBag.Programs = LoadPrograms(true);

            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            var entity = Utils.Map<VehicleViewModel, CCVehicle>(model);
            entity.StartYW = ParseYYYYWW(model.StartWeek);
            entity.EndYW = ParseYYYYWW(model.EndWeek);

            if (entity.StartYW == 0 || entity.EndYW ==0)
            {
                ModelState.AddModelError("1", "Start Week or End Week is invalid");
                return View(model);
            }

            if (model.Id > 0)
            {
                entity.UpdatedByUser = _workContext.CurrentCustomer.Id;
                _vRepository.Update(entity);
                NotifySuccess("Vehicle has been updated successfully");
            }
            else
            {
                entity.CreatedByUser = _workContext.CurrentCustomer.Id;
                _vRepository.Insert(entity);
                NotifySuccess("Vehicle has been added successfully");
            }

            return RedirectToAction("Index");
        }

        private int ParseYYYYWW(string weekRange)
        {
            if (string.IsNullOrEmpty(weekRange)) return 0;
            var arr = weekRange.Split(new string[]{" - "}, StringSplitOptions.None);
            if (arr == null || arr.Count() <2) return 0;
            return  WeeksOfYearHelper.GetWeekOfYearByDateTime(DateTime.Parse(arr[0]));
        }

        private IList<SelectListItem> LoadtbVehicles(bool isEdit = false)
        {
            string sqlQuery = @"Select * from tbVehicleID";

            var query = _services.DbContext.SqlQuery<tbVehicleViewModel>(sqlQuery).ToList();
            if (!isEdit)
            {
                query.Insert(0, (new tbVehicleViewModel
                {
                    VehicleId = 0,
                    Vehicle = "All"
                }));
            }

            return query
                .Select(x => new SelectListItem
                {
                    Text = x.Vehicle,
                    Value = x.VehicleId.ToString()
                })
                .ToList();
        }

        private IList<SelectListItem> LoadPrograms(bool isEdit = false)
        {
            var query = _proRepository.Table.ToList();

            
            if (!isEdit)
            {
                query.Insert(0, (new CCProgram
                {
                    Id = 0,
                    Name = "All"
                })); ;
            }

            return query
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        [AdminAuthorize]
        [AdminThemed]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Permission(Permissions.Configuration.Currency.Delete)]
        public ActionResult DeleteConfirmed(int id)
        {           

            try
            {
                var pro = _vRepository.GetById(id);
                if (pro != null)
                {
                    pro.Deleted = true;
                    _vRepository.Update(pro);
                    NotifySuccess("Vehicle has been deleted successfully");
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
        public ActionResult LoadVehicles(GridCommand command, VehicleViewModel model)
        {
            // We use own own binder for searchCustomerRoleIds property.
            var gridModel = new GridModel<VehicleViewModel>();

            var filter = new VehicleViewModel
            {
                PageIndex = command.Page - 1,
                PageSize = command.PageSize,
                Deleted = model.Deleted,
                Name = model.Name,
                ProgramId = model.ProgramId,
                tbVehicleId = model.tbVehicleId
            };

            var sqlQuery = QueryStringVehicle();

            var predicate = GenericFilter.BuildFilterQuery<VehicleViewModel, VehicleViewModel>(filter);
           
            sqlQuery = sqlQuery.Where(predicate);
            var result = sqlQuery.ToList();

            foreach ( var item in result)
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

        private IQueryable<VehicleViewModel> QueryVehicles(string id = "")
        {
            var sqlQuery = (from v in _vRepository.Table
                            join cus in _tbvRepository.Table on v.tbVehicleId equals cus.Id
                            join p in _proRepository.Table on v.ProgramId equals p.Id
                            where v.Deleted == false || v.Deleted == null
                            select new VehicleViewModel
                            {
                                Id = v.Id,
                                EndYW = v.EndYW,
                                Name = v.Name,
                                CreatedByUser = v.CreatedByUser,
                                CreatedOnUtc = v.CreatedOnUtc,
                                NumberOfLevels = v.NumberOfLevels,
                                ProgramId = v.ProgramId,
                                SellUnitPrice = v.SellUnitPrice,
                                StartYW = v.StartYW,
                                tbVehicleId = v.tbVehicleId,
                                UpdatedByUser = v.UpdatedByUser,
                                UpdatedOnUtc = v.UpdatedOnUtc,
                                tbVehicleName = cus.Vehicle,
                                ProgramName = p.Name
                            });

            return sqlQuery;
        }

        private IEnumerable<VehicleViewModel> QueryStringVehicle(string id = "")
        {
            string sqlQuery = @"Select v.*, tb.Vehicle as tbVehicleName , p.Name as ProgramName
                                from CCVehicle v 
                                inner join CCProgram p on v.ProgramId = p.Id
                                inner join tbVehicleId tb on v.tbVehicleId = tb.VehicleID
                                where (v.Deleted = 0 or v.Deleted is null)";



            if (id != "")
                sqlQuery = sqlQuery + " and v.Id=" + id;

            return _services.DbContext.SqlQuery<VehicleViewModel>(sqlQuery);
        }
    }
}