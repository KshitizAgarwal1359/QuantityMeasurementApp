namespace QuantityMeasurement.Models
{
    // QuantityLength class represents a length measurement with a value and unit
    // Supports equality comparison and unit-to-unit conversion
    // Instances are immutable (value object semantics) — convertTo returns a new instance
    public class QuantityLength
    {
        private readonly double measurementValue;
        private readonly LengthUnit lengthUnit;
        // Constructor takes a numerical value and the unit type
        // Validates that the value is a finite number (not NaN or Infinity)
        public QuantityLength(double measurementValue, LengthUnit lengthUnit)
        {
            if (double.IsNaN(measurementValue) || double.IsInfinity(measurementValue))
            {
                throw new ArgumentException("Measurement value must be a finite number. NaN and Infinity are not allowed.");
            }
            this.measurementValue = measurementValue;
            this.lengthUnit = lengthUnit;
        }
        // Gets the measurement value
        public double MeasurementValue => this.measurementValue;
        // Gets the length unit type
        public LengthUnit Unit => this.lengthUnit;
        // Private helper method: converts the measurement value to the base unit (feet)
        // This centralizes the conversion logic for both equality and conversion operations
        private double ConvertToBaseUnit()
        {
            return this.measurementValue * this.lengthUnit.GetConversionFactor();
        }
        // Private utility method: converts a value from base unit (feet) to the target unit
        // Used internally by ConvertTo and the static Convert method
        private static double ConvertFromBaseUnit(double baseUnitValue, LengthUnit targetUnit)
        {
            return baseUnitValue / targetUnit.GetConversionFactor();
        }
        // Instance method for unit conversion
        // Converts this measurement to the specified target unit
        // Returns a NEW QuantityLength instance (preserving immutability / value object semantics)
        // Formula: result = value * (sourceUnit.factor / targetUnit.factor)
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            // Normalize to base unit, then convert to target unit
            double baseUnitValue = this.ConvertToBaseUnit();
            double convertedValue = ConvertFromBaseUnit(baseUnitValue, targetUnit);
            // Round to 6 decimal places for precision handling
            convertedValue = Math.Round(convertedValue, 6);
            return new QuantityLength(convertedValue, targetUnit);
        }
        // Static conversion method — provides a simple API for converting values between units
        // Equivalent to: new QuantityLength(value, sourceUnit).ConvertTo(targetUnit).MeasurementValue
        public static double Convert(double value, LengthUnit sourceUnit, LengthUnit targetUnit)
        {
            // Validate the input value
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Value must be a finite number. NaN and Infinity are not allowed.");
            }
            QuantityLength sourceQuantity = new QuantityLength(value, sourceUnit);
            QuantityLength convertedQuantity = sourceQuantity.ConvertTo(targetUnit);
            return convertedQuantity.MeasurementValue;
        }
        // Override Equals for value-based comparison with cross-unit support
        // Compares by converting both values to base unit before comparison
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            QuantityLength otherQuantity = (QuantityLength)obj;
            // Convert both values to base unit for cross-unit comparison
            double thisBaseValue = this.ConvertToBaseUnit();
            double otherBaseValue = otherQuantity.ConvertToBaseUnit();
            return thisBaseValue.CompareTo(otherBaseValue) == 0;
        }
        // Override GetHashCode based on base unit value for consistency with Equals
        public override int GetHashCode()
        {
            return this.ConvertToBaseUnit().GetHashCode();
        }
        // Override ToString for human-readable representation useful for debugging and logging
        public override string ToString()
        {
            string unitLabel = this.lengthUnit switch
            {
                LengthUnit.FEET => "feet",
                LengthUnit.INCH => "inch",
                LengthUnit.YARDS => "yards",
                LengthUnit.CENTIMETERS => "cm",
                _ => "unknown"
            };
            return $"{this.measurementValue} {unitLabel}";
        }
    }
}
