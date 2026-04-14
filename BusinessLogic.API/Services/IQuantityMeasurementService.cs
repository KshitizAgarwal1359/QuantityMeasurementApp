using BusinessLogic.API.Models;
namespace BusinessLogic.API.Services
{
    // UC21: Quantity measurement service interface — async for interservice calls.
    public interface IQuantityMeasurementService
    {
        QuantityDTO Compare(QuantityDTO first, QuantityDTO second);
        QuantityDTO Convert(QuantityDTO source, string targetUnitName);
        QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnitName);
        QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnitName);
        QuantityDTO Divide(QuantityDTO first, QuantityDTO second);

        // Async methods — call DataRepository.API via HttpClient
        Task<List<QuantityMeasurementDTO>> GetHistoryByOperationAsync(string operationType);
        Task<List<QuantityMeasurementDTO>> GetHistoryByUsernameAsync(string username);
        Task<List<QuantityMeasurementDTO>> GetHistoryByTypeAsync(string measurementType);
        Task<int> GetCountByOperationAsync(string operationType);
        Task<List<QuantityMeasurementDTO>> GetErrorHistoryAsync();
    }
}
