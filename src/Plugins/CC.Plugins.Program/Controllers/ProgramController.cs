using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Plugins.Core.Domain;
using CC.Plugins.Core.Utilities;
using CC.Plugins.Program.Models;
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

namespace CC.Plugins.Program.Controllers
{
    public class ProgramController : PluginControllerBase
    {
        // GET: Program
        
        private readonly ICommonServices _services;
        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly IRepository<CCProgram> _proRepository;
        private readonly IWorkContext _workContext;
      

        public ProgramController(ICommonServices services, AdminAreaSettings adminAreaSettings, 
            IRepository<CCProgram> proRepository, IWorkContext workContext
           )
        {
            _services = services;
            _adminAreaSettings = adminAreaSettings;
            _proRepository = proRepository;
            _workContext = workContext;
            
        }
        

        [AdminAuthorize]
        [AdminThemed]
        public ActionResult Index()
        {
           
            var model = new ProgramViewModel
            {
                PageSize = _adminAreaSettings.GridPageSize,
                PageIndex = 1
            };

            ViewBag.tbProgramList = LoadtbProgram();
            return View(model);
        }

        [AdminAuthorize]
        [AdminThemed]
        [Permission(Permissions.Configuration.Country.Create)]
        [Permission(Permissions.Configuration.Country.Update)]
        public ActionResult AddEditProgram(int id=0)
        {
            var model = new ProgramViewModel();
            if (id != 0)
            {
                model = QueryPrograms(id.ToString()).FirstOrDefault();
            }

            ViewBag.tbProgramList = LoadtbProgram(true);
            return View(model);
        }
        [HttpPost]
        [AdminAuthorize]
        [AdminThemed]
        [ValidateAntiForgeryToken]
        [Permission(Permissions.Configuration.Country.Create)]
        [Permission(Permissions.Configuration.Country.Update)]
        public ActionResult AddEditProgram(ProgramViewModel model, FormCollection form)
        {         
            
            if (!ModelState.IsValid)
            {
                ViewBag.tbProgramList = LoadtbProgram(true);
                return View(model);
            }

            var entity = Utils.Map<ProgramViewModel, CCProgram>(model);
            
            if (model.Id > 0)
            {
                entity.UpdatedByUser = _workContext.CurrentCustomer.Id;
                _proRepository.Update(entity);
                NotifySuccess("Program has been updated successfully");
            }
            else
            {
                entity.CreatedByUser = _workContext.CurrentCustomer.Id;
                _proRepository.Insert(entity);
                NotifySuccess("Program has been added successfully");
            }
            
            return RedirectToAction("Index");
        }

        private IList<SelectListItem> LoadtbProgram(bool isEdit=false)
        {
            string sqlQuery = @"Select * from tbProgram";

            var query = _services.DbContext.SqlQuery<tbProgramViewModel>(sqlQuery).ToList();
            if (!isEdit)
            {
                query.Insert(0, (new tbProgramViewModel
                {
                    ProgramCode = string.Empty,
                    ProgramName = "All"
                }));
            }
            
            return query
                .Select(x => new SelectListItem
                {
                    Text = x.ProgramName,
                    Value = x.ProgramCode
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
                    NotifySuccess("Program has been deleted successfully");
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
        public ActionResult LoadPrograms(GridCommand command, ProgramViewModel model)
        {
            // We use own own binder for searchCustomerRoleIds property.
            var gridModel = new GridModel<ProgramViewModel>();

            var filter = new ProgramViewModel
            {
                PageIndex = command.Page - 1,
                PageSize = command.PageSize,
                Deleted = model.Deleted,
                Name = model.Name,
                Code = model.Code,
                ShortDescription = model.ShortDescription,
                tbProgramCode = model.tbProgramCode,
                LongDescription = model.LongDescription,
                 ProgramType = model.ProgramType
            };
           

            var query = QueryPrograms();

            var predicate = GenericFilter.BuildFilterQuery<ProgramViewModel, ProgramViewModel>(filter);
            query = query.Where(predicate);

            gridModel.Total = query.Count();
            gridModel.Data = query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize);                           

            return new JsonResult
            {
                Data = gridModel
            };
        }

        private IEnumerable<ProgramViewModel> QueryPrograms(string id ="")
        {
            string sqlQuery = @"Select cc.* , tb.ProgramName as tbProgramName
                                From CCProgram cc inner join tbProgram tb on tb.ProgramCode = cc.tbProgramCode
                                where (cc.Deleted is null or cc.Deleted =0)";
            if (id != "")
                sqlQuery = sqlQuery + " and cc.Id=" + id;

            return _services.DbContext.SqlQuery<ProgramViewModel>(sqlQuery);
        }
    }
}