using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.Models;
namespace QuantityMeasurement.Repository
{
    // UC18: EF Core DbContext for QuantityMeasurements and Users tables.
    public class QuantityMeasurementDbContext : DbContext
    {
        // DbSet for quantity measurements
        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }

        // UC18: DbSet for user authentication
        public DbSet<UserEntity> Users { get; set; }

        public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options)
            : base(options)
        {
        }
    }
}

