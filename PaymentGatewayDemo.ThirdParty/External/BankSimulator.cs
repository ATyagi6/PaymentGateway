using Microsoft.Extensions.Logging;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Core.ThirdParty.Interfaces;
using PaymentGatewayDemo.ThirdParty.Interfaces;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.ThirdParty.External
{
   public class BankSimulator : IBankCKO
    {
        private readonly IBankClient _bankClient;
        private readonly ILogger<BankSimulator> _logger;
       public BankSimulator(IBankClient bankClient,ILogger<BankSimulator> logger)
        {
            _bankClient = bankClient;
            _logger = logger;
        }

        public Task<bool> VerifyPaymentWithBank(PaymentDetails paymentDetails)
        {
            return _bankClient.CheckMerchantDetails(paymentDetails);
        }
    }
}
