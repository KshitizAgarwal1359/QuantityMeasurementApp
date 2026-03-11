using QuantityMeasurement.Models;

namespace QuantityMeasurement.Repository
{
    // UC15: IQuantityMeasurementRepository — data access layer interface.
    // Abstracts persistence details (in-memory cache, database, file, etc.)
    // and provides a clean interface for managing quantity measurement data.
    //
    // Designed according to the Interface Segregation Principle (ISP):
    // different repository implementations can implement this interface,
    // allowing easy substitution without affecting the service layer.
    //
    // Current implementation: QuantityMeasurementCacheRepository (in-memory cache)
    // Future implementations: DatabaseRepository, FileRepository, etc.
    public interface IQuantityMeasurementRepository
    {
        // Saves a QuantityMeasurementEntity to the repository.
        // Returns the saved entity for confirmation/chaining.
        QuantityMeasurementEntity Save(QuantityMeasurementEntity entity);

        // Retrieves all measurement entities from the repository.
        List<QuantityMeasurementEntity> GetAllMeasurements();

        // Retrieves the count of all measurements in the repository.
        int GetCount();
    }
}
