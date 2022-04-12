using CC.Plugins.Subscription.Models;
using SmartStore.Services;
using SmartStore.Web.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CC.Plugins.Subscription.Controllers
{
    public class SubscriptionController : Controller
    {
        // GET: Subscription

        private readonly ICommonServices _services;
        public SubscriptionController(ICommonServices services)
        {
            _services = services;
        }

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

        public ActionResult ListDetail(int marketId)
        {
            string sqlQuery = "select ms.Id , m.Id as MarketId, m.MarketCode, m.MarketName, p.ProgramCode, p.ProgramName, p.ShortDescription, p.LongDescription, p.NumberOfLevels, ISNULL(ms.Level,0) as Level, ms.MaxVolume " +
                                "from[greatclips_api_dev].dbo.Market m " +
                                "cross join[greatclips_api_dev].dbo.Program p " +
                                "left join[greatclips_api_dev].dbo.MarketSubscription ms on m.ID = ms.fk_MarketID and ms.fk_programid = p.id " +
                                "where m.id =" + marketId.ToString() + " and year = 2021 and p.ProgramType = 'Journey'";
            var response = _services.DbContext.SqlQuery<MarketSubscriptionResponse>(sqlQuery).ToList();
            return View(response);
        }
        
    }
}