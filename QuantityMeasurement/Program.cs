using QuantityMeasurement.Models;
using QuantityMeasurement.Services;
namespace QuantityMeasurement
{
    public class Program
    {
        //demonstrates Feet equality check (UC1)
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
        //demonstrates Inches equality check (UC2)
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
        //demonstrates equality check between two QuantityLength objects
        public static void DemonstrateLengthEquality(QuantityLength firstLength, QuantityLength secondLength)
        {
            bool isEqual = firstLength.Equals(secondLength);
            Console.WriteLine($"Comparing {firstLength} and {secondLength}");
            Console.WriteLine($"Result: Equal ({isEqual})");
            Console.WriteLine();
        }
        //demonstrates comparison by creating QuantityLength objects from raw values
        public static void DemonstrateLengthComparison(double firstValue, LengthUnit firstUnit, double secondValue, LengthUnit secondUnit)
        {
            QuantityLength firstLength = new QuantityLength(firstValue, firstUnit);
            QuantityLength secondLength = new QuantityLength(secondValue, secondUnit);
            DemonstrateLengthEquality(firstLength, secondLength);
        }
        //method Overloading: DemonstrateLengthConversion with raw values (UC5)
        public static void DemonstrateLengthConversion(double value, LengthUnit fromUnit, LengthUnit toUnit)
        {
            double convertedValue = QuantityLength.Convert(value, fromUnit, toUnit);
            Console.WriteLine($"Converting {value} {fromUnit} to {toUnit}");
            Console.WriteLine($"Result: {convertedValue}");
            Console.WriteLine();
        }
        //method Overloading: DemonstrateLengthConversion with QuantityLength object (UC5)
        public static void DemonstrateLengthConversion(QuantityLength quantity, LengthUnit targetUnit)
        {
            QuantityLength convertedQuantity = quantity.ConvertTo(targetUnit);
            Console.WriteLine($"Converting {quantity} to {targetUnit}");
            Console.WriteLine($"Result: {convertedQuantity}");
            Console.WriteLine();
        }
        //demonstrates addition with result in first operand's unit (UC6)
        public static void DemonstrateLengthAddition(QuantityLength first, QuantityLength second)
        {
            QuantityLength result = first.Add(second);
            Console.WriteLine($"Adding {first} + {second}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        //method Overloading: DemonstrateLengthAddition with raw values (UC6)
        public static void DemonstrateLengthAddition(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
        {
            QuantityLength first = new QuantityLength(value1, unit1);
            QuantityLength second = new QuantityLength(value2, unit2);
            DemonstrateLengthAddition(first, second);
        }
        //method Overloading: DemonstrateLengthAddition with explicit target unit (UC7)
        public static void DemonstrateLengthAddition(double value1, LengthUnit unit1, double value2, LengthUnit unit2, LengthUnit targetUnit)
        {
            QuantityLength first = new QuantityLength(value1, unit1);
            QuantityLength second = new QuantityLength(value2, unit2);
            QuantityLength result = first.Add(second, targetUnit);
            Console.WriteLine($"Adding {first} + {second} => target: {targetUnit}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        //method Overloading: DemonstrateLengthAddition with QuantityLength objects and target unit (UC7)
        public static void DemonstrateLengthAddition(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
        {
            QuantityLength result = first.Add(second, targetUnit);
            Console.WriteLine($"Adding {first} + {second} => target: {targetUnit}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        // Demonstrates weight equality check (UC9)
        public static void DemonstrateWeightEquality(QuantityWeight firstWeight, QuantityWeight secondWeight)
        {
            bool isEqual = firstWeight.Equals(secondWeight);
            Console.WriteLine($"Comparing {firstWeight} and {secondWeight}");
            Console.WriteLine($"Result: Equal ({isEqual})");
            Console.WriteLine();
        }
        // Demonstrates weight comparison by creating QuantityWeight objects from raw values (UC9)
        public static void DemonstrateWeightComparison(double firstValue, WeightUnit firstUnit, double secondValue, WeightUnit secondUnit)
        {
            QuantityWeight firstWeight = new QuantityWeight(firstValue, firstUnit);
            QuantityWeight secondWeight = new QuantityWeight(secondValue, secondUnit);
            DemonstrateWeightEquality(firstWeight, secondWeight);
        }
        // Demonstrates weight conversion with raw values (UC9)
        public static void DemonstrateWeightConversion(double value, WeightUnit fromUnit, WeightUnit toUnit)
        {
            double convertedValue = QuantityWeight.Convert(value, fromUnit, toUnit);
            Console.WriteLine($"Converting {value} {fromUnit} to {toUnit}");
            Console.WriteLine($"Result: {convertedValue}");
            Console.WriteLine();
        }
        // Demonstrates weight addition with raw values (UC9)
        public static void DemonstrateWeightAddition(double value1, WeightUnit unit1, double value2, WeightUnit unit2)
        {
            QuantityWeight first = new QuantityWeight(value1, unit1);
            QuantityWeight second = new QuantityWeight(value2, unit2);
            QuantityWeight result = first.Add(second);
            Console.WriteLine($"Adding {first} + {second}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        // Demonstrates weight addition with explicit target unit (UC9)
        public static void DemonstrateWeightAddition(double value1, WeightUnit unit1, double value2, WeightUnit unit2, WeightUnit targetUnit)
        {
            QuantityWeight first = new QuantityWeight(value1, unit1);
            QuantityWeight second = new QuantityWeight(value2, unit2);
            QuantityWeight result = first.Add(second, targetUnit);
            Console.WriteLine($"Adding {first} + {second} => target: {targetUnit}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        //main method demonstrates all features including UC8 refactored design
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC9: Weight Measurement Support");
            Console.WriteLine("========================================");
            Console.WriteLine();
            // UC1 & UC2: Feet and Inches equality
            DemonstrateFeetEquality();
            DemonstrateInchesEquality();
            // UC3/UC4: Cross-unit equality
            Console.WriteLine("--- Cross-Unit Equality (UC3/UC4) ---");
            Console.WriteLine();
            DemonstrateLengthComparison(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH);
            DemonstrateLengthComparison(36.0, LengthUnit.INCH, 1.0, LengthUnit.YARDS);
            // UC5: Conversion using refactored design
            Console.WriteLine("--- Unit Conversion (UC5) ---");
            Console.WriteLine();
            DemonstrateLengthConversion(1.0, LengthUnit.FEET, LengthUnit.INCH);
            // UC6: Addition
            Console.WriteLine("--- Length Addition (UC6) ---");
            Console.WriteLine();
            DemonstrateLengthAddition(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH);
            // UC7: Addition with target unit
            Console.WriteLine("--- Addition with Target Unit (UC7) ---");
            Console.WriteLine();
            DemonstrateLengthAddition(1.0, LengthUnit.YARDS, 3.0, LengthUnit.FEET, LengthUnit.YARDS);
            //uc8: LengthUnit standalone conversion methods
            Console.WriteLine("--- LengthUnit Standalone Conversion (UC8) ---");
            Console.WriteLine();
            double inchBaseValue = LengthUnit.INCH.ConvertToBaseUnit(12.0);
            Console.WriteLine($"LengthUnit.INCH.ConvertToBaseUnit(12.0) = {inchBaseValue}");
            Console.WriteLine();
            // UC9: Weight Measurement Support
            Console.WriteLine("--- Weight Equality (UC9) ---");
            Console.WriteLine();
            DemonstrateWeightComparison(1.0, WeightUnit.KILOGRAM, 1.0, WeightUnit.KILOGRAM);
            DemonstrateWeightComparison(1.0, WeightUnit.KILOGRAM, 1000.0, WeightUnit.GRAM);
            DemonstrateWeightComparison(500.0, WeightUnit.GRAM, 0.5, WeightUnit.KILOGRAM);
            Console.WriteLine("--- Weight Conversion (UC9) ---");
            Console.WriteLine();
            DemonstrateWeightConversion(1.0, WeightUnit.KILOGRAM, WeightUnit.GRAM);
            DemonstrateWeightConversion(2.0, WeightUnit.POUND, WeightUnit.KILOGRAM);
            DemonstrateWeightConversion(0.0, WeightUnit.KILOGRAM, WeightUnit.GRAM);
            Console.WriteLine("--- Weight Addition (UC9) ---");
            Console.WriteLine();
            DemonstrateWeightAddition(1.0, WeightUnit.KILOGRAM, 2.0, WeightUnit.KILOGRAM);
            DemonstrateWeightAddition(1.0, WeightUnit.KILOGRAM, 1000.0, WeightUnit.GRAM);
            DemonstrateWeightAddition(500.0, WeightUnit.GRAM, 0.5, WeightUnit.KILOGRAM);
            Console.WriteLine("--- Weight Addition with Target Unit (UC9) ---");
            Console.WriteLine();
            DemonstrateWeightAddition(1.0, WeightUnit.KILOGRAM, 1000.0, WeightUnit.GRAM, WeightUnit.GRAM);
            DemonstrateWeightAddition(2.0, WeightUnit.KILOGRAM, 4.0, WeightUnit.POUND, WeightUnit.KILOGRAM);
            // Category Incompatibility
            Console.WriteLine("--- Category Incompatibility (UC9) ---");
            Console.WriteLine();
            QuantityWeight oneKg = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            QuantityLength oneFoot = new QuantityLength(1.0, LengthUnit.FEET);
            bool isEqual = oneKg.Equals(oneFoot);
            Console.WriteLine($"Comparing {oneKg} (weight) and {oneFoot} (length)");
            Console.WriteLine($"Result: Equal ({isEqual})");
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("   All Operations Complete");
            Console.WriteLine("========================================");
        }
    }
}
