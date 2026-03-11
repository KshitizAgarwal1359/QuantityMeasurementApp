using QuantityMeasurement.Models;
using QuantityMeasurement.Service;

namespace QuantityMeasurement.Controllers
{
    // UC15: QuantityMeasurementController — orchestration and presentation layer.
    // Handles user interactions and delegates business logic to the Service layer.
    //
    // Key principles:
    //   - Thin controller philosophy: minimal business logic
    //   - Delegates all computation to IQuantityMeasurementService
    //   - Acts as a Facade for the service layer complexity
    //   - Methods named performXXX to reflect REST API readiness
    //
    // REST API Readiness (future extension):
    //   POST /api/quantity/compare
    //   POST /api/quantity/convert
    //   POST /api/quantity/add
    //   POST /api/quantity/subtract
    //   POST /api/quantity/divide
    //
    // Dependency Injection: IQuantityMeasurementService injected via constructor.
    public class QuantityMeasurementController
    {
        // Service dependency — injected via constructor (DIP)
        private readonly IQuantityMeasurementService service;

        // Constructor injection — promotes loose coupling and testability
        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            this.service = service ?? throw new ArgumentNullException(
                nameof(service), "Service cannot be null.");
        }

        // Performs quantity comparison and displays result.
        // Delegates to service.Compare() and formats output.
        public void PerformComparison(double value1, string unit1, double value2, string unit2,
            string measurementType)
        {
            QuantityDTO first = new QuantityDTO(value1, unit1, measurementType);
            QuantityDTO second = new QuantityDTO(value2, unit2, measurementType);
            try
            {
                QuantityDTO result = service.Compare(first, second);
                bool isEqual = result.Value == 1.0;
                Console.WriteLine($"Comparing {first} and {second}");
                Console.WriteLine($"Result: Equal ({isEqual})");
                Console.WriteLine();
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Comparison Error: {ex.Message}");
                Console.WriteLine();
            }
        }

        // Performs unit conversion and displays result.
        // Delegates to service.Convert() and formats output.
        public void PerformConversion(double value, string sourceUnit, string targetUnit,
            string measurementType)
        {
            QuantityDTO source = new QuantityDTO(value, sourceUnit, measurementType);
            try
            {
                QuantityDTO result = service.Convert(source, targetUnit);
                Console.WriteLine($"Converting {value} {sourceUnit} to {targetUnit}");
                Console.WriteLine($"Result: {result.Value}");
                Console.WriteLine();
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Conversion Error: {ex.Message}");
                Console.WriteLine();
            }
        }

        // Performs quantity addition and displays result.
        // Delegates to service.Add() and formats output.
        public void PerformAddition(double value1, string unit1, double value2, string unit2,
            string targetUnit, string measurementType)
        {
            QuantityDTO first = new QuantityDTO(value1, unit1, measurementType);
            QuantityDTO second = new QuantityDTO(value2, unit2, measurementType);
            try
            {
                QuantityDTO result = service.Add(first, second, targetUnit);
                Console.WriteLine($"Adding {first} + {second} => target: {targetUnit}");
                Console.WriteLine($"Result: {result.Value} {result.UnitName}");
                Console.WriteLine();
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Addition Error: {ex.Message}");
                Console.WriteLine();
            }
        }

        // Performs quantity subtraction and displays result.
        // Delegates to service.Subtract() and formats output.
        public void PerformSubtraction(double value1, string unit1, double value2, string unit2,
            string targetUnit, string measurementType)
        {
            QuantityDTO first = new QuantityDTO(value1, unit1, measurementType);
            QuantityDTO second = new QuantityDTO(value2, unit2, measurementType);
            try
            {
                QuantityDTO result = service.Subtract(first, second, targetUnit);
                Console.WriteLine($"Subtracting {first} - {second} => target: {targetUnit}");
                Console.WriteLine($"Result: {result.Value} {result.UnitName}");
                Console.WriteLine();
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Subtraction Error: {ex.Message}");
                Console.WriteLine();
            }
        }

        // Performs quantity division and displays result.
        // Delegates to service.Divide() and formats dimensionless scalar result.
        public void PerformDivision(double value1, string unit1, double value2, string unit2,
            string measurementType)
        {
            QuantityDTO first = new QuantityDTO(value1, unit1, measurementType);
            QuantityDTO second = new QuantityDTO(value2, unit2, measurementType);
            try
            {
                QuantityDTO result = service.Divide(first, second);
                Console.WriteLine($"Dividing {first} / {second}");
                Console.WriteLine($"Result: {result.Value}");
                Console.WriteLine();
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Division Error: {ex.Message}");
                Console.WriteLine();
            }
        }
    }
}
