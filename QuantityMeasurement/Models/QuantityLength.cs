namespace QuantityMeasurement.Models
{
    // QuantityLength class consolidates Feet and Inches into a single generic class
    public class QuantityLength
    {
        private readonly double measurementValue;
        private readonly LengthUnit lengthUnit;
        // Constructor takes a numerical value and the unit type
        public QuantityLength(double measurementValue, LengthUnit lengthUnit)
        {
            this.measurementValue = measurementValue;
            this.lengthUnit = lengthUnit;
        }
        // Gets the measurement value
        public double MeasurementValue => this.measurementValue;
        // Gets the length unit type
        public LengthUnit Unit => this.lengthUnit;
        // Converts the measurement value to the base unit (feet) using the conversion factor
        // This enables cross-unit comparison (e.g., 1 Foot == 12 Inches)
        private double ConvertToBaseUnit()
        {
            return this.measurementValue * this.lengthUnit.GetConversionFactor();
        }
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
        public override int GetHashCode()
        {
            return this.ConvertToBaseUnit().GetHashCode();
        }
        public override string ToString()
        {
            string unitLabel = this.lengthUnit == LengthUnit.FEET ? "feet" : "inch";
            return $"{this.measurementValue} {unitLabel}";
        }
    }
}
