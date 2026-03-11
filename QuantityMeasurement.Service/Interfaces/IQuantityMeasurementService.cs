using QuantityMeasurement.Models;

namespace QuantityMeasurement.Service
{
    // UC15: IQuantityMeasurementService — service layer contract.
    // Defines operations for working with quantities and their measurements.
    // Accepts input as QuantityDTO objects and returns results as QuantityDTO objects.
    //
    // Supported operations:
    //   Compare — Compare two quantities for equality
    //   Convert — Convert a quantity from one unit to another
    //   Add — Add two quantities
    //   Subtract — Subtract one quantity from another
    //   Divide — Divide two quantities (returns dimensionless scalar)
    public interface IQuantityMeasurementService
    {
        // Compares two quantities for equality.
        // Returns QuantityDTO with result "true" or "false".
        QuantityDTO Compare(QuantityDTO first, QuantityDTO second);

        // Converts a quantity from its unit to a target unit.
        // Returns QuantityDTO with the converted value and target unit.
        QuantityDTO Convert(QuantityDTO source, string targetUnitName);

        // Adds two quantities. Result expressed in the target unit.
        // Returns QuantityDTO with the sum value and target unit.
        QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnitName);

        // Subtracts second quantity from first. Result expressed in the target unit.
        // Returns QuantityDTO with the difference value and target unit.
        QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnitName);

        // Divides first quantity by second. Returns dimensionless scalar result.
        // Returns QuantityDTO with the ratio value.
        QuantityDTO Divide(QuantityDTO first, QuantityDTO second);
    }
}
