using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;
using GlobalPayments.Api.Services;
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
                Id = CustomerId,
                FirstName = holder.FirstName,
                LastName = holder.LastName,
                Address = new Address
                {
                    StreetAddress1 = holder.Address,
                    City = holder.City,
                    Country = holder.Country,
                    State = holder.State,
                    PostalCode = holder.Zip,
                     
                     
                },
                MobilePhone = holder.PhoneNumber ?? string.Empty,
                Email = holder.Email
                  
                 
            }.Create();
            
            return validCardHolder.Id;
        }

        public Customer FindCustomer(string CustomerId)
        {
            return Customer.Find(CustomerId);
        }

        public IList<Customer> FindAll()
        {
            return Customer.FindAll();
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
                                     ExpYear = card.ExpYear,
                                     Cvn = card.Cvv,
                                    // Token = card.Token
                                 }
                             )
               
                .Create();
            return paymentMethod.Id;
        }



        public IList<PaymentMethod> GetAllPaymentMethods(string customerId)
        {            
            var result = new List<PaymentMethod>();
            var cus = FindCustomer(customerId);
            var paymentMethod = cus.PaymentMethods;           

            for (int i =0; i < paymentMethod.Count; i++)
            {
                
                var payment = new PaymentMethod
                {
                    CardHolderName = paymentMethod[i].NameOnAccount,
                    CardType = paymentMethod[i].PaymentType,
                    ExpireDate = paymentMethod[i].ExpirationDate
                };

                var tran = ReportingService.FindTransactions()
                             .Where(SearchCriteria.PaymentMethodKey, paymentMethod[i].Key)                             
                             .Execute()
                             .FirstOrDefault();

                if (tran != null) payment.CardMask = tran.MaskedCardNumber;

                result.Add(payment);

            }
            return result;
        }

        private string PaymentId(string type)
        {
            return string.Format("{0}-GlobalApi-{1}-{2}", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(1, 2000000), type);
        }

        private string CustomerId
        {
            get
            {
                return string.Format("{0}-GlobalApi-{1}", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(1,2000000));
            }
        }
    }

}