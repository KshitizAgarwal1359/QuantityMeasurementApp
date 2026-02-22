namespace QuantityMeasurement.Models
{
    /// <summary>
    /// QuantityWeight represents a weight measurement with a value and unit.
    /// Mirrors the QuantityLength design — supports equality, conversion, and addition.
    /// 
    /// Delegates ALL conversion logic to the standalone WeightUnit enum (SRP).
    /// Instances are immutable (value object semantics) — all operations return new instances.
    /// 
    /// Weight and length measurements are separate, incomparable categories.
    /// The equals() method checks GetType() to reject cross-category comparisons.
    /// </summary>
    public class QuantityWeight
    {
        private readonly double measurementValue;
        private readonly WeightUnit weightUnit;

        /// <summary>
        /// Constructor takes a numerical value and the weight unit type.
        /// Validates that the value is a finite number (not NaN or Infinity).
        /// </summary>
        public QuantityWeight(double measurementValue, WeightUnit weightUnit)
        {
            if (double.IsNaN(measurementValue) || double.IsInfinity(measurementValue))
            {
                throw new ArgumentException("Measurement value must be a finite number. NaN and Infinity are not allowed.");
            }
            this.measurementValue = measurementValue;
            this.weightUnit = weightUnit;
        }

        /// <summary>Gets the measurement value.</summary>
        public double MeasurementValue => this.measurementValue;

        /// <summary>Gets the weight unit type.</summary>
        public WeightUnit Unit => this.weightUnit;

        /// <summary>
        /// Instance method for unit conversion.
        /// Delegates to WeightUnit.ConvertToBaseUnit() and WeightUnit.ConvertFromBaseUnit().
        /// Returns a NEW QuantityWeight instance (preserving immutability).
        /// </summary>
        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            double baseUnitValue = this.weightUnit.ConvertToBaseUnit(this.measurementValue);
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseUnitValue);
            convertedValue = Math.Round(convertedValue, 6);
            return new QuantityWeight(convertedValue, targetUnit);
        }

        /// <summary>
        /// Static conversion method — provides a simple API for converting weight values between units.
        /// </summary>
        public static double Convert(double value, WeightUnit sourceUnit, WeightUnit targetUnit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Value must be a finite number. NaN and Infinity are not allowed.");
            }
            QuantityWeight sourceQuantity = new QuantityWeight(value, sourceUnit);
            QuantityWeight convertedQuantity = sourceQuantity.ConvertTo(targetUnit);
            return convertedQuantity.MeasurementValue;
        }

        /// <summary>
        /// Private utility addition method — converts both weights to base unit (kilogram),
        /// sums them, and converts to target unit.
        /// Used by both Add(other) and Add(other, targetUnit) to avoid DRY violation.
        /// </summary>
        private QuantityWeight AddAndConvertToTargetUnit(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other is null)
            {
                throw new ArgumentException("Cannot add null measurement. The second operand must be a valid QuantityWeight.");
            }
            double thisBaseValue = this.weightUnit.ConvertToBaseUnit(this.measurementValue);
            double otherBaseValue = other.weightUnit.ConvertToBaseUnit(other.measurementValue);
            double sumInBaseUnit = thisBaseValue + otherBaseValue;
            double resultValue = targetUnit.ConvertFromBaseUnit(sumInBaseUnit);
            resultValue = Math.Round(resultValue, 6);
            return new QuantityWeight(resultValue, targetUnit);
        }

        /// <summary>
        /// Instance method for addition — result in THIS object's unit (first operand's unit).
        /// </summary>
        public QuantityWeight Add(QuantityWeight other)
        {
            return AddAndConvertToTargetUnit(other, this.weightUnit);
        }

        /// <summary>
        /// Instance method for addition with explicit target unit.
        /// </summary>
        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            return AddAndConvertToTargetUnit(other, targetUnit);
        }

        /// <summary>
        /// Static Add method — adds two QuantityWeight objects. Result in first operand's unit.
        /// </summary>
        public static QuantityWeight Add(QuantityWeight first, QuantityWeight second)
        {
            if (first is null)
            {
                throw new ArgumentException("First operand cannot be null.");
            }
            return first.Add(second);
        }

        /// <summary>
        /// Static Add method — adds two QuantityWeight objects with explicit target unit.
        /// </summary>
        public static QuantityWeight Add(QuantityWeight first, QuantityWeight second, WeightUnit targetUnit)
        {
            if (first is null)
            {
                throw new ArgumentException("First operand cannot be null.");
            }
            return first.Add(second, targetUnit);
        }

        /// <summary>
        /// Static Add method — adds two raw values with their weight units.
        /// Result in unit1 (first operand's unit).
        /// </summary>
        public static QuantityWeight Add(double value1, WeightUnit unit1, double value2, WeightUnit unit2)
        {
            QuantityWeight first = new QuantityWeight(value1, unit1);
            QuantityWeight second = new QuantityWeight(value2, unit2);
            return first.Add(second);
        }

        /// <summary>
        /// Override Equals for value-based comparison with cross-unit support.
        /// Delegates to WeightUnit.ConvertToBaseUnit() for normalization.
        /// Rejects cross-category comparisons (weight vs. length returns false).
        /// </summary>
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
            QuantityWeight otherQuantity = (QuantityWeight)obj;
            double thisBaseValue = this.weightUnit.ConvertToBaseUnit(this.measurementValue);
            double otherBaseValue = otherQuantity.weightUnit.ConvertToBaseUnit(otherQuantity.measurementValue);
            return thisBaseValue.CompareTo(otherBaseValue) == 0;
        }

        /// <summary>
        /// Override GetHashCode based on base unit value for consistency with Equals.
        /// </summary>
        public override int GetHashCode()
        {
            return this.weightUnit.ConvertToBaseUnit(this.measurementValue).GetHashCode();
        }

        /// <summary>
        /// Override ToString for human-readable representation useful for debugging and logging.
        /// </summary>
        public override string ToString()
        {
            string unitLabel = this.weightUnit switch
            {
                WeightUnit.KILOGRAM => "kg",
                WeightUnit.GRAM => "g",
                WeightUnit.POUND => "lb",
                _ => "unknown"
            };
            return $"{this.measurementValue} {unitLabel}";
        }
    }
}
