using PaymentGatewayDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Core.Repositories.Interfaces
{
    public interface IPaymentGatewayRepository:IRepository<PaymentDetails>
    {
        Task<PaymentDetails> GetPaymentDetailsByPaymentIdentifier(string paymentId);

        Task<Merchant> GetMerchant(string clientId);
    }
}
