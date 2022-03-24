using SmartStore.ComponentModel;
using SmartStore.Core;
using SmartStore.Core.Data;
using SmartStore.CreditCardPay.Domain;
using SmartStore.CreditCardPay.Models;
using SmartStore.CreditCardPay.Services;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.CreditCardPay.Controllers
{
    public class CreditCardPayController : PluginControllerBase
    {
        private readonly IHeartlandRecurrService _cardService;
        private readonly IRepository<CustomerPaymentProfile> _cusPayRepository;
      //  private readonly CreditCardPaySettings _settings;
        private readonly IWorkContext _workContext;

        public CreditCardPayController(IHeartlandRecurrService cardService,
                                    IWorkContext workContext,
                                //    CreditCardPaySettings settings,
                                   IRepository<CustomerPaymentProfile> cusPayRepository)

        {
            _cardService = cardService;
            _cusPayRepository = cusPayRepository;
            _workContext = workContext;
          //  _settings = settings;
        }
        // GET: CreditPay
        public ActionResult Index()
        {
            return View();
        }

        [AdminAuthorize]
        [ChildActionOnly]
        [LoadSetting]
        public ActionResult Configure(CreditCardPaySettings settings)
        {
            var model = new ConfigurationModel();
            MiniMapper.Map(settings, model);

            return View(model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        [SaveSetting]
        public ActionResult Configure(CreditCardPaySettings settings, ConfigurationModel model, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                return Configure(settings);
            }
            //var storeDependingSettingHelper = new StoreDependingSettingHelper(ViewData);
            //var storeScope = this.GetActiveStoreScopeConfiguration(Services.StoreService, Services.WorkContext);
            //var setting = Services.Settings.LoadSetting<CreditCardPaySettings>(storeScope);
            //using (Services.Settings.BeginScope())
            //{
            //    storeDependingSettingHelper.UpdateSettings(settings, form, storeScope, Services.Settings);
            //}
            MiniMapper.Map(model, settings);
            return RedirectToConfiguration("SmartStore.CreditCardPay");
        }
        

        public ActionResult CardMenuItem()
        {
            return PartialView();
        }

        [LoadSetting]
        public ActionResult CardListDetail(CreditCardPaySettings settings)
        {
            ViewBag.PageTitle = "Credit Card List";

            int customerid = _workContext.CurrentCustomer.Id;
            var customerPayment = _cusPayRepository.Table.FirstOrDefault(x => x.CustomerProfileId == customerid && x.HlCustomerProfileId != null);

            if (customerPayment == null)
                return View();
           // _cardService.ba
            var cardList = _cardService.GetAllPaymentMethods(customerPayment.HlCustomerProfileId);
           
            return View(cardList);

        }
    }
}