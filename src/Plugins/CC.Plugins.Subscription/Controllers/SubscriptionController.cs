using CC.Plugins.Subscription.Models;
using SmartStore.Services;
using SmartStore.Web.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var startMonth = new DateTime(DateTime.Now.Year, 1, 1);
            var calendarModel = InitializeCalendarView(startMonth, startMonth.AddMonths(11));
            var tupleModel = new Tuple<SubscriptionCalendarView, IEnumerable<SalonSubscriptionDetailResponse>>(calendarModel, model);
            return PartialView("_SubscriptionCardList", tupleModel);
        }

        public PartialViewResult RederCalendarView(string fromDate, string toDate)
        {
            
            if     (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
            {
                var fromArr = fromDate.Split(new char[] { '/' });
                var toArr = toDate.Split(new char[] { '/' });
                var start = new DateTime(int.Parse(fromArr[1]), int.Parse(fromArr[0]), 1);
                var end = new DateTime(int.Parse(toArr[1]), int.Parse(toArr[0]), 1);
               
                var calendarModel = InitializeCalendarView(start, end);
                                                                        
                return PartialView("_SubscriptionCalendarView", calendarModel);
            }
            return PartialView("_SubscriptionCalendarView");

        }

        private  IEnumerable<DateTime> LoopYearMonths(DateTime startDate, DateTime endDate)
        {
          //  string format = "yyyy-MM";
            //DateTime startDT = DateTime.ParseExact(startYearMonth, format, CultureInfo.InvariantCulture);
           // DateTime endDT = DateTime.ParseExact(endYearMonth, format, CultureInfo.InvariantCulture);
            while (startDate <= endDate)
            {
                yield return startDate;
                startDate = startDate.AddMonths(1);
            }
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
        public ActionResult GetFutureOfferList()
        {

            var model = new List<MarketFutureOffersResponse>();

            model.Add(new MarketFutureOffersResponse
            {
                StartDate = "04/29/2022",
                EndDate = "05/09/2022",
                App = 6,
                Email = 7,
                Estimated = 2203,
                Print = 7
            });

            model.Add(new MarketFutureOffersResponse
            {
                StartDate = "05/13/2022",
                EndDate = "05/20/2022",
                App = 2,
                Email = 6,
                Estimated = 180,
                Print = 9
            });
            model.Add(new MarketFutureOffersResponse
            {
                StartDate = "06/01/2022",
                EndDate = "06/09/2022",
                App = 6,
                Email = 10,
                Estimated = 5220,
                Print = 1
            });

            model.Add(new MarketFutureOffersResponse
            {
                StartDate = "07/01/2022",
                EndDate = "07/14/2022",
                App = 4,
                Email = 9,
                Estimated = 2120,
                Print = 6
            });

            model.Add(new MarketFutureOffersResponse
            {
                StartDate = "07/20/2022",
                EndDate = "07/29/2022",
                App = 1,
                Email = 8,
                Estimated = 1220,
                Print = 4
            });

            model.Add(new MarketFutureOffersResponse
            {
                StartDate = "08/01/2022",
                EndDate = "08/09/2022",
                App = 7,
                Email = 7,
                Estimated = 150,
                Print = 3
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

            int total = rnd.Next(5, 20);

            for (int i = 1; i <= total; i++)
            {
                model.Add(new Order
                {
                    Number = $"#{i}",
                    Date = date.AddDays(-i),
                    Status = "Completed",
                    TotalAmount = rnd.Next(1, 50)
                }); 
            }

            
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

            int total = rnd.Next(5, 20);

            for(int i = 1; i <= total; i++)
            {
                model.Add(new Order
                {
                    Number = $"#{i}",
                    Date = date.AddDays(i),
                    Status = "Pending",
                    TotalAmount = rnd.Next(1, 50)
                });;
            }


            return new JsonResult()
            {
                Data = model,
            };
        }

        public ActionResult MarketList()
        {
            string sqlQuery = @"select MarketID as id, REPLACE(STR(MarketID, 3), SPACE(1), '0') as MarketCode, MarketName
                                from dbo.tbMarket
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
            string sqlQuery = @"select p.id, m.id as MarketId, m.MarketCode, m.MarketName, p.ProgramCode, p.ProgramName, p.ShortDescription, p.LongDescription, p.NumberOfLevels, ISNULL(ms.Level,0) as Level, ms.MaxVolume 
                                    from [greatclips_api_dev].dbo.Market m
                                    cross join [greatclips_api_dev].dbo.Program p
                                    left join [greatclips_api_dev].dbo.MarketSubscription ms on m.ID = ms.fk_MarketId and ms.fk_programid = p.id
                                    where m.id = {0} and ms.Year = 2021 and p.ProgramType = 'Journey' and ProgramCode <> 'NBS'";
            return  _services.DbContext.SqlQuery<SalonSubscriptionDetailResponse>(sqlQuery, marketId).ToList();

           
        }

        private SubscriptionCalendarView InitializeCalendarView(DateTime fromDate, DateTime toDate)
        {
            var events = new List<Event>();         

            var title = "B2G";
            var monthRange = LoopYearMonths(fromDate, toDate);

            foreach (var date in monthRange)
            {
                Random rnd = new Random();
              //  var num1 = rnd.Next(1, 27);
               // var num2 = rnd.Next(1, 27);
                var total = rnd.Next(100, 3000);
                var firstMonthMonday = date.AddDays((DayOfWeek.Monday + 7 - date.DayOfWeek) % 7);
                var num1 = rnd.Next(0,3);
                var starDate = firstMonthMonday.AddDays(num1 + 7);
                var num2 = rnd.Next(2, 5);
                var endDate = starDate.AddDays(num2);
                //var starDate = new DateTime(date.Year, date.Month, 1);
                //var endDate = new DateTime(date.Year, date.Month, 3);
                if (date.Month % 3 == 0) title = "N2B";
                if (date.Month % 2 == 0) title = "P2N";
                events.Add(new Event
                {
                    id = date.Ticks.ToString(),
                    start = starDate,
                    end = endDate,
                    title = title,
                    level = 10,
                    total = total
                }); ;
            }


            var view = new SubscriptionCalendarView
            {

                StartDate = fromDate,
                EndDate = toDate,
                Events = events,
                MonthRange = monthRange
            };

            return view;
        }

        private SalonSubscriptionDetailResponse GetMarketSubscriptionDetail(int programId)
        {
            string sqlQuery = @"select p.id, m.id as MarketId, m.MarketCode, m.MarketName, p.ProgramCode, p.ProgramName, p.ShortDescription, p.LongDescription, p.NumberOfLevels, ISNULL(ms.Level,0) as Level, ms.MaxVolume 
                                    from [greatclips_api_dev].dbo.Market m
                                    cross join [greatclips_api_dev].dbo.Program p
                                    left join [greatclips_api_dev].dbo.MarketSubscription ms on m.ID = ms.fk_MarketId and ms.fk_programid = p.id
                                    where p.id = {0} and ms.Year = 2021 and p.ProgramType = 'Journey' and ProgramCode <> 'NBS'";
            return _services.DbContext.SqlQuery<SalonSubscriptionDetailResponse>(sqlQuery, programId).FirstOrDefault();


        }

    }
}