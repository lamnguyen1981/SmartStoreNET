using GlobalPayments.Api.Entities;
using SmartStore.Core;
using SmartStore.Core.Data;
using SmartStore.CreditCardPay.Domain;
using SmartStore.CreditCardPay.Exceptions;
using SmartStore.CreditCardPay.Models;
using System;
using System.Linq;

namespace SmartStore.CreditCardPay.Services
{
    public class CreditCardPaymentProcess: ICreditCardPaymentProcess
    {
        private readonly IHeartlandCreditService _cardSubmitService;
        private readonly IHeartlandRecurrService _cardRecurrService;
        private readonly IRepository<CustomerPaymentProfile> _cusPayRepository; 
       // private readonly IWorkContext _workContext;

        public CreditCardPaymentProcess(
            IRepository<CustomerPaymentProfile> cusPayRepository,         
            IHeartlandCreditService cardSubmitService,
            IHeartlandRecurrService cardRecurrService
         //   IWorkContext workContext
            )
        {
            _cardSubmitService = cardSubmitService;
            _cusPayRepository = cusPayRepository;
            _cardRecurrService = cardRecurrService;
           // _workContext = workContext;
        }

        public int ProcessPayment(CreditCardChargeDetail order, int clientCustomerId)
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
           
        }
    }
}