using System.Text.Json.Serialization;

namespace QuantityMeasurement.Models
{
    // UC15: QuantityMeasurementEntity — comprehensive data holder for quantity measurement operations.
    // Stores operands, operation type, result, and error information.
    //
    // Designed to be immutable — initialized through constructors for thread safety and consistency.
    // Fields are not marked readonly due to serialization requirements (System.Text.Json needs
    // property setters or init accessors for deserialization).
    //
    // Implements Serializable pattern via [Serializable] attribute, allowing instances to be
    // serialized/deserialized for storage and retrieval from disk. This is essential for the
    // QuantityMeasurementCacheRepository which saves operation history across application restarts.
    //
    // Multiple constructors accommodate different operation scenarios:
    //   - Single operand: conversion operations
    //   - Double operand: comparison, addition, subtraction operations
    //   - Division: returns dimensionless scalar result
    //   - Error: captures exception details for failed operations
    //
    // This entity tracks operation history for logging, debugging, and auditing purposes.
    [Serializable]
    public class QuantityMeasurementEntity
    {
        // The type of operation performed (e.g., "COMPARE", "CONVERT", "ADD", "SUBTRACT", "DIVIDE")
        [JsonInclude]
        public string OperationType { get; init; }

        // First operand as a string representation (e.g., "100 °C")
        [JsonInclude]
        public string? Operand1 { get; init; }

        // Second operand as a string representation (for binary operations)
        [JsonInclude]
        public string? Operand2 { get; init; }

        // Target unit name (for conversion and arithmetic with explicit target unit)
        [JsonInclude]
        public string? TargetUnit { get; init; }

        // Result of the operation as a string representation
        [JsonInclude]
        public string? Result { get; init; }

        // Indicates whether this entity represents an error
        [JsonInclude]
        public bool HasError { get; init; }

        // Error message (populated when HasError is true)
        [JsonInclude]
        public string? ErrorMessage { get; init; }

        // Timestamp of when the operation was performed
        [JsonInclude]
        public DateTime Timestamp { get; init; }

        // Constructor for single-operand operations (e.g., conversion)
        // Captures the operand, target unit, and result
        public QuantityMeasurementEntity(string operationType, string operand1,
            string targetUnit, string result)
        {
            this.OperationType = operationType;
            this.Operand1 = operand1;
            this.TargetUnit = targetUnit;
            this.Result = result;
            this.HasError = false;
            this.Timestamp = DateTime.UtcNow;
        }

        // Constructor for binary operations (e.g., comparison, addition, subtraction)
        // Captures both operands, optional target unit, and the result
        public QuantityMeasurementEntity(string operationType, string operand1,
            string operand2, string? targetUnit, string result)
        {
            this.OperationType = operationType;
            this.Operand1 = operand1;
            this.Operand2 = operand2;
            this.TargetUnit = targetUnit;
            this.Result = result;
            this.HasError = false;
            this.Timestamp = DateTime.UtcNow;
        }

        // Constructor for error scenarios
        // Captures operation type and error message for failed operations
        public QuantityMeasurementEntity(string operationType, string errorMessage)
        {
            this.OperationType = operationType;
            this.HasError = true;
            this.ErrorMessage = errorMessage;
            this.Timestamp = DateTime.UtcNow;
        }

        // Parameterless constructor for deserialization
        [JsonConstructor]
        public QuantityMeasurementEntity()
        {
            this.OperationType = "UNKNOWN";
            this.Timestamp = DateTime.UtcNow;
        }

        // toString representation for logging and display
        public override string ToString()
        {
            if (HasError)
            {
                return $"[{OperationType}] ERROR: {ErrorMessage}";
            }
            if (Operand2 is not null)
            {
                return $"[{OperationType}] {Operand1} | {Operand2} => {Result}";
            }
            return $"[{OperationType}] {Operand1} => {Result}";
        }
    }
}
