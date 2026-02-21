using QuantityMeasurement.Models;
namespace QuantityMeasurement.Services
{
    public class QuantityMeasurementService
    {
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
    }
}
