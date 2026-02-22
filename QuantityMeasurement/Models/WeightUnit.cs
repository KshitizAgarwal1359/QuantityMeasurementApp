namespace QuantityMeasurement.Models
{
    //weightUnit enum is a standalone, top-level class encapsulating all supported weight units.
    //each unit carries its conversion factor relative to the base unit (kilogram).
    //following the UC8 pattern: WeightUnit has full responsibility for unit conversions.
    //this mirrors the LengthUnit design, reinforcing consistency and maintainability.
    //Conversion factors:
    //KILOGRAM: 1.0 (base unit)
    //GRAM: 0.001 (1 g = 0.001 kg)
    //POUND: 0.453592 (1 lb ≈ 0.453592 kg)
    //Enum constants are inherently immutable and thread-safe.
    public enum WeightUnit
    {
        //base unit: 1 kilogram = 1 kilogram (conversion factor = 1.0)
        KILOGRAM,
        //1 gram = 0.001 kilogram (conversion factor = 0.001)
        GRAM,
        //1 pound ≈ 0.453592 kilogram (conversion factor = 0.453592)
        POUND
    }
    //extension methods for WeightUnit providing conversion responsibility.
    //Mirrors the LengthUnitExtensions design from uc8. 
    //Centralizes all weight unit-specific conversion logic:
    //GetConversionFactor(): Returns the conversion factor to the base unit (kilogram)
    //ConvertToBaseUnit(): Converts a value in this unit to the base unit (kilogram)
    //ConvertFromBaseUnit(): Converts a value from the base unit (kilogram) to this unit
    public static class WeightUnitExtensions
    {
        //Returns the conversion factor to convert the given unit to the base unit (kilogram).
        //Formula: baseValue = value * conversionFactor
        public static double GetConversionFactor(this WeightUnit weightUnit)
        {
            return weightUnit switch
            {
                WeightUnit.KILOGRAM => 1.0,
                WeightUnit.GRAM => 0.001,
                WeightUnit.POUND => 0.453592,
                _ => throw new ArgumentException($"Unsupported unit: {weightUnit}")
            };
        }
        //converts a value in this unit to the base unit (kilogram).
        //example: WeightUnit.GRAM.ConvertToBaseUnit(1000.0) returns 1.0
        //example: WeightUnit.POUND.ConvertToBaseUnit(1.0) returns 0.453592
        public static double ConvertToBaseUnit(this WeightUnit weightUnit, double value)
        {
            return value * weightUnit.GetConversionFactor();
        }
        //converts a value from the base unit (kilogram) to this unit.
        //example: WeightUnit.GRAM.ConvertFromBaseUnit(1.0) returns 1000.0
        //example: WeightUnit.POUND.ConvertFromBaseUnit(0.453592) returns 1.0
        public static double ConvertFromBaseUnit(this WeightUnit weightUnit, double baseValue)
        {
            return baseValue / weightUnit.GetConversionFactor();
        }
    }
}
