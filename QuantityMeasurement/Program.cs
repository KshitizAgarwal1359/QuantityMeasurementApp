using QuantityMeasurement.Controllers;
using QuantityMeasurement.Repository;
using QuantityMeasurement.Service;

namespace QuantityMeasurement
{
    // UC15: QuantityMeasurementApp — Application Entry Point (N-Tier Architecture).
    // This class serves as the bootstrap point for the entire application.
    //
    // Design Patterns Used:
    //   Factory Pattern: Creates instances of controller and service layers
    //   Facade Pattern: Controller serves as a simplified interface for all operations
    //   Singleton Pattern: QuantityMeasurementCacheRepository is a singleton
    //   Dependency Injection: Service receives repository via constructor injection
    //   Interface Segregation: Service and Repository layers have specific interfaces
    //
    // Responsibilities:
    //   - Initialize the application
    //   - Instantiate service and controller dependencies
    //   - Coordinate the startup sequence
    //   - Delegate all operations to the controller
    //
    // The main method creates the dependency chain:
    //   Repository (Singleton) -> Service (DI) -> Controller (DI) -> Demonstrations
    public class QuantityMeasurementApp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC15: N-Tier Architecture");
            Console.WriteLine("========================================");
            Console.WriteLine();

            // Step 1: Create Repository (Singleton pattern)
            IQuantityMeasurementRepository repository =
                QuantityMeasurementCacheRepository.GetInstance();

            // Step 2: Create Service with Repository dependency (DI pattern)
            IQuantityMeasurementService service =
                new QuantityMeasurementServiceImpl(repository);

            // Step 3: Create Controller with Service dependency (DI pattern)
            QuantityMeasurementController controller =
                new QuantityMeasurementController(service);

            // ---- Length Equality (UC1/UC3/UC4 via N-Tier) ----
            Console.WriteLine("--- Length Equality (UC1/UC3/UC4 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformComparison(1.0, "FEET", 1.0, "FEET", "LENGTH");
            controller.PerformComparison(1.0, "FEET", 12.0, "INCH", "LENGTH");
            controller.PerformComparison(36.0, "INCH", 1.0, "YARDS", "LENGTH");

            // ---- Length Conversion (UC5 via N-Tier) ----
            Console.WriteLine("--- Length Conversion (UC5 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformConversion(1.0, "FEET", "INCH", "LENGTH");

            // ---- Length Addition (UC6/UC7 via N-Tier) ----
            Console.WriteLine("--- Length Addition (UC6/UC7 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformAddition(1.0, "FEET", 12.0, "INCH", "FEET", "LENGTH");
            controller.PerformAddition(1.0, "YARDS", 3.0, "FEET", "YARDS", "LENGTH");

            // ---- Weight Operations (UC9 via N-Tier) ----
            Console.WriteLine("--- Weight Equality (UC9 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformComparison(1.0, "KILOGRAM", 1000.0, "GRAM", "WEIGHT");
            Console.WriteLine("--- Weight Conversion (UC9 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformConversion(1.0, "KILOGRAM", "GRAM", "WEIGHT");
            Console.WriteLine("--- Weight Addition (UC9 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformAddition(1.0, "KILOGRAM", 1000.0, "GRAM", "KILOGRAM", "WEIGHT");

            // ---- Volume Operations (UC11 via N-Tier) ----
            Console.WriteLine("--- Volume Equality (UC11 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformComparison(1.0, "LITRE", 1000.0, "MILLILITRE", "VOLUME");
            Console.WriteLine("--- Volume Conversion (UC11 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformConversion(1.0, "LITRE", "MILLILITRE", "VOLUME");
            Console.WriteLine("--- Volume Addition (UC11 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformAddition(1.0, "LITRE", 1000.0, "MILLILITRE", "LITRE", "VOLUME");

            // ---- Subtraction (UC12 via N-Tier) ----
            Console.WriteLine("--- Subtraction (UC12 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformSubtraction(10.0, "FEET", 6.0, "INCH", "FEET", "LENGTH");
            controller.PerformSubtraction(10.0, "KILOGRAM", 5000.0, "GRAM", "KILOGRAM", "WEIGHT");

            // ---- Division (UC12 via N-Tier) ----
            Console.WriteLine("--- Division (UC12 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformDivision(10.0, "FEET", 2.0, "FEET", "LENGTH");
            controller.PerformDivision(10.0, "KILOGRAM", 5.0, "KILOGRAM", "WEIGHT");

            // ---- Temperature (UC14 via N-Tier) ----
            Console.WriteLine("--- Temperature Equality (UC14 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformComparison(100.0, "CELSIUS", 212.0, "FAHRENHEIT", "TEMPERATURE");
            controller.PerformComparison(0.0, "CELSIUS", 32.0, "FAHRENHEIT", "TEMPERATURE");
            controller.PerformComparison(-40.0, "CELSIUS", -40.0, "FAHRENHEIT", "TEMPERATURE");
            Console.WriteLine("--- Temperature Conversion (UC14 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformConversion(100.0, "CELSIUS", "FAHRENHEIT", "TEMPERATURE");
            controller.PerformConversion(32.0, "FAHRENHEIT", "CELSIUS", "TEMPERATURE");

            // ---- Temperature Unsupported Operations (UC14 via N-Tier) ----
            Console.WriteLine("--- Temperature Unsupported Operations (UC14 via N-Tier) ---");
            Console.WriteLine();
            controller.PerformAddition(100.0, "CELSIUS", 50.0, "CELSIUS", "CELSIUS", "TEMPERATURE");
            controller.PerformSubtraction(100.0, "CELSIUS", 50.0, "CELSIUS", "CELSIUS", "TEMPERATURE");
            controller.PerformDivision(100.0, "CELSIUS", 50.0, "CELSIUS", "TEMPERATURE");

            // ---- Cross-Category Safety (UC10/UC15 via N-Tier) ----
            Console.WriteLine("--- Cross-Category Safety (UC10/UC15 via N-Tier) ---");
            Console.WriteLine();
            // These should produce errors (different measurement types)
            try
            {
                QuantityMeasurement.Models.QuantityDTO lengthDTO =
                    new QuantityMeasurement.Models.QuantityDTO(1.0, "FEET", "LENGTH");
                QuantityMeasurement.Models.QuantityDTO weightDTO =
                    new QuantityMeasurement.Models.QuantityDTO(1.0, "KILOGRAM", "WEIGHT");
                service.Compare(lengthDTO, weightDTO);
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Cross-Category Compare: {ex.Message}");
                Console.WriteLine();
            }

            // ---- Repository History ----
            Console.WriteLine("--- Repository History (UC15) ---");
            Console.WriteLine();
            Console.WriteLine($"Total operations recorded: {repository.GetCount()}");
            Console.WriteLine();

            Console.WriteLine("========================================");
            Console.WriteLine("   All Operations Complete");
            Console.WriteLine("========================================");
        }
    }
}
