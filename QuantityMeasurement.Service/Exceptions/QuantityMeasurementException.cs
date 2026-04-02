namespace QuantityMeasurement.Service
{
    // UC15: QuantityMeasurementException — custom exception for quantity measurement operations.
    // Extends Exception (C# equivalent of Java's RuntimeException — unchecked exception).
    //
    // Used to encapsulate all quantity measurement-related errors:
    //   - Invalid unit names or measurement types
    //   - Cross-category comparison/arithmetic attempts
    //   - Unsupported operations (e.g., temperature arithmetic)
    //   - Division by zero
    //   - Null operand validation failures
    //
    // Provides constructors for:
    //   - Custom message only
    //   - Custom message with inner exception (exception chaining for detailed error reporting)
    public class QuantityMeasurementException : Exception
    {
        // Constructor with custom message
        public QuantityMeasurementException(string message) : base(message)
        {
        }

        // Constructor with custom message and inner exception (exception chaining)
        public QuantityMeasurementException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
