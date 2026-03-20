using Microsoft.Extensions.Configuration;
using QuantityMeasurement.Controllers;
using QuantityMeasurement.Repository;
using QuantityMeasurement.Service;
namespace QuantityMeasurement
{
    // UC15/UC16: QuantityMeasurementApp — Application Entry Point (N-Tier Architecture).
    // UC16 additions:
    //   - Reads appsettings.json via Microsoft.Extensions.Configuration
    //   - If "UseDatabase": true → uses QuantityMeasurementDatabaseRepository (SQL Server)
    //   - If "UseDatabase": false → falls back to QuantityMeasurementCacheRepository (in-memory)
    // Dependency Chain:
    //   Config → Repository (DB or Cache) → Service (DI) → Controller (DI) → Menu (DI) → Run()
    public class QuantityMeasurementApp
    {
        public static void Main(string[] args)
        {
            // Step 1: Load configuration from appsettings.json
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .Build();
            // Step 2: Determine repository type from configuration
            bool useDatabase = configuration.GetValue<bool>("UseDatabase");
            IQuantityMeasurementRepository repository;
            if (useDatabase)
            {
                string? connectionString = configuration.GetConnectionString("SqlServer");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    Console.WriteLine("WARNING: No connection string found in appsettings.json.");
                    Console.WriteLine("Falling back to in-memory cache repository.");
                    Console.WriteLine();
                    repository = QuantityMeasurementCacheRepository.GetInstance();
                }
                else
                {
                    try
                    {
                        repository = new QuantityMeasurementDatabaseRepository(connectionString);
                        Console.WriteLine("Connected to SQL Server database successfully.");
                        Console.WriteLine();
                    }
                    catch (DatabaseException ex)
                    {
                        Console.WriteLine($"WARNING: Database connection failed: {ex.Message}");
                        Console.WriteLine("Falling back to in-memory cache repository.");
                        Console.WriteLine();
                        repository = QuantityMeasurementCacheRepository.GetInstance();
                    }
                }
            }
            else
            {
                repository = QuantityMeasurementCacheRepository.GetInstance();
                Console.WriteLine("Using in-memory cache repository.");
                Console.WriteLine();
            }
            // Step 3: Create Service with Repository dependency (DI pattern)
            IQuantityMeasurementService service =
                new QuantityMeasurementServiceImpl(repository);
            // Step 4: Create Controller with Service dependency (DI pattern)
            QuantityMeasurementController controller =
                new QuantityMeasurementController(service);
            // Step 5: Create Menu with Controller dependency and run
            IMenu menu = new Menu(controller);
            menu.Run();
        }
    }
}
