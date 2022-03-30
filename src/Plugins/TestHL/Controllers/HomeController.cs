﻿using SmartStore.CreditCardPay.Models;
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
        private readonly ICreditCardManagementService _service;
        private readonly IHeartlandRecurrService _recurrService;

        public HomeController(ICreditCardManagementService service, IHeartlandRecurrService recurrService)
        {
            this._service = service;
           this._recurrService = recurrService;
        }

        public ActionResult Index()
        {

             TestWithNewPayment();
           // _recurrService.GetAllPaymentMethods(1);
            return View();
        }


        private void TestWithNewPayment()
        {
            var charge = new CreditCardChargeDetailRequest
            {
                Card = new PaymentMethodInfo
                {
                    Number = "4012002000060016",
                    Cvv = "123",
                    ExpMonth = 12,
                    ExpYear = 2025

                },
                CardHolder = new CustomerInfo
                {
                    Address = "6860 Dallas Pkwy",
                    City = "HCM",
                    Email = "haisa@gmail.com",
                    FirstName = "Tony",
                    LastName = "Pham",
                    PhoneNumber = "9876543210",
                    Zip = "750241234",
                    Country = "USA"


                },
                Amount = 34,
                Currency = "USD",
                IsSaveCard = true,
                OrderId = "2323",
                WithShippingAmt = 2
            };
             //  _service.ProcessPayment(charge, 1);
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