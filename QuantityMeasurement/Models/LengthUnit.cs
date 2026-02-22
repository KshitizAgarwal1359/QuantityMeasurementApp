namespace QuantityMeasurement.Models
{
    public enum LengthUnit
    {
        // Base unit: 1 foot = 1 foot (conversion factor = 1.0)
        FEET,
        // 1 inch = 1/12 foot (conversion factor = 1/12)
        INCH,
        // 1 yard = 3 feet (conversion factor = 3.0)
        YARDS,
        // 1 cm = 0.393701 inches = 0.393701/12 feet (conversion factor = 0.393701/12)
        CENTIMETERS
    }
    public static class LengthUnitExtensions
    {
        public static double GetConversionFactor(this LengthUnit lengthUnit)
        {
            return lengthUnit switch
            {
                LengthUnit.FEET => 1.0,
                LengthUnit.INCH => 1.0 / 12.0,
                LengthUnit.YARDS => 3.0,
                LengthUnit.CENTIMETERS => 0.393701 / 12.0,
                _ => throw new ArgumentException($"Unsupported unit: {lengthUnit}")
            };
        }
        public static double ConvertToBaseUnit(this LengthUnit lengthUnit, double value)
        {
            return value * lengthUnit.GetConversionFactor();
        }
        public static double ConvertFromBaseUnit(this LengthUnit lengthUnit, double baseValue)
        {
            return baseValue / lengthUnit.GetConversionFactor();
        }
    }
}
