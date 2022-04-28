using MediatR;
using Microsoft.Extensions.Logging;
using PaymentGatewayDemo.Application.Commands;
using PaymentGatewayDemo.Application.Queries;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Core.Models;
using PaymentGatewayDemo.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Application.Handlers.CommandHandlers
{
    class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, Tokens>
    {
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;
        private readonly IAuthenticationRepository _authentication;
        private readonly ILogger<CreateTokenCommandHandler> _logger;
        public CreateTokenCommandHandler(IPaymentGatewayRepository paymentGatewayRepository, IAuthenticationRepository authentication, ILogger<CreateTokenCommandHandler> logger)
        {
            _paymentGatewayRepository = paymentGatewayRepository;
            _authentication = authentication;
            _logger = logger;
        }
        public async Task<Tokens> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var merchantDetail = await _paymentGatewayRepository.GetMerchant(request.ClientId);
            if (merchantDetail is null)
            {
                return null;
            }
            var token = _authentication.Authenticate(merchantDetail);
            return token;
        }
    }
}
