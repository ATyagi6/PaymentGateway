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
            var error = exception switch
            {
                ValidationException validationException => HandleValidationException(validationException),
                _ => HandleUnhandledExceptions(exception)
            };

            return error;
        }

        private ErrorResponse HandleValidationException(ValidationException validationException)
        {
            _logger.LogInformation(validationException, validationException.Message);

            var error = new ErrorResponse
            {
                Title =     "Validation Error",
                StatusCode = HttpStatusCode.BadRequest,
                Details=    validationException.Message
            };

            if (validationException.Errors != null && validationException.Errors.Any())
            {
                error.Entries = new List<ErrorEntry>();

                error.Entries.AddRange(validationException.Errors.Select(validationError =>
                    new ErrorEntry
                    {
                        Code = validationError.ErrorCode,
                        Title = validationError.ErrorMessage,
                        Source = validationError.PropertyName
                    }));
            }

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
