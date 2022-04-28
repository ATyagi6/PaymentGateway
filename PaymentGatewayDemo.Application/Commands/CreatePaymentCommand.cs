using MediatR;
using PaymentGatewayDemo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Application.Commands
{
    public class CreatePaymentCommand : IRequest<PaymentDetailsDTO>
    {
        public string CreditCard { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public string MerchantID { get; set; }
    }
}
