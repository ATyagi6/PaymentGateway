using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.API.Responses
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public List<ErrorEntry> Entries { get; set; }

    }
    public class ErrorEntry
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
    }
}
