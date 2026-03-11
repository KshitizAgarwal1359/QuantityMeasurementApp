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
        // UC13: Centralized validation helper — single source of truth for all arithmetic operand validation.
        // Eliminates duplicated null checks, category checks, and finiteness validation across Add/Subtract/Divide.
        // All arithmetic operations call this ONCE before executing, ensuring consistent error handling.
        // Parameters:
        //   other — the second operand (must not be null)
        //   targetUnit — the target unit for the result (validated only when targetUnitRequired is true)
        //   targetUnitRequired — true for Add/Subtract with explicit target unit, false for Divide and implicit cases
        private void ValidateArithmeticOperands(Quantity<U> other, U? targetUnit, bool targetUnitRequired)
        {
            if (other is null)
            {
                throw new ArgumentException("Cannot perform arithmetic with null measurement. The operand must be a valid Quantity.");
            }
            if (targetUnitRequired && targetUnit is null)
            {
                throw new ArgumentException("Target unit cannot be null when explicitly specified.");
            }
        }
        // UC13: Core arithmetic helper — converts both operands to base unit and applies the operation.
        // This is the SINGLE location for base-unit conversion + arithmetic computation.
        // Uses ArithmeticOperation enum dispatch (lambda-based) to execute ADD, SUBTRACT, or DIVIDE.
        // Returns the raw double result in base-unit terms.
        // For Add/Subtract, callers convert the result to the target unit.
        // For Divide, callers return the raw dimensionless ratio directly.
        // UC14: Validates operation support BEFORE performing arithmetic — TemperatureUnit throws
        // InvalidOperationException here, preventing meaningless temperature arithmetic.
        private double PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            // UC14: Check if this unit supports arithmetic operations before proceeding
            // For LengthUnit/WeightUnit/VolumeUnit, ValidateOperationSupport does nothing (default)
            // For TemperatureUnit, it throws InvalidOperationException with clear message
            // Cast to IMeasurable required for C# default interface method dispatch
            ((IMeasurable)this.unit).ValidateOperationSupport(operation.ToString());
            double thisBaseValue = this.unit.ConvertToBaseUnit(this.measurementValue);
            double otherBaseValue = other.unit.ConvertToBaseUnit(other.measurementValue);
            return operation.Compute(thisBaseValue, otherBaseValue);
        }
        // UC13 Refactored: Instance method for addition — result in THIS object's unit (first operand's unit).
        // Delegates to ValidateArithmeticOperands + PerformBaseArithmetic with ArithmeticOperation.ADD.
        public Quantity<U> Add(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, null, false);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            double resultValue = Math.Round(this.unit.ConvertFromBaseUnit(baseResult), 6);
            return new Quantity<U>(resultValue, this.unit);
        }
        // UC13 Refactored: Instance method for addition with explicit target unit.
        // Delegates to ValidateArithmeticOperands + PerformBaseArithmetic with ArithmeticOperation.ADD.
        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            double resultValue = Math.Round(targetUnit.ConvertFromBaseUnit(baseResult), 6);
            return new Quantity<U>(resultValue, targetUnit);
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
        // UC13 Refactored: Instance method for subtraction — result in THIS object's unit (first operand's unit).
        // Subtraction is non-commutative: A.Subtract(B) ≠ B.Subtract(A)
        // Delegates to ValidateArithmeticOperands + PerformBaseArithmetic with ArithmeticOperation.SUBTRACT.
        public Quantity<U> Subtract(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, null, false);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            double resultValue = Math.Round(this.unit.ConvertFromBaseUnit(baseResult), 6);
            return new Quantity<U>(resultValue, this.unit);
        }
        // UC13 Refactored: Instance method for subtraction with explicit target unit.
        // Delegates to ValidateArithmeticOperands + PerformBaseArithmetic with ArithmeticOperation.SUBTRACT.
        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            double resultValue = Math.Round(targetUnit.ConvertFromBaseUnit(baseResult), 6);
            return new Quantity<U>(resultValue, targetUnit);
        }
        // UC13 Refactored: Division method — returns a dimensionless double (ratio).
        // Division by zero is handled by ArithmeticOperation.DIVIDE.Compute (fail-fast via lambda).
        // Division is non-commutative: A.Divide(B) ≠ B.Divide(A)
        // Result interpretation: > 1.0 means first is larger, < 1.0 means second is larger, = 1.0 means equal.
        // No rounding applied — raw double ratio returned for full precision.
        public double Divide(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, null, false);
            return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
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
