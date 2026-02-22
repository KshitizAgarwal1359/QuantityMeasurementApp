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
        // Demonstrates the equality check feature by comparing two QuantityLength objects
        // API-like static method for showcasing comparison functionality
        public static void DemonstrateLengthEquality(QuantityLength firstLength, QuantityLength secondLength)
        {
            bool isEqual = firstLength.Equals(secondLength);
            Console.WriteLine($"Comparing {firstLength} and {secondLength}");
            Console.WriteLine($"Result: Equal ({isEqual})");
            Console.WriteLine();
        }
        // Demonstrates the comparison feature by creating two QuantityLength objects
        // and checking their equality using DemonstrateLengthEquality
        public static void DemonstrateLengthComparison(double firstValue, LengthUnit firstUnit, double secondValue, LengthUnit secondUnit)
        {
            QuantityLength firstLength = new QuantityLength(firstValue, firstUnit);
            QuantityLength secondLength = new QuantityLength(secondValue, secondUnit);
            DemonstrateLengthEquality(firstLength, secondLength);
        }
        // Method Overloading: DemonstrateLengthConversion with raw values (value, fromUnit, toUnit)
        // Used when you have raw values to convert
        public static void DemonstrateLengthConversion(double value, LengthUnit fromUnit, LengthUnit toUnit)
        {
            double convertedValue = QuantityLength.Convert(value, fromUnit, toUnit);
            Console.WriteLine($"Converting {value} {fromUnit} to {toUnit}");
            Console.WriteLine($"Result: {convertedValue}");
            Console.WriteLine();
        }
        // Method Overloading: DemonstrateLengthConversion with existing QuantityLength object
        // Used when you already have a QuantityLength instance
        public static void DemonstrateLengthConversion(QuantityLength quantity, LengthUnit targetUnit)
        {
            QuantityLength convertedQuantity = quantity.ConvertTo(targetUnit);
            Console.WriteLine($"Converting {quantity} to {targetUnit}");
            Console.WriteLine($"Result: {convertedQuantity}");
            Console.WriteLine();
        }
        // Main method to demonstrate all features
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC5: Unit-to-Unit Conversion");
            Console.WriteLine("========================================");
            Console.WriteLine();
            // UC1: Feet equality
            DemonstrateFeetEquality();
            // UC2: Inches equality
            DemonstrateInchesEquality();
            // UC3/UC4: Cross-unit equality using DemonstrateLengthComparison
            Console.WriteLine("--- Cross-Unit Equality (UC3/UC4) ---");
            Console.WriteLine();
            DemonstrateLengthComparison(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH);
            DemonstrateLengthComparison(1.0, LengthUnit.YARDS, 3.0, LengthUnit.FEET);
            // UC5: Unit-to-Unit Conversion demos
            Console.WriteLine("--- Unit-to-Unit Conversion (UC5) ---");
            Console.WriteLine();
            // Method 1: DemonstrateLengthConversion with raw values
            DemonstrateLengthConversion(1.0, LengthUnit.FEET, LengthUnit.INCH);
            DemonstrateLengthConversion(3.0, LengthUnit.YARDS, LengthUnit.FEET);
            DemonstrateLengthConversion(36.0, LengthUnit.INCH, LengthUnit.YARDS);
            DemonstrateLengthConversion(1.0, LengthUnit.CENTIMETERS, LengthUnit.INCH);
            DemonstrateLengthConversion(0.0, LengthUnit.FEET, LengthUnit.INCH);

            // Method 2: DemonstrateLengthConversion with existing QuantityLength object
            QuantityLength lengthInYards = new QuantityLength(2.0, LengthUnit.YARDS);
            DemonstrateLengthConversion(lengthInYards, LengthUnit.INCH);
            DemonstrateLengthConversion(lengthInYards, LengthUnit.FEET);
            Console.WriteLine("========================================");
            Console.WriteLine("   All Operations Complete");
            Console.WriteLine("========================================");
        }
    }
}
