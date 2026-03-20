using Microsoft.Data.SqlClient;
using QuantityMeasurement.Models;

namespace QuantityMeasurement.Repository
{
    // SQL Server ADO.NET implementation.
    // Implements IQuantityMeasurementRepository using parameterised SqlCommand queries.
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly string connectionString;

        public QuantityMeasurementDatabaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
            // Test connectivity on initialization
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to connect to database: " + ex.Message, ex);
            }
        }
        // Saves a QuantityMeasurementEntity to the QuantityMeasurements table.
        // Uses parameterised INSERT query for SQL-injection prevention.
        public QuantityMeasurementEntity Save(QuantityMeasurementEntity entity)
        {
            const string sql = @"
                INSERT INTO QuantityMeasurements
                    (FirstOperand, SecondOperand, OperationType, MeasurementType,
                     FinalResult, HasError, ErrorMessage, RecordedAt)
                VALUES
                    (@FirstOperand, @SecondOperand, @OperationType, @MeasurementType,
                     @FinalResult, @HasError, @ErrorMessage, @RecordedAt)";
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FirstOperand", entity.Operand1 ?? "N/A");
                command.Parameters.AddWithValue("@SecondOperand", entity.Operand2 ?? "N/A");
                command.Parameters.AddWithValue("@OperationType", entity.OperationType);
                command.Parameters.AddWithValue("@MeasurementType", entity.MeasurementType);
                command.Parameters.AddWithValue("@FinalResult", entity.Result ?? "N/A");
                command.Parameters.AddWithValue("@HasError", entity.HasError);
                command.Parameters.AddWithValue("@ErrorMessage", entity.ErrorMessage ?? "None");
                command.Parameters.AddWithValue("@RecordedAt", entity.Timestamp);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to save measurement: " + ex.Message, ex);
            }

            return entity;
        }
        // Retrieves all measurement entities from the database.
        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            List<QuantityMeasurementEntity> results = new List<QuantityMeasurementEntity>();
            const string sql = "SELECT * FROM QuantityMeasurements ORDER BY RecordedAt DESC";
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(MapReaderToEntity(reader));
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to retrieve measurements: " + ex.Message, ex);
            }

            return results;
        }

        // Returns the count of all measurements in the database.
        public int GetCount()
        {
            return GetTotalCount();
        }
        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            List<QuantityMeasurementEntity> results = new List<QuantityMeasurementEntity>();
            const string sql = "SELECT * FROM QuantityMeasurements WHERE OperationType = @OperationType ORDER BY RecordedAt DESC";
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@OperationType", operationType);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(MapReaderToEntity(reader));
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to filter by operation: " + ex.Message, ex);
            }
            return results;
        }

        public int GetTotalCount()
        {
            const string sql = "SELECT COUNT(*) FROM QuantityMeasurements";

            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                return (int)command.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to get count: " + ex.Message, ex);
            }
        }

        private static QuantityMeasurementEntity MapReaderToEntity(SqlDataReader reader)
        {
            return new QuantityMeasurementEntity
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Operand1 = reader.GetString(reader.GetOrdinal("FirstOperand")),
                Operand2 = reader.GetString(reader.GetOrdinal("SecondOperand")),
                OperationType = reader.GetString(reader.GetOrdinal("OperationType")),
                MeasurementType = reader.GetString(reader.GetOrdinal("MeasurementType")),
                Result = reader.GetString(reader.GetOrdinal("FinalResult")),
                HasError = reader.GetBoolean(reader.GetOrdinal("HasError")),
                ErrorMessage = reader.GetString(reader.GetOrdinal("ErrorMessage")),
                Timestamp = reader.GetDateTime(reader.GetOrdinal("RecordedAt"))
            };
        }
    }
}
