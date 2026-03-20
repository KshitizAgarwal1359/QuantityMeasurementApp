namespace QuantityMeasurement.Repository
{
    // UC16: DatabaseException — custom exception for database operation failures.
    // Wraps SqlException and other DB-related errors with meaningful error messages.
    // Used by QuantityMeasurementDatabaseRepository to provide clean error propagation
    // to the Service layer without leaking ADO.NET implementation details.
    public class DatabaseException : Exception
    {
        // Constructor with custom message
        public DatabaseException(string message) : base(message){}
        // Constructor with custom message and inner exception (exception chaining)
        public DatabaseException(string message, Exception innerException) : base(message, innerException){}
    }
}
