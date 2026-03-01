namespace QuantityMeasurement.Models
{
    //VolumeUnit is a sealed class implementing IMeasurable for volume measurements.
    //Each static readonly instance represents a supported volume unit with its conversion factor relative to the base unit (litre). 
    //UC11: Validates UC10 scalability — only this file is needed to add a new measurement category.
    //No modifications to Quantity&lt;U&gt;, IMeasurable, or QuantityMeasurementApp are required.
    // 
    //mirrors the LengthUnit and WeightUnit design for consistency:
    //- Sealed class with private constructor prevents external instantiation
    //- Static readonly instances are immutable and thread-safe
    //- Implements IMeasurable for polymorphic usage with Quantity&lt;U&gt; 
    //conversion factors relative to litre (base unit):
    //LITRE: 1.0
    //MILLILITRE: 0.001 (1 mL = 0.001 L)
    //GALLON: 3.78541 (1 US gallon ≈ 3.78541 L)
    public sealed class VolumeUnit : IMeasurable
    {
        // Static readonly instances — immutable, thread-safe, globally accessible
        // Base unit: 1 litre = 1 litre
        public static readonly VolumeUnit LITRE = new VolumeUnit("LITRE", "L", 1.0);
        // 1 millilitre = 0.001 litre
        public static readonly VolumeUnit MILLILITRE = new VolumeUnit("MILLILITRE", "mL", 0.001);
        // 1 US gallon ≈ 3.78541 litres
        public static readonly VolumeUnit GALLON = new VolumeUnit("GALLON", "gal", 3.78541);
        private readonly string name;
        private readonly string unitLabel;
        private readonly double conversionFactor;
        //private constructor prevents external instantiation — only predefined instances exist
        private VolumeUnit(string name, string unitLabel, double conversionFactor)
        {
            this.name = name;
            this.unitLabel = unitLabel;
            this.conversionFactor = conversionFactor;
        }
        //returns the conversion factor to convert this unit to the base unit (litre).
        public double GetConversionFactor()
        {
            return this.conversionFactor;
        }
        //converts a value in this unit to the base unit (litre).
        //example: VolumeUnit.MILLILITRE.ConvertToBaseUnit(1000.0) returns 1.0
        //example: VolumeUnit.GALLON.ConvertToBaseUnit(1.0) returns 3.78541
        public double ConvertToBaseUnit(double value)
        {
            return value * this.conversionFactor;
        }
        //converts a value from the base unit (litre) to this unit.
        //example: VolumeUnit.MILLILITRE.ConvertFromBaseUnit(1.0) returns 1000.0
        //example: VolumeUnit.GALLON.ConvertFromBaseUnit(3.78541) returns ~1.0
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / this.conversionFactor;
        }
        //Returns the human-readable display label (e.g., "L", "mL", "gal").
        public string GetUnitName()
        {
            return this.unitLabel;
        }
        //Returns the enum-style name for backward compatibility and console output.
        public override string ToString()
        {
            return this.name;
        }
    }
}
