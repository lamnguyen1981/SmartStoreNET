using SmartStore.CreditCardPay.Models;
using SmartStore.CreditCardPay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.CreditCardPay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICreditCardPaymentProcess _paymentService;
        HomeController(ICreditCardPaymentProcess paymentService)
        {
            _paymentService = paymentService;
        }
        // GET: Home
        public ActionResult Index()
        {

            var validCardHolder = new CardHolder
            {
                FirstName = "lam",
                LastName = "nguyen",
                Address = "ssdsd",
                City = "dsd",
                Email ="lam@dsdsd.com",
                State = "dsdsd",
                Zip = "dsdsd",
                PhoneNumber = "dsdsdds"
            };

            var card = new CreditCard
            {
                Number = "4111111111111111",
                ExpMonth = 12,
                ExpYear = 2025,
                Cvv = "012"
            };

            var order = new OrderDetail
            {
                 cardHolder = validCardHolder,
                 card = card,
                 Amount = 12,
                 Currency = "usd",
                 useToken=true,
                 isSaveCustomerInfor =true
            };
          //  var result = _paymentService.ProcessPayment(order);
            return View();
        }
    }
}