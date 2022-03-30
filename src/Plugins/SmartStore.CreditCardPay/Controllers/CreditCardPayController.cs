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
        private readonly ICreditCardManagementService _cardService;
        private readonly IRepository<CustomerPaymentProfile> _cusPayRepository;     
        private readonly IWorkContext _workContext;

        public CreditCardPayController(ICreditCardManagementService cardService,
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
            NotifySuccess(T("Admin.Common.DataSuccessfullySaved"));

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

             var model = _cardService.GetAllPaymentMethods(_workContext.CurrentCustomer.Id);

            return View(model);

        }

        [LoadSetting]
        public ActionResult AddCard(CreditCardPaySettings settings)
        {
            ViewBag.PublicKey = settings.HearlandPublicKey;
            return View();
        }

        
        [LoadSetting]
        public ActionResult DeleteCard(CreditCardPaySettings settings, string paymentProfileId)
        {
            if (!String.IsNullOrEmpty(paymentProfileId))
            {
                _cardService.DeletePaymentMethod(paymentProfileId);                
            }

            return RedirectToAction("CardListDetail");

        }

        
        public ActionResult SaveCard(string cardHolderName, string cardAlias, string Token_value)
        {
            var request = new HeartlandRequestBase
            {
                customerId = _workContext.CurrentCustomer.Id,
                IsSaveCard = true,
                Card = new PaymentMethodInfo
                {
                    CardAlias = cardAlias,
                    CardHolderName = cardHolderName,
                    Token = Token_value
                },

                CardHolder = new CustomerInfo
                {
                    Email = _workContext.CurrentCustomer.Email,
                    Country = "USA",
                    FirstName = _workContext.CurrentCustomer.FirstName,
                    LastName = _workContext.CurrentCustomer.LastName,
                }

            };
            _cardService.AddPaymentMethod(request);            

            return RedirectToAction("CardListDetail");

        }
    }
}