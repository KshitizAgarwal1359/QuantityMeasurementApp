// -----------------------------------------------------------------------
// <copyright file="Feet.cs" company="QuantityMeasurement">
//     QuantityMeasurementApp - UC1: Feet measurement equality
// </copyright>
// <summary>
//     This class represents a measurement value in feet.
//     It encapsulates the value and provides equality comparison
//     with proper handling of null checks, type safety, and
//     floating-point precision using tolerance-based comparison.
// </summary>
// -----------------------------------------------------------------------

namespace QuantityMeasurement.Models
{
    /// <summary>
    /// Represents a quantity measurement in feet.
    /// Provides value-based equality comparison between Feet objects.
    /// </summary>
    public class Feet
    {
        /// <summary>
        /// The numerical value of the measurement in feet.
        /// Marked as readonly to ensure immutability after construction.
        /// </summary>
        private readonly double measurementValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Feet"/> class
        /// with the specified measurement value.
        /// </summary>
        /// <param name="measurementValue">The numerical value in feet.</param>
        public Feet(double measurementValue)
        {
            this.measurementValue = measurementValue;
        }

        /// <summary>
        /// Gets the measurement value in feet.
        /// </summary>
        public double MeasurementValue => this.measurementValue;

        /// <summary>
        /// Determines whether the specified object is equal to the current Feet instance.
        /// Implements value-based equality with the following checks:
        /// 1. Reference Check: If both references point to the same object, return true
        /// 2. Null Check: If the compared object is null, return false
        /// 3. Type Check: If the compared object is not of type Feet, return false
        /// 4. Value Comparison: Uses Double comparison to compare the double values for equality
        /// </summary>
        /// <param name="obj">The object to compare with the current Feet instance.</param>
        /// <returns>
        /// True if both Feet objects have the same measurement value; false otherwise.
        /// </returns>
        public override bool Equals(object? obj)
        {
            // Reference Check: If both references point to the same object, return true
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // Null Check: If the compared object is null, return false
            if (obj is null)
            {
                return false;
            }

            // Type Check: If the compared object is not of type Feet, return false
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            // Value Comparison: Cast and compare the measurement values
            Feet otherFeet = (Feet)obj;
            return this.measurementValue.CompareTo(otherFeet.measurementValue) == 0;
        }

        /// <summary>
        /// Returns a hash code for the current Feet instance based on the measurement value.
        /// </summary>
        /// <returns>A hash code derived from the measurement value.</returns>
        public override int GetHashCode()
        {
            return this.measurementValue.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the Feet measurement.
        /// </summary>
        /// <returns>A formatted string showing the measurement value in feet.</returns>
        public override string ToString()
        {
            return $"{this.measurementValue} ft";
        }
    }
}
