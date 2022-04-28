using Microsoft.Extensions.Logging;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Core.Exceptions;
using PaymentGatewayDemo.ThirdParty.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PaymentGatewayDemo.ThirdParty.Shared
{
    public class BankClient : IBankClient
    {
        public static readonly string NamedClient = "BankClient";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BankClient> _logger;

        public BankClient(IHttpClientFactory httpClientFactory, ILogger<BankClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<bool> CheckMerchantDetails(PaymentDetails details)
        {
            bool result = await PostRequest("/bank", details);
            return result;
        }

        private async Task<bool> PostRequest(string relativeAddress, PaymentDetails details)
        {
            try
            {
                bool result = false;
                var detailsJson = new StringContent(JsonSerializer.Serialize(details), Encoding.UTF8, Application.Json);
                var httpClient = _httpClientFactory.CreateClient(BankClient.NamedClient);
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(String.Concat(httpClient.BaseAddress, relativeAddress)), detailsJson);
                if (response.IsSuccessStatusCode)
                {
                    result = bool.Parse(response.Content.ReadAsStringAsync().Result);

                }
                return result;
            }
            catch(Exception )
            {
                throw new PaymentGatewayDemoException("Error while connecting to Bank.Please try after sometime");
            }

        }
    }
}
