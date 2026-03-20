using QuantityMeasurement.Models;
using QuantityMeasurement.Repository;

namespace QuantityMeasurement.Service
{
    // UC15: QuantityMeasurementServiceImpl — core business logic implementation.
    // Implements IQuantityMeasurementService and encapsulates all quantity measurement operations.
    //
    // This class is similar to the Quantity<U> class from the perspective of services offered
    // (comparison, conversion, arithmetic), but differs in that it does NOT encapsulate
    // value/unit attributes. Instead, it operates on QuantityDTO objects,
    // separating data representation from business logic (SRP).
    //
    // Follows SOLID principles:
    //   SRP: Solely responsible for quantity measurement operations
    //   OCP: Open for extension (new measurement types) without modification
    //   DIP: Depends on IQuantityMeasurementRepository abstraction, not concrete implementation
    //
    // Interacts with IQuantityMeasurementRepository to save operation results for audit/history.
    // Dependency injection via constructor provides the repository instance.
    //
    // Broad operation flow:
    //   1. Accept QuantityDTO input
    //   2. Resolve IMeasurable units from string names
    //   3. Validate cross-category constraints
    //   4. Perform business logic using Quantity<U> instances
    //   5. Handle exceptions and convert to QuantityMeasurementException
    //   6. Create QuantityMeasurementEntity and save to repository
    //   7. Return standardized QuantityDTO result
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        // Repository dependency — injected via constructor (DIP)
        private readonly IQuantityMeasurementRepository repository;

        // Constructor injection
        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
        {
            this.repository = repository;
        }

        // Compares two quantities for equality.
        // Validates that both quantities belong to the same measurement category.
        // Returns QuantityDTO with result "true" or "false".
        public QuantityDTO Compare(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                ValidateNotNull(first, "First operand");
                ValidateNotNull(second, "Second operand");
                ValidateSameCategory(first, second);

                IMeasurable unit1 = ResolveUnit(first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.UnitName);

                // Convert both to base unit for comparison
                double baseValue1 = unit1.ConvertToBaseUnit(first.Value);
                double baseValue2 = unit2.ConvertToBaseUnit(second.Value);
                bool isEqual = baseValue1.CompareTo(baseValue2) == 0;

                string result = isEqual.ToString();
                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                    "COMPARE", first.ToString(), second.ToString(), null, result, first.MeasurementType);
                repository.Save(entity);

