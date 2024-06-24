using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface ISentimentAnalysisService
    {
        Task<SentimentAnalysisResult> AnalyzeSentimentAsync(string text);
    }

    public class SentimentAnalysisResult
    {
        public string Sentiment { get; set; }
        public double Score { get; set; }
    }
}
