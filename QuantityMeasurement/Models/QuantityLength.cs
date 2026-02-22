namespace QuantityMeasurement.Models
{
    // QuantityLength class consolidates Feet and Inches into a single generic class
    // Applies the DRY principle by eliminating code duplication
    // Supports cross-unit equality comparison by converting to a common base unit (feet)
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

        // Override Equals for value-based comparison with cross-unit support
        // 1. Reference Check: same object returns true
        // 2. Null Check: null returns false
        // 3. Type Check: different type returns false
        // 4. Value Comparison: converts both values to base unit (feet) before comparing
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
