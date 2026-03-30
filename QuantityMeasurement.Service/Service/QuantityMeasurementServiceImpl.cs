using QuantityMeasurement.Models;
using QuantityMeasurement.Repository;

namespace QuantityMeasurement.Service
{
    // UC17: Service implementation with history/count methods.
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository repository;

        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
        {
            this.repository = repository;
        }

        // Compare two quantities for equality
        public QuantityDTO Compare(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                ValidateNotNull(first, "First operand");
                ValidateNotNull(second, "Second operand");
                ValidateSameCategory(first, second);

                IMeasurable unit1 = ResolveUnit(first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.UnitName);

                double baseValue1 = unit1.ConvertToBaseUnit(first.Value);
                double baseValue2 = unit2.ConvertToBaseUnit(second.Value);
                bool isEqual = baseValue1.CompareTo(baseValue2) == 0;

                string result = isEqual.ToString();
                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                    "COMPARE", first.ToString(), second.ToString(), "N/A", result, first.MeasurementType);
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

        // Convert a quantity to a target unit
        public QuantityDTO Convert(QuantityDTO source, string targetUnitName)
        {
            try
            {
                ValidateNotNull(source, "Source quantity");

                IMeasurable sourceUnit = ResolveUnit(source.UnitName);
                IMeasurable targetUnit = ResolveUnit(targetUnitName);
                ValidateSameCategoryByUnit(sourceUnit, targetUnit);

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

        // Add two quantities
        public QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnitName)
        {
            return PerformArithmetic(first, second, targetUnitName, "ADD", AddValues);
        }

        private double AddValues(double base1, double base2)
        {
            return base1 + base2;
        }

        // Subtract second from first
        public QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnitName)
        {
            return PerformArithmetic(first, second, targetUnitName, "SUBTRACT", SubtractValues);
        }

        private double SubtractValues(double base1, double base2)
        {
            return base1 - base2;
        }

        // Divide first by second
        public QuantityDTO Divide(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                ValidateNotNull(first, "First operand");
                ValidateNotNull(second, "Second operand");
                ValidateSameCategory(first, second);

                IMeasurable unit1 = ResolveUnit(first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.UnitName);

                ((IMeasurable)unit1).ValidateOperationSupport("DIVIDE");

                double baseValue1 = unit1.ConvertToBaseUnit(first.Value);
                double baseValue2 = unit2.ConvertToBaseUnit(second.Value);

                if (baseValue2 == 0.0)
                {
                    throw new ArithmeticException("Division by zero is not allowed.");
                }

                double ratio = baseValue1 / baseValue2;

                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                    "DIVIDE", first.ToString(), second.ToString(), "N/A", ratio.ToString(), first.MeasurementType);
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

        // UC17: Get history by operation type
        public List<QuantityMeasurementDTO> GetHistoryByOperation(string operationType)
        {
            List<QuantityMeasurementEntity> entities = repository.GetMeasurementsByOperation(operationType);
            return QuantityMeasurementDTO.FromEntityList(entities);
        }

        // UC17: Get history by measurement type
        public List<QuantityMeasurementDTO> GetHistoryByType(string measurementType)
        {
            List<QuantityMeasurementEntity> entities = repository.GetMeasurementsByType(measurementType);
            return QuantityMeasurementDTO.FromEntityList(entities);
        }

        // UC17: Get count of successful operations
        public int GetCountByOperation(string operationType)
        {
            return repository.GetCountByOperation(operationType);
        }

        // UC17: Get all errored measurements
        public List<QuantityMeasurementDTO> GetErrorHistory()
        {
            List<QuantityMeasurementEntity> entities = repository.GetErroredMeasurements();
            return QuantityMeasurementDTO.FromEntityList(entities);
        }

        // Arithmetic helper — shared by Add and Subtract
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

        private static void ValidateNotNull(QuantityDTO dto, string name)
        {
            if (dto is null)
            {
                throw new QuantityMeasurementException($"{name} cannot be null.");
            }
        }

        private static void ValidateSameCategory(QuantityDTO first, QuantityDTO second)
        {
            if (!first.MeasurementType.Equals(second.MeasurementType, StringComparison.OrdinalIgnoreCase))
            {
                throw new QuantityMeasurementException(
                    $"Cannot operate across different measurement categories: " +
                    $"{first.MeasurementType} vs {second.MeasurementType}");
            }
        }

        private static void ValidateSameCategoryByUnit(IMeasurable unit1, IMeasurable unit2)
        {
            if (!unit1.GetMeasurementType().Equals(unit2.GetMeasurementType(), StringComparison.OrdinalIgnoreCase))
            {
                throw new QuantityMeasurementException(
                    $"Cannot operate across different measurement categories: " +
                    $"{unit1.GetMeasurementType()} vs {unit2.GetMeasurementType()}");
            }
        }

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
