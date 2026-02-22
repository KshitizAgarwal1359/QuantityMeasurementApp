namespace QuantityMeasurement.Models
{
    //QuantityLength class represents a length measurement with a value and unit
    // Supports equality comparison,unit-to-unit conversion,and addition of lengths
    //Instances are immutable (value obj semantics) — all operations return new instances
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
        //gets the measurement value
        public double MeasurementValue => this.measurementValue;
        //gets the length unit type
        public LengthUnit Unit => this.lengthUnit;
        private double ConvertToBaseUnit()
        {
            return this.measurementValue * this.lengthUnit.GetConversionFactor();
        }
        //private utility method: converts a value from base unit (feet) to the target unit
        //used internally by ConvertTo, Convert, and Add methods
        private static double ConvertFromBaseUnit(double baseUnitValue, LengthUnit targetUnit)
        {
            return baseUnitValue / targetUnit.GetConversionFactor();
        }
        //instance method for unit conversion
        //converts this measurement to the specified target unit
        //returns a NEW QuantityLength instance (preserving immutability / value object semantics)
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double baseUnitValue = this.ConvertToBaseUnit();
            double convertedValue = ConvertFromBaseUnit(baseUnitValue, targetUnit);
            convertedValue = Math.Round(convertedValue, 6);
            return new QuantityLength(convertedValue, targetUnit);
        }
        // Static conversion method — provides a simple API for converting values between units
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
        //instance method for addition — adds another QuantityLength to this instance
        //returns a NEW QuantityLength with the sum expressed in THIS object's unit (first operand's unit)
        //both values are normalized to the base unit, added, then converted back to this unit
        public QuantityLength Add(QuantityLength other)
        {
            if (other is null)
            {
                throw new ArgumentException("Cannot add null measurement. The second operand must be a valid QuantityLength.");
            }
            //convert both to base unit (feet), add, then convert back to this unit
            double thisBaseValue = this.ConvertToBaseUnit();
            double otherBaseValue = other.ConvertToBaseUnit();
            double sumInBaseUnit = thisBaseValue + otherBaseValue;
            // Convert sum back to this object's unit
            double resultValue = ConvertFromBaseUnit(sumInBaseUnit, this.lengthUnit);
            resultValue = Math.Round(resultValue, 6);
            return new QuantityLength(resultValue, this.lengthUnit);
        }
        //static Add method (overloaded) — adds two QuantityLength objects
        //result is expressed in the unit of the first operand
        //acts as a factory method, creating a new QuantityLength instance
        public static QuantityLength Add(QuantityLength first, QuantityLength second)
        {
            if (first is null)
            {
                throw new ArgumentException("First operand cannot be null.");
            }
            return first.Add(second);
        }
        //static Add method (overloaded) — adds two raw values with their units
        //result is expressed in unit1 (first operand's unit)
        //provides flexibility for different use cases
        public static QuantityLength Add(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
        {
            QuantityLength first = new QuantityLength(value1, unit1);
            QuantityLength second = new QuantityLength(value2, unit2);
            return first.Add(second);
        }
        //override Equals for value-based comparison with cross-unit support
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
            double thisBaseValue = this.ConvertToBaseUnit();
            double otherBaseValue = otherQuantity.ConvertToBaseUnit();
            return thisBaseValue.CompareTo(otherBaseValue) == 0;
        }
        //override GetHashCode based on base unit value for consistency with Equals
        public override int GetHashCode()
        {
            return this.ConvertToBaseUnit().GetHashCode();
        }
        //override ToString for human-readable representation useful for debugging and logging
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
