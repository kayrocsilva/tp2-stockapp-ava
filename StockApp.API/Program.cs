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
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Serilog;
using Serilog.Events;
using StockApp.API.Hubs;

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

        // Registro do serviço de detecção de fraudes
        builder.Services.AddSingleton<IFraudDetectionService, FraudDetectionService>();
        // Registro do serviço de análise de dados
        builder.Services.AddHttpClient<IDataAnalysisService, DataAnalysisService>(client =>
        {
            client.BaseAddress = new Uri("https://api.dataanalysis.com/");
            // Configurações adicionais do HttpClient, se necessário
        });

        builder.Services.AddStackExchangeRedisCache(Options =>
        {
            Options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });

        Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

        builder.Host.UseSerilog();

        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });


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
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            // Define the JWT Bearer security scheme
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securitySchema);

            // Add a security requirement to the Swagger UI 
            var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

            c.AddSecurityRequirement(securityRequirement);
        });

        builder.Services.AddSignalR();

        var app = builder.Build();

        app.MapHub<StockHub>("/stockhub");

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

        app.UseCors("AllowAll");

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Endpoint padrão
       

        app.Run();
    }
}
