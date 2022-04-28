using FluentAssertions;
using Moq;
using PaymentGatewayDemo.API.Request;
using PaymentGatewayDemo.API.Responses;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.ThirdParty.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGatewayDemo.IntegrationTesting
{
    public class PaymentGatewayTests : IntegrationTest
    {
       
        private const string GETPAYMENTURL= @"api/PaymentGateway/Payment/";
        [Fact]
        public async Task GetPayment_WithNotExistsPaymentId_Returns_NotFoundResponse()
        {
            //Arrange 
            await AuthenticateRequest();
            string paymentId = "12345";
            //Act 
            var response = await TestClient.GetAsync(requestUri: String.Concat(GETPAYMENTURL,paymentId));
            //Assert
            response.StatusCode.Should().Be(expected: System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetPayment_WithExistPaymentId_Returns_SuccessResponse()
        {
            //Arrange 
            await AuthenticateRequest();
          
            var response = await TestClient.PostAsJsonAsync(requestUri: "api/PaymentGateway/Payment", new PaymentRequest { CreditCard = "123456789987654", CVV = "123", Amount = 1220 ,ExpiryMonthYear = "12/24", Currency = "GBP" });
            var paymentResponse = await response.Content.ReadAsAsync<PaymentResponse>();
            string paymentId = paymentResponse.PaymentId;

            //Act 
            var result = await TestClient.GetAsync(requestUri: String.Concat(GETPAYMENTURL, paymentId));

            //Assert
            result.StatusCode.Should().Be(expected: System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPayment_WithOut_Token_Returns_UnauthorizedResponse()
        {
            //Arrange
            string paymentId = "1234";
            //Act 
            var result = await TestClient.GetAsync(requestUri: String.Concat(GETPAYMENTURL, paymentId));

            //Assert
            result.StatusCode.Should().Be(expected: System.Net.HttpStatusCode.Unauthorized);
        }

    }
}
