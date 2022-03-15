using SecureSubmit.Infrastructure;
using SmartStore.Core.Data;
using SmartStore.CreditCardPay.Domain;
using SmartStore.CreditCardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay.Services
{
    public class CreditCardPaymentProcess: ICreditCardPaymentProcess
    {
        private readonly ISecureSubmitService _cardSubmitService;
        private readonly IRepository<CustomerPayment> _cusPayRepository;
        private readonly IRepository<CustomerAddress> _addrRepository;
        private readonly IRepository<CustomerProfile> _cusProfilerRepository;

        CreditCardPaymentProcess(
            IRepository<CustomerPayment> cusPayRepository,
            IRepository<CustomerAddress> addrRepository,
            IRepository<CustomerProfile> cusProfilerRepository,
            ISecureSubmitService cardSubmitService
            )
        {
            _cardSubmitService = cardSubmitService;
            _cusPayRepository = cusPayRepository;
            _addrRepository = addrRepository;
            _cusProfilerRepository = cusProfilerRepository;
        }

        public int ProcessPayment(OrderDetail order)
        {
            try
            {
                var response = _cardSubmitService.Charge(order.card, order.cardHolder, order.useToken,
                                                         order.Amount, order.Currency);

                if (response.ResponseCode == "00" && order.isSaveCustomerInfor)
                {
                    // save to database
                }

                return 0;
            }
            catch (HpsInvalidRequestException e)
            {
                throw;
            }
            catch (HpsAuthenticationException e)
            {
                // handle errors related to your HpsServiceConfig
                throw;
            }
            catch (HpsCreditException e)
            {
                // handle card-related exceptions: card declined, processing error, etc
                throw;
            }
            catch (HpsGatewayException e)
            {
                // handle gateway-related exceptions: invalid cc number, gateway-timeout, etc
                throw;
            }
            catch (Exception e)
            {
                // handle gateway-related exceptions: invalid cc number, gateway-timeout, etc
                throw;
            }
        }
    }
}