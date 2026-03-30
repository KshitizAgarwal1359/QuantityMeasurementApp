using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurement.Models
{
    // UC17: Input DTO encapsulating two operands for REST API requests.
    public class QuantityInputDTO
    {
        [Required]
        public QuantityDTO ThisQuantityDTO { get; set; } = null!;

        public QuantityDTO? ThatQuantityDTO { get; set; }

        // Target unit for conversion/arithmetic result
        public string? TargetUnit { get; set; }
    }
}
