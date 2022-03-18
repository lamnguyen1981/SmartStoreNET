using GlobalPayments.Api.Entities;
using GlobalPayments.Api.Services;
using SmartStore.Core.Configuration;
using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public class HeartlandReportService :  IHeartlandReportService
    {
        public HeartlandReportService()
        {

        }

        //public HeartlandReportService(CreditCardPaySettings settings): base(settings)
        //{


        //   // cardSubmitService = new HpsFluentCreditService(ServicesConfig);
        //}




        public IEnumerable<PaymentTransaction> GetAllTransactions(string customerId, DateTime startDate, DateTime endDate)
        {
            return null;
        }
    }
}