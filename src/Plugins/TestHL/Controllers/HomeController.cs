using SmartStore.CreditCardPay.Models;
using SmartStore.CreditCardPay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestHL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICreditCardPaymentProcess _service;
        private readonly IHeartlandRecurrService _recurrService;

        public HomeController(ICreditCardPaymentProcess service, IHeartlandRecurrService recurrService)
        {
            this._service = service;
           this._recurrService = recurrService;
        }

        public ActionResult Index()
        {
            var charge = new CreditCardChargeDetail
            {
                Card = new CreditCard
                {
                    Number = "4012002000060016",
                    Cvv = "123",
                    ExpMonth = 12,
                    ExpYear = 2025

                },
                Holder = new CardHolder
                {
                     Address= "6860 Dallas Pkwy",
                      City="HCM",
                       Email="haisa@gmail.com",
                        FirstName="Tony",
                         LastName="Pham",
                           PhoneNumber = "9876543210",
                             Zip = "750241234",
                              Country = "USA"
                             

                },
                 Amount=34,
                  Currency="USD",
                   isSaveCard = true,
                    OrderId = "2323",
                     WithShippingAmt =2
            };
         //   _service.ProcessPayment(charge, 3);
            _recurrService.GetAllPaymentMethods("202203214656-GlobalApi-1780274");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}