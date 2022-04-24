using CC.Plugins.Zoho.Models;
using CC.Plugins.Zoho.Services;
using SmartStore.ComponentModel;
using SmartStore.Core;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Security;
using SmartStore.Linq;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Settings;
using SmartStore.Web.Framework.Theming;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace CC.Plugins.Zoho.Controllers
{
    public class ZohoController : PluginControllerBase
    {
       

        private readonly IWorkContext _workContext;
        IZohoService _zohoService;
        private readonly AdminAreaSettings _adminAreaSettings;

        public ZohoController( IZohoService zohoService,
                                    IWorkContext workContext,
                                    AdminAreaSettings adminAreaSettings)


        {
            _zohoService = zohoService;
             _workContext = workContext;
            _adminAreaSettings = adminAreaSettings;

        }
        // GET: CreditPay
        public ActionResult Index()
        {
            return View();
        }

        [AdminAuthorize]
        [ChildActionOnly]
        [LoadSetting]
        public ActionResult Configure(ZohoSettings settings)
        {
            var model = new ConfigurationModel();
            MiniMapper.Map(settings, model);

            return View(model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        [SaveSetting]
        public ActionResult Configure(ZohoSettings settings, ConfigurationModel model, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                return Configure(settings);
            }
            MiniMapper.Map(model, settings);
            NotifySuccess(T("Admin.Common.DataSuccessfullySaved"));

            return RedirectToConfiguration("CC.Plugins.Zoho");
        }


        public ActionResult CardMenuItem()
        {
            return PartialView();
        }

        [AdminAuthorize]
        [AdminThemed]
        public ActionResult GetInvoices()
        {
            var model = new InvoiceModel
            {
                PageSize = _adminAreaSettings.GridPageSize,
                PageIndex = 1
            };

            return View(model);
        }

        [AdminAuthorize]
        [AdminThemed]
        public async Task<ActionResult> EditInvoices(string invoiceId)
        {
            var response = await _zohoService.GetInvoiceDetail_v2(invoiceId);
            if (response.Success)
            {
                var invoice = response.Zoho_Object;
                var model = new InvoiceModel
                {
                    CurrencySymbol = invoice.Value<string>("currency_symbol"),
                    CustomerName = invoice.Value<string>("customer_name"),
                    InvoiveDate = invoice.Value<System.DateTime>("date"),
                    InvoiveNumber = invoice.Value<string>("invoice_number"),
                    Status = invoice.Value<string>("status"),
                    Total = invoice.Value<decimal>("total")


                };
                return View(model);
            }

                return View();
        }

        [AdminAuthorize]
        [AdminThemed]
        [HttpPost, GridAction(EnableCustomBinding = true)]
        [Permission(Permissions.Customer.Read)]
        public async Task<ActionResult> GetInvoices(GridCommand command, InvoiceModel request)
        {
            // We use own own binder for searchCustomerRoleIds property.
            var gridModel = new GridModel<InvoiceModel>();

            var filter = new InvoiceModel
            {
                PageIndex = command.Page - 1,
                PageSize = command.PageSize,
                CustomerName = request.CustomerName,
                InvoiveNumber = request.InvoiveNumber,
                Status = request.Status
            };

            var response = await _zohoService.GetInvoicesByStatus("draft");
            if (response.Success)
            {
                var model = response.Zoho_Objects.Select(x => new InvoiceModel
                {
                    CurrencySymbol = x.Value<string>("currency_symbol"),
                    CustomerName = x.Value<string>("customer_name"),
                    InvoiveDate = x.Value<System.DateTime>("date"),
                    InvoiveNumber = x.Value<string>("invoice_number"),
                    Status = x.Value<string>("status"),
                    Total = x.Value<decimal>("total"),
                    InvoiveId = x.Value<string>("invoice_id")

                });

                var predicate = PredicateBuilder.New<InvoiceModel>();
                predicate = predicate.Start(x => x.InvoiveNumber != string.Empty);
                
                if (!string.IsNullOrEmpty(filter.CustomerName))
                    predicate.And(x => x.CustomerName.ToLower().Contains(filter.CustomerName.ToLower()));
                if (!string.IsNullOrEmpty(filter.InvoiveNumber))
                    predicate.And(x => x.InvoiveNumber.ToLower().Contains(filter.InvoiveNumber.ToLower()));
                if (!string.IsNullOrEmpty(filter.Status))
                    predicate.And(x => x.Status.ToLower().Contains(filter.Status.ToLower()));

                var result = model.Where(predicate);

                gridModel.Total = result.Count();
                gridModel.Data = result.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize);


                return new JsonResult
                {
                    Data = gridModel
                };
            }


            return View();
            
        }

        

    }
}