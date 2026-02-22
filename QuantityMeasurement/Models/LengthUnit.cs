namespace QuantityMeasurement.Models
{
    // LengthUnit enum encapsulates all possible measurement units
    // Each unit has a conversion factor relative to the base unit (feet)
    // Provides type-safe constants instead of magic strings/numbers
    public enum LengthUnit
    {
        // Base unit: 1 foot = 1 foot
        FEET,
        // 1 inch = 1/12 foot
        INCH
    }

    // Extension methods to get conversion factor for each LengthUnit
    public static class LengthUnitExtensions
    {
        // Returns the conversion factor to convert the given unit to the base unit (feet)
        public static double GetConversionFactor(this LengthUnit lengthUnit)
        {
            return lengthUnit switch
            {
                LengthUnit.FEET => 1.0,
                LengthUnit.INCH => 1.0 / 12.0,
                _ => throw new ArgumentException($"Unsupported unit: {lengthUnit}")
            };
        }
    }
}
