using SmartStore.Core;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Security;
using SmartStore.CreditCardPay.Models;
using SmartStore.CreditCardPay.Services;
using SmartStore.Services.Directory;
using SmartStore.Services.Localization;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Theming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace SmartStore.CreditCardPay.Controllers
{
    public class CustomerCreditCardController :  PluginControllerBase
    {
        private readonly ICreditCardManagementService _cardService;
        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly ICurrencyService _currencyService;

        private readonly IWorkContext _workContext;

        public CustomerCreditCardController(ICreditCardManagementService cardService,
                                    IWorkContext workContext, 
                                    AdminAreaSettings adminAreaSettings,
                                    ICurrencyService currencyService
            )


        {
            _cardService = cardService;
            _workContext = workContext;
            _adminAreaSettings = adminAreaSettings;
            _currencyService = currencyService;
        }
        // GET: CustomerCreditCard
        [AdminAuthorize]
        [AdminThemed]
        public ActionResult Index()
        {
            return View();
        }

        [AdminAuthorize]
        [AdminThemed]
        public ActionResult LoadCustomers()
        {
            var filter = new PaymentMethodSearchCondition
            {
                 PageSize = _adminAreaSettings.GridPageSize,
                 PageIndex =1
            };
           
            return View(filter);
        }
        [AdminAuthorize]
        [AdminThemed]
        [HttpPost, GridAction(EnableCustomBinding = true)]
        [Permission(Permissions.Customer.Read)]
        public ActionResult LoadCustomers(GridCommand command, PaymentMethodSearchCondition model)
        {
            // We use own own binder for searchCustomerRoleIds property.
            var gridModel = new GridModel<PaymentMethodResponse>();

            var filter = new PaymentMethodSearchCondition
            {
                CardAlias = model.CardAlias,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CardMask = model.CardMask,
                CardType = model.CardType,
                PageIndex = command.Page,
                PageSize = command.PageSize
            };

            var payments = _cardService.SearchPaymentMethod(filter);

            gridModel.Data = payments;
            gridModel.Total = payments.Count();

            return new JsonResult
            {
                Data = gridModel
            };
        }

        [Permission(Permissions.Customer.Update)]
        public ActionResult DeleteCard(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                _cardService.DeletePaymentMethod(id);
            }

            // NotifySuccess("Your card has been deleted successfully");
            NotifySuccess(T("Plugins.CreditCard.DeleteSucess"));
            return RedirectToAction("LoadCustomers");

        }

        [AdminAuthorize]
        [AdminThemed]
        [Permission(Permissions.Customer.Create)]
        public ActionResult Charge(string id)
        {
            var model = new CreditCardChargeDetailRequest();
            model.Card.PaymentProfileId = id;
            
            // ViewBag.MyList = new SelectList(_currencyService.GetAllCurrencies(false), "CurrencyCode", "Name");
            ViewBag.CurrencyList = _currencyService.GetAllCurrencies(false)
                .Select(x => new SelectListItem
                {
                    Text = x.GetLocalized(y => y.Name),
                    Value = x.CurrencyCode
                })
                .ToList();

            return View(model);

        }

        [Permission(Permissions.Customer.Create)]
        [HttpPost]
        public ActionResult Charge(CreditCardChargeDetailRequest model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CurrencyList = _currencyService.GetAllCurrencies(false)
                .Select(x => new SelectListItem
                {
                    Text = x.GetLocalized(y => y.Name),
                    Value = x.CurrencyCode
                })
                .ToList();
                return View(model);
            }
          //  model.Card.PaymentProfileId = paymentProfileId;
            _cardService.Charge(model);
            NotifySuccess(T("Plugins.CreditCard.ChargeSucess"));            
            return RedirectToAction("LoadCustomers");

        }

    }
}