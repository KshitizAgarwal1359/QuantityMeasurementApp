using QuantityMeasurement.Models;
namespace QuantityMeasurement.Services
{
    // Service class responsible for performing quantity measurement operations
    // Handles equality comparisons (UC1-UC4) and unit conversions (UC5)
    public class QuantityMeasurementService
    {
        // Compares two Feet measurement objects for equality (UC1 backward compatibility)
        public bool CompareFeetMeasurements(Feet? firstMeasurement, Feet? secondMeasurement)
        {
            if (firstMeasurement is null)
            {
                return false;
            }
            return firstMeasurement.Equals(secondMeasurement);
        }
        // Compares two Inches measurement objects for equality (UC2 backward compatibility)
        public bool CompareInchesMeasurements(Inches? firstMeasurement, Inches? secondMeasurement)
        {
            if (firstMeasurement is null)
            {
                return false;
            }
            return firstMeasurement.Equals(secondMeasurement);
        }
        // Compares two QuantityLength measurement objects for equality (UC3/UC4)
        // Supports cross-unit comparison by converting to a common base unit
        public bool CompareQuantityLengthMeasurements(QuantityLength? firstMeasurement, QuantityLength? secondMeasurement)
        {
            if (firstMeasurement is null)
            {
                return false;
            }
            return firstMeasurement.Equals(secondMeasurement);
        }
        // Converts a value from one unit to another (UC5)
        // Delegates to the static QuantityLength.Convert method
        public double ConvertUnits(double value, LengthUnit sourceUnit, LengthUnit targetUnit)
        {
            return QuantityLength.Convert(value, sourceUnit, targetUnit);
        }
    }
}
