using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;
using GlobalPayments.Api.Services;
using SmartStore.CreditCardPay.Exceptions;
using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartStore.CreditCardPay.Services
{
    public class HeartlandRecurrService : HeartlandBaseService, IHeartlandRecurrService
    {
        public HeartlandRecurrService(CreditCardPaySettings settings)
            : base(settings)
        {

        }

        public HlResponse Charge(CreditCardChargeDetail cardChargeInfo)
        {
            RecurringPaymentMethod paymentMethod = null;

            if (cardChargeInfo.isSaveCard)
            {
                var customer =  AddCustomer(cardChargeInfo.Holder);
                cardChargeInfo.HlCustomerId = customer.Id;

                paymentMethod = AddPaymentMethod(customer, cardChargeInfo.Card);
                cardChargeInfo.PaymentProfileId = paymentMethod.Id;
            }
            else if (!String.IsNullOrEmpty(cardChargeInfo.PaymentProfileId) )
            {
                 paymentMethod = RecurringPaymentMethod.Find(cardChargeInfo.PaymentProfileId);
            }
           

            var builder = paymentMethod.Charge(cardChargeInfo.Amount);
            // builder.WithP
            if (cardChargeInfo.WithConvenienceAmt != 0)
            {
                builder.WithConvenienceAmount(cardChargeInfo.WithConvenienceAmt);
            }

            if (!String.IsNullOrEmpty(cardChargeInfo.OrderId))
            {
                builder.WithOrderId(cardChargeInfo.OrderId);
            }

            //if (cardChargeInfo.WithShippingAmt != 0)
            //{
            //    builder.WithShippingAmt(cardChargeInfo.WithShippingAmt);
            //}

            if (cardChargeInfo.WithSurchargeAmount != 0)
            {
                builder.WithSurchargeAmount(cardChargeInfo.WithSurchargeAmount);
            }

            var response = builder.WithCurrency(cardChargeInfo.Currency)
                   .WithAmount(cardChargeInfo.Amount)
                   .WithCustomerId(cardChargeInfo.HlCustomerId)
                  .WithPaymentLinkId(cardChargeInfo.PaymentProfileId)
                   .Execute();

            return MapResponse(response, cardChargeInfo.HlCustomerId, paymentMethod.Id);

        }

        
        public Customer AddCustomer(CardHolder holder)
        {
            var validCardHolder = new Customer
            {
                Id = CustomerId,
                FirstName = holder.FirstName,
                LastName = holder.LastName,
                Address = new Address
                {
                    StreetAddress1 = holder.Address,
                    City = holder.City,
                    Country = holder.Country,                   
                    PostalCode = holder.Zip,


                },
                MobilePhone = holder.PhoneNumber ?? string.Empty,
                Email = holder.Email


            }.Create();

            return validCardHolder;
        }

        public Customer FindCustomer(string CustomerId)
        {
            return Customer.Find(CustomerId);
        }

        public IList<Customer> FindAll()
        {
            return Customer.FindAll();
        }

        public string AddPaymentMethod(CardHolder customer, CreditCard card)
        {
            var cus = FindCustomer(CustomerId);
            var paymentMethod = cus.AddPaymentMethod(
                                 PaymentId("credit"),
                                 new CreditCardData
                                 {
                                     Number = card.Number,
                                     ExpMonth = card.ExpMonth,
                                     ExpYear = card.ExpYear,
                                     Cvn = card.Cvv,
                                     // Token = card.Token
                                 }
                             )

                .Create();
            return paymentMethod.Id;
        }

        public string AddPaymentMethod(string customerId, CreditCard card)
        {
            CreditCardData cardData = null;

            if (card.Token != null)
            {
                cardData = new CreditCardData { Token = card.Token };
            }
            else

            {
                cardData = new CreditCardData
                {
                    Number = card.Number,
                    ExpMonth = card.ExpMonth,
                    ExpYear = card.ExpYear,
                    Cvn = card.Cvv,
                    // Token = card.Token
                };
            };

           /* var validCard = cardData.Verify()
                             .WithCurrency("USD")
                             .WithCustomerId(customerId)
                             .Execute();

            if (validCard.ResponseCode != "00")
            {
                throw new HeartlandCustomerErrorException
                {
                    Code = ErrorCode.InvalidCardData,
                    Detail = "Card Data is invalid"
                };
            }*/

            var cus = FindCustomer(customerId);
            var paymentMethod = cus.AddPaymentMethod(
                                 PaymentId("credit"),
                                 cardData
                             )
               
                .Create();
            return paymentMethod.Id;
        }

        private RecurringPaymentMethod AddPaymentMethod(Customer customer, CreditCard card)
        {           
            var paymentMethod = customer.AddPaymentMethod(
                                 PaymentId("credit"),
                                 new CreditCardData
                                 {
                                     Number = card.Number,
                                     ExpMonth = card.ExpMonth,
                                     ExpYear = card.ExpYear,
                                     Cvn = card.Cvv,
                                     // Token = card.Token
                                 }
                             )

                .Create();
            return paymentMethod;
        }

        public string DeletePaymentMethod(string paymentProfileId)
        {
            var paymentMethod = RecurringPaymentMethod.Find(paymentProfileId);
            paymentMethod.Delete();
            return "00";
        }

        public IList<PaymentMethod> GetAllPaymentMethods(string customerId)
        {            
            var result = new List<PaymentMethod>();
            var cus = FindCustomer(customerId);
            if (cus == null) return result;

            var paymentMethod = cus.PaymentMethods;           

            for (int i =0; i < paymentMethod.Count; i++)
            {
                
                var payment = new PaymentMethod
                {
                    CardHolderName = paymentMethod[i].NameOnAccount,
                    CardType = paymentMethod[i].PaymentType,
                    ExpireDate = paymentMethod[i].ExpirationDate,
                    PaymentProfileId = paymentMethod[i].Id
                };

                var tran = ReportingService.FindTransactions()
                             .Where(SearchCriteria.PaymentMethodKey, paymentMethod[i].Key)                             
                             .Execute()
                             .FirstOrDefault();

                if (tran != null)
                {
                    payment.CardMask = tran.MaskedCardNumber;
                }

                result.Add(payment);

            }
            return result;
        }

        private string PaymentId(string type)
        {
            return string.Format("{0}-GlobalApi-{1}-{2}", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(1, 2000000), type);
        }

        private string CustomerId => string.Format("{0}-GlobalApi-{1}", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(1, 2000000));

        private HlResponse MapResponse(Transaction sac, string customerId , string paymentId)
        {
            return new HlResponse
            {
                AuthorizationCode = sac.AuthorizationCode,
                ResponseCode = sac.ResponseCode,
                ResponseText = sac.ResponseMessage,
                TransactionId = sac.TransactionId,
                HlCustomerId = customerId,
                PaymentMethodType = sac.PaymentMethodType.ToString(),
                CardType = sac.CardType,
                PaymentLinkId = paymentId
            };
        }
    }

}