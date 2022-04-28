using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Application.DTOs
{
    public class PaymentDetailsDTO
    {
        public string CreditCard { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public string PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get;set;}

    }
}
