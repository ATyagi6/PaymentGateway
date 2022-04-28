using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PaymentGatewayDemo.API;
using PaymentGatewayDemo.Application.Commands;
using PaymentGatewayDemo.Core.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PaymentGatewayDemo.ThirdParty.Extensions;
using PaymentGatewayDemo.Infrastructure.Data.InMemory;
using Microsoft.EntityFrameworkCore;
using PaymentGatewayDemo.API.Responses;
using PaymentGatewayDemo.API.Request;

namespace PaymentGatewayDemo.IntegrationTesting
{
   public class IntegrationTest
    {
        public readonly HttpClient TestClient;
        private const string  SIMULATORTESTAPI= "https://ckobanksimulatorapi.azurewebsites.net/api/Bank";
        public IntegrationTest()
        {
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddReilientBankSimulatorClient(new System.Uri(SIMULATORTESTAPI), 0);
                        services.RemoveAll(typeof(PaymentGatewayDemoContext));
                        services.AddDbContext<PaymentGatewayDemoContext>(options => options.UseInMemoryDatabase(databaseName: "PaymentTests"));

                    });
                });
            TestClient = factory.CreateClient();
        }

        public async Task AuthenticateRequest()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", await GetToken());
        }

        private async Task<string> GetToken()
        {
            var response = await TestClient.PostAsJsonAsync("/api/PaymentGateway/Authenticate", new AuthenticationRequest
            {
                ClientId = "abcdret187543"
            });

            var resonseToken = await response.Content.ReadAsAsync<AuthenticationResponse>();
            return resonseToken.Token;
        }
    }
}
