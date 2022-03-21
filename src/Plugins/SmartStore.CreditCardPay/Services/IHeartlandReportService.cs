using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;

namespace SmartStore.CreditCardPay.Services
{
    public interface IHeartlandReportService
    {
        // HlResponse VerifyCard(CreditCard card);       

        IList<PaymentTransaction> GetAllTransactions(string customerId, DateTime startDate, DateTime endDate);
    }
}