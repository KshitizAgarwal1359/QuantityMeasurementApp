using QuantityMeasurement.Models;
using QuantityMeasurement.Repository;
namespace QuantityMeasurement.Tests
{
    // UC16: QuantityMeasurementDatabaseRepositoryTest — tests for ADO.NET database repository.
    // These tests require:
    //   1. SQL Server running (SQLEXPRESS or LocalDB)
    //   2. QuantityMeasurementTestDB database created via test_schema.sql
    //   3. Valid connection string in the TEST_CONNECTION_STRING constant below
    //
    // IMPORTANT: Tests run against QuantityMeasurementTestDB (separate test database)
    // so that DeleteAll and other destructive operations do NOT affect production data
    // in QuantityMeasurementDB.
    // Each test uses DeleteAll() in setup to ensure clean state.
    public class QuantityMeasurementDatabaseRepositoryTest
    {
        // Points to the TEST database — not the production database
        private const string TEST_CONNECTION_STRING =
            "Server=.\\SQLEXPRESS;Database=QuantityMeasurementTestDB;Trusted_Connection=True;TrustServerCertificate=True;";
        // Helper: creates a fresh repository and clears the table
        private QuantityMeasurementDatabaseRepository CreateCleanRepository()
        {
            QuantityMeasurementDatabaseRepository repo =
                new QuantityMeasurementDatabaseRepository(TEST_CONNECTION_STRING);
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(TEST_CONNECTION_STRING))
            {
                connection.Open();
                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("DELETE FROM QuantityMeasurements", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            return repo;
        }
        // Test 1: Save a measurement and verify it persists
        [Fact]
        public void TestSaveMeasurement_PersistsToDatabase()
        {
            QuantityMeasurementDatabaseRepository repo = CreateCleanRepository();

            QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                "COMPARE", "1 FEET", "12 INCH", null, "True", "LENGTH");
            repo.Save(entity);

            Assert.Equal(1, repo.GetTotalCount());
        }
        // Test 2: Save multiple and retrieve all
        [Fact]
        public void TestGetAllMeasurements_ReturnsAllSaved()
        {
            QuantityMeasurementDatabaseRepository repo = CreateCleanRepository();
            repo.Save(new QuantityMeasurementEntity(
                "COMPARE", "1 FEET", "12 INCH", null, "True", "LENGTH"));
            repo.Save(new QuantityMeasurementEntity(
                "CONVERT", "100 CELSIUS", "FAHRENHEIT", "212", "TEMPERATURE"));
            repo.Save(new QuantityMeasurementEntity(
                "ADD", "1 KILOGRAM", "1000 GRAM", "KILOGRAM", "2", "WEIGHT"));

            List<QuantityMeasurementEntity> all = repo.GetAllMeasurements();
            Assert.Equal(3, all.Count);
        }
        // Test 3: Filter by operation type
        [Fact]
        public void TestGetMeasurementsByOperation_FiltersCorrectly()
        {
            QuantityMeasurementDatabaseRepository repo = CreateCleanRepository();
            repo.Save(new QuantityMeasurementEntity(
                "COMPARE", "1 FEET", "12 INCH", null, "True", "LENGTH"));
            repo.Save(new QuantityMeasurementEntity(
                "CONVERT", "100 CELSIUS", "FAHRENHEIT", "212", "TEMPERATURE"));
            repo.Save(new QuantityMeasurementEntity(
                "COMPARE", "1 KILOGRAM", "1000 GRAM", null, "True", "WEIGHT"));
            List<QuantityMeasurementEntity> compares =
                repo.GetMeasurementsByOperation("COMPARE");
            Assert.Equal(2, compares.Count);
            List<QuantityMeasurementEntity> converts =
                repo.GetMeasurementsByOperation("CONVERT");
            Assert.Single(converts);
        }

        // Test 7: SQL injection prevention — malicious input is treated as literal string
        [Fact]
        public void TestSqlInjectionPrevention_TreatedAsLiteral()
        {
            QuantityMeasurementDatabaseRepository repo = CreateCleanRepository();
            // Malicious input attempts
            string maliciousOperand = "'; DROP TABLE QuantityMeasurements; --";
            string maliciousType = "LENGTH'; DELETE FROM QuantityMeasurements; --";
            QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                "COMPARE", maliciousOperand, maliciousOperand, null,
                "True", maliciousType);
            repo.Save(entity);
            // Table should still exist with 1 record (injection was treated as literal)
            Assert.Equal(1, repo.GetTotalCount());
            // Retrieve and verify the malicious string was stored as-is (not executed)
            List<QuantityMeasurementEntity> all = repo.GetAllMeasurements();
            Assert.Equal(maliciousOperand, all[0].Operand1);
            Assert.Equal(maliciousType, all[0].MeasurementType);
        }
    }
}
