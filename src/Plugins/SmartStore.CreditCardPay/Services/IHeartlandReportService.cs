using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public interface IHeartlandReportService
    {
        // HlResponse VerifyCard(CreditCard card);       

        IEnumerable<PaymentTransaction> GetAllTransactions(string customerId, DateTime startDate, DateTime endDate);
    }
}