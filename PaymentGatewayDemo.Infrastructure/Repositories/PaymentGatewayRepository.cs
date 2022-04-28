using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Core.Repositories.Interfaces;
using PaymentGatewayDemo.Infrastructure.Data.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Infrastructure.Repositories
{
    public class PaymentGatewayRepository : Repository<PaymentDetails>, IPaymentGatewayRepository
    {
        public PaymentGatewayRepository(PaymentGatewayDemoContext paymentContext,ILogger<PaymentGatewayRepository> logger) : base(paymentContext,logger) {  }

        public async Task<Merchant> GetMerchant(string clientId)
        {
           return  await Task.FromResult(_paymentContext.Merchants.Local.Where(m => m.ClientId == clientId).FirstOrDefault());
        }

        public  async Task<PaymentDetails> GetPaymentDetailsByPaymentIdentifier(string paymentId)
        {
            return await Task.FromResult( _paymentContext.PaymentDetails.Where(m => m.PaymentId== paymentId).FirstOrDefault());
        }
    }
}
