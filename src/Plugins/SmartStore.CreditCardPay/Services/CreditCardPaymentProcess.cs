using GlobalPayments.Api.Entities;
using SmartStore.Core.Data;
using SmartStore.CreditCardPay.Domain;
using SmartStore.CreditCardPay.Exceptions;
using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public class CreditCardPaymentProcess: ICreditCardPaymentProcess
    {
        private readonly IHeartlandCreditService _cardSubmitService;
        private readonly IRepository<CustomerPayment> _cusPayRepository;
      //  private readonly IRepository<CustomerAddress> _addrRepository;
      //  private readonly IRepository<CustomerProfile> _cusProfilerRepository;

        public CreditCardPaymentProcess(
            IRepository<CustomerPayment> cusPayRepository,
          //  IRepository<CustomerAddress> addrRepository,
          //  IRepository<CustomerProfile> cusProfilerRepository,
            IHeartlandCreditService cardSubmitService
            )
        {
            _cardSubmitService = cardSubmitService;
            _cusPayRepository = cusPayRepository;
           // _addrRepository = addrRepository;
          //  _cusProfilerRepository = cusProfilerRepository;
        }

        public int ProcessPayment(CreditCardChargeDetail order, int clientCustomerId)
        {
            try
            {
                // var hlCustomerId = string.Empty;
                var customerIds = _cusPayRepository.Table.FirstOrDefault(x => x.CustomerProfileId == clientCustomerId && x.HlCustomerProfileId != null);
                var existingCustomerId = string.Empty;

                if (customerIds != null)
                {
                    existingCustomerId = customerIds.HlCustomerProfileId;
                    order.HlCustomerId = existingCustomerId;
                }

                var response = _cardSubmitService.Charge(order);                               

                var payment = new CustomerPayment
                {
                    CreateDate = DateTime.UtcNow,
                    CustomerProfileId = clientCustomerId,                  
                    TransactionId = response.TransactionId,
                    PaymentMethodType = response.PaymentMethodType.ToString()

                 };

                if (String.IsNullOrEmpty(existingCustomerId))
                     payment.HlCustomerProfileId = response.HlCustomerId;
                 _cusPayRepository.Insert(payment);
                return 0;
              
            }
            catch (BuilderException e)
            {
                throw new HeartlandCustomErrorException
                {
                    Code = ErrorCode.BuilderException,
                    Detail = e.Message
                };
            }
            catch (ConfigurationException e)
            {
                throw new HeartlandCustomErrorException
                {
                    Code = ErrorCode.ConfigurationException,
                    Detail = e.Message
                };
            }
            catch (GatewayException e)
            {
                throw new HeartlandCustomErrorException
                {
                    Code = ErrorCode.GatewayException,
                    Detail = e.Message
                };
            }
            catch (UnsupportedTransactionException e)
            {
                throw new HeartlandCustomErrorException
                {
                    Code = ErrorCode.UnsupportedTransactionException,
                    Detail = e.Message
                };
            }
            catch (Exception e)
            {
                throw;
            }
           
        }
    }
}