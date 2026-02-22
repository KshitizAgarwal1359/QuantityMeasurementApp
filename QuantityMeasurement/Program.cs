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
        }
        // Define a static method to demonstrate Yards and Centimeters equality check
        public static void DemonstrateExtendedUnitEquality()
        {
            Console.WriteLine("--- Extended Unit Equality Check (UC4) ---");
            Console.WriteLine();
            QuantityMeasurementService quantityMeasurementService = new QuantityMeasurementService();
            // Yard to Feet: 1 Yard should equal 3 Feet
            QuantityLength oneYard = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength threeFeet = new QuantityLength(3.0, LengthUnit.FEET);
            bool isYardToFeetEqual = quantityMeasurementService.CompareQuantityLengthMeasurements(oneYard, threeFeet);
            Console.WriteLine($"Comparing {oneYard} and {threeFeet}");
            Console.WriteLine($"Result: Equal ({isYardToFeetEqual})");
            Console.WriteLine();
            // Yard to Inches: 1 Yard should equal 36 Inches
            QuantityLength thirtySixInches = new QuantityLength(36.0, LengthUnit.INCH);
            bool isYardToInchesEqual = quantityMeasurementService.CompareQuantityLengthMeasurements(oneYard, thirtySixInches);
            Console.WriteLine($"Comparing {oneYard} and {thirtySixInches}");
            Console.WriteLine($"Result: Equal ({isYardToInchesEqual})");
            Console.WriteLine();
            // Yard to Yard: 2 Yards should equal 2 Yards
            QuantityLength firstTwoYards = new QuantityLength(2.0, LengthUnit.YARDS);
            QuantityLength secondTwoYards = new QuantityLength(2.0, LengthUnit.YARDS);
            bool isYardToYardEqual = quantityMeasurementService.CompareQuantityLengthMeasurements(firstTwoYards, secondTwoYards);
            Console.WriteLine($"Comparing {firstTwoYards} and {secondTwoYards}");
            Console.WriteLine($"Result: Equal ({isYardToYardEqual})");
            Console.WriteLine();
            // CM to CM: 2 CM should equal 2 CM
            QuantityLength firstTwoCm = new QuantityLength(2.0, LengthUnit.CENTIMETERS);
            QuantityLength secondTwoCm = new QuantityLength(2.0, LengthUnit.CENTIMETERS);
            bool isCmToCmEqual = quantityMeasurementService.CompareQuantityLengthMeasurements(firstTwoCm, secondTwoCm);
            Console.WriteLine($"Comparing {firstTwoCm} and {secondTwoCm}");
            Console.WriteLine($"Result: Equal ({isCmToCmEqual})");
            Console.WriteLine();
            // CM to Inches: 1 CM should equal 0.393701 Inches
            QuantityLength oneCm = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            QuantityLength equivalentInches = new QuantityLength(0.393701, LengthUnit.INCH);
            bool isCmToInchEqual = quantityMeasurementService.CompareQuantityLengthMeasurements(oneCm, equivalentInches);
            Console.WriteLine($"Comparing {oneCm} and {equivalentInches}");
            Console.WriteLine($"Result: Equal ({isCmToInchEqual})");
            Console.WriteLine();
        }
        // Main method to demonstrate all equality checks
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC4: Extended Unit Support");
            Console.WriteLine("========================================");
            Console.WriteLine();
            DemonstrateFeetEquality();
            DemonstrateInchesEquality();
            DemonstrateQuantityLengthEquality();
            DemonstrateExtendedUnitEquality();
            Console.WriteLine("========================================");
            Console.WriteLine("   Equality Comparison Complete");
            Console.WriteLine("========================================");
        }
    }
}
