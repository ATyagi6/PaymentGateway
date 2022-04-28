using FluentValidation;
using Microsoft.Extensions.Logging;
using PaymentGatewayDemo.API.Responses;
using PaymentGatewayDemo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace PaymentGatewayDemo.API.Middlewares
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            this._logger = logger;
        }

        public ErrorResponse HandleException(Exception exception)
        {
            var error = HandleUnhandledExceptions(exception);
           
            return error;
        }

        private ErrorResponse HandleUnhandledExceptions(Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            return new ErrorResponse
            {
                Title = "An unhandled error occurred while processing this request",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
