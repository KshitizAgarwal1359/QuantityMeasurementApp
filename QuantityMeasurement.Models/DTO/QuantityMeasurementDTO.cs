namespace QuantityMeasurement.Models
{
    // UC17: Response DTO mapping entity fields to API response.
    public class QuantityMeasurementDTO
    {
        public double ThisValue { get; set; }
        public string? ThisUnit { get; set; }
        public string? ThisMeasurementType { get; set; }
        public double ThatValue { get; set; }
        public string? ThatUnit { get; set; }
        public string? ThatMeasurementType { get; set; }
        public string? Operation { get; set; }
        public string? ResultString { get; set; }
        public double ResultValue { get; set; }
        public string? ResultUnit { get; set; }
        public string? ResultMeasurementType { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsError { get; set; }

        // Factory method — equivalent of fromEntity() in Spring DTO
        public static QuantityMeasurementDTO FromEntity(QuantityMeasurementEntity entity)
        {
            return new QuantityMeasurementDTO
            {
                Operation = entity.OperationType,
                ThisUnit = entity.Operand1,
                ThatUnit = entity.Operand2,
                ThisMeasurementType = entity.MeasurementType,
                ThatMeasurementType = entity.MeasurementType,
                ResultString = entity.Result,
                ResultUnit = entity.TargetUnit,
                ErrorMessage = entity.ErrorMessage,
                IsError = entity.HasError
            };
        }

        // Factory method for list — equivalent of fromEntityList()
        public static List<QuantityMeasurementDTO> FromEntityList(List<QuantityMeasurementEntity> entities)
        {
            List<QuantityMeasurementDTO> dtos = new List<QuantityMeasurementDTO>();
            foreach (QuantityMeasurementEntity entity in entities)
            {
                dtos.Add(FromEntity(entity));
            }
            return dtos;
        }
    }
}
