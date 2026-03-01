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
        // UC10: Generic equality demonstration — works with ANY measurement category
        // Replaces both DemonstrateLengthEquality and DemonstrateWeightEquality
        // Single method handles all categories through polymorphism
        public static void DemonstrateEquality<U>(Quantity<U> first, Quantity<U> second) where U : class, IMeasurable
        {
            bool isEqual = first.Equals(second);
            Console.WriteLine($"Comparing {first} and {second}");
            Console.WriteLine($"Result: Equal ({isEqual})");
            Console.WriteLine();
        }
        // UC10: Generic comparison from raw values — works with ANY measurement category
        public static void DemonstrateComparison<U>(double firstValue, U firstUnit, double secondValue, U secondUnit) where U : class, IMeasurable
        {
            Quantity<U> first = new Quantity<U>(firstValue, firstUnit);
            Quantity<U> second = new Quantity<U>(secondValue, secondUnit);
            DemonstrateEquality(first, second);
        }
        // UC10: Generic conversion demonstration — works with ANY measurement category
        // Replaces both DemonstrateLengthConversion and DemonstrateWeightConversion
        public static void DemonstrateConversion<U>(double value, U fromUnit, U toUnit) where U : class, IMeasurable
        {
            double convertedValue = Quantity<U>.Convert(value, fromUnit, toUnit);
            Console.WriteLine($"Converting {value} {fromUnit} to {toUnit}");
            Console.WriteLine($"Result: {convertedValue}");
            Console.WriteLine();
        }
        // UC10: Generic addition demonstration — works with ANY measurement category
        // Replaces both DemonstrateLengthAddition and DemonstrateWeightAddition
        public static void DemonstrateAddition<U>(double value1, U unit1, double value2, U unit2) where U : class, IMeasurable
        {
            Quantity<U> first = new Quantity<U>(value1, unit1);
            Quantity<U> second = new Quantity<U>(value2, unit2);
            Quantity<U> result = first.Add(second);
            Console.WriteLine($"Adding {first} + {second}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        // UC10: Generic addition with explicit target unit — works with ANY measurement category
        public static void DemonstrateAddition<U>(double value1, U unit1, double value2, U unit2, U targetUnit) where U : class, IMeasurable
        {
            Quantity<U> first = new Quantity<U>(value1, unit1);
            Quantity<U> second = new Quantity<U>(value2, unit2);
            Quantity<U> result = first.Add(second, targetUnit);
            Console.WriteLine($"Adding {first} + {second} => target: {targetUnit}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
        //main method demonstrates all features with UC10 generic design
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Quantity Measurement Application");
            Console.WriteLine("   UC11: Volume Measurement Support");
            Console.WriteLine("========================================");
            Console.WriteLine();

            // UC1 & UC2: Feet and Inches equality
            DemonstrateFeetEquality();
            DemonstrateInchesEquality();

            // UC3/UC4: Cross-unit length equality — using generic DemonstrateComparison
            Console.WriteLine("--- Cross-Unit Length Equality (UC3/UC4) ---");
            Console.WriteLine();
            DemonstrateComparison(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH);
            DemonstrateComparison(36.0, LengthUnit.INCH, 1.0, LengthUnit.YARDS);

            // UC5: Length conversion — using generic DemonstrateConversion
            Console.WriteLine("--- Length Conversion (UC5) ---");
            Console.WriteLine();
            DemonstrateConversion(1.0, LengthUnit.FEET, LengthUnit.INCH);

            // UC6/UC7: Length addition — using generic DemonstrateAddition
            Console.WriteLine("--- Length Addition (UC6/UC7) ---");
            Console.WriteLine();
            DemonstrateAddition(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCH);
            DemonstrateAddition(1.0, LengthUnit.YARDS, 3.0, LengthUnit.FEET, LengthUnit.YARDS);
            // UC8: LengthUnit standalone conversion (now via IMeasurable)
            Console.WriteLine("--- LengthUnit IMeasurable Methods (UC8/UC10) ---");
            Console.WriteLine();
            double inchBaseValue = LengthUnit.INCH.ConvertToBaseUnit(12.0);
            Console.WriteLine($"LengthUnit.INCH.ConvertToBaseUnit(12.0) = {inchBaseValue}");
            Console.WriteLine();
            // UC9: Weight — using SAME generic methods as length
            Console.WriteLine("--- Weight Equality (UC9 via Generic) ---");
            Console.WriteLine();
            DemonstrateComparison(1.0, WeightUnit.KILOGRAM, 1.0, WeightUnit.KILOGRAM);
            DemonstrateComparison(1.0, WeightUnit.KILOGRAM, 1000.0, WeightUnit.GRAM);
            Console.WriteLine("--- Weight Conversion (UC9 via Generic) ---");
            Console.WriteLine();
            DemonstrateConversion(1.0, WeightUnit.KILOGRAM, WeightUnit.GRAM);
            DemonstrateConversion(2.0, WeightUnit.POUND, WeightUnit.KILOGRAM);
            Console.WriteLine("--- Weight Addition (UC9 via Generic) ---");
            Console.WriteLine();
            DemonstrateAddition(1.0, WeightUnit.KILOGRAM, 1000.0, WeightUnit.GRAM);
            DemonstrateAddition(1.0, WeightUnit.KILOGRAM, 1000.0, WeightUnit.GRAM, WeightUnit.GRAM);
            // UC10: Cross-category prevention — type safety
            Console.WriteLine("--- Cross-Category Type Safety (UC10) ---");
            Console.WriteLine();
            QuantityLength oneFoot = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityWeight oneKg = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            bool crossCategoryEqual = oneKg.Equals(oneFoot);
            Console.WriteLine($"Comparing {oneKg} (weight) and {oneFoot} (length)");
            Console.WriteLine($"Result: Equal ({crossCategoryEqual})");
            Console.WriteLine();
            // UC11: Volume Measurement — using SAME generic methods (validates UC10 scalability)
            Console.WriteLine("--- Volume Equality (UC11 via Generic) ---");
            Console.WriteLine();
            DemonstrateComparison(1.0, VolumeUnit.LITRE, 1.0, VolumeUnit.LITRE);
            DemonstrateComparison(1.0, VolumeUnit.LITRE, 1000.0, VolumeUnit.MILLILITRE);
            DemonstrateComparison(500.0, VolumeUnit.MILLILITRE, 0.5, VolumeUnit.LITRE);
            Console.WriteLine("--- Volume Conversion (UC11 via Generic) ---");
            Console.WriteLine();
            DemonstrateConversion(1.0, VolumeUnit.LITRE, VolumeUnit.MILLILITRE);
            DemonstrateConversion(2.0, VolumeUnit.GALLON, VolumeUnit.LITRE);
            DemonstrateConversion(0.0, VolumeUnit.LITRE, VolumeUnit.MILLILITRE);
            Console.WriteLine("--- Volume Addition (UC11 via Generic) ---");
            Console.WriteLine();
            DemonstrateAddition(1.0, VolumeUnit.LITRE, 2.0, VolumeUnit.LITRE);
            DemonstrateAddition(1.0, VolumeUnit.LITRE, 1000.0, VolumeUnit.MILLILITRE);
            DemonstrateAddition(1.0, VolumeUnit.LITRE, 1000.0, VolumeUnit.MILLILITRE, VolumeUnit.MILLILITRE);
            // UC11: Cross-category incompatibility — volume vs length and weight
            Console.WriteLine("--- Volume Cross-Category Safety (UC11) ---");
            Console.WriteLine();
            QuantityVolume oneLitre = new QuantityVolume(1.0, VolumeUnit.LITRE);
            bool volumeVsLength = oneLitre.Equals(oneFoot);
            Console.WriteLine($"Comparing {oneLitre} (volume) and {oneFoot} (length)");
            Console.WriteLine($"Result: Equal ({volumeVsLength})");
            Console.WriteLine();
            bool volumeVsWeight = oneLitre.Equals(oneKg);
            Console.WriteLine($"Comparing {oneLitre} (volume) and {oneKg} (weight)");
            Console.WriteLine($"Result: Equal ({volumeVsWeight})");
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("   All Operations Complete");
            Console.WriteLine("========================================");
        }
    }
}
