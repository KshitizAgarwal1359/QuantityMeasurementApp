using QuantityMeasurement.Models;

namespace QuantityMeasurement.Repository
{
    public interface IQuantityMeasurementRepository
    {
        QuantityMeasurementEntity Save(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAllMeasurements();
        int GetCount();
        List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType);
        int GetTotalCount();
    }
}
