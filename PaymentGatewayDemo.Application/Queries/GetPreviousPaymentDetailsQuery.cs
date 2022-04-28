using MediatR;
using PaymentGatewayDemo.Application.DTOs;
using PaymentGatewayDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Application.Queries
{
    public class GetPreviousPaymentDetailsQuery : IRequest<PaymentDetailsDTO>
    {
        public string PaymentId { get; set; }
    }
}
