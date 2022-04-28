
using MediatR;
using Microsoft.Extensions.Logging;
using PaymentGatewayDemo.Application.DTOs;
using PaymentGatewayDemo.Application.Extensions;
using PaymentGatewayDemo.Application.Queries;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Application.Handlers.QueryHandlers
{
    public class GetPreviousPaymentDetailsHandler: IRequestHandler<GetPreviousPaymentDetailsQuery, PaymentDetailsDTO>
    {
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;
        private readonly ILogger<GetPreviousPaymentDetailsHandler> _logger;

        public GetPreviousPaymentDetailsHandler(IPaymentGatewayRepository paymentGatewayRepository, ILogger<GetPreviousPaymentDetailsHandler> logger)
        {
            _paymentGatewayRepository = paymentGatewayRepository;
            _logger = logger;
        }

        public async Task<PaymentDetailsDTO> Handle(GetPreviousPaymentDetailsQuery request, CancellationToken cancellationToken)
        {
            var paymentDetail=   await _paymentGatewayRepository.GetPaymentDetailsByPaymentIdentifier(request.PaymentId);
            if(paymentDetail is null)
            {
                _logger.LogError("Something Went wrong while getting the payment details.");
                return null;
            }
            return Mapper.PaymentDetailsEntityToDTO(paymentDetail);

        }
    }
}
