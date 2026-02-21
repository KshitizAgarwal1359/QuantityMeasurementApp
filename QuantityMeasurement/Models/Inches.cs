namespace QuantityMeasurement.Models
{
    // Inches class represents a measurement value in inches
    // Provides value-based equality comparison between Inches objects
    public class Inches
    {
        private readonly double measurementValue;
        public Inches(double measurementValue)
        {
            this.measurementValue = measurementValue;
        }
        public double MeasurementValue => this.measurementValue;
        // Override Equals for value-based comparison
        // 1. Reference Check: same object returns true
        // 2. Null Check: null returns false
        // 3. Type Check: different type returns false
        // 4. Value Comparison: uses CompareTo for floating-point precision
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
            Inches otherInches = (Inches)obj;
            return this.measurementValue.CompareTo(otherInches.measurementValue) == 0;
        }
        public override int GetHashCode()
        {
            return this.measurementValue.GetHashCode();
        }
        public override string ToString()
        {
            return $"{this.measurementValue} inch";
        }
    }
}
