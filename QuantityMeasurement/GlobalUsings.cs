// UC10: Global type aliases for backward compatibility
// QuantityLength and QuantityWeight are now type aliases for the generic Quantity<U> class.
// This ensures all existing code (UC1–UC9) works without modification.
global using QuantityLength = QuantityMeasurement.Models.Quantity<QuantityMeasurement.Models.LengthUnit>;
global using QuantityWeight = QuantityMeasurement.Models.Quantity<QuantityMeasurement.Models.WeightUnit>;
