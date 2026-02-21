using QuantityMeasurement.Models;
using QuantityMeasurement.Services;
namespace QuantityMeasurement
{
    public class Program
    {
        // Define a static method to demonstrate Feet equality check
        public static void DemonstrateFeetEquality()
        {
            Console.WriteLine("--- Feet Equality Check ---");
            Console.WriteLine();
            QuantityMeasurementService quantityMeasurementService = new QuantityMeasurementService();
            // Compare two Feet objects with the same value
            Feet firstFeetValue = new Feet(1.0);
            Feet secondFeetValue = new Feet(1.0);
            bool isEqualSameValue = quantityMeasurementService.CompareFeetMeasurements(firstFeetValue, secondFeetValue);
            Console.WriteLine($"Comparing {firstFeetValue} and {secondFeetValue}");
            Console.WriteLine($"Result: Equal ({isEqualSameValue})");
            Console.WriteLine();
            // Compare two Feet objects with different values
            Feet thirdFeetValue = new Feet(2.0);
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
        }
        // Define a static method to demonstrate Inches equality check
        public static void DemonstrateInchesEquality()
        {
            Console.WriteLine("--- Inches Equality Check ---");
            Console.WriteLine();
            QuantityMeasurementService quantityMeasurementService = new QuantityMeasurementService();
            // Compare two Inches objects with the same value
            Inches firstInchesValue = new Inches(1.0);
            Inches secondInchesValue = new Inches(1.0);
            bool isEqualSameValue = quantityMeasurementService.CompareInchesMeasurements(firstInchesValue, secondInchesValue);
            Console.WriteLine($"Comparing {firstInchesValue} and {secondInchesValue}");
            Console.WriteLine($"Result: Equal ({isEqualSameValue})");
            Console.WriteLine();
            // Compare two Inches objects with different values
            Inches thirdInchesValue = new Inches(2.0);
            bool isEqualDifferentValue = quantityMeasurementService.CompareInchesMeasurements(firstInchesValue, thirdInchesValue);
            Console.WriteLine($"Comparing {firstInchesValue} and {thirdInchesValue}");
            Console.WriteLine($"Result: Equal ({isEqualDifferentValue})");
            Console.WriteLine();
            // Compare Inches object with null
            bool isEqualWithNull = quantityMeasurementService.CompareInchesMeasurements(firstInchesValue, null);
            Console.WriteLine($"Comparing {firstInchesValue} and null");
            Console.WriteLine($"Result: Equal ({isEqualWithNull})");
            Console.WriteLine();
            // Compare same reference
            bool isEqualSameReference = quantityMeasurementService.CompareInchesMeasurements(firstInchesValue, firstInchesValue);
            Console.WriteLine($"Comparing {firstInchesValue} with itself (same reference)");
            Console.WriteLine($"Result: Equal ({isEqualSameReference})");
            Console.WriteLine();
        }
        // Main method to demonstrate both Feet and Inches equality checks
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC2: Feet and Inches Measurement Equality");
            Console.WriteLine("========================================");
            Console.WriteLine();
            DemonstrateFeetEquality();
            DemonstrateInchesEquality();
            Console.WriteLine("========================================");
            Console.WriteLine("   Equality Comparison Complete");
            Console.WriteLine("========================================");
        }
    }
}
