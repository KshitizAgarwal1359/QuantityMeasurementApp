using QuantityMeasurement.Models;
namespace QuantityMeasurement.Services
{
    // Service class responsible for performing quantity measurement operations
    // Handles both Feet and Inches equality comparisons
    public class QuantityMeasurementService
    {
        // Compares two Feet measurement objects for equality
        public bool CompareFeetMeasurements(Feet? firstMeasurement, Feet? secondMeasurement)
        {
            // Handle null cases: if firstMeasurement is null, they cannot be equal
            if (firstMeasurement is null)
            {
                return false;
            }
            // Delegate to the Feet class's Equals method for value-based comparison
            return firstMeasurement.Equals(secondMeasurement);
        }
        // Compares two Inches measurement objects for equality
        public bool CompareInchesMeasurements(Inches? firstMeasurement, Inches? secondMeasurement)
        {
            // Handle null cases: if firstMeasurement is null, they cannot be equal
            if (firstMeasurement is null)
            {
                return false;
            }
            // Delegate to the Inches class's Equals method for value-based comparison
            return firstMeasurement.Equals(secondMeasurement);
        }
    }
}
