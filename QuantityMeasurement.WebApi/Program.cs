using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuantityMeasurement.Repository;
using QuantityMeasurement.Service;
using QuantityMeasurement.WebApi.Filters;
namespace QuantityMeasurement.WebApi
{
    // UC17: ASP.NET Core Web API entry point.
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
            // Register Service via DI
            builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
            // Add Controllers with global exception filter
            builder.Services.AddControllers(ConfigureControllers);
            // Add Swagger/OpenAPI
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
            app.MapControllers();
            app.Run();
        }
        // Configuration methods
        private static void ConfigureDbContext(DbContextOptionsBuilder options)
        {
            string connectionString = configuration.GetConnectionString("SqlServer")!;
            options.UseSqlServer(connectionString);
        }
        private static void ConfigureControllers(Microsoft.AspNetCore.Mvc.MvcOptions options)
        {
            options.Filters.Add<GlobalExceptionFilter>();
        }
        private static void ConfigureSwagger(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions c)
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Quantity Measurement API",
                Version = "v1",
                Description = "UC17 — REST API for quantity measurement operations"
            });
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
