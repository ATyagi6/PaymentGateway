using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Core.Models;
using PaymentGatewayDemo.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Infrastructure.Repositories
{
    public class PaymentGatewayAuthenticationRepository : IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentGatewayAuthenticationRepository> _logger;
       public PaymentGatewayAuthenticationRepository(IConfiguration configuration,ILogger<PaymentGatewayAuthenticationRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public Tokens Authenticate(Merchant merchant)
        {
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("MerchantId", merchant.MerchantId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
