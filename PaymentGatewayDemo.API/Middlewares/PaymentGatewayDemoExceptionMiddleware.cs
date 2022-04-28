using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.API.Middlewares
{
    public class PaymentGatewayDemoExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ILogger<PaymentGatewayDemoExceptionMiddleware> _logger;

        public PaymentGatewayDemoExceptionMiddleware(RequestDelegate next, IExceptionHandler exceptionHandler, ILogger<PaymentGatewayDemoExceptionMiddleware> logger)
        {
            _next = next;
            _exceptionHandler = exceptionHandler;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in the application with following exception {ex.Message}");
                var error = _exceptionHandler.HandleException(ex);
                if (!httpContext.Response.HasStarted)
                {
                    httpContext.Response.Clear();
                    httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                    httpContext.Response.StatusCode = (int)error.StatusCode;
                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(error));
                }
            }
        }
    }
}
