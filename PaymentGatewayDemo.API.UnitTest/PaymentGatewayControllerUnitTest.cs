using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGatewayDemo.API.Controllers;
using PaymentGatewayDemo.API.Interface;
using PaymentGatewayDemo.API.Mapping;
using PaymentGatewayDemo.API.Request;
using PaymentGatewayDemo.API.Responses;
using PaymentGatewayDemo.Application.Commands;
using PaymentGatewayDemo.Application.DTOs;
using PaymentGatewayDemo.Application.Queries;
using PaymentGatewayDemo.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGatewayDemo.API.UnitTest
{
    public class PaymentGatewayControllerUnitTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<PaymentGatewayController>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly PaymentGatewayController _controller;
        private readonly PaymentDetailsDTO dto;
        private readonly PaymentRequest request;
        private readonly PaymentResponse response;
        private readonly CreatePaymentCommand command;
        private readonly MerchantClaim _merchant;
        public  PaymentGatewayControllerUnitTest()
        {
             
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<PaymentGatewayController>>();
             _mapper = new Mock<IMapper>();
             response = GetResponseMock();
             command =  GetPaymentCommandMock();
             dto =      GetPaymentDTOMock();
             request  = GetRequestMock();
            _merchant = new MerchantClaim{ MerchantId = "12345678" };


            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Merchant-ID"] = "12345678";
                  
            _mapper.Setup(x => x.PaymentRequestToPaymentCommand(request, "12345678")).Returns(command);
            _mapper.Setup(x => x.PaymentDTOToPaymentResponse(dto)).Returns(response);
            _controller = new PaymentGatewayController(_mediator.Object, _logger.Object, _mapper.Object, _merchant);
          

            
        }

        [Fact]
        public async Task CreatePayment_Should_Return_Correct_Type()
        {
           
            var result = await _controller.CreatePayment(request);
            result.Should().BeOfType<ActionResult<PaymentResponse>>();
        }

        [Fact]
        public async Task CreatePayment_Should_Return_Correct_StatusCode()
        {
            int expectedStatusCode = StatusCodes.Status201Created;
            _mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Returns(Task.FromResult(dto));
            var result = await _controller.CreatePayment(request);
            var actualStatusCode=((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode;
            actualStatusCode.Should().Be(expectedStatusCode);        
        }
        

        [Fact]
        public async Task CreatePayment_Should_Return_Correct_ResponseValues()
        {
            _mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Returns(Task.FromResult(dto));
            var result = await _controller.CreatePayment(request);
            var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value;
            actualResult.Should().BeEquivalentTo(response);



        }

        [Fact]
        public async Task GetPayment_Should_Return_NotFound_StatusCode()
        {
            string paymentId = "123456789";
            int expectedStatusCode = StatusCodes.Status404NotFound;
            _mediator.Setup(x => x.Send(new GetPreviousPaymentDetailsQuery { PaymentId = paymentId }, It.IsAny<CancellationToken>())).Returns(Task.FromResult(dto));
            var result = await _controller.GetPayment(paymentId);
            var actualStatusCode = ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode;
            actualStatusCode.Should().Be(expectedStatusCode);

        }

        private static PaymentRequest GetRequestMock()
        {
             return  new PaymentRequest()
            {
                 Amount = 1234,
                 CVV = "123",
                 CreditCard = "5241932083537011",
                 Currency = "TST",
                 ExpiryMonthYear = "10/22"
            };
        }

        private static PaymentResponse GetResponseMock()
        {
            return new PaymentResponse()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                Currency = "TST",
                CVV = "123",              
                ExpiryMonthYear = "10/22",
                PaymentId = "123456",
                PaymentDate = Convert.ToDateTime("12/12/2017"),          
                PaymentStatusCode = 1
            
            };
        }

        private static PaymentDetailsDTO GetPaymentDTOMock()
        {
            return  new PaymentDetailsDTO()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                Currency = "TST",
                CVV = "123",             
                ExpiryMonthYear = "10/22",
                PaymentId="123456",
                PaymentDate = Convert.ToDateTime("12/12/2017"),
                PaymentStatus="Success"

            };
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
                MerchantID ="12345678"
            };
        }
    }
}
