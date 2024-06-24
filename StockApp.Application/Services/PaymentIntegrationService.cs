using Newtonsoft.Json;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class PaymentIntegrationService : IPaymentIntegrationService
    {
        private readonly HttpClient _httpClient;
        public PaymentIntegrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaymentResultDTO> ProcessPaymentAsync(PaymentRequestDTO paymentRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("process", paymentRequest);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var paymentResult = JsonConvert.DeserializeObject<PaymentResultDTO>(content);

            return paymentResult;
        }
    }
}