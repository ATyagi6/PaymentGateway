using PaymentGatewayDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.ThirdParty.Interfaces
{
   public interface IBankClient
    {
        Task<bool> CheckMerchantDetails(PaymentDetails details);
    }
}
