using QuantityMeasurement.Models;
namespace QuantityMeasurement.Services
{
    // UC15: Legacy backward-compatible service class.
    // Preserved from UC1-UC14 to maintain existing test compatibility.
    // New code should use IQuantityMeasurementService / QuantityMeasurementServiceImpl in the Service layer.
    public class QuantityMeasurementService
    {
        public bool CompareFeetMeasurements(Feet? firstMeasurement, Feet? secondMeasurement)
        {
            if (firstMeasurement is null) return false;
            return firstMeasurement.Equals(secondMeasurement);
        }
        public bool CompareInchesMeasurements(Inches? firstMeasurement, Inches? secondMeasurement)
        {
            if (firstMeasurement is null) return false;
            return firstMeasurement.Equals(secondMeasurement);
        }
        public bool CompareQuantityLengthMeasurements(QuantityLength? firstMeasurement, QuantityLength? secondMeasurement)
        {
            if (firstMeasurement is null) return false;
            return firstMeasurement.Equals(secondMeasurement);
        }
        public double ConvertUnits(double value, LengthUnit sourceUnit, LengthUnit targetUnit)
        {
            return QuantityLength.Convert(value, sourceUnit, targetUnit);
        }
        public QuantityLength AddLengthMeasurements(QuantityLength first, QuantityLength second)
        {
            return QuantityLength.Add(first, second);
        }
        public QuantityLength AddLengthMeasurements(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
        {
            return QuantityLength.Add(first, second, targetUnit);
        }
        public bool CompareWeightMeasurements(QuantityWeight? firstMeasurement, QuantityWeight? secondMeasurement)
        {
            if (firstMeasurement is null) return false;
            return firstMeasurement.Equals(secondMeasurement);
        }
        public double ConvertWeightUnits(double value, WeightUnit sourceUnit, WeightUnit targetUnit)
        {
            return QuantityWeight.Convert(value, sourceUnit, targetUnit);
        }
        public QuantityWeight AddWeightMeasurements(QuantityWeight first, QuantityWeight second)
        {
            return QuantityWeight.Add(first, second);
        }
        public QuantityWeight AddWeightMeasurements(QuantityWeight first, QuantityWeight second, WeightUnit targetUnit)
        {
            return QuantityWeight.Add(first, second, targetUnit);
        }
        public bool CompareQuantityMeasurements<U>(Quantity<U>? first, Quantity<U>? second) where U : class, IMeasurable
        {
            if (first is null) return false;
            return first.Equals(second);
        }
        public double ConvertQuantityUnits<U>(double value, U sourceUnit, U targetUnit) where U : class, IMeasurable
        {
            return Quantity<U>.Convert(value, sourceUnit, targetUnit);
        }
        public Quantity<U> AddQuantityMeasurements<U>(Quantity<U> first, Quantity<U> second) where U : class, IMeasurable
        {
            return Quantity<U>.Add(first, second);
        }
        public Quantity<U> AddQuantityMeasurements<U>(Quantity<U> first, Quantity<U> second, U targetUnit) where U : class, IMeasurable
        {
            return Quantity<U>.Add(first, second, targetUnit);
        }
        public Quantity<U> SubtractQuantityMeasurements<U>(Quantity<U> first, Quantity<U> second) where U : class, IMeasurable
        {
            return first.Subtract(second);
        }
        public Quantity<U> SubtractQuantityMeasurements<U>(Quantity<U> first, Quantity<U> second, U targetUnit) where U : class, IMeasurable
        {
            return first.Subtract(second, targetUnit);
        }
        public double DivideQuantityMeasurements<U>(Quantity<U> first, Quantity<U> second) where U : class, IMeasurable
        {
            return first.Divide(second);
        }
    }
}
