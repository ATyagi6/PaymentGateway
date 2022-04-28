
using PaymentGatewayDemo.API.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.API.Middlewares
{
    public interface IExceptionHandler
    {
        public ErrorResponse HandleException(Exception exception);
    }
}
