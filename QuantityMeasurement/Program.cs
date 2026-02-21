using QuantityMeasurement.Models;
using QuantityMeasurement.Services;
namespace QuantityMeasurement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC1: Feet Measurement Equality");
            Console.WriteLine("========================================");
            Console.WriteLine();
            // Initialize the service for measurement comparisons
            QuantityMeasurementService quantityMeasurementService = new QuantityMeasurementService();
            // Create Feet measurement objects with same values
            Feet firstFeetValue = new Feet(1.0);
            Feet secondFeetValue = new Feet(1.0);
            // Compare two Feet objects with the same value
            bool isEqualSameValue = quantityMeasurementService.CompareFeetMeasurements(firstFeetValue, secondFeetValue);
            Console.WriteLine($"Comparing {firstFeetValue} and {secondFeetValue}");
            Console.WriteLine($"Result: Equal ({isEqualSameValue})");
            Console.WriteLine();
            // Create Feet measurement objects with different values
            Feet thirdFeetValue = new Feet(2.0);
            // Compare two Feet objects with different values
            bool isEqualDifferentValue = quantityMeasurementService.CompareFeetMeasurements(firstFeetValue, thirdFeetValue);
            Console.WriteLine($"Comparing {firstFeetValue} and {thirdFeetValue}");
            Console.WriteLine($"Result: Equal ({isEqualDifferentValue})");
            Console.WriteLine();
            // Compare Feet object with null
            bool isEqualWithNull = quantityMeasurementService.CompareFeetMeasurements(firstFeetValue, null);
            Console.WriteLine($"Comparing {firstFeetValue} and null");
            Console.WriteLine($"Result: Equal ({isEqualWithNull})");
            Console.WriteLine();
            // Compare same reference
            bool isEqualSameReference = quantityMeasurementService.CompareFeetMeasurements(firstFeetValue, firstFeetValue);
            Console.WriteLine($"Comparing {firstFeetValue} with itself (same reference)");
            Console.WriteLine($"Result: Equal ({isEqualSameReference})");
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("   Feet Equality Comparison Complete");
            Console.WriteLine("========================================");
        }
    }
}
