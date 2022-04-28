using Microsoft.Extensions.DependencyInjection;
using PaymentGatewayDemo.ThirdParty.Shared;
using Polly;
using Polly.Extensions.Http;
using System;


namespace PaymentGatewayDemo.ThirdParty.Extensions
{
    public static class HttpClientExtension
    {
        public static IHttpClientBuilder  AddRetryPolicy(this IHttpClientBuilder httpClientBuilder, int numberOfRetries)
        {
            return httpClientBuilder.AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(numberOfRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
        }

        public static IHttpClientBuilder AddReilientBankSimulatorClient(this IServiceCollection service,Uri baseAddress,int numberOfRetries)
        {
            return service.AddHttpClient(BankClient.NamedClient, client => client.BaseAddress = baseAddress).AddRetryPolicy(numberOfRetries);
        }
    }
}
