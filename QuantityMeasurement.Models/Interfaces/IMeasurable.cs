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
        double ConvertToBaseUnit(double value);
        //converts a value from the base unit to this unit.
        double ConvertFromBaseUnit(double baseValue);
        //returns a human-readable display label for this unit.
        string GetUnitName();

        // UC15: Returns the measurement type/category for this unit.
        // Used to ensure comparisons and conversions are only performed between compatible types
        // and for converting QuantityDTO to IMeasurable units.
        // Example: "LENGTH", "WEIGHT", "VOLUME", "TEMPERATURE"
        string GetMeasurementType();

        // UC14: Default method indicating whether this unit supports arithmetic operations.
        bool SupportsArithmetic() => true;
        // UC14: Default method to validate arithmetic operation support.
        void ValidateOperationSupport(string operation) { }

        // UC15: Static helper to resolve an IMeasurable unit instance from a unit name string.
        // Used by the service layer to convert QuantityDTO (string-based) to typed IMeasurable units.
        static IMeasurable? GetUnitByName(string unitName)
        {
            return unitName.ToUpper() switch
            {
                "FEET" => LengthUnit.FEET,
                "INCH" => LengthUnit.INCH,
                "YARDS" => LengthUnit.YARDS,
                "CENTIMETERS" => LengthUnit.CENTIMETERS,
                "KILOGRAM" => WeightUnit.KILOGRAM,
                "GRAM" => WeightUnit.GRAM,
                "POUND" => WeightUnit.POUND,
                "LITRE" => VolumeUnit.LITRE,
                "MILLILITRE" => VolumeUnit.MILLILITRE,
                "GALLON" => VolumeUnit.GALLON,
                "CELSIUS" => TemperatureUnit.CELSIUS,
                "FAHRENHEIT" => TemperatureUnit.FAHRENHEIT,
                _ => null
            };
        }
    }
}
