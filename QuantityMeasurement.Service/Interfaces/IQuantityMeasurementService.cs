using QuantityMeasurement.Models;
namespace QuantityMeasurement.Service
{
    // UC17: Service interface — mostly same as UC15/16 with new history/count endpoints.
    public interface IQuantityMeasurementService
    {
        // Compare two quantities for equality
        QuantityDTO Compare(QuantityDTO first, QuantityDTO second);
        // Convert from one unit to another
        QuantityDTO Convert(QuantityDTO source, string targetUnitName);
        // Add two quantities
        QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnitName);
        // Subtract second from first
        QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnitName);
        // Divide first by second
        QuantityDTO Divide(QuantityDTO first, QuantityDTO second);
        // UC17: Get operation history by operation type
        List<QuantityMeasurementDTO> GetHistoryByOperation(string operationType);
        // UC17: Get measurements by measurement type
        List<QuantityMeasurementDTO> GetHistoryByType(string measurementType);
        // UC17: Get count of successful operations
        int GetCountByOperation(string operationType);
        // UC17: Get all errored measurements
        List<QuantityMeasurementDTO> GetErrorHistory();
        // UC17: Get history by username
        List<QuantityMeasurementDTO> GetHistoryByUsername(string username);
    }
}
