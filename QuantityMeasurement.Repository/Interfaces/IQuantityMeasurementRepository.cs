using QuantityMeasurement.Models;
namespace QuantityMeasurement.Repository
{
    // UC17: Repository interface — equivalent of Spring Data JPA repository.
    public interface IQuantityMeasurementRepository
    {
        QuantityMeasurementEntity Save(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAllMeasurements();
        int GetCount();
        int GetTotalCount();
        List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType);
        List<QuantityMeasurementEntity> GetMeasurementsByType(string measurementType);
        int GetCountByOperation(string operationType);
        List<QuantityMeasurementEntity> GetErroredMeasurements();
    }
}
