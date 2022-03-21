﻿using GlobalPayments.Api.Entities;
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




        public IList<PaymentTransaction> GetAllTransactions(string customerId, DateTime startDate, DateTime endDate)
        {
            var result = new List<PaymentTransaction>();
            var response = ReportingService.FindTransactions()
                        .Where(SearchCriteria.CustomerId, customerId)
                        .And(SearchCriteria.StartDate, startDate)
                        .And(SearchCriteria.EndDate, endDate)
                        .Execute();

            foreach(var tranSummary in result)
            {
                var returnTran = new PaymentTransaction
                {
                    CardType = tranSummary.CardType,
                    AdjustmentCurrency = tranSummary.AdjustmentCurrency,
                    AdjustmentAmount = tranSummary.AdjustmentAmount,
                    AdjustmentReason = tranSummary.AdjustmentReason,
                    Amount = tranSummary.Amount,
                    OrderId = tranSummary.OrderId,
                    CardHolderName = tranSummary.CardHolderName,
                    TransactionDate = tranSummary.TransactionDate,
                    Currency = tranSummary.Currency,
                    CustomerId = tranSummary.CustomerId,
                    CardMask = tranSummary.CardMask                   

                };
                result.Add(returnTran);
            }

            return result;
        }
    }
}