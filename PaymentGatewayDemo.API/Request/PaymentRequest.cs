using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.API.Request
{
    public class PaymentRequest
    { 
        public string CreditCard { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
    }
}
