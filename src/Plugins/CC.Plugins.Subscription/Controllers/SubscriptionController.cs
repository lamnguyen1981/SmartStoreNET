using CC.Plugins.Subscription.Models;
using SmartStore.Services;
using SmartStore.Web.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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

        public ActionResult List()
        {
          
            return View();
        }

        public ActionResult MarketList()
        {
            string sqlQuery = @"select Id, MarketCode, MarketName from [greatclips_api_dev].dbo.market
                               order by MarketName";
            var response = _services.DbContext.SqlQuery<MarketResponse>(sqlQuery);


            return new JsonResult()
            {
                Data = response,                
            };
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SalonSubscriptionList([FromBody] int marketId)
        {
            string sqlQuery = @"SELECT ID, SalonCode, SalonName, Email,[P2N],[N2B],[B2G]
                                FROM  
                                (
                                  select s.ID,s.SalonCode,s.SalonName, u.Email,p.ProgramCode, ISNULL(se.NewLevel,ss.Level) as Level
	                                from [greatclips_api_dev].dbo.Salon s
	                                inner join [greatclips_api_dev].dbo.Users u on s.fk_userid_do = u.userid
	                                cross join [greatclips_api_dev].dbo.Program p
	                                left join [greatclips_api_dev].dbo.SalonSubscription ss on s.ID = ss.fk_salonid and ss.fk_programid = p.id
	                                left join [greatclips_api_dev].dbo.SalonSubscriptionEdit se on ss.id = se.fk_salonsubscriptionid
	                                where s.fk_MarketID = {0} and p.ProgramType = 'Journey'
	
	                                --order by SalonCode
                                ) AS SourceTable  
                                PIVOT  
                                (  
                                  AVG(Level)  
                                  FOR programCode IN ([P2N],[N2B],[B2G])  
                                ) AS PivotTable  
                                order by saloncode;";
            var response = _services.DbContext.SqlQuery<SalonSubscriptionResponse>(sqlQuery, marketId);

            return new JsonResult()
            {
                Data = response,
            };
        }

        public ActionResult ListDetail(int salonId)
        {
            string sqlQuery = @"select p.Id, s.Id as SalonId, s.SalonCode, s.SalonName, p.ProgramCode, p.ProgramName, p.ShortDescription, p.LongDescription, p.NumberOfLevels, ISNULL(ss.Level,0) as Level, ss.MaxVolume 
                                    from [greatclips_api_dev].dbo.Salon s
                                    cross join [greatclips_api_dev].dbo.Program p
                                    left join [greatclips_api_dev].dbo.SalonSubscription ss on s.ID = ss.fk_SalonID and ss.fk_programid = p.id
                                    where s.id = {0} and p.ProgramType = 'Journey' and ProgramCode <> 'NBS'";
            var response = _services.DbContext.SqlQuery<SalonSubscriptionDetailResponse>(sqlQuery, salonId).ToList();
            return View(response);
        }
        
    }
}