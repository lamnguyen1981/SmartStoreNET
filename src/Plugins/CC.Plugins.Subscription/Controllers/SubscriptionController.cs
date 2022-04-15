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

        public ActionResult SubscriptionDetail(int programId)
        {
            var model = GetMarketSubscriptionDetail(programId);
            return View(model);
        }

        public PartialViewResult MarketSubscriptionList(int marketId)
        {
            var model = GetMarketSubscriptionList(marketId);
            return PartialView("_SubscriptionCardList", model);
        }

        public ActionResult OfferList()
        {            

            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult GetOfferList()
        {
            var model = new List<MarketOffersResponse>();

            model.Add(new MarketOffersResponse
            {
                Vehicle = "Print",
                OfferCode = "N2BSPW2",
                OfferDescription = "2nd Skipped Personal Week",
                DefaultAmount = "$6.99",
                MarketRecommendation = ""
            });

            model.Add(new MarketOffersResponse
            {
                Vehicle = "Email",
                OfferCode = "N2BSPW2",
                OfferDescription = "2nd Skipped Personal Week",
                DefaultAmount = "$6.99",
                MarketRecommendation = "$7.99"
            });
            model.Add(new MarketOffersResponse
            {
                Vehicle = "App",
                OfferCode = "N2BSPW2",
                OfferDescription = "2nd Skipped Personal Week",
                DefaultAmount = "$6.99",
                MarketRecommendation = ""
            });

            model.Add(new MarketOffersResponse
            {
                Vehicle = "Print",
                OfferCode = "N2B2Step",
                OfferDescription = "2nd Visit",
                DefaultAmount = "$5 Off",
                MarketRecommendation = ""
            });

            model.Add(new MarketOffersResponse
            {
                Vehicle = "Email",
                OfferCode = "N2B2Step",
                OfferDescription = "2nd Visit",
                DefaultAmount = "$5 Off",
                MarketRecommendation = ""
            });

            model.Add(new MarketOffersResponse
            {
                Vehicle = "App",
                OfferCode = "N2B2Step",
                OfferDescription = "2nd Visit",
                DefaultAmount = "$5 Off",
                MarketRecommendation = ""
            });


            return new JsonResult()
            {
                Data = model,
            };
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult GetOrderHistoryList()
        {
            var model = new List<Order>();
            Random rnd = new Random();
            var date = DateTime.Now.Date;

            model.Add(new Order
            {
                Number = "#1",
                Date = date.AddDays(-rnd.Next(1, 15)),
                Status = "Completed",
                TotalAmount = 25
            });

            model.Add(new Order
            {
                Number = "#2",
                Date = date.AddDays(-rnd.Next(1, 15)),
                Status = "Cancelled",
                TotalAmount = 12
            });

            model.Add(new Order
            {
                Number = "#3",
                Date = date.AddDays(-rnd.Next(1, 15)),
                Status = "Completed",
                TotalAmount = 40
            });

            model.Add(new Order
            {
                Number = "#4",
                Date = date.AddDays(-rnd.Next(1, 15)),
                Status = "Completed",
                TotalAmount = 36
            });

            model.Add(new Order
            {
                Number = "#5",
                Date = date.AddDays(-rnd.Next(1, 15)),
                Status = "Cancelled",
                TotalAmount = 8
            });

            return new JsonResult()
            {
                Data = model,
            };
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult GetOrderPendingList()
        {
            var model = new List<Order>();
            Random rnd = new Random();
            var date = DateTime.Now.Date;

            model.Add(new Order
            {
                Number = "#1",
                Date = date.AddDays(rnd.Next(1, 15)),
                Status = "Pending",
                TotalAmount = 25
            });

            model.Add(new Order
            {
                Number = "#2",
                Date = date.AddDays(rnd.Next(1, 15)),
                Status = "Pending",
                TotalAmount = 12
            });

            model.Add(new Order
            {
                Number = "#3",
                Date = date.AddDays(rnd.Next(1, 15)),
                Status = "Pending",
                TotalAmount = 40
            });

            model.Add(new Order
            {
                Number = "#4",
                Date = date.AddDays(rnd.Next(1, 15)),
                Status = "Pending",
                TotalAmount = 36
            });

            model.Add(new Order
            {
                Number = "#5",
                Date = date.AddDays(rnd.Next(1, 15)),
                Status = "Pending",
                TotalAmount = 8
            });

            return new JsonResult()
            {
                Data = model,
            };
        }

        public ActionResult MarketList()
        {
            string sqlQuery = @"select Id, REPLACE(STR(MarketCode, 3), SPACE(1), '0') as MarketCode, MarketName from [greatclips_api_dev].dbo.market
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

        private IEnumerable<SalonSubscriptionDetailResponse> GetMarketSubscriptionList(int marketId )
        {
            string sqlQuery = @"select p.Id, m.Id as MarketId, m.MarketCode, m.MarketName, p.ProgramCode, p.ProgramName, p.ShortDescription, p.LongDescription, p.NumberOfLevels, ISNULL(ms.Level,0) as Level, ms.MaxVolume 
                                    from [greatclips_api_dev].dbo.Market m
                                    cross join [greatclips_api_dev].dbo.Program p
                                    left join [greatclips_api_dev].dbo.MarketSubscription ms on m.ID = ms.fk_MarketId and ms.fk_programid = p.id
                                    where m.id = {0} and ms.Year = 2021 and p.ProgramType = 'Journey' and ProgramCode <> 'NBS'";
            return  _services.DbContext.SqlQuery<SalonSubscriptionDetailResponse>(sqlQuery, marketId).ToList();

           
        }

        private SalonSubscriptionDetailResponse GetMarketSubscriptionDetail(int programId)
        {
            string sqlQuery = @"select p.Id, m.Id as MarketId, m.MarketCode, m.MarketName, p.ProgramCode, p.ProgramName, p.ShortDescription, p.LongDescription, p.NumberOfLevels, ISNULL(ms.Level,0) as Level, ms.MaxVolume 
                                    from [greatclips_api_dev].dbo.Market m
                                    cross join [greatclips_api_dev].dbo.Program p
                                    left join [greatclips_api_dev].dbo.MarketSubscription ms on m.ID = ms.fk_MarketId and ms.fk_programid = p.id
                                    where p.Id = {0} and ms.Year = 2021 and p.ProgramType = 'Journey' and ProgramCode <> 'NBS'";
            return _services.DbContext.SqlQuery<SalonSubscriptionDetailResponse>(sqlQuery, programId).FirstOrDefault();


        }

    }
}