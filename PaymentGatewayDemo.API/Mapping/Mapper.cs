using PaymentGatewayDemo.API.Interface;
using PaymentGatewayDemo.API.Request;
using PaymentGatewayDemo.API.Responses;
using PaymentGatewayDemo.Application.Commands;
using PaymentGatewayDemo.Application.DTOs;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Core.Exceptions;
using PaymentGatewayDemo.Core.Helper;
using PaymentGatewayDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.API.Mapping
{
   public class Mapper: IMapper
    {
        public  CreatePaymentCommand PaymentRequestToPaymentCommand( PaymentRequest paymentRequest,string merchantId)
        {
            if (paymentRequest is null || String.IsNullOrEmpty(merchantId))
            {
                return null;
            }
            return new CreatePaymentCommand
            {
                Amount = paymentRequest.Amount,
                CreditCard = paymentRequest.CreditCard,
                Currency = paymentRequest.Currency,
                CVV = paymentRequest.CVV,
                ExpiryMonthYear = paymentRequest.ExpiryMonthYear,
                MerchantID = merchantId
            };
        }

        public  PaymentResponse PaymentDTOToPaymentResponse( PaymentDetailsDTO dto)
        {
            if (dto is null)
            {
                return null;
            }
            return new PaymentResponse
            {
                PaymentId =dto.PaymentId,
                Amount = dto.Amount,
                CreditCard = dto.CreditCard,
                Currency = dto.Currency,
                CVV = dto.CVV,
                ExpiryMonthYear = dto.ExpiryMonthYear,
                PaymentDate=dto.PaymentDate,
                PaymentStatusCode=dto.PaymentStatus=="Success"? 1 : 0
               

            };
        }

        public AuthenticationResponse TokensToAuthenticationResponse (Tokens token)
        {
            if (token is null) return null;

            return new AuthenticationResponse
            {
                Token = token.Token
            };
        }

        public CreateTokenCommand AuthenticationRequestToCommand(AuthenticationRequest request)
        {
            if (request is null) return null;

            return new CreateTokenCommand
            {
                ClientId = request.ClientId
            };
        }
    }
}
