namespace QuantityMeasurement.Models
{
    //WeightUnit is a sealed class implementing IMeasurable, replacing the previous enum.
    // Each static readonly instance represents a supported weight unit with its conversion factor relative to the base unit (kilogram).
    //uc10 Refactoring: Converted from enum to sealed class (mirroring LengthUnit) so it can implement the IMeasurable interface. Consistency across unit types reinforces the scalable generic design pattern. 
    //Conversion factors relative to kilogram (base unit):
    //KILOGRAM: 1.0
    //GRAM: 0.001 (1 g = 0.001 kg)
    //POUND: 0.453592 (1 lb ≈ 0.453592 kg)
    public sealed class WeightUnit : IMeasurable
    {
        // Static readonly instances — immutable, thread-safe, globally accessible
        // Base unit: 1 kilogram = 1 kilogram
        public static readonly WeightUnit KILOGRAM = new WeightUnit("KILOGRAM", "kg", 1.0);
        // 1 gram = 0.001 kilogram
        public static readonly WeightUnit GRAM = new WeightUnit("GRAM", "g", 0.001);
        // 1 pound ≈ 0.453592 kilogram
        public static readonly WeightUnit POUND = new WeightUnit("POUND", "lb", 0.453592);
        private readonly string name;
        private readonly string unitLabel;
        private readonly double conversionFactor;
        //private constructor prevents external instantiation — only predefined instances exist
        private WeightUnit(string name, string unitLabel, double conversionFactor)
        {
            this.name = name;
            this.unitLabel = unitLabel;
            this.conversionFactor = conversionFactor;
        }
        //returns the conversion factor to convert this unit to the base unit (kilogram).
        public double GetConversionFactor()
        {
            return this.conversionFactor;
        }
        //converts a value in this unit to the base unit (kilogram).
        public double ConvertToBaseUnit(double value)
        {
            return value * this.conversionFactor;
        }
        //converts a value from the base unit (kilogram) to this unit.
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / this.conversionFactor;
        }
        //returns the human-readable display label (e.g., "kg", "g", "lb").
        public string GetUnitName()
        {
            return this.unitLabel;
        }
        //returns the enum-style name for backward compatibility.
        public override string ToString()
        {
            return this.name;
        }
    }
}
