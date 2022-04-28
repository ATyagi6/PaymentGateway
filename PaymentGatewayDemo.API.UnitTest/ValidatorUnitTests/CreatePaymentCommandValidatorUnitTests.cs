using FluentValidation.TestHelper;
using PaymentGatewayDemo.API.Validators;
using PaymentGatewayDemo.Application.Commands;
using Xunit;

namespace PaymentGatewayDemo.API.UnitTest.ValidatorUnitTests
{
    public class CreatePaymentCommandValidatorUnitTests
    {
        private readonly CreatePaymentCommandValidator _validator;
        public CreatePaymentCommandValidatorUnitTests()
        {
            _validator = new CreatePaymentCommandValidator();
        }

        [Fact]
        public void Should_Validate_CreditCard_NullCheck()
        {
            CreatePaymentCommand command = new ()
            {
                
                Amount = 1234,
                CVV = "123",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(p => p.CreditCard);
        }

        [Fact]
        public void Should_Check_CreditCard_IsValidorNot()
        {
            CreatePaymentCommand command = new ()
            {
                CreditCard = "444444",
                Amount = 1234,
                Currency = "TST",
                CVV = "123",
                
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(p => p.CreditCard);
        }

        [Fact]
        public void Should_ThrowValidationError_When_CVV_IsNull()
        {
            CreatePaymentCommand command = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                //CVV = "123",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(p => p.CVV);
        }

        [Fact]
        public void Should_ThrowValidationError_When_CVV_Length_Is_Less_Than_Three()
        {
            CreatePaymentCommand command = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "12",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(p => p.CVV);
        }

        [Fact]
        public void Should_ThrowValidationError_When_CVV_Length_Is_Greater_Than_Four()
        {
            CreatePaymentCommand command = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "12345",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(p => p.CVV);
        }


        [Fact]
        public void Should_ThrowValidationError_When_In_ExpiryMonthYear_Month_Is_Not_Valid()
        {
            CreatePaymentCommand command = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "1234",
                Currency = "TST",
                ExpiryMonthYear = "100/22"
            };

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(p => p.ExpiryMonthYear);
        }

        [Fact]
        public void Should_ThrowValidationError_When_In_ExpiryMonthYear_Year_Is_Not_Valid()
        {
            CreatePaymentCommand command = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "1234",
                Currency = "TST",
                ExpiryMonthYear = "10/2022"
            };

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(p => p.ExpiryMonthYear);
        }

        [Fact]
        public void Should_Validate_Succesfully_CreatePaymentCommand()
        {
            CreatePaymentCommand command = new ()
            {
                CreditCard = "5241932083537011",
                Amount = 1234,
                CVV = "123",
                Currency = "TST",
                ExpiryMonthYear = "10/22"
            };

            _validator.TestValidate(command).ShouldNotHaveAnyValidationErrors();
        }
    }
}
