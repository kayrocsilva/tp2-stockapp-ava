using Newtonsoft.Json;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class DataAnalysisService : IDataAnalysisService
    {
        private readonly HttpClient _httpClient;

        public DataAnalysisService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DataAnalysisDTO> AnalyzeDataAsync()
        {
            var response = await _httpClient.GetAsync("analyze");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var analysis = JsonConvert.DeserializeObject<DataAnalysisDTO>(content);

            return analysis;
        }
    }
}
