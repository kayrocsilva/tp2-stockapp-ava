using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockApp.Application.Interfaces;
using StockApp.Application.Services;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Repositories;
using StockApp.Infra.IoC;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfrastructureAPI(builder.Configuration);

        builder.Services.AddControllers();

        builder.Services.AddSingleton<ICompetitivenessAnalysisService, CompetitivenessAnalysisService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<IEmployeePerformanceEvaluationService, EmployeePerformanceEvaluationService>();
        builder.Services.AddSingleton<IProcessAutomationService, ProcessAutomationService>();
        builder.Services.AddSingleton<IProductionPlanningService, ProductionPlanningService>();
        builder.Services.AddSingleton<IProjectManagementService, ProjectManagementService>();
        builder.Services.AddSingleton<ICustomReportService, CustomReportService>();
        builder.Services.AddScoped<IFeedbackService, FeedbackService>();
        builder.Services.AddScoped<ISentimentAnalysisService, SentimentAnalysisService>(); // Substitua pelo seu serviço real de análise de sentimento

        // Configuração do HttpClient para o serviço de preços
        builder.Services.AddHttpClient<IPricingService, PricingService>(client =>
        {
            client.BaseAddress = new Uri("https://api.pricing.com/");
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        // Endpoint padrão
       

        app.Run();
    }
}
