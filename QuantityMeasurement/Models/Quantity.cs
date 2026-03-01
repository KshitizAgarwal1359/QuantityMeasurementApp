namespace QuantityMeasurement.Models
{
    //generic Quantity&lt;U&gt; class that works with any measurement category through the IMeasurable interface.
    //replaces both QuantityLength and QuantityWeight — logic is implemented ONCE and reused across all categories.
    /// 
    //uc10 Refactoring: Eliminates code duplication by consolidating identical comparison, conversion, and addition logic into a single generic class. Supports:
    //Quantity&lt;LengthUnit&gt; for length measurements (backward compatible as QuantityLength via alias)
    //Quantity&lt;WeightUnit&gt; for weight measurements (backward compatible as QuantityWeight via alias)
    //Quantity&lt;AnyFutureUnit&gt; for any new measurement category implementing IMeasurable 
    //Cross-category type safety is enforced through C# reified generics:
    //Quantity&lt;LengthUnit&gt; and Quantity&lt;WeightUnit&gt; are distinct types at runtime
    //GetType() check in Equals() prevents comparing measurements of different categories
    //No type erasure in C# — full type information is preserved at runtime 
    //instances are immutable (value object semantics) — all operations return new instances.
    public class Quantity<U> where U : class, IMeasurable
    {
        private readonly double measurementValue;
        private readonly U unit;
        //constructor takes a numerical value and the unit type.
        //validates that the value is a finite number (not NaN or Infinity).
        public Quantity(double measurementValue, U unit)
        {
            if (double.IsNaN(measurementValue) || double.IsInfinity(measurementValue))
            {
                throw new ArgumentException("Measurement value must be a finite number. NaN and Infinity are not allowed.");
            }
            this.measurementValue = measurementValue;
            this.unit = unit;
        }
        //gets the measurement value
        public double MeasurementValue => this.measurementValue;
        //gets the unit type
        public U Unit => this.unit;
        //instance method for unit conversion.
        //delegates to IMeasurable.ConvertToBaseUnit() and IMeasurable.ConvertFromBaseUnit().
        //returns a NEW Quantity&lt;U&gt; instance (preserving immutability).
        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseUnitValue = this.unit.ConvertToBaseUnit(this.measurementValue);
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseUnitValue);
            convertedValue = Math.Round(convertedValue, 6);
            return new Quantity<U>(convertedValue, targetUnit);
        }
        //static conversion method — provides a simple API for converting values between units.
        public static double Convert(double value, U sourceUnit, U targetUnit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Value must be a finite number. NaN and Infinity are not allowed.");
            }
            Quantity<U> sourceQuantity = new Quantity<U>(value, sourceUnit);
            Quantity<U> convertedQuantity = sourceQuantity.ConvertTo(targetUnit);
            return convertedQuantity.MeasurementValue;
        }
        //private utility addition method — delegates to IMeasurable for conversions.
        //converts both quantities to base unit, sums them, and converts result to target unit.
        //used by both Add(other) and Add(other, targetUnit) to avoid DRY violation.
        private Quantity<U> AddAndConvertToTargetUnit(Quantity<U> other, U targetUnit)
        {
            if (other is null)
            {
                throw new ArgumentException("Cannot add null measurement. The second operand must be a valid Quantity.");
            }
            double thisBaseValue = this.unit.ConvertToBaseUnit(this.measurementValue);
            double otherBaseValue = other.unit.ConvertToBaseUnit(other.measurementValue);
            double sumInBaseUnit = thisBaseValue + otherBaseValue;
            double resultValue = targetUnit.ConvertFromBaseUnit(sumInBaseUnit);
            resultValue = Math.Round(resultValue, 6);
            return new Quantity<U>(resultValue, targetUnit);
        }
        /// Instance method for addition — result in THIS object's unit (first operand's unit).
        public Quantity<U> Add(Quantity<U> other)
        {
            return AddAndConvertToTargetUnit(other, this.unit);
        }
        /// Instance method for addition with explicit target unit.
        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            return AddAndConvertToTargetUnit(other, targetUnit);
        }
        //static Add method — adds two Quantity objects. Result in first operand's unit.
        public static Quantity<U> Add(Quantity<U> first, Quantity<U> second)
        {
            if (first is null)
            {
                throw new ArgumentException("First operand cannot be null.");
            }
            return first.Add(second);
        }
        //static Add method — adds two Quantity objects with explicit target unit.
        public static Quantity<U> Add(Quantity<U> first, Quantity<U> second, U targetUnit)
        {
            if (first is null)
            {
                throw new ArgumentException("First operand cannot be null.");
            }
            return first.Add(second, targetUnit);
        }
        //static Add method — adds two raw values with their units.
        //result in unit1 (first operand's unit).
        public static Quantity<U> Add(double value1, U unit1, double value2, U unit2)
        {
            Quantity<U> first = new Quantity<U>(value1, unit1);
            Quantity<U> second = new Quantity<U>(value2, unit2);
            return first.Add(second);
        }
        // UC12: Private utility subtraction method — mirrors AddAndConvertToTargetUnit pattern.
        // Converts both quantities to base unit, subtracts them, and converts result to target unit.
        // Used by both Subtract(other) and Subtract(other, targetUnit) to avoid DRY violation.
        private Quantity<U> SubtractAndConvertToTargetUnit(Quantity<U> other, U targetUnit)
        {
            if (other is null)
            {
                throw new ArgumentException("Cannot subtract null measurement. The second operand must be a valid Quantity.");
            }
            double thisBaseValue = this.unit.ConvertToBaseUnit(this.measurementValue);
            double otherBaseValue = other.unit.ConvertToBaseUnit(other.measurementValue);
            double differenceInBaseUnit = thisBaseValue - otherBaseValue;
            double resultValue = targetUnit.ConvertFromBaseUnit(differenceInBaseUnit);
            resultValue = Math.Round(resultValue, 6);
            return new Quantity<U>(resultValue, targetUnit);
        }
        // UC12: Instance method for subtraction — result in THIS object's unit (first operand's unit).
        // Subtraction is non-commutative: A.Subtract(B) ≠ B.Subtract(A)
        public Quantity<U> Subtract(Quantity<U> other)
        {
            return SubtractAndConvertToTargetUnit(other, this.unit);
        }
        // UC12: Instance method for subtraction with explicit target unit.
        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            return SubtractAndConvertToTargetUnit(other, targetUnit);
        }
        // UC12: Division method — returns a dimensionless double (ratio).
        // Converts both quantities to base unit and divides.
        // Division by zero throws ArithmeticException (fail-fast principle).
        // Division is non-commutative: A.Divide(B) ≠ B.Divide(A)
        // Result interpretation: > 1.0 means first is larger, < 1.0 means second is larger, = 1.0 means equal.
        public double Divide(Quantity<U> other)
        {
            if (other is null)
            {
                throw new ArgumentException("Cannot divide by null measurement. The divisor must be a valid Quantity.");
            }
            double thisBaseValue = this.unit.ConvertToBaseUnit(this.measurementValue);
            double otherBaseValue = other.unit.ConvertToBaseUnit(other.measurementValue);
            if (otherBaseValue == 0.0)
            {
                throw new ArithmeticException("Cannot divide by zero. The divisor quantity must have a non-zero value.");
            }
            return thisBaseValue / otherBaseValue;
        }
        //override Equals for value-based comparison with cross-unit support.
        //delegates to IMeasurable.ConvertToBaseUnit() for normalization. 
        // Cross-category type safety: GetType() check ensures that Quantity<LengthUnit> and Quantity<WeightUnit> are never considered equal. C# reified generics make these distinct runtime types (no type erasure).
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
            Quantity<U> otherQuantity = (Quantity<U>)obj;
            double thisBaseValue = this.unit.ConvertToBaseUnit(this.measurementValue);
            double otherBaseValue = otherQuantity.unit.ConvertToBaseUnit(otherQuantity.measurementValue);
            return thisBaseValue.CompareTo(otherBaseValue) == 0;
        }
        //override GetHashCode based on base unit value for consistency with Equals.
        public override int GetHashCode()
        {
            return this.unit.ConvertToBaseUnit(this.measurementValue).GetHashCode();
        }
        //override ToString using IMeasurable.GetUnitName() for display label.
        public override string ToString()
        {
            return $"{this.measurementValue} {this.unit.GetUnitName()}";
        }
    }
}
