using QuantityMeasurement.Controllers;
using QuantityMeasurement.Repository;
using QuantityMeasurement.Service;

namespace QuantityMeasurement
{
    // UC15: QuantityMeasurementApp — Application Entry Point (N-Tier Architecture).
    // Simplified to only initialize the dependency chain and start the interactive menu.
    //
    // Design Patterns:
    //   Factory: Creates instances of repository, service, controller, menu
    //   Singleton: QuantityMeasurementCacheRepository
    //   Dependency Injection: Repository → Service → Controller → Menu
    //   Facade: Controller + Menu serve as simplified interface
    //
    // Dependency Chain:
    //   Repository (Singleton) → Service (DI) → Controller (DI) → Menu (DI) → Run()
    public class QuantityMeasurementApp
    {
        public static void Main(string[] args)
        {
            // Step 1: Create Repository (Singleton pattern)
            IQuantityMeasurementRepository repository =
                QuantityMeasurementCacheRepository.GetInstance();

            // Step 2: Create Service with Repository dependency (DI pattern)
            IQuantityMeasurementService service =
                new QuantityMeasurementServiceImpl(repository);

            // Step 3: Create Controller with Service dependency (DI pattern)
            QuantityMeasurementController controller =
                new QuantityMeasurementController(service);

            // Step 4: Create Menu with Controller dependency and run
            IMenu menu = new Menu(controller);
            menu.Run();
        }
    }
}
