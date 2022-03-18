using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;
using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public class HeartlandRecurrService : HeartlandBaseService, IHeartlandRecurrService
    {
        public HeartlandRecurrService(CreditCardPaySettings settings)
            : base(settings)
        {

        }

        public string AddCustomer(CardHolder holder)
        {
            var validCardHolder = new Customer
            {
                FirstName = holder.FirstName,
                LastName = holder.LastName,
                Address = new Address
                {
                    StreetAddress1 = holder.Address,
                    City = holder.City,
                    Country = holder.City,
                    State = holder.State,
                    PostalCode = holder.Zip
                },
                MobilePhone = holder.PhoneNumber ?? string.Empty
            }.Create();
            
            return validCardHolder.Id;
        }

        public Customer FindCustomer(string CustomerId)
        {
            return Customer.Find(CustomerId);
        }

        public string AddPaymentMethod(string CustomerId, CreditCard card)
        {
            var cus = FindCustomer(CustomerId);
            var paymentMethod = cus.AddPaymentMethod(
                                 PaymentId("credit"),
                                 new CreditCardData
                                 {
                                     Number = card.Number,
                                     ExpMonth = card.ExpMonth,
                                     ExpYear = card.ExpYear
                                 }
                             ).Create();
            return paymentMethod.Id;
        }

        public IEnumerable<PaymentMethod> GetAllPaymentMethods(string CustomerId)
        {
            var cus = FindCustomer(CustomerId);
            var paymentMethod = cus.PaymentMethods;

            for(int i =0; i < paymentMethod.Count; i++)
            {
                var payment = new PaymentMethod
                {
                    CardHolderName = paymentMethod[i].NameOnAccount,
                    CardType = paymentMethod[i].PaymentType,
                    ExpireDate = paymentMethod[i].ExpirationDate
                };

                yield return payment;

            }
           
        }

        private string PaymentId(string type)
        {
            return string.Format("{0}-GlobalApi-{1}", DateTime.Now.ToString("yyyyMMdd"), type);
        }
    }

}