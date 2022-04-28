using PaymentGatewayDemo.API.Request;
using PaymentGatewayDemo.API.Responses;
using PaymentGatewayDemo.Application.Commands;
using PaymentGatewayDemo.Application.DTOs;
using PaymentGatewayDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.API.Interface
{
    public interface IMapper
    {
        public  CreatePaymentCommand PaymentRequestToPaymentCommand( PaymentRequest paymentRequest, string merchantId);
        public PaymentResponse PaymentDTOToPaymentResponse(PaymentDetailsDTO dto);
        public AuthenticationResponse TokensToAuthenticationResponse(Tokens token);
        public CreateTokenCommand AuthenticationRequestToCommand(AuthenticationRequest request);
    }
}