                return new QuantityDTO(isEqual ? 1.0 : 0.0, "BOOLEAN", "RESULT");
            }
            catch (QuantityMeasurementException)
            {
                throw;
            }
            catch (Exception ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    "COMPARE", ex.Message, first?.MeasurementType ?? "N/A");
                repository.Save(errorEntity);
                throw new QuantityMeasurementException("Comparison failed: " + ex.Message, ex);
            }
        }

        // Converts a quantity from its current unit to a target unit.
        // Validates that source and target units are in the same measurement category.
        public QuantityDTO Convert(QuantityDTO source, string targetUnitName)
        {
            try
            {
                ValidateNotNull(source, "Source quantity");

                IMeasurable sourceUnit = ResolveUnit(source.UnitName);
                IMeasurable targetUnit = ResolveUnit(targetUnitName);

                ValidateSameCategoryByUnit(sourceUnit, targetUnit);

                // Convert: source -> base -> target
                double baseValue = sourceUnit.ConvertToBaseUnit(source.Value);
                double convertedValue = Math.Round(targetUnit.ConvertFromBaseUnit(baseValue), 6);

                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                    "CONVERT", source.ToString(), targetUnitName, convertedValue.ToString(), source.MeasurementType);
                repository.Save(entity);

                return new QuantityDTO(convertedValue, targetUnitName, source.MeasurementType);
            }
            catch (QuantityMeasurementException)
            {
                throw;
            }
            catch (Exception ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    "CONVERT", ex.Message, source?.MeasurementType ?? "N/A");
                repository.Save(errorEntity);
                throw new QuantityMeasurementException("Conversion failed: " + ex.Message, ex);
            }
        }

        // Adds two quantities. Result expressed in the specified target unit.
        // Validates same category and arithmetic support.
        public QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnitName)
        {
            return PerformArithmetic(first, second, targetUnitName, "ADD",
                (base1, base2) => base1 + base2);
        }

        // Subtracts second quantity from first. Result expressed in the specified target unit.
        // Validates same category and arithmetic support.
        public QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnitName)
        {
            return PerformArithmetic(first, second, targetUnitName, "SUBTRACT",
                (base1, base2) => base1 - base2);
        }

        // Divides first quantity by second. Returns dimensionless scalar result.
        // Division by zero is handled with clear error message.
        public QuantityDTO Divide(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                ValidateNotNull(first, "First operand");
                ValidateNotNull(second, "Second operand");
                ValidateSameCategory(first, second);

                IMeasurable unit1 = ResolveUnit(first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.UnitName);

                // Validate arithmetic support (temperature throws here)
                ((IMeasurable)unit1).ValidateOperationSupport("DIVIDE");

                double baseValue1 = unit1.ConvertToBaseUnit(first.Value);
                double baseValue2 = unit2.ConvertToBaseUnit(second.Value);

                if (baseValue2 == 0.0)
                {
                    throw new ArithmeticException("Division by zero is not allowed.");
                }

                double ratio = baseValue1 / baseValue2;

                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                    "DIVIDE", first.ToString(), second.ToString(), null, ratio.ToString(), first.MeasurementType);
                repository.Save(entity);

                return new QuantityDTO(ratio, "RATIO", "RESULT");
            }
            catch (QuantityMeasurementException)
            {
                throw;
            }
            catch (InvalidOperationException ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    "DIVIDE", ex.Message, first?.MeasurementType ?? "N/A");
                repository.Save(errorEntity);
                throw new QuantityMeasurementException("Division failed: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    "DIVIDE", ex.Message, first?.MeasurementType ?? "N/A");
                repository.Save(errorEntity);
                throw new QuantityMeasurementException("Division failed: " + ex.Message, ex);
            }
        }

        // UC13-pattern centralized arithmetic helper — eliminates duplication between Add and Subtract.
        // Validates operands, resolves units, checks arithmetic support, performs operation,
        // converts to target unit, saves entity, and returns result.
        private QuantityDTO PerformArithmetic(QuantityDTO first, QuantityDTO second,
            string targetUnitName, string operationType, Func<double, double, double> operation)
        {
            try
            {
                ValidateNotNull(first, "First operand");
                ValidateNotNull(second, "Second operand");
                ValidateSameCategory(first, second);

                IMeasurable unit1 = ResolveUnit(first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.UnitName);
                IMeasurable targetUnit = ResolveUnit(targetUnitName);

                // Validate arithmetic support (temperature throws here via IMeasurable default/override)
                ((IMeasurable)unit1).ValidateOperationSupport(operationType);

                ValidateSameCategoryByUnit(unit1, targetUnit);

                double baseValue1 = unit1.ConvertToBaseUnit(first.Value);
                double baseValue2 = unit2.ConvertToBaseUnit(second.Value);
                double baseResult = operation(baseValue1, baseValue2);
                double resultValue = Math.Round(targetUnit.ConvertFromBaseUnit(baseResult), 6);

                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                    operationType, first.ToString(), second.ToString(),
                    targetUnitName, resultValue.ToString(), first.MeasurementType);
                repository.Save(entity);

                return new QuantityDTO(resultValue, targetUnitName, first.MeasurementType);
            }
            catch (QuantityMeasurementException)
            {
                throw;
            }
            catch (InvalidOperationException ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    operationType, ex.Message, first?.MeasurementType ?? "N/A");
                repository.Save(errorEntity);
                throw new QuantityMeasurementException(
                    $"{operationType} failed: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    operationType, ex.Message, first?.MeasurementType ?? "N/A");
                repository.Save(errorEntity);
                throw new QuantityMeasurementException(
                    $"{operationType} failed: " + ex.Message, ex);
            }
        }

        // ---- Validation Helpers ----

        // Validates that a QuantityDTO is not null.
        private static void ValidateNotNull(QuantityDTO dto, string name)
        {
            if (dto is null)
            {
                throw new QuantityMeasurementException($"{name} cannot be null.");
            }
        }

        // Validates that two QuantityDTOs belong to the same measurement category.
        private static void ValidateSameCategory(QuantityDTO first, QuantityDTO second)
        {
            if (!first.MeasurementType.Equals(second.MeasurementType, StringComparison.OrdinalIgnoreCase))
            {
                throw new QuantityMeasurementException(
                    $"Cannot operate across different measurement categories: " +
                    $"{first.MeasurementType} vs {second.MeasurementType}");
            }
        }

        // Validates that two IMeasurable units belong to the same measurement category.
        private static void ValidateSameCategoryByUnit(IMeasurable unit1, IMeasurable unit2)
        {
            if (!unit1.GetMeasurementType().Equals(unit2.GetMeasurementType(), StringComparison.OrdinalIgnoreCase))
            {
                throw new QuantityMeasurementException(
                    $"Cannot operate across different measurement categories: " +
                    $"{unit1.GetMeasurementType()} vs {unit2.GetMeasurementType()}");
            }
        }

        // Resolves an IMeasurable unit instance from a string unit name.
        // Uses the static helper in IMeasurable interface.
        private static IMeasurable ResolveUnit(string unitName)
        {
            IMeasurable? unit = IMeasurable.GetUnitByName(unitName);
            if (unit is null)
            {
                throw new QuantityMeasurementException($"Unknown unit: {unitName}");
            }
            return unit;
        }
    }
}
