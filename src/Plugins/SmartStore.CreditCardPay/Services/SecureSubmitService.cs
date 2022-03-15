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
    public class SecureSubmitService : ISecureSubmitService
    {
        private readonly HpsFluentCreditService cardSubmitService;

        SecureSubmitService(CreditCardPaySettings settings)
        {
            HpsServicesConfig ServicesConfig = new HpsServicesConfig
            {
                SecretApiKey = settings.SecretKey
            };

            cardSubmitService = new HpsFluentCreditService(ServicesConfig);
        }
    

        public HlResponse VerifyCard(CreditCard card, CardHolder cardHolder)
        {            
            var visaCard = new HpsCreditCard
            {
                Number = card.Number,
                ExpMonth = card.ExpMonth,
                ExpYear = card.ExpYear
            };

            var validCardHolder = new HpsCardHolder
            {
                FirstName = cardHolder.FirstName,
                LastName = cardHolder.LastName,
                Address = new HpsAddress {  Address = cardHolder.Address,
                                            City = cardHolder.City, Country = cardHolder.City,
                                            State = cardHolder.State, Zip= cardHolder.Zip},
                Phone = cardHolder.PhoneNumber ?? string.Empty
            };

            var response = cardSubmitService.Verify()
                 .WithCard(visaCard)
                 .WithRequestMultiUseToken(false)
                 .Execute();

            return new HlResponse
            {
                AuthorizationCode = response.AuthorizationCode,
                ResponseCode = response.ResponseCode,
                ResponseText = response.ResponseText
            };
        }

        public HlResponse Charge(CreditCard card, CardHolder cardHolder, bool useToken, decimal amount, string currency)
        {
            var visaCard = new HpsCreditCard
            {
                Number = card.Number,
                ExpMonth = card.ExpMonth,
                ExpYear = card.ExpYear,
                Cvv = card.Cvv
            };

            var validCardHolder = new HpsCardHolder
            {
                FirstName = cardHolder.FirstName,
                LastName = cardHolder.LastName,
                Address = new HpsAddress
                {
                    Address = cardHolder.Address,
                    City = cardHolder.City,
                    Country = cardHolder.City,
                    State = cardHolder.State,
                    Zip = cardHolder.Zip
                },
                Phone = cardHolder.PhoneNumber ?? string.Empty
            };

            var response = cardSubmitService.Charge(amount)
                .WithCard(visaCard)
                .WithCurrency(currency)
                .WithCardHolder(validCardHolder)
                .WithRequestMultiUseToken(useToken)
                .WithAllowDuplicates(true)
                .Execute();

            return new HlResponse
            {
                AuthorizationCode = response.AuthorizationCode,
                ResponseCode = response.ResponseCode,
                ResponseText = response.ResponseText
                //Token = 
            };

        }
    }
}