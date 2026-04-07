using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuantityMeasurement.Models
{
    // UC17: Entity mapped to QuantityMeasurements table via EF Core.
    [Table("QuantityMeasurements")]
    [Microsoft.EntityFrameworkCore.Index(nameof(OperationType))]
    [Microsoft.EntityFrameworkCore.Index(nameof(MeasurementType))]
    public class QuantityMeasurementEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("OperationType")]
        [Required]
        [MaxLength(50)]
        public string OperationType { get; set; } = "UNKNOWN";

        [Column("FirstOperand")]
        [MaxLength(200)]
        public string Operand1 { get; set; } = "N/A";

        [Column("SecondOperand")]
        [MaxLength(200)]
        public string Operand2 { get; set; } = "N/A";

        [Column("TargetUnit")]
        [MaxLength(50)]
        public string TargetUnit { get; set; } = "N/A";

        [Column("FinalResult")]
        [MaxLength(200)]
        public string Result { get; set; } = "N/A";

        [Column("MeasurementType")]
        [MaxLength(50)]
        public string MeasurementType { get; set; } = "N/A";

        [Column("HasError")]
        public bool HasError { get; set; }

        [Column("ErrorMessage")]
        [MaxLength(500)]
        public string ErrorMessage { get; set; } = "None";

        [Column("Username")]
        [MaxLength(50)]
        public string Username { get; set; } = "Guest";

        [Column("RecordedAt")]
        public DateTime Timestamp { get; set; }

        // Single-operand constructor (conversion)
        public QuantityMeasurementEntity(string operationType, string operand1,
            string targetUnit, string result)
        {
            OperationType = operationType;
            Operand1 = operand1;
            Operand2 = "N/A";
            TargetUnit = targetUnit;
            Result = result;
            MeasurementType = "N/A";
            HasError = false;
            ErrorMessage = "None";
            Timestamp = DateTime.UtcNow;
        }

        // Single-operand with measurement type
        public QuantityMeasurementEntity(string operationType, string operand1,
            string targetUnit, string result, string measurementType)
        {
            OperationType = operationType;
            Operand1 = operand1;
            Operand2 = "N/A";
            TargetUnit = targetUnit;
            Result = result;
            MeasurementType = measurementType;
            HasError = false;
            ErrorMessage = "None";
            Timestamp = DateTime.UtcNow;
        }

        // Binary-operand constructor (arithmetic)
        public QuantityMeasurementEntity(string operationType, string operand1,
            string operand2, string targetUnit, string result, string measurementType = "N/A")
        {
            OperationType = operationType;
            Operand1 = operand1;
            Operand2 = operand2;
            TargetUnit = targetUnit ?? "N/A";
            Result = result;
            MeasurementType = measurementType;
            HasError = false;
            ErrorMessage = "None";
            Timestamp = DateTime.UtcNow;
        }

        // Error constructor
        public QuantityMeasurementEntity(string operationType, string errorMessage,
            string measurementType = "N/A")
        {
            OperationType = operationType;
            Operand1 = "N/A";
            Operand2 = "N/A";
            Result = "N/A";
            HasError = true;
            ErrorMessage = errorMessage;
            MeasurementType = measurementType;
            Timestamp = DateTime.UtcNow;
        }

        // Parameterless constructor required by EF Core
        [JsonConstructor]
        public QuantityMeasurementEntity()
        {
            OperationType = "UNKNOWN";
            Operand1 = "N/A";
            Operand2 = "N/A";
            Result = "N/A";
            ErrorMessage = "None";
            MeasurementType = "N/A";
            Timestamp = DateTime.UtcNow;
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
