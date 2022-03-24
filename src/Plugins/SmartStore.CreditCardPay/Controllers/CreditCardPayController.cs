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
        private readonly IWorkContext _workContext;

        public CreditCardPayController(IHeartlandRecurrService cardService,
                                    IWorkContext workContext,
                                   IRepository<CustomerPaymentProfile> cusPayRepository)

        {
            _cardService = cardService;
            _cusPayRepository = cusPayRepository;
            _workContext = workContext;
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

        public ActionResult CardList()
        {
            ViewBag.PageTitle = "Credit Card List";                     

             return View();
            
        }

        public ActionResult CardListDetail()
        {
            ViewBag.PageTitle = "Credit Card List";

            int customerid = _workContext.CurrentCustomer.Id;
            var customerPayment = _cusPayRepository.Table.FirstOrDefault(x => x.CustomerProfileId == customerid && x.HlCustomerProfileId != null);

            if (customerPayment == null)
                return View();

            // var cardList = _cardService.GetAllPaymentMethods(customerPayment.HlCustomerProfileId);
            var TestData = new List<PaymentMethod>();

            TestData.Add(new PaymentMethod
            {
                CardMask = "4546********3434",
                CardType = "Visa",
                ExpireDate = "1225"
            });

            TestData.Add(new PaymentMethod
            {
                CardMask = "45126********3489",
                CardType = "Visa",
                ExpireDate = "1229"
            });
            return View(TestData);

        }
    }
}