using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;
using SmartStore.CreditCardPay.Models;
using SmartStore.Services;
using System;

namespace SmartStore.CreditCardPay.Services
{
    public class HeartlandCreditService: HeartlandBaseService, IHeartlandCreditService
    {
        private readonly IHeartlandRecurrService _recurrService;

        public HeartlandCreditService(IHeartlandRecurrService recurrService,
                                      ICommonServices _services
                                      ) : base(_services)

        {
            _recurrService = recurrService;
        }

        public HlServiceResponse VerifyCard(PaymentMethodInfo card)
        {
           
            var hlCard = new CreditCardData
            {
                Number = card.Number,
                ExpMonth = card.ExpMonth,
                ExpYear = card.ExpYear,
                Cvn = card.Cvv
            };

            var response = hlCard.Verify()
                            .WithCurrency("USD")                           
                            .Execute();

            return MapResponse(response);
        }

        public HlServiceResponse VerifyCard(PaymentMethodInfo card, CustomerInfo cardHolder)
        {

            var hlCard = new CreditCardData
            {
                Number = card.Number,
                ExpMonth = card.ExpMonth,
                ExpYear = card.ExpYear,
                Cvn = card.Cvv
            };

            var validCardHolder = new Customer
            {
                FirstName = cardHolder.FirstName,
                LastName = cardHolder.LastName,
                Address = new Address
                {
                    StreetAddress1 = cardHolder.Address,
                    City = cardHolder.City,
                    Country = cardHolder.City,
                    State = cardHolder.State,
                    PostalCode = cardHolder.Zip
                },
                MobilePhone = cardHolder.PhoneNumber ?? string.Empty
            };

            var response = hlCard.Verify()
                            .WithCurrency("USD")
                            .WithCustomer(validCardHolder)
                            .Execute();

            return MapResponse(response);

        }

        public HlServiceResponse Charge(PaymentMethodInfo card, string currency, decimal amount)
        {
            var hlCard = new CreditCardData
            {
                Number = card.Number,
                ExpMonth = card.ExpMonth,
                ExpYear = card.ExpYear,
                Cvn = card.Cvv
            };

            var response = hlCard.Charge(amount)
                            .WithCurrency(currency)
                            .Execute();

            return MapResponse(response);
        }

        //public HlServiceResponse Charge(CreditCardChargeDetailRequest cardChargeInfo)
        //{

        //    var hlCard = InitializeCard(cardChargeInfo);

        //    if (String.IsNullOrEmpty(cardChargeInfo.HlCustomerId) && cardChargeInfo.Holder != null)
        //    {
        //        cardChargeInfo.HlCustomerId = _recurrService.AddCustomer(cardChargeInfo.Holder);
        //    }

        //    if (cardChargeInfo.IsSaveCard)
        //    {
        //        cardChargeInfo.PaymentProfileId = _recurrService.AddPaymentMethod(cardChargeInfo.HlCustomerId, cardChargeInfo.Card);
        //    }

        //    var builder = hlCard.Charge(cardChargeInfo.Amount);
        //   // builder.WithP
        //    if (cardChargeInfo.WithConvenienceAmt != 0)
        //    {
        //        builder.WithConvenienceAmount(cardChargeInfo.WithConvenienceAmt);
        //    }

        //    if (!String.IsNullOrEmpty(cardChargeInfo.OrderId))
        //    {
        //        builder.WithOrderId(cardChargeInfo.OrderId);
        //    }

        //    if (cardChargeInfo.WithShippingAmt != 0)
        //    {
        //        builder.WithShippingAmt(cardChargeInfo.WithShippingAmt);
        //    }

        //    if (cardChargeInfo.WithSurchargeAmount != 0)
        //    {
        //        builder.WithSurchargeAmount(cardChargeInfo.WithSurchargeAmount);
        //    }

        //    var response = builder.WithCurrency(cardChargeInfo.Currency)
        //           .WithAmount(cardChargeInfo.Amount)
        //           .WithCustomerId(cardChargeInfo.HlCustomerId)
        //          .WithPaymentLinkId(cardChargeInfo.PaymentProfileId)
        //           .Execute();

        //    return MapResponse(response, cardChargeInfo.HlCustomerId);
            
        //}
        

        public HlServiceResponse Refund(string transactionId, decimal amount, string currency)
        {
            var trans = Transaction.FromId(transactionId)
                .Refund(amount)
                .WithCurrency(currency)
                .WithAllowDuplicates(true)
                .Execute();

            return MapResponse(trans);
        }


        public HlServiceResponse ReverseTransaction(string transactionId, decimal amount, string currency)
        {
            var trans = Transaction.FromId(transactionId)
                .Reverse(amount)
                .WithCurrency(currency)
                .WithAllowDuplicates(true)
                .Execute();

            return MapResponse(trans);
        }

        public HlServiceResponse VoildTransaction(string transactionId)
        {
            var trans = Transaction.FromId(transactionId)
                .Void()               
                .Execute();

            return MapResponse(trans);
        }

        public HlServiceResponse EditTransaction(string transactionId, decimal newAmount, string currency)
        {
            var trans = Transaction.FromId(transactionId)
                                .Edit()
                                .WithAmount(newAmount)
                                .WithCurrency(currency)
                                .Execute();

            return MapResponse(trans);
        }


        private HlServiceResponse MapResponse(Transaction sac, string customerId = "")
        {
            return new HlServiceResponse
            {
                AuthorizationCode = sac.AuthorizationCode,
                ResponseCode = sac.ResponseCode,
                ResponseText = sac.ResponseMessage,
                TransactionId = sac.TransactionId,
                HlCustomerId = customerId,
                PaymentMethodType = sac.PaymentMethodType.ToString(),
                CardType = sac.CardType
            };
        }

        private CreditCardData InitializeCard(CreditCardChargeDetailRequest cardChargeInfo)
        {
            CreditCardData card = null ;

            if (!String.IsNullOrEmpty(cardChargeInfo.Card.Token))
            {
                card = new CreditCardData
                {
                    Token = cardChargeInfo.Card.Token
                };
            }
            else
            {
                card = new CreditCardData
                {
                    Number = cardChargeInfo.Card.Number,
                    ExpMonth = cardChargeInfo.Card.ExpMonth,
                    ExpYear = cardChargeInfo.Card.ExpYear,
                    Cvn = cardChargeInfo.Card.Cvv
                };
            }

            cardChargeInfo.Card.Token = card.Tokenize(true, configName:"default", paymentMethodUsageMode: PaymentMethodUsageMode.Multiple);
           
            return card;
        }


    }
}