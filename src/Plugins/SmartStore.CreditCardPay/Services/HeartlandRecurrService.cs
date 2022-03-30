using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;
using GlobalPayments.Api.Services;
using SmartStore.CreditCardPay.Exceptions;
using SmartStore.CreditCardPay.Models;
using SmartStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartStore.CreditCardPay.Services
{
    public class HeartlandRecurrService : HeartlandBaseService, IHeartlandRecurrService
    {
        public HeartlandRecurrService(ICommonServices _services)
            : base(_services)
        {

        }

        public HlServiceResponse Charge(CreditCardChargeDetailRequest cardChargeInfo)
        {
            /* RecurringPaymentMethod paymentMethod = null;

             if (cardChargeInfo.IsSaveCard)
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

             return MapResponse(response, cardChargeInfo.HlCustomerId, paymentMethod.Id);*/
            return null;

        }

        
        public Customer AddCustomer(CustomerInfo holder)
        {
            var validCardHolder = new Customer
            {
                Id = CustomerId,
                FirstName = holder.FirstName,
                LastName = holder.LastName,
                Address = new Address
                {
                    // StreetAddress1 = holder.Address,
                    // City = holder.City,
                    Country = holder.Country,
                    // PostalCode = holder.Zip,


                },
                // MobilePhone = holder.PhoneNumber ?? string.Empty,
                Email = holder.Email,
                Company = ""
                 


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

        public string AddPaymentMethod(CustomerInfo customer, PaymentMethodInfo card)
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
                                     CardHolderName = card.CardHolderName
                                     // Token = card.Token
                                 }
                             )

                .Create();
            return paymentMethod.Id;
        }

        public string AddPaymentMethod(string customerId, PaymentMethodInfo card)
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
                };
            };
            cardData.CardHolderName = card.CardHolderName;

            var cus = FindCustomer(customerId);
            var paymentMethod = cus.AddPaymentMethod(
                                 PaymentId("credit"),
                                 cardData
                             )
               
                .Create();
            return paymentMethod.Id;
        }

        

        public string DeletePaymentMethod(string paymentProfileId)
        {
            var paymentMethod = RecurringPaymentMethod.Find(paymentProfileId);
            paymentMethod.Delete();
            return "00";
        }

        public string EditPaymentMethod(PaymentMethodInfo payment)
        {            
            return "00";
        }

        public IList<PaymentMethodResponse> GetAllPaymentMethods(string customerId)
        {            
            var result = new List<PaymentMethodResponse>();
            var cus = FindCustomer(customerId);
            if (cus == null) return result;

            var paymentMethod = cus.PaymentMethods;           

            for (int i =0; i < paymentMethod.Count; i++)
            {
                
                var payment = new PaymentMethodResponse
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

        private RecurringPaymentMethod AddPaymentMethod(Customer customer, PaymentMethodInfo card)
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

        private string PaymentId(string type)
        {
            return string.Format("{0}-GlobalApi-{1}-{2}", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(1, 2000000), type);
        }

        private string CustomerId => string.Format("{0}-GlobalApi-{1}", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(1, 2000000));

        private HlServiceResponse MapResponse(Transaction sac, string customerId , string paymentId)
        {
            return new HlServiceResponse
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