using FluentValidation.TestHelper;
using PaymentGatewayDemo.API.Request;
using PaymentGatewayDemo.API.Validators;
using PaymentGatewayDemo.Application.Commands;
using Xunit;

namespace PaymentGatewayDemo.API.UnitTest.ValidatorUnitTests
{
    public class PaymentRequestValidatorUnitTests
    {
        private readonly PaymentRequestValidator _validator;
        public PaymentRequestValidatorUnitTests()
        {
            _validator = new PaymentRequestValidator();
        }

        [Fact]
        public void Should_Validate_CreditCard_NullCheck()
        {
            PaymentRequest request = new ()
            {
                
                Amount = 1234,
                CVV = "123",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(request).ShouldHaveValidationErrorFor(p => p.CreditCard);
        }

        [Fact]
        public void Should_Check_CreditCard_IsValidorNot()
        {
            PaymentRequest request = new ()
            {
                CreditCard = "444444",
                Amount = 1234,
                Currency = "TST",
                CVV = "123",
                
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(request).ShouldHaveValidationErrorFor(p => p.CreditCard);
        }

        [Fact]
        public void Should_ThrowValidationError_When_CVV_IsNull()
        {
            PaymentRequest request = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                //CVV = "123",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(request).ShouldHaveValidationErrorFor(p => p.CVV);
        }

        [Fact]
        public void Should_ThrowValidationError_When_CVV_Length_Is_Less_Than_Three()
        {
            PaymentRequest request = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "12",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(request).ShouldHaveValidationErrorFor(p => p.CVV);
        }

        [Fact]
        public void Should_ThrowValidationError_When_CVV_Length_Is_Greater_Than_Four()
        {
            PaymentRequest request = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "12345",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(request).ShouldHaveValidationErrorFor(p => p.CVV);
        }


        [Fact]
        public void Should_ThrowValidationError_When_In_ExpiryMonthYear_Month_Is_Not_Valid()
        {
            PaymentRequest request = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "1234",
                Currency = "TST",
                ExpiryMonthYear = "100/22"
            };

            _validator.TestValidate(request).ShouldHaveValidationErrorFor(p => p.ExpiryMonthYear);
        }

        [Fact]
        public void Should_ThrowValidationError_When_In_ExpiryMonthYear_Year_Is_Not_Valid()
        {
            PaymentRequest request = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "1234",
                Currency = "TST",
                ExpiryMonthYear = "10/2022"
            };

            _validator.TestValidate(request).ShouldHaveValidationErrorFor(p => p.ExpiryMonthYear);
        }

        [Fact]
        public void Should_Validate_Succesfully_CreatePaymentCommand()
        {
            PaymentRequest request = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "123",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(request).ShouldNotHaveAnyValidationErrors();
        }
    }
}
