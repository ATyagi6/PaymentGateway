using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Core.Exceptions
{
    [Serializable]
   public class PaymentGatewayDemoException:Exception
    {
        public PaymentGatewayDemoException(string message):base (message)
        {

        }
        public PaymentGatewayDemoException(string message ,Exception innerException):base(message,innerException)
        {
                
        }
        public PaymentGatewayDemoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
