namespace QuantityMeasurement.Models
{
    public class Feet
    {
        private readonly double measurementValue;
        public Feet(double measurementValue)
        {
            this.measurementValue = measurementValue;
        }
        public double MeasurementValue => this.measurementValue;
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
            Feet otherFeet = (Feet)obj;
            return this.measurementValue.CompareTo(otherFeet.measurementValue) == 0;
        }
        public override int GetHashCode()
        {
            return this.measurementValue.GetHashCode();
        }
        public override string ToString()
        {
            return $"{this.measurementValue} ft";
        }
    }
}
