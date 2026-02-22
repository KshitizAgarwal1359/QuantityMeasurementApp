using QuantityMeasurement.Models;
using QuantityMeasurement.Services;
namespace QuantityMeasurement
{
    public class Program
    {
        // Define a static method to demonstrate Feet equality check
        public static void DemonstrateFeetEquality()
        {
            Console.WriteLine("--- Feet Equality Check (UC1) ---");
            Console.WriteLine();
            QuantityMeasurementService quantityMeasurementService = new QuantityMeasurementService();
            Feet firstFeetValue = new Feet(1.0);
            Feet secondFeetValue = new Feet(1.0);
            bool isEqualSameValue = quantityMeasurementService.CompareFeetMeasurements(firstFeetValue, secondFeetValue);
            Console.WriteLine($"Comparing {firstFeetValue} and {secondFeetValue}");
            Console.WriteLine($"Result: Equal ({isEqualSameValue})");
            Console.WriteLine();
        }

        // Define a static method to demonstrate Inches equality check
        public static void DemonstrateInchesEquality()
        {
            Console.WriteLine("--- Inches Equality Check (UC2) ---");
            Console.WriteLine();
            QuantityMeasurementService quantityMeasurementService = new QuantityMeasurementService();
            Inches firstInchesValue = new Inches(1.0);
            Inches secondInchesValue = new Inches(1.0);
            bool isEqualSameValue = quantityMeasurementService.CompareInchesMeasurements(firstInchesValue, secondInchesValue);
            Console.WriteLine($"Comparing {firstInchesValue} and {secondInchesValue}");
            Console.WriteLine($"Result: Equal ({isEqualSameValue})");
            Console.WriteLine();
        }

        // Define a static method to demonstrate QuantityLength cross-unit equality check
        public static void DemonstrateQuantityLengthEquality()
        {
            Console.WriteLine("--- QuantityLength Cross-Unit Equality Check (UC3) ---");
            Console.WriteLine();
            QuantityMeasurementService quantityMeasurementService = new QuantityMeasurementService();

            // Cross-unit comparison: 1 Foot should equal 12 Inches
            QuantityLength oneFoot = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength twelveInches = new QuantityLength(12.0, LengthUnit.INCH);
            bool isCrossUnitEqual = quantityMeasurementService.CompareQuantityLengthMeasurements(oneFoot, twelveInches);
            Console.WriteLine($"Comparing {oneFoot} and {twelveInches}");
            Console.WriteLine($"Result: Equal ({isCrossUnitEqual})");
            Console.WriteLine();

            // Same unit comparison: 1 Inch should equal 1 Inch
            QuantityLength firstInch = new QuantityLength(1.0, LengthUnit.INCH);
            QuantityLength secondInch = new QuantityLength(1.0, LengthUnit.INCH);
            bool isSameUnitEqual = quantityMeasurementService.CompareQuantityLengthMeasurements(firstInch, secondInch);
            Console.WriteLine($"Comparing {firstInch} and {secondInch}");
            Console.WriteLine($"Result: Equal ({isSameUnitEqual})");
            Console.WriteLine();

            // Different value comparison: 1 Foot should NOT equal 2 Feet
            QuantityLength oneFootValue = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength twoFeetValue = new QuantityLength(2.0, LengthUnit.FEET);
            bool isDifferentValueEqual = quantityMeasurementService.CompareQuantityLengthMeasurements(oneFootValue, twoFeetValue);
            Console.WriteLine($"Comparing {oneFootValue} and {twoFeetValue}");
            Console.WriteLine($"Result: Equal ({isDifferentValueEqual})");
            Console.WriteLine();
        }

        // Main method to demonstrate Feet, Inches, and QuantityLength equality checks
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC3: Generic Quantity Class (DRY)");
            Console.WriteLine("========================================");
            Console.WriteLine();
            DemonstrateFeetEquality();
            DemonstrateInchesEquality();
            DemonstrateQuantityLengthEquality();
            Console.WriteLine("========================================");
            Console.WriteLine("   Equality Comparison Complete");
            Console.WriteLine("========================================");
        }
    }
}
