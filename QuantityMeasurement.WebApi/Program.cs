using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuantityMeasurement.Repository;
using QuantityMeasurement.Service;
using QuantityMeasurement.Service.Security;
using QuantityMeasurement.WebApi.Filters;

namespace QuantityMeasurement.WebApi
{
    // UC18: ASP.NET Core Web API entry point with JWT authentication.
    public class Program
    {
        private static IConfiguration configuration = null!;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            configuration = builder.Configuration;

            // Register EF Core with SQL Server
            builder.Services.AddDbContext<QuantityMeasurementDbContext>(ConfigureDbContext);

            // Register Repository via DI
            builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Register Service via DI
            builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
            builder.Services.AddScoped<IAuthService, AuthServiceImpl>();

            // Register Security services
            builder.Services.AddSingleton<PasswordHasher>();
            builder.Services.AddSingleton(new JwtTokenGenerator(configuration));
            builder.Services.AddSingleton(new AesEncryptionService(configuration));

            // UC18: Configure JWT Bearer authentication
            ConfigureAuthentication(builder.Services);

            // Add Controllers with global exception filter
            builder.Services.AddControllers(ConfigureControllers);

            // Add Swagger/OpenAPI with Bearer support
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(ConfigureSwagger);

            // Add CORS
            builder.Services.AddCors(ConfigureCors);

            var app = builder.Build();

            // Ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<QuantityMeasurementDbContext>();
                db.Database.EnsureCreated();
            }

            // Enable Swagger in development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(ConfigureSwaggerUI);
            }

            app.UseHttpsRedirection();
            app.UseCors();

            // UC18: Authentication and Authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }

        // Configuration methods
        private static void ConfigureDbContext(DbContextOptionsBuilder options)
        {
            string connectionString = configuration.GetConnectionString("SqlServer")!;
            options.UseSqlServer(connectionString);
        }

        // UC18: Configure JWT Bearer authentication
        private static void ConfigureAuthentication(IServiceCollection services)
        {
            string secretKey = configuration["Jwt:SecretKey"]!;
            string issuer = configuration["Jwt:Issuer"]!;
            string audience = configuration["Jwt:Audience"]!;

            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(keyBytes);

            services.AddAuthentication(ConfigureAuthenticationDefaults)
                .AddJwtBearer(ConfigureJwtBearerOptions);
        }

        // Set default authentication scheme to JWT Bearer
        private static void ConfigureAuthenticationDefaults(
            Microsoft.AspNetCore.Authentication.AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        // Configure JWT Bearer token validation
        private static void ConfigureJwtBearerOptions(JwtBearerOptions options)
        {
            string secretKey = configuration["Jwt:SecretKey"]!;
            string issuer = configuration["Jwt:Issuer"]!;
            string audience = configuration["Jwt:Audience"]!;

            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
            };
        }

        private static void ConfigureControllers(Microsoft.AspNetCore.Mvc.MvcOptions options)
        {
            options.Filters.Add<GlobalExceptionFilter>();
        }

        // UC18: Swagger with Bearer token support
        private static void ConfigureSwagger(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions c)
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Quantity Measurement API",
                Version = "v1",
                Description = "UC18 — REST API with JWT authentication"
            });

            // Add Bearer token security definition
            OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter 'Bearer' followed by your JWT token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            };
            c.AddSecurityDefinition("Bearer", securityScheme);

            // Add security requirement for all endpoints
            OpenApiSecurityRequirement securityRequirement = new OpenApiSecurityRequirement();
            OpenApiSecurityScheme reference = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            securityRequirement.Add(reference, new List<string>());
            c.AddSecurityRequirement(securityRequirement);
        }

        private static void ConfigureCors(Microsoft.AspNetCore.Cors.Infrastructure.CorsOptions options)
        {
            options.AddDefaultPolicy(ConfigureCorsPolicy);
        }

        private static void ConfigureCorsPolicy(Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder policy)
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }

        private static void ConfigureSwaggerUI(Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIOptions c)
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantity Measurement API v1");
        }
    }
}
