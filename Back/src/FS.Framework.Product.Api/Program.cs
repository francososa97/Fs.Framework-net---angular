﻿using FS.Framework.Product.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using FS.Framework.Product.Infrastructure.UnitOfWork;
using FS.FakeTwitter.Infrastructure.Repositories;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using FS.FakeTwitter.Application.Services;
using FS.Framework.Product.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using FS.Framework.Product.Application.Interfaces.Cache;
using FS.Framework.Product.Infrastructure.Cache;
using Serilog;
using Serilog.Events;
using Polly.Extensions.Http;
using Polly;
using FS.Framework.Product.Application.Features.Product.Mappings;
using FS.Framework.Product.Application.Features.Product.Commands.CreateProduct;
using FS.Framework.Product.Application.Features.Product.Commands.UpdateProduct;
using FS.Framework.Product.Application.Features.Product.Commands.DeleteProduct;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        ConfigureSerilog();
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);
        ConfigureMediatR(builder);
        ConfigureDatabase(builder);
        ConfigureSwagger(builder);
        ConfigureAuthentication(builder);
        var app = builder.Build();

        ConfigureMiddleware(app);
        app.Run();
    }


    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog();
        builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHealthChecks();

        builder.Services.AddHttpClient("ExternalApi", client =>
        {
            client.BaseAddress = new Uri("https://api.external.com"); // Cambiar por tu URL real
        })
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy());

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
        });

        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddMemoryCache();
        builder.Services.AddScoped<ICacheHelper, CacheHelper>();
        // FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<DeleteProductCommand>();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
    }

    private static void ConfigureMediatR(WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(Program).Assembly,
            typeof(IProductService).Assembly
        ));
    }

    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        var useInMemory = builder.Configuration.GetValue<bool>("UseInMemory");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (useInMemory)
                options.UseInMemoryDatabase("FakeTwitterDb");
            else
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
    }

    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FS.Framework API", Version = "v1", Description = " API para gestión de productos 🔐 API Key: `super-secret-key` o con JWT: `Email: admin` `Password: admin123`" });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            // 🔐 API Key
            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "API Key debe ir en el header: `X-API-KEY: {valor}`",
                In = ParameterLocation.Header,
                Name = "X-API-KEY",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKeyScheme"
            });

            // 🔐 JWT
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT token usando el esquema Bearer. Ejemplo: `Bearer {token}`",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    Array.Empty<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        // 🔒 MIDDLEWARE GLOBAL DE CORRELACIÓN
        app.UseMiddleware<CorrelationIdMiddleware>();

        app.UseMiddleware<RequestLoggingMiddleware>();

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors("AllowAngular");
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "FS.FakeTwitter API V1");
        });

        app.MapControllers();
        app.MapHealthChecks("/health");

    }

    private static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");

        builder.Services
            .AddAuthentication("SmartAuth")
            .AddPolicyScheme("SmartAuth", "JWT o API Key", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    var hasApiKey = context.Request.Headers.ContainsKey("X-API-KEY");
                    var hasBearer = context.Request.Headers.ContainsKey("Authorization") &&
                                    context.Request.Headers["Authorization"].ToString().StartsWith("Bearer ");
                    return hasApiKey ? "ApiKey" :
                           hasBearer ? JwtBearerDefaults.AuthenticationScheme :
                           "ApiKey";
                };
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
                    ValidateLifetime = true
                };
            })
            .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", null);
    }

    public static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithEnvironmentName()
            .WriteTo.Async(a => a.Console())
            .WriteTo.Async(a => a.File("logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 15)) // se guardan solo los últimos 15 días
            .CreateLogger();
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }

}


