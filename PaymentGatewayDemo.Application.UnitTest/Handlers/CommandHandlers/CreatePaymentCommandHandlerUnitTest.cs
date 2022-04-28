using Moq;
using PaymentGatewayDemo.Core.ThirdParty.Interfaces;
using PaymentGatewayDemo.Infrastructure.Repositories;
using PaymentGatewayDemo.Application.Handlers.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Application.DTOs;
using PaymentGatewayDemo.Application.Commands;
using System.Threading;
using Xunit;
using FluentAssertions;
using PaymentGatewayDemo.Core.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace PaymentGatewayDemo.Application.UnitTest.Handlers.CommandHandlers
{
    public class CreatePaymentCommandHandlerUnitTest
    {
        private readonly Mock<IPaymentGatewayRepository> _paymentGatewayRepository;
        private readonly CreatePaymentCommandHandler _handler;       
        private readonly Mock<IBankCKO> _bankSimulator;
        private readonly Mock<ILogger<CreatePaymentCommandHandler>> _logger;
        private readonly CreatePaymentCommand command;
        private readonly PaymentDetails paymentDetails;
        public CreatePaymentCommandHandlerUnitTest()
        {
            command = GetPaymentCommandMock();
            paymentDetails = GetPaymentDetailsMock();
            _paymentGatewayRepository = new Mock<IPaymentGatewayRepository>();
            _bankSimulator = new Mock<IBankCKO>();
            _logger = new Mock<ILogger<CreatePaymentCommandHandler>>();
            _paymentGatewayRepository.Setup(x => x.AddAsync(It.IsAny<PaymentDetails>())).Returns(Task.FromResult(paymentDetails));
            _bankSimulator.Setup(b => b.VerifyPaymentWithBank(It.IsAny<PaymentDetails>())).Returns(Task.FromResult(true));
            _handler = new CreatePaymentCommandHandler(_paymentGatewayRepository.Object, _bankSimulator.Object,_logger.Object);
           
        }

        [Fact]
        public async Task CommandPaymentHandler_Is_Returning_ExpectedType()
        {

           var response= await _handler.Handle(command, CancellationToken.None);
            response.Should().BeOfType<PaymentDetailsDTO>();
          
        }

        [Fact]
        public async Task CommandPaymentHandler_Is_Returning_Expected_PaymentStatus()
        {
            string expectedPaymentStatus = "Success";
            var response = await _handler.Handle(command, CancellationToken.None);
            response.PaymentStatus.Should().BeEquivalentTo(expectedPaymentStatus);
        }

        private static CreatePaymentCommand GetPaymentCommandMock()
        {
            return new CreatePaymentCommand()
            {
                Amount = 1234,
                CVV = "123",
                CreditCard = "5241932083537011",
                Currency = "TST",
                ExpiryMonthYear = "10/22",
                MerchantID = "12345678"
            };
        }

        private static PaymentDetails GetPaymentDetailsMock()
        {
            return new PaymentDetails()
            {
                Amount = 1234,
                CVV = "123",
                CreditCard = "5241932083537011",
                Currency = "TST",
                ExpiryMonthYear = "10/22",
                MercentIdentifier = "12345678",
                RequestId = 1,
                PaymentId = "afste5151",
                PaymentStatus = "Success"
                
            };
        }
    }
}
