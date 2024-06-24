using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    public class DataAnalysisController : ControllerBase
    {
        private readonly IDataAnalysisService _dataAnalysisService;

        public DataAnalysisController(IDataAnalysisService dataAnalysisService)
        {
            _dataAnalysisService = dataAnalysisService;
        }

        [HttpGet("analyze-data")]
        public async Task<IActionResult> AnalyzeData()
        {
            try
            {
                var analysisResult = await _dataAnalysisService.AnalyzeDataAsync();
                return Ok(analysisResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao analisar dados: {ex.Message}");
            }
        }
    }
}
