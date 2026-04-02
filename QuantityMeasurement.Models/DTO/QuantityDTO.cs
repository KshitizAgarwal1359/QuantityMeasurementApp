using System.ComponentModel.DataAnnotations;
namespace QuantityMeasurement.Models
{
    // UC17: Data Transfer Object for API input.
    public class QuantityDTO
    {
        // Inner enums for type-safe unit specification
        public interface IMeasurableUnit { }
        public enum LengthUnit : byte { FEET, INCH, YARDS, CENTIMETERS }
        public enum WeightUnit : byte { KILOGRAM, GRAM, POUND }
        public enum VolumeUnit : byte { LITRE, MILLILITRE, GALLON }
        public enum TemperatureUnit : byte { CELSIUS, FAHRENHEIT }

        // The numerical value
        [Required]
        public double Value { get; set; }

        // The unit name string (e.g., "FEET", "KILOGRAM")
        [Required]
        public string UnitName { get; set; } = "";

        // The measurement category (e.g., "LENGTH", "WEIGHT")
        [Required]
        public string MeasurementType { get; set; } = "";

        // Parameterless constructor for JSON deserialization
        public QuantityDTO() { }

        // Constructor for creating a QuantityDTO
        public QuantityDTO(double value, string unitName, string measurementType)
        {
            Value = value;
            UnitName = unitName;
            MeasurementType = measurementType;
        }

        public override string ToString()
        {
            return $"{Value} {UnitName} ({MeasurementType})";
        }
    }
}
