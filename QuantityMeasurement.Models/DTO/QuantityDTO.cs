namespace QuantityMeasurement.Models
{
    // UC15: Data Transfer Object (POCO) for holding quantity measurement input data.
    // Transfers data between external systems (Controller) and the Service layer.
    // Contains value, unit name (string), and measurement type (string) — no dependencies on IMeasurable.
    //
    // The IMeasurableUnit interface and corresponding inner enums provide a self-contained
    // representation of quantity input, making it easier to create QuantityDTO instances
    // and to map these units to the existing IMeasurable units for conversion.
    //
    // Supported measurement types:
    //   LENGTH (feet, inches, yards, centimeters)
    //   WEIGHT (kilogram, gram, pound)
    //   VOLUME (litre, millilitre, gallon)
    //   TEMPERATURE (celsius, fahrenheit)
    public class QuantityDTO
    {
        // IMeasurableUnit interface defined within DTO for self-contained unit representation.
        // Different from IMeasurable — this is a simple marker for DTO-level unit categorization.
        public interface IMeasurableUnit { }

        // Inner enums implementing IMeasurableUnit for type-safe unit specification in DTOs.
        public enum LengthUnit : byte { FEET, INCH, YARDS, CENTIMETERS }
        public enum WeightUnit : byte { KILOGRAM, GRAM, POUND }
        public enum VolumeUnit : byte { LITRE, MILLILITRE, GALLON }
        public enum TemperatureUnit : byte { CELSIUS, FAHRENHEIT }

        // The numerical value of the measurement
        public double Value { get; }
        // The unit name as a string (e.g., "FEET", "KILOGRAM", "CELSIUS")
        public string UnitName { get; }
        // The measurement category (e.g., "LENGTH", "WEIGHT", "VOLUME", "TEMPERATURE")
        public string MeasurementType { get; }

        // Constructor for creating a QuantityDTO with value, unit, and measurement type.
        public QuantityDTO(double value, string unitName, string measurementType)
        {
            this.Value = value;
            this.UnitName = unitName;
            this.MeasurementType = measurementType;
        }

        // Override ToString for readable display
        public override string ToString()
        {
            return $"{Value} {UnitName} ({MeasurementType})";
        }
    }
}
