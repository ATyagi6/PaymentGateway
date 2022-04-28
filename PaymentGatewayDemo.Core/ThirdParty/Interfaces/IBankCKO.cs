using PaymentGatewayDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Core.ThirdParty.Interfaces
{
    public interface IBankCKO
    {
        public Task<bool> VerifyPaymentWithBank(PaymentDetails paymentDetails);
    }
}
