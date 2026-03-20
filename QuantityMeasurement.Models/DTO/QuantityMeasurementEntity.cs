using System.Text.Json.Serialization;
namespace QuantityMeasurement.Models
{
    // UC15/UC16: QuantityMeasurementEntity — comprehensive data holder for quantity measurement operations.
    // Stores operands, operation type, result, measurement type, and error information.
    // Stores operands, operation type, result, measurement type, and error information.
    // Data holder for operations
    public class QuantityMeasurementEntity
    {
        public int Id { get; init; }
        public string OperationType { get; init; }
        public string? Operand1 { get; init; }
        public string? Operand2 { get; init; }
        public string? TargetUnit { get; init; }
        public string? Result { get; init; }
        public string MeasurementType { get; init; } = "N/A";
        public bool HasError { get; init; }
        public string? ErrorMessage { get; init; }
        public DateTime Timestamp { get; init; }

        public QuantityMeasurementEntity(string operationType, string operand1,
            string targetUnit, string result)
        {
            this.OperationType = operationType;
            this.Operand1 = operand1;
            this.Operand2 = null;
            this.TargetUnit = targetUnit;
            this.Result = result;
            this.MeasurementType = "N/A";
            this.HasError = false;
            this.ErrorMessage = "None";
            this.Timestamp = DateTime.UtcNow;
        }
        public QuantityMeasurementEntity(string operationType, string operand1,
            string targetUnit, string result, string measurementType)
        {
            this.OperationType = operationType;
            this.Operand1 = operand1;
            this.Operand2 = null;
            this.TargetUnit = targetUnit;
            this.Result = result;
            this.MeasurementType = measurementType;
            this.HasError = false;
            this.ErrorMessage = "None";
            this.Timestamp = DateTime.UtcNow;
        }
        public QuantityMeasurementEntity(string operationType, string operand1,
            string operand2, string? targetUnit, string result, string measurementType = "N/A")
        {
            this.OperationType = operationType;
            this.Operand1 = operand1;
            this.Operand2 = operand2;
            this.TargetUnit = targetUnit;
            this.Result = result;
            this.MeasurementType = measurementType;
            this.HasError = false;
            this.ErrorMessage = "None";
            this.Timestamp = DateTime.UtcNow;
        }
        public QuantityMeasurementEntity(string operationType, string errorMessage,
            string measurementType = "N/A")
        {
            this.OperationType = operationType;
            this.Operand1 = null;
            this.Operand2 = null;
            this.Result = "N/A";
            this.HasError = true;
            this.ErrorMessage = errorMessage;
            this.MeasurementType = measurementType;
            this.Timestamp = DateTime.UtcNow;
        }
        [JsonConstructor]
        public QuantityMeasurementEntity()
        {
            this.OperationType = "UNKNOWN";
            this.Operand1 = "N/A";
            this.Operand2 = "N/A";
            this.Result = "N/A";
            this.ErrorMessage = "None";
            this.MeasurementType = "N/A";
            this.Timestamp = DateTime.UtcNow;
        }
        public override string ToString()
        {
            if (HasError)
            {
                return $"[{OperationType}] ERROR: {ErrorMessage}";
            }
            if (Operand2 is not null && Operand2 != "N/A")
            {
                return $"[{OperationType}] {Operand1} | {Operand2} => {Result}";
            }
            return $"[{OperationType}] {Operand1} => {Result}";
        }
    }
}
