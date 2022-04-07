using GlobalPayments.Api.Entities;
using SmartStore.Core;
using SmartStore.Core.Data;
using SmartStore.CreditCardPay.Domain;
using SmartStore.CreditCardPay.Exceptions;
using SmartStore.CreditCardPay.Models;
using SmartStore.Linq;
using SmartStore.Services.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartStore.CreditCardPay.Services
{
    public class CreditCardManagementService: ICreditCardManagementService
    {        
        private readonly IHeartlandRecurrService _cardRecurrService;
        private readonly IRepository<CCCustomerPaymentProfile> _cusPayRepository;
        private readonly IRepository<CCCustomerProfile> _cusRepository;
        

        public CreditCardManagementService(
            IRepository<CCCustomerPaymentProfile> cusPayRepository,
            IRepository<CCCustomerProfile> cusRepository,            
            IHeartlandRecurrService cardRecurrService            
            )
        {           
            _cusPayRepository = cusPayRepository;
            _cardRecurrService = cardRecurrService;
            _cusRepository = cusRepository;           
        }

        public IList<PaymentMethodResponse> GetAllPaymentMethods(int customerId)
        {            
            var customer = _cusRepository.Table.FirstOrDefault(x => x.CustomerId == customerId);

            if (customer == null)
                return null;
            
            var cardList = _cardRecurrService.GetAllPaymentMethods(customer.HlCustomerProfileId);
            return cardList;
        }

        public string AddPaymentMethod(HeartlandRequestBase request)
        {            
            string hlCustomerId = String.Empty;
            var ccCustomerProfile = _cusRepository.Table.FirstOrDefault(x => x.CustomerId == request.customerId);            

            if (ccCustomerProfile != null)
                hlCustomerId = ccCustomerProfile.HlCustomerProfileId;
            else
            {
                hlCustomerId = _cardRecurrService.AddCustomer(request.CardHolder).Id;
                ccCustomerProfile = new CCCustomerProfile
                {
                    CustomerId = request.customerId,
                    HlCustomerProfileId = hlCustomerId,
                    CreateByUser = request.customerId,
                    CreatedOnUtc = DateTime.UtcNow

                };
                _cusRepository.Insert(ccCustomerProfile);
            }

            var paymentProfileId = _cardRecurrService.AddPaymentMethod(hlCustomerId, request.Card);            

            if (!String.IsNullOrEmpty(paymentProfileId))
            {
                var payment = new CCCustomerPaymentProfile
                {
                     CreatedOnUtc = DateTime.UtcNow,
                     CCustomerProfileId = ccCustomerProfile.Id,
                     CustomerPaymentProfileId = paymentProfileId,
                     CreatedByUser = request.customerId,
                     CustomerPaymentProfileAlias = request.Card.CardAlias
                }; 
                
                _cusPayRepository.Insert(payment);
            }
            
            return paymentProfileId;
        }

        public string DeletePaymentMethod(string paymentProfileId)
        {           
            _cardRecurrService.DeletePaymentMethod(paymentProfileId);

            var customerPayment = _cusPayRepository.Table.FirstOrDefault(x => x.CustomerPaymentProfileId == paymentProfileId);
            if (customerPayment != null)
            {
                _cusPayRepository.Delete(customerPayment);
            }
            return "00";
        }

        public string EditPaymentMethod(PaymentMethodInfo payment)
        {            
            return "00";
        }

          public int Charge(CreditCardChargeDetailRequest order)
          {
              try
              {
                var customer = (from pay in _cusPayRepository.Table
                            join cus in _cusRepository.Table on pay.CCustomerProfileId equals cus.Id
                            where pay.CustomerPaymentProfileId == order.Card.PaymentProfileId
                            select new { cus.HlCustomerProfileId }).FirstOrDefault();

                if (customer != null)
                {
                    order.CardHolder.HlCustomerId = customer.HlCustomerProfileId;
                    var response = _cardRecurrService.Charge(order);

                }
                else
                {
                    throw new HeartlandCustomerErrorException
                    {
                        Code = ErrorCode.InvalidCardData,
                        Detail = "Can not find customer"
                    };
                }
                              
                return 0;

              }
              catch (BuilderException e)
              {
                  throw new HeartlandCustomerErrorException
                  {
                      Code = ErrorCode.BuilderException,
                      Detail = e.Message
                  };
              }
              catch (ConfigurationException e)
              {
                  throw new HeartlandCustomerErrorException
                  {
                      Code = ErrorCode.ConfigurationException,
                      Detail = e.Message
                  };
              }
              catch (GatewayException e)
              {
                  throw new HeartlandCustomerErrorException
                  {
                      Code = ErrorCode.GatewayException,
                      Detail = e.Message
                  };
              }
              catch (UnsupportedTransactionException e)
              {
                  throw new HeartlandCustomerErrorException
                  {
                      Code = ErrorCode.UnsupportedTransactionException,
                      Detail = e.Message
                  };
              }
              catch (Exception)
              {
                  throw;
              }

          }

        
        public IList<PaymentMethodResponse> SearchPaymentMethod(PaymentMethodSearchCondition request)
        {
            var result = new List<PaymentMethodResponse>();
            var cusList = (from pay in _cusPayRepository.Table
                        join cus in _cusRepository.Table on pay.CCustomerProfileId equals cus.Id                       
                        select new { cus.HlCustomerProfileId, pay.CustomerPaymentProfileId, pay.CustomerPaymentProfileAlias }).ToList();
                     

            foreach (var cus in cusList.GroupBy(x=>x.HlCustomerProfileId))
            {
                var payments = _cardRecurrService.GetAllPaymentMethods(cus.Key);
                if (payments != null)
                {
                    foreach(var payment in payments)
                    {
                        var alias = cusList.FirstOrDefault(x => x.CustomerPaymentProfileId == payment.PaymentProfileId);
                        if (alias != null)
                            payment.CardAlias = alias.CustomerPaymentProfileAlias;
                    }
                    result.AddRange(payments);
                }
                   
            }
            
            IEnumerable<PaymentMethodResponse> filter = result;
            cusList = null; result = null;

            if (!String.IsNullOrEmpty(request.LastName))               
                filter = filter.Where(x => x.LastName.Contains(request.LastName));

            if (!String.IsNullOrEmpty(request.FirstName))              
                filter = filter.Where(x => x.FirstName.Contains(request.FirstName));
            

            if (!String.IsNullOrEmpty(request.Email))                
                filter = filter.Where(x => x.Email.Contains(request.Email));
            
            if (!String.IsNullOrEmpty(request.CardAlias))                
                filter = filter.Where(x => x.CardAlias.Contains(request.CardAlias));            

            if (!String.IsNullOrEmpty(request.CardMask))                
                filter = filter.Where(x => x.CardMask.Contains(request.CardMask));

            if (!String.IsNullOrEmpty(request.CardType))                
                filter = filter.Where(x => x.CardType== request.CardType);



            return filter.Skip(request.PageSize * (request.PageIndex - 1))
                          .Take(request.PageSize).ToList(); ;
        }

    }

    
}
