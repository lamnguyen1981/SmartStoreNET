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
            var test = _protbTactic.Table.ToList();
            var model = new TacticViewModel
            {
                PageSize = _adminAreaSettings.GridPageSize,
                PageIndex = 1
            };

            ViewBag.tbTacticList = LoadtbTactic();
            return View(model);
        }

        [AdminAuthorize]
        [AdminThemed]
        [Permission(Permissions.Configuration.Country.Create)]
        [Permission(Permissions.Configuration.Country.Update)]
        public ActionResult AddEditTactic(int id=0)
        {
            var model = new TacticViewModel();
            if (id != 0)
            {
                model = QueryTactics(id.ToString()).FirstOrDefault();
            }

            ViewBag.tbTacticList = LoadtbTactic(true);
            return View(model);
        }
        [HttpPost]
        [AdminAuthorize]
        [AdminThemed]
        [ValidateAntiForgeryToken]
        [Permission(Permissions.Configuration.Country.Create)]
        [Permission(Permissions.Configuration.Country.Update)]
        public ActionResult AddEditTactic(TacticViewModel model, FormCollection form)
        {         
            
            if (!ModelState.IsValid)
            {
                ViewBag.tbTacticList = LoadtbTactic(true);
                return View(model);
            }

            var entity = Utils.Map<TacticViewModel, CCTactic>(model);
            
            if (model.Id > 0)
            {
                entity.UpdatedByUser = _workContext.CurrentCustomer.Id;
                _proRepository.Update(entity);
                NotifySuccess("Tactic has been updated successfully");
            }
            else
            {
                entity.CreatedByUser = _workContext.CurrentCustomer.Id;
                _proRepository.Insert(entity);
                NotifySuccess("Tactic has been added successfully");
            }
            
            return RedirectToAction("Index");
        }

        private IList<SelectListItem> LoadtbTactic(bool isEdit=false)
        {
            string sqlQuery = @"Select * from tbTactic";

            var query = _services.DbContext.SqlQuery<tbTacticViewModel>(sqlQuery).ToList();
            if (!isEdit)
            {
                query.Insert(0, (new tbTacticViewModel
                {
                    TacticCode = string.Empty,
                    TacticName = "All"
                }));
            }
            
            return query
                .Select(x => new SelectListItem
                {
                    Text = x.TacticName,
                    Value = x.TacticCode
                })
                .ToList();
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
                Name = model.Name,
                Code = model.Code,
                ShortDescription = model.ShortDescription,
                tbTacticCode = model.tbTacticCode,
                LongDescription = model.LongDescription,
                 TacticType = model.TacticType
            };
           

            var query = QueryTactics();

            var predicate = GenericFilter.BuildFilterQuery<TacticViewModel, TacticViewModel>(filter);
            query = query.Where(predicate);

            gridModel.Total = query.Count();
            gridModel.Data = query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize);                           

            return new JsonResult
            {
                Data = gridModel
            };
        }

        private IEnumerable<TacticViewModel> QueryTactics(string id ="")
        {
            string sqlQuery = @"Select cc.* , tb.TacticName as tbTacticName
                                From CCTactic cc inner join tbTactic tb on tb.TacticCode = cc.tbTacticCode
                                where (cc.Deleted is null or cc.Deleted =0)";
            if (id != "")
                sqlQuery = sqlQuery + " and cc.Id=" + id;

            return _services.DbContext.SqlQuery<TacticViewModel>(sqlQuery);
        }
    }
}