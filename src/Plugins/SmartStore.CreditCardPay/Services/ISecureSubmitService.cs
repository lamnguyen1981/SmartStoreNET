using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public interface ISecureSubmitService
    {
       // HlResponse VerifyCard(CreditCard card);

        HlResponse VerifyCard(CreditCard card, CardHolder holder);

        HlResponse Charge(CreditCard card, CardHolder holder, bool useToken, decimal amount, string currency);

    }
}