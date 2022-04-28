using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CKOBankSimulator.API.Models
{
    public class BankDetails
    {
        public string PaymentId { get; set; }
        public string MercentIdentifier { get; set; }
        public string CreditCard { get; set; }
        public decimal Amount { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
    }
}
