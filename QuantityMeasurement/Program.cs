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
        //demonstrates the UC6 addition feature (result in first operand's unit)
        public static void DemonstrateLengthAddition(QuantityLength first, QuantityLength second)
        {
            QuantityLength result = first.Add(second);
            Console.WriteLine($"Adding {first} + {second}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        // Method Overloading: DemonstrateLengthAddition with raw values
        public static void DemonstrateLengthAddition(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
        {
            QuantityLength first = new QuantityLength(value1, unit1);
            QuantityLength second = new QuantityLength(value2, unit2);
            DemonstrateLengthAddition(first, second);
        }
        //uc7: demonstrates addition with explicit target unit specification
        public static void DemonstrateLengthAddition(double value1, LengthUnit unit1, double value2, LengthUnit unit2, LengthUnit targetUnit)
        {
            QuantityLength first = new QuantityLength(value1, unit1);
            QuantityLength second = new QuantityLength(value2, unit2);
            QuantityLength result = first.Add(second, targetUnit);
            Console.WriteLine($"Adding {first} + {second} => target: {targetUnit}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        //uc7: demonstrates addition with explicit target unit using QuantityLength objects
        public static void DemonstrateLengthAddition(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
        {
            QuantityLength result = first.Add(second, targetUnit);
            Console.WriteLine($"Adding {first} + {second} => target: {targetUnit}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        // Main method to demonstrate all features
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC7: Addition with Target Unit");
            Console.WriteLine("========================================");
            Console.WriteLine();
            // UC1 & UC2
            DemonstrateFeetEquality();
            DemonstrateInchesEquality();
            // UC3/UC4: Cross-unit equality
            Console.WriteLine("--- Cross-Unit Equality (UC3/UC4) ---");
            Console.WriteLine();
            DemonstrateLengthComparison(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH);
            // UC5: Conversion
            Console.WriteLine("--- Unit Conversion (UC5) ---");
            Console.WriteLine();
            DemonstrateLengthConversion(1.0, LengthUnit.FEET, LengthUnit.INCH);
            // UC6: Addition (result in first operand's unit)
            Console.WriteLine("--- Length Addition (UC6) ---");
            Console.WriteLine();
            DemonstrateLengthAddition(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH);
            // UC7: Addition with explicit target unit
            Console.WriteLine("--- Addition with Target Unit (UC7) ---");
            Console.WriteLine();
            // Result in FEET
            DemonstrateLengthAddition(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH, LengthUnit.FEET);
            // Result in INCHES
            DemonstrateLengthAddition(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH, LengthUnit.INCH);
            // Result in YARDS (different from both operands)
            DemonstrateLengthAddition(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH, LengthUnit.YARDS);
            // Yards + Feet -> YARDS
            DemonstrateLengthAddition(1.0, LengthUnit.YARDS, 3.0, LengthUnit.FEET, LengthUnit.YARDS);
            // 36 Inches + 1 Yard -> FEET
            DemonstrateLengthAddition(36.0, LengthUnit.INCH, 1.0, LengthUnit.YARDS, LengthUnit.FEET);
            // CM + Inches -> CENTIMETERS
            DemonstrateLengthAddition(2.54, LengthUnit.CENTIMETERS, 1.0, LengthUnit.INCH, LengthUnit.CENTIMETERS);
            // Zero with target unit: 5 feet + 0 inches -> YARDS
            DemonstrateLengthAddition(5.0, LengthUnit.FEET, 0.0, LengthUnit.INCH, LengthUnit.YARDS);
            // Negative with target unit: 5 feet + (-2 feet) -> INCHES
            DemonstrateLengthAddition(5.0, LengthUnit.FEET, -2.0, LengthUnit.FEET, LengthUnit.INCH);
            Console.WriteLine("========================================");
            Console.WriteLine("   All Operations Complete");
            Console.WriteLine("========================================");
        }
    }
}
