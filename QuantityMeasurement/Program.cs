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
        public static void DemonstrateLengthEquality(QuantityLength firstLength, QuantityLength secondLength)
        {
            bool isEqual = firstLength.Equals(secondLength);
            Console.WriteLine($"Comparing {firstLength} and {secondLength}");
            Console.WriteLine($"Result: Equal ({isEqual})");
            Console.WriteLine();
        }
        // Demonstrates the comparison feature by creating two QuantityLength objects
        public static void DemonstrateLengthComparison(double firstValue, LengthUnit firstUnit, double secondValue, LengthUnit secondUnit)
        {
            QuantityLength firstLength = new QuantityLength(firstValue, firstUnit);
            QuantityLength secondLength = new QuantityLength(secondValue, secondUnit);
            DemonstrateLengthEquality(firstLength, secondLength);
        }
        // Method Overloading: DemonstrateLengthConversion with raw values
        public static void DemonstrateLengthConversion(double value, LengthUnit fromUnit, LengthUnit toUnit)
        {
            double convertedValue = QuantityLength.Convert(value, fromUnit, toUnit);
            Console.WriteLine($"Converting {value} {fromUnit} to {toUnit}");
            Console.WriteLine($"Result: {convertedValue}");
            Console.WriteLine();
        }
        // Method Overloading: DemonstrateLengthConversion with existing QuantityLength object
        public static void DemonstrateLengthConversion(QuantityLength quantity, LengthUnit targetUnit)
        {
            QuantityLength convertedQuantity = quantity.ConvertTo(targetUnit);
            Console.WriteLine($"Converting {quantity} to {targetUnit}");
            Console.WriteLine($"Result: {convertedQuantity}");
            Console.WriteLine();
        }
        //demonstrates the addition feature with two QuantityLength objects
        //result is in the unit of the first operand
        public static void DemonstrateLengthAddition(QuantityLength first, QuantityLength second)
        {
            QuantityLength result = first.Add(second);
            Console.WriteLine($"Adding {first} + {second}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        //method Overloading: DemonstrateLengthAddition with raw values and units
        public static void DemonstrateLengthAddition(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
        {
            QuantityLength first = new QuantityLength(value1, unit1);
            QuantityLength second = new QuantityLength(value2, unit2);
            DemonstrateLengthAddition(first, second);
        }
        // Main method to demonstrate all features
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC6: Length Addition");
            Console.WriteLine("========================================");
            Console.WriteLine();
            // UC1: Feet equality
            DemonstrateFeetEquality();
            // UC2: Inches equality
            DemonstrateInchesEquality();
            // UC3/UC4: Cross-unit equality
            Console.WriteLine("--- Cross-Unit Equality (UC3/UC4) ---");
            Console.WriteLine();
            DemonstrateLengthComparison(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH);
            // UC5: Conversion
            Console.WriteLine("--- Unit Conversion (UC5) ---");
            Console.WriteLine();
            DemonstrateLengthConversion(1.0, LengthUnit.FEET, LengthUnit.INCH);
            // UC6: Length Addition
            Console.WriteLine("--- Length Addition (UC6) ---");
            Console.WriteLine();
            // Same unit: 1 foot + 2 feet = 3 feet
            DemonstrateLengthAddition(1.0, LengthUnit.FEET, 2.0, LengthUnit.FEET);
            // Cross-unit: 1 foot + 12 inches = 2 feet
            DemonstrateLengthAddition(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH);
            // Cross-unit: 12 inches + 1 foot = 24 inches (result in first operand's unit)
            DemonstrateLengthAddition(12.0, LengthUnit.INCH, 1.0, LengthUnit.FEET);
            // Yard + Feet: 1 yard + 3 feet = 2 yards
            DemonstrateLengthAddition(1.0, LengthUnit.YARDS, 3.0, LengthUnit.FEET);
            // Inches + Yard: 36 inches + 1 yard = 72 inches
            DemonstrateLengthAddition(36.0, LengthUnit.INCH, 1.0, LengthUnit.YARDS);
            // CM + Inches: 2.54 cm + 1 inch = ~5.08 cm
            DemonstrateLengthAddition(2.54, LengthUnit.CENTIMETERS, 1.0, LengthUnit.INCH);
            // Adding zero: 5 feet + 0 inches = 5 feet
            DemonstrateLengthAddition(5.0, LengthUnit.FEET, 0.0, LengthUnit.INCH);
            // Negative: 5 feet + (-2 feet) = 3 feet
            DemonstrateLengthAddition(5.0, LengthUnit.FEET, -2.0, LengthUnit.FEET);
            Console.WriteLine("========================================");
            Console.WriteLine("   All Operations Complete");
            Console.WriteLine("========================================");
        }
    }
}
