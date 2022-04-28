using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PaymentGatewayDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.API.Middlewares
{

    public class ClaimsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ClaimsMiddleware> _logger;
        
        public ClaimsMiddleware(RequestDelegate next,  ILogger<ClaimsMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }
        public async Task InvokeAsync(HttpContext httpContext,MerchantClaim merchantClaim)
        {
           
            if (httpContext.User.Identity.IsAuthenticated)
            {
                
                var claims = httpContext.User.Claims.ToList();
                merchantClaim.MerchantId = claims.Where(x => x.Type == "MerchantId").FirstOrDefault().Value;
                _logger.LogInformation("Claim has been added to the MerchantClass.");
            }
            await _next(httpContext);
        }
    }
}
