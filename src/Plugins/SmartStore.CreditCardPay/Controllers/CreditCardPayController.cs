using SmartStore.ComponentModel;
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
        private readonly IRepository<CustomerPayment> _cusPayRepository;

        public CreditCardPayController(IHeartlandRecurrService cardService,
                                   IRepository<CustomerPayment> cusPayRepository)

        {
            _cardService = cardService;
            _cusPayRepository = cusPayRepository;
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

            MiniMapper.Map(model, settings);
            return RedirectToConfiguration("SmartStore.CreditCardPay");
        }

        public ActionResult CardList(int customerId)
        {
            ViewBag.PageTitle = "Credit Card List";

            /* var customerPayment = _cusPayRepository.Table.FirstOrDefault(x => x.CustomerProfileId == customerId && x.HlCustomerProfileId != null);

             if (customerPayment == null) 
                 return View();

             var cardList = _cardService.GetAllPaymentMethods(customerPayment.HlCustomerProfileId);           

             return View(cardList);*/
            return View();
        }
    }
}