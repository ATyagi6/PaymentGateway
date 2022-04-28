using MediatR;
using PaymentGatewayDemo.Application.Commands;
using PaymentGatewayDemo.Application.DTOs;
using PaymentGatewayDemo.Application.Enum;
using PaymentGatewayDemo.Application.Extensions;
using PaymentGatewayDemo.Core.ThirdParty.Interfaces;
using PaymentGatewayDemo.Core.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaymentGatewayDemo.Core.Entities;

namespace PaymentGatewayDemo.Application.Handlers.CommandHandlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDetailsDTO>
    {
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;
        private readonly IBankCKO _bankCKO;
        private readonly ILogger<CreatePaymentCommandHandler> _logger;
        
        public CreatePaymentCommandHandler(IPaymentGatewayRepository paymentGatewayRepository, IBankCKO bankCKO,ILogger<CreatePaymentCommandHandler> logger)
        {
            _paymentGatewayRepository = paymentGatewayRepository;
            _bankCKO = bankCKO;
            _logger = logger;
        }

        public async Task<PaymentDetailsDTO> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            
            var paymentEntity = Mapper.PaymenDetailsCommandToEntity(request);
            if(paymentEntity is null)
            {
                _logger.LogError($"Mapping went wrong , while mapping {nameof(CreatePaymentCommand)} to {nameof(PaymentDetailsDTO)} object.");
                return null;
            }
            var status=await _bankCKO.VerifyPaymentWithBank(paymentEntity);
            if(status)
            {
                paymentEntity.PaymentStatus = PaymentStatus.Success.ToString();
            }
            else
            {
                paymentEntity.PaymentStatus = PaymentStatus.Failed.ToString();
            }
            var newPaymentDetails = await _paymentGatewayRepository.AddAsync(paymentEntity);

            if(newPaymentDetails is null)
            {
                _logger.LogError($"Something went wrong while saving the Payment details");
                return null;
            }
            
            var paymentResponse = Mapper.PaymentDetailsEntityToDTO(newPaymentDetails);


            if (newPaymentDetails is null)
            {
                _logger.LogError($"Mapping went wrong , while mapping {nameof(PaymentDetails)} to {nameof(PaymentDetailsDTO)} object.");
                return null;
            }

            return paymentResponse;
        }
    }
}
