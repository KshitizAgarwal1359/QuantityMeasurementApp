using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BusinessLogic.API.Filters;
using BusinessLogic.API.Security;
using BusinessLogic.API.Services;

namespace BusinessLogic.API
{
    // UC21: BusinessLogic Microservice — Port 5001
    // Handles authentication, JWT, quantity calculations.
    // Calls DataRepository.API for data persistence (Interservice Communication).
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // UC21: Register HttpClient for Interservice Communication with DataRepository.API
            string dataRepoUrl = configuration["ServiceUrls:DataRepositoryAPI"] ?? "http://localhost:5002";
            builder.Services.AddHttpClient("DataRepositoryAPI", client =>
            {
                client.BaseAddress = new Uri(dataRepoUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Register Services via DI
            builder.Services.AddScoped<IAuthService, AuthServiceImpl>();
            builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
            builder.Services.AddSingleton<PasswordHasher>();
            builder.Services.AddSingleton<JwtTokenGenerator>();
            builder.Services.AddSingleton<AesEncryptionService>();
            builder.Services.AddHttpContextAccessor();

            // JWT Authentication
            string secretKey = configuration["Jwt:SecretKey"]!;
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                    };
                });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });
            builder.Services.AddEndpointsApiExplorer();

            // Swagger with JWT Bearer support
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BusinessLogic API",
                    Version = "v1",
                    Description = "UC21 — Business Logic Microservice with JWT"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter 'Bearer' followed by your JWT token",
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
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
