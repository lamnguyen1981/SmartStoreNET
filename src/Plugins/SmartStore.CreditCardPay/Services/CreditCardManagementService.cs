using GlobalPayments.Api.Entities;
using SmartStore.Core;
using SmartStore.Core.Data;
using SmartStore.CreditCardPay.Domain;
using SmartStore.CreditCardPay.Exceptions;
using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /*  public int ProcessPayment(CreditCardChargeDetailRequest order, int clientCustomerId)
          {
              try
              {
                  var hlCustomerId = string.Empty;
                  var customerPayment = _cusPayRepository.Table.FirstOrDefault(x => x.CustomerProfileId == clientCustomerId && x.HlCustomerProfileId != null);
                  var existingCustomerId = string.Empty;

                  if (customerPayment != null)
                  {
                      existingCustomerId = customerPayment.HlCustomerProfileId;
                      order.HlCustomerId = existingCustomerId;
                  }

                  var response = _cardRecurrService.Charge(order);                               

                  if (order.IsSaveCard)
                  {
                      var payment = new CustomerPaymentProfile
                      {
                          CreateDate = DateTime.UtcNow,
                          CustomerProfileId = clientCustomerId,
                          CustomerPaymentProfileId = response.PaymentLinkId,
                          HlCustomerProfileId = order.HlCustomerId,
                          CustomerPaymentProfileAlias = order.Card.CardAlias
                         // CreateByUser = _workContext.CurrentCustomer.Id
                      };
                      _cusPayRepository.Insert(payment);
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

          }*/
    }
}