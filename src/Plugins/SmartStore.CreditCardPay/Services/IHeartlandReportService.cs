using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;

namespace SmartStore.CreditCardPay.Services
{
    public interface IHeartlandReportService
    {
        // HlServiceResponse VerifyCard(PaymentMethodInfo card);       

        IList<PaymentTransactionResponse> GetAllTransactions(string customerId, DateTime startDate, DateTime endDate);
    }
}