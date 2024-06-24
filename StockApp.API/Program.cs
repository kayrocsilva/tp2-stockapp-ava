using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockApp.Application.Interfaces;
using StockApp.Application.Services;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Repositories;
using StockApp.Infra.IoC;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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


        var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddControllers();
        builder.Services.AddSingleton<IEmployeePerformanceEvaluationService, EmployeePerformanceEvaluationService>();
        builder.Services.AddSingleton<IProcessAutomationService, ProcessAutomationService>();
        builder.Services.AddSingleton<IProductionPlanningService, ProductionPlanningService>();
        builder.Services.AddSingleton<IProjectManagementService, ProjectManagementService>();

        // Configurar servi�os
        builder.Services.AddSingleton<IDiscountService, DiscountService>();
        builder.Services.AddControllers();

        // Configura��o dos servi�os
        builder.Services.AddControllers();
        builder.Services.AddSingleton<ICustomerRelationshipManagementService, CustomerRelationshipManagementService>();

        builder.Services.AddControllers();
        builder.Services.AddSingleton<IFinancialManagementService, FinancialManagementService>();

        // Configura??o dos servi?os
        builder.Services.AddControllers();
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

        // Configurar o Swagger
       

        // Configurar o ambiente de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockApp API v1");
            });
        }


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Endpoint padrão
       

        app.Run();
    }
}
