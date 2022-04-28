using FluentValidation;
using PaymentGatewayDemo.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.API.Validators
{
    public class CreatePaymentCommandValidator: AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(p => p.CreditCard).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("{PropertyName} is required field")
                                                                .CreditCard().WithMessage("{PropertyName} is not valid number");

            RuleFor(p => p.CVV).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("{PropertyName} is required field").Length(3, 4);

            RuleFor(p => p.ExpiryMonthYear).Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$").When(p => p.ExpiryMonthYear != null);
            
                                                                
        }
    }
}
