using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using PaymentGatewayDemo.API.Interface;
using PaymentGatewayDemo.API.Request;
using PaymentGatewayDemo.API.Responses;
using PaymentGatewayDemo.Application.Commands;
using PaymentGatewayDemo.Application.DTOs;
using PaymentGatewayDemo.Application.Queries;
using PaymentGatewayDemo.Core.Exceptions;
using PaymentGatewayDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentGatewayController : Controller
    {

        private readonly IMediator _mediator;
        private readonly ILogger<PaymentGatewayController> _logger;
        private readonly IMapper _mapper;
        private readonly MerchantClaim _merchant;
        public PaymentGatewayController(IMediator mediator, ILogger<PaymentGatewayController> logger, IMapper mapper,MerchantClaim merchant)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _merchant = merchant;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
        {
            try
            {
                var authenticationCommand = _mapper.AuthenticationRequestToCommand(request);
                    
                var token = await _mediator.Send(authenticationCommand);

                if (token is null) Unauthorized(new ErrorResponse { StatusCode = HttpStatusCode.Unauthorized, Title = "Unautorized", Details = $"Not able to Authorised the Merchant with the given clientid {request.ClientId}." });

                var result = _mapper.TokensToAuthenticationResponse(token);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while authenticating the clientId,failed with an exception {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { StatusCode = HttpStatusCode.InternalServerError, Title = "Internal Server Error", Details = $"Unable to Process the request at this time." });
            }
           
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("Payment")]
        public async Task<ActionResult<PaymentResponse>> CreatePayment([FromBody] PaymentRequest request)
        {
            string merchantId = String.Empty;
            try
            {
                //if (HttpContext.Request.Headers.TryGetValue(MerchantIdHeaderKey, out StringValues merchantIds))
                //    merchantId = merchantIds.FirstOrDefault();
               

                var command = _mapper.PaymentRequestToPaymentCommand(request, _merchant.MerchantId);

                var result = await _mediator.Send(command);

                var response = _mapper.PaymentDTOToPaymentResponse(result);

                return CreatedAtAction(nameof(GetPayment), new { paymentId = result.PaymentId }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while saving the payment details,failed with an exception {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { StatusCode = HttpStatusCode.InternalServerError, Title = "Internal Server Error", Details = $"Unable to Process the request at this time." });
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Payment/{paymentId}")]
        public async Task<ActionResult<PaymentResponse>> GetPayment(string paymentId)
        {
            if (string.IsNullOrEmpty(paymentId)) return BadRequest(new ErrorResponse { StatusCode = HttpStatusCode.BadRequest, Title = "Bad Request", Details = $"PaymentId is required to process further." });
            
            try
            {
                var result = await _mediator.Send(new GetPreviousPaymentDetailsQuery { PaymentId = paymentId });

                if (result is null) return NotFound(new ErrorResponse { StatusCode = HttpStatusCode.NotFound, Title = "Not Found", Details = $"Not able to find the payment id: {paymentId}." });

                var response = _mapper.PaymentDTOToPaymentResponse(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while geting the payment details,failed with an exception {ex.Message} for paymentId :{paymentId}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { StatusCode = HttpStatusCode.InternalServerError, Title = "Internal Server Error", Details = $"Unable to process the request for the paymentId {paymentId}at this moment." });
            }              

           
        }
    }
}
