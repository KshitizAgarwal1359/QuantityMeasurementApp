using Microsoft.EntityFrameworkCore;
using DataRepository.API;
using DataRepository.API.Models;

namespace DataRepository.API
{
    // UC21: DataRepository Microservice — Port 5002
    // Responsible for all database CRUD operations.
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Register EF Core with dual DB support
            string provider = configuration["DatabaseProvider"] ?? "SqlServer";

            builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
            {
                if (provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
                {
                    string connectionUrl = configuration["DATABASE_URL"]
                        ?? configuration.GetConnectionString("DefaultConnection") ?? "";
                    connectionUrl = connectionUrl.Trim().Trim('"', '\'');
                    string npgsqlConnectionString = connectionUrl;

                    if (connectionUrl.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) ||
                        connectionUrl.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
                    {
                        var uri = new Uri(connectionUrl);
                        var userInfo = uri.UserInfo.Split(':');
                        npgsqlConnectionString = $"Host={uri.Host};" +
                            $"Port={(uri.Port > 0 ? uri.Port : 5432)};" +
                            $"Database={uri.LocalPath.TrimStart('/')};" +
                            $"Username={(userInfo.Length > 0 ? userInfo[0] : "")};" +
                            $"Password={(userInfo.Length > 1 ? userInfo[1] : "")};" +
                            $"Ssl Mode=Prefer;Trust Server Certificate=true;";
                    }
                    if (npgsqlConnectionString.Contains("?"))
                        npgsqlConnectionString = npgsqlConnectionString.Substring(0, npgsqlConnectionString.IndexOf('?'));

                    options.UseNpgsql(npgsqlConnectionString);
                }
                else
                {
                    string connectionString = configuration.GetConnectionString("SqlServer")!;
                    options.UseSqlServer(connectionString);
                }
            });

            // Register Repositories via DI
            builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Ensure database tables are created
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<QuantityMeasurementDbContext>();
                db.Database.EnsureCreated();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
            app.Run();
        }
    }
}
