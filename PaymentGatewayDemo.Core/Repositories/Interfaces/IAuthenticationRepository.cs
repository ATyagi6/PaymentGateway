using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Core.Repositories.Interfaces
{
    public interface IAuthenticationRepository
    {
        Tokens Authenticate(Merchant merchant);
    }
}
