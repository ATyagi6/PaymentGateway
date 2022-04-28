using MediatR;
using PaymentGatewayDemo.Core.Models;

namespace PaymentGatewayDemo.Application.Commands
{
    public class CreateTokenCommand : IRequest<Tokens>
    {
        public string ClientId { get; set; }
    }
}
