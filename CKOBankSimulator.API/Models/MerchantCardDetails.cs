using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CKOBankSimulator.API.Models
{
    public class MerchantCardDetails
    {
        public string MercentId { get; set; }
        public string CreditCard { get; set; }
        public decimal CreditLimit { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
    }
}
