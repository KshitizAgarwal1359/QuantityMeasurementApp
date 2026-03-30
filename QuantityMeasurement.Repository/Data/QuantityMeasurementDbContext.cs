using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.Models;
namespace QuantityMeasurement.Repository
{
    // UC17: EF Core DbContext for QuantityMeasurements table.
    public class QuantityMeasurementDbContext : DbContext
    {
        // DbSet — equivalent of JpaRepository<QuantityMeasurementEntity, int>
        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }
        public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options)
            : base(options)
        {
        }
    }
}
