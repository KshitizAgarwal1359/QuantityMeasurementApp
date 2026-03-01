// UC10: Global type aliases for backward compatibility in the test project
// Ensures all UC1–UC9 test code referencing QuantityLength and QuantityWeight works unchanged.
global using QuantityLength = QuantityMeasurement.Models.Quantity<QuantityMeasurement.Models.LengthUnit>;
global using QuantityWeight = QuantityMeasurement.Models.Quantity<QuantityMeasurement.Models.WeightUnit>;
// UC11: QuantityVolume alias for volume measurement support
global using QuantityVolume = QuantityMeasurement.Models.Quantity<QuantityMeasurement.Models.VolumeUnit>;
