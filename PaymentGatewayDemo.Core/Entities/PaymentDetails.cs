using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Core.Entities
{
    public class PaymentDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }
        public string PaymentId { get; set; }
        public string MercentIdentifier { get; set; }
        public string CreditCard { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public string PaymentStatus {get;set;}

    }
}
