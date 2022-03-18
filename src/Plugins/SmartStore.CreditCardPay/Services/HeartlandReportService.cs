using GlobalPayments.Api.Entities;
using GlobalPayments.Api.Services;
using SecureSubmit.Entities;
using SecureSubmit.Fluent.Services;
using SecureSubmit.Services;
using SmartStore.Core.Configuration;
using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public class HeartlandReportService : HeartlandBaseService, IHeartlandReportService
    {


        public HeartlandReportService(CreditCardPaySettings settings): base(settings)
        {
            

           // cardSubmitService = new HpsFluentCreditService(ServicesConfig);
        }
    

       

        public IEnumerable<PaymentTransaction> GetAllTransactions(string customerId, DateTime startDate, DateTime endDate)
        {
            return null;
        }
    }
}