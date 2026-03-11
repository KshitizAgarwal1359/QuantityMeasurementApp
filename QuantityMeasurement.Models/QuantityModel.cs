namespace QuantityMeasurement.Models
{
    // UC15: Generic POCO model class for internal service-layer representation of a quantity.
    // Used within the service layer for performing operations (conversion, comparison, arithmetic).
    // Also used as a DTO for storing quantities in the repository.
    //
    // The generic type parameter U allows for any unit implementing IMeasurable,
    // making it adaptable to LENGTH, WEIGHT, VOLUME, and TEMPERATURE categories.
    // Provides type safety — Quantity<LengthUnit> and Quantity<WeightUnit> are distinct types.
    public class QuantityModel<U> where U : class, IMeasurable
    {
        // The numerical value of the measurement
        public double Value { get; }
        // The unit instance (typed IMeasurable implementation)
        public U Unit { get; }

        // Constructor for creating a QuantityModel with value and unit.
        public QuantityModel(double value, U unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        // Override ToString for readable display
        public override string ToString()
        {
            return $"{Value} {Unit.GetUnitName()}";
        }
    }
}
