
using SmartStore.Services;
using SmartStore.Web.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CC.Plugins.Location.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location

        private readonly ICommonServices _services;
       

        public ActionResult Index()
        {
            return View();
        }

        [AdminAuthorize]
        [ChildActionOnly]
        
        public ActionResult Configure()
        {
           

            return View();
        }

        public ActionResult List()
        {
          
            return View();
        }

      

    }
}