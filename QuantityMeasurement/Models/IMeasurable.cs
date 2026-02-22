namespace QuantityMeasurement.Models
{
    //IMeasurable interface defines the contract for all measurement unit types.
    //any unit enum/class (LengthUnit, WeightUnit, VolumeUnit, etc.) must implement this interface. 
    //establishes the Liskov Substitution Principle: any IMeasurable implementation can be used interchangeably with Quantity&lt;U&gt;.
    public interface IMeasurable
    {
        //returns the conversion factor relative to the base unit for this measurement category.
        //formula: baseValue = value * GetConversionFactor()
        double GetConversionFactor();
        //converts a value in this unit to the base unit for its category.
        //example: LengthUnit.INCH.ConvertToBaseUnit(12.0) returns 1.0 (feet)
        //example: WeightUnit.GRAM.ConvertToBaseUnit(1000.0) returns 1.0 (kilogram)
        double ConvertToBaseUnit(double value);
        //converts a value from the base unit to this unit.
        //example: LengthUnit.INCH.ConvertFromBaseUnit(1.0) returns 12.0
        //example: WeightUnit.GRAM.ConvertFromBaseUnit(1.0) returns 1000.0
        double ConvertFromBaseUnit(double baseValue);
        //returns a human-readable display label for this unit.
        //used by Quantity&lt;U&gt;.ToString() for readable output.
        //example: "feet", "inch", "kg", "g"
        string GetUnitName();
    }
}
