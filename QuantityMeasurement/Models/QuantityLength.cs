namespace QuantityMeasurement.Models
{
    //quantityLength represents a length measurement with a value and unit.
    //supports equality comparison, unit-to-unit conversion, and addition of lengths.
    //uc8 Refactoring: QuantityLength is simplified to delegate ALL conversion logic to the standalone LengthUnit enum. This follows the Delegation Pattern and
    //single Responsibility Principle:
    //lengthUnit handles: conversions (ConvertToBaseUnit, ConvertFromBaseUnit)
    //quantityLength handles: value comparison, arithmetic, and immutability 
    //instances are immutable (value object semantics) — all operations return new instances.
    public class QuantityLength
    {
        private readonly double measurementValue;
        private readonly LengthUnit lengthUnit;
        //constructor takes a numerical value and the unit type.
        //validates that the value is a finite number (not NaN or Infinity).
        public QuantityLength(double measurementValue, LengthUnit lengthUnit)
        {
            if (double.IsNaN(measurementValue) || double.IsInfinity(measurementValue))
            {
                throw new ArgumentException("Measurement value must be a finite number. NaN and Infinity are not allowed.");
            }
            this.measurementValue = measurementValue;
            this.lengthUnit = lengthUnit;
        }
        //gets the measurement value.
        public double MeasurementValue => this.measurementValue;
        //gets the length unit type
        public LengthUnit Unit => this.lengthUnit;
        //instance method for unit conversion.
        //delegates to LengthUnit.ConvertToBaseUnit() and LengthUnit.ConvertFromBaseUnit().
        //returns a NEW QuantityLength instance (preserving immutability / value object semantics).
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            //delegate to LengthUnit for conversion — SRP and Delegation Pattern
            double baseUnitValue = this.lengthUnit.ConvertToBaseUnit(this.measurementValue);
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseUnitValue);
            convertedValue = Math.Round(convertedValue, 6);
            return new QuantityLength(convertedValue, targetUnit);
        }
        //static conversion method — provides a simple API for converting values between units.
        public static double Convert(double value, LengthUnit sourceUnit, LengthUnit targetUnit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Value must be a finite number. NaN and Infinity are not allowed.");
            }
            QuantityLength sourceQuantity = new QuantityLength(value, sourceUnit);
            QuantityLength convertedQuantity = sourceQuantity.ConvertTo(targetUnit);
            return convertedQuantity.MeasurementValue;
        }
        //private utility addition method — delegates to LengthUnit for conversions.
        //converts both lengths to base unit, sums them, and converts result to target unit.
        //used by both Add(other) and Add(other, targetUnit) to avoid DRY violation.
        //maintains immutability by returning a new QuantityLength instance.
        private QuantityLength AddAndConvertToTargetUnit(QuantityLength other, LengthUnit targetUnit)
        {
            if (other is null)
            {
                throw new ArgumentException("Cannot add null measurement. The second operand must be a valid QuantityLength.");
            }
            //delegate to LengthUnit for base unit conversion
            double thisBaseValue = this.lengthUnit.ConvertToBaseUnit(this.measurementValue);
            double otherBaseValue = other.lengthUnit.ConvertToBaseUnit(other.measurementValue);
            double sumInBaseUnit = thisBaseValue + otherBaseValue;
            //delegate to LengthUnit for target unit conversion
            double resultValue = targetUnit.ConvertFromBaseUnit(sumInBaseUnit);
            resultValue = Math.Round(resultValue, 6);
            return new QuantityLength(resultValue, targetUnit);
        }
        //instance method for addition (UC6) — adds another QuantityLength to this instance.
        //returns a NEW QuantityLength with the sum expressed in THIS object's unit.
        public QuantityLength Add(QuantityLength other)
        {
            return AddAndConvertToTargetUnit(other, this.lengthUnit);
        }
        //instance method for addition with explicit target unit(uc7).
        //returns a NEW QuantityLength with the sum expressed in the specified target unit.
        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            return AddAndConvertToTargetUnit(other, targetUnit);
        }
        //static Add method — adds two QuantityLength objects.
        ///result is expressed in the unit of the first operand (UC6).
        public static QuantityLength Add(QuantityLength first, QuantityLength second)
        {
            if (first is null)
            {
                throw new ArgumentException("First operand cannot be null.");
            }
            return first.Add(second);
        }
        //static Add method — adds two QuantityLength objects with explicit target unit (UC7).
        public static QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
        {
            if (first is null)
            {
                throw new ArgumentException("First operand cannot be null.");
            }
            return first.Add(second, targetUnit);
        }
        //static Add method — adds two raw values with their units (UC6).
        //result is expressed in unit1 (first operand's unit).
        public static QuantityLength Add(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
        {
            QuantityLength first = new QuantityLength(value1, unit1);
            QuantityLength second = new QuantityLength(value2, unit2);
            return first.Add(second);
        }
        //override Equals for value-based comparison with cross-unit support.
        //delegates to LengthUnit.ConvertToBaseUnit() for normalization.
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
            //delegate to LengthUnit for base unit conversion
            double thisBaseValue = this.lengthUnit.ConvertToBaseUnit(this.measurementValue);
            double otherBaseValue = otherQuantity.lengthUnit.ConvertToBaseUnit(otherQuantity.measurementValue);
            return thisBaseValue.CompareTo(otherBaseValue) == 0;
        }
        //override GetHashCode based on base unit value for consistency with Equals.
        public override int GetHashCode()
        {
            return this.lengthUnit.ConvertToBaseUnit(this.measurementValue).GetHashCode();
        }
        //override ToString for human-readable representation useful for debugging and logging.
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
