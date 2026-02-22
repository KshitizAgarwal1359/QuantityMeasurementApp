namespace QuantityMeasurement.Models
{
    //LengthUnit is a sealed class implementing IMeasurable, replacing the previous enum.
    //Each static readonly instance represents a supported length unit with its conversion factor relative to the base unit (feet).
    //UC10 Refactoring: Converted from enum to sealed class so it can implement the IMeasurable
    /// interface. This enables the generic Quantity&lt;U&gt; class to work with length units polymorphically alongside weight, volume, and other measurement categories. 
    //The sealed class pattern (sometimes called "smart enum" or "type-safe enum" in C#)
    //provides the same immutability and thread-safety guarantees as enums while supporting interface implementation. 
    //Conversion factors relative to feet (base unit):
    //FEET: 1.0
    //INCH: 1/12
    //YARDS: 3.0
    //CENTIMETERS: 0.393701/12
    public sealed class LengthUnit : IMeasurable
    {
        // Static readonly instances — immutable, thread-safe, globally accessible
        // Base unit: 1 foot = 1 foot
        public static readonly LengthUnit FEET = new LengthUnit("FEET", "feet", 1.0);
        // 1 inch = 1/12 foot
        public static readonly LengthUnit INCH = new LengthUnit("INCH", "inch", 1.0 / 12.0);
        // 1 yard = 3 feet
        public static readonly LengthUnit YARDS = new LengthUnit("YARDS", "yards", 3.0);
        // 1 cm = 0.393701 inches = 0.393701/12 feet
        public static readonly LengthUnit CENTIMETERS = new LengthUnit("CENTIMETERS", "cm", 0.393701 / 12.0);
        private readonly string name;
        private readonly string unitLabel;
        private readonly double conversionFactor;
        //private constructor prevents external instantiation — only predefined instances exist
        private LengthUnit(string name, string unitLabel, double conversionFactor)
        {
            this.name = name;
            this.unitLabel = unitLabel;
            this.conversionFactor = conversionFactor;
        }
        /// Returns the conversion factor to convert this unit to the base unit (feet).
        public double GetConversionFactor()
        {
            return this.conversionFactor;
        }
        /// Converts a value in this unit to the base unit (feet).
        public double ConvertToBaseUnit(double value)
        {
            return value * this.conversionFactor;
        }
        /// Converts a value from the base unit (feet) to this unit.
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / this.conversionFactor;
        }
        /// Returns the human-readable display label (e.g., "feet", "inch", "yards", "cm").
        /// Used by Quantity&lt;U&gt;.ToString() for output formatting.
        public string GetUnitName()
        {
            return this.unitLabel;
        }
        //Returns the enum-style name (e.g., "FEET", "INCH") for backward compatibility with console output and debugging.
        public override string ToString()
        {
            return this.name;
        }
    }
}
