using DataRepository.API.Models;
namespace DataRepository.API
{
    // UC17: EF Core repository for all CRUD operations.
    public class QuantityMeasurementEfRepository : IQuantityMeasurementRepository
    {
        private readonly QuantityMeasurementDbContext context;
        public QuantityMeasurementEfRepository(QuantityMeasurementDbContext context)
        {
            this.context = context;
        }
        // Save entity — equivalent of JpaRepository.save()
        public QuantityMeasurementEntity Save(QuantityMeasurementEntity entity)
        {
            context.QuantityMeasurements.Add(entity);
            context.SaveChanges();
            return entity;
        }
        // Get all — equivalent of JpaRepository.findAll()
        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            var query = from e in context.QuantityMeasurements
                        orderby e.Timestamp descending
                        select e;
            return query.ToList();
        }
        public int GetCount()
        {
            return context.QuantityMeasurements.Count();
        }
        public int GetTotalCount()
        {
            return context.QuantityMeasurements.Count();
        }
        // Equivalent of findByOperation(String operation)
        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            var query = from e in context.QuantityMeasurements
                        where e.OperationType == operationType
                        orderby e.Timestamp descending
                        select e;
            return query.ToList();
        }
        // Equivalent of findByThisMeasurementType(String measurementType)
        public List<QuantityMeasurementEntity> GetMeasurementsByType(string measurementType)
        {
            var query = from e in context.QuantityMeasurements
                        where e.MeasurementType == measurementType
                        orderby e.Timestamp descending
                        select e;
            return query.ToList();
        }
        
        public List<QuantityMeasurementEntity> GetMeasurementsByUsername(string username)
        {
            var query = from e in context.QuantityMeasurements
                        where e.Username == username
                        orderby e.Timestamp descending
                        select e;
            return query.ToList();
        }
        
        // Equivalent of countByOperationAndIsErrorFalse(String operation)
        public int GetCountByOperation(string operationType)
        {
            var query = from e in context.QuantityMeasurements
                        where e.OperationType == operationType && !e.HasError
                        select e;
            return query.Count();
        }
        // Equivalent of findByIsErrorTrue()
        public List<QuantityMeasurementEntity> GetErroredMeasurements()
        {
            var query = from e in context.QuantityMeasurements
                        where e.HasError
                        orderby e.Timestamp descending
                        select e;
            return query.ToList();
        }
    }
}
