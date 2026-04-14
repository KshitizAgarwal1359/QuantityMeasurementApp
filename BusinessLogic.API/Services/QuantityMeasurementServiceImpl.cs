using System.Net.Http.Json;
using BusinessLogic.API.Models;

namespace BusinessLogic.API.Services
{
    // UC21: Service implementation with Interservice Communication.
    // Calculation logic stays here; data persistence calls DataRepository.API via HttpClient.
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public QuantityMeasurementServiceImpl(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClientFactory.CreateClient("DataRepositoryAPI");
            this.httpContextAccessor = httpContextAccessor;
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
                SaveEntityAsync(entity).Wait();

                return new QuantityDTO(isEqual ? 1.0 : 0.0, "BOOLEAN", "RESULT");
            }
            catch (QuantityMeasurementException) { throw; }
            catch (Exception ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    "COMPARE", ex.Message, first?.MeasurementType ?? "N/A");
                SaveEntityAsync(errorEntity).Wait();
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
                SaveEntityAsync(entity).Wait();

                return new QuantityDTO(convertedValue, targetUnitName, source.MeasurementType);
            }
            catch (QuantityMeasurementException) { throw; }
            catch (Exception ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    "CONVERT", ex.Message, source?.MeasurementType ?? "N/A");
                SaveEntityAsync(errorEntity).Wait();
                throw new QuantityMeasurementException("Conversion failed: " + ex.Message, ex);
            }
        }

        public QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnitName)
        {
            return PerformArithmetic(first, second, targetUnitName, "ADD", (a, b) => a + b);
        }

        public QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnitName)
        {
            return PerformArithmetic(first, second, targetUnitName, "SUBTRACT", (a, b) => a - b);
        }

        public QuantityDTO Divide(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                ValidateNotNull(first, "First operand");
                ValidateNotNull(second, "Second operand");
                ValidateSameCategory(first, second);

                IMeasurable unit1 = ResolveUnit(first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.UnitName);
                unit1.ValidateOperationSupport("DIVIDE");

                double baseValue1 = unit1.ConvertToBaseUnit(first.Value);
                double baseValue2 = unit2.ConvertToBaseUnit(second.Value);

                if (baseValue2 == 0.0)
                    throw new ArithmeticException("Division by zero is not allowed.");

                double ratio = baseValue1 / baseValue2;

                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                    "DIVIDE", first.ToString(), second.ToString(), "N/A", ratio.ToString(), first.MeasurementType);
                SaveEntityAsync(entity).Wait();

                return new QuantityDTO(ratio, "RATIO", first.MeasurementType);
            }
            catch (QuantityMeasurementException) { throw; }
            catch (Exception ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    "DIVIDE", ex.Message, first?.MeasurementType ?? "N/A");
                SaveEntityAsync(errorEntity).Wait();
                throw new QuantityMeasurementException("Division failed: " + ex.Message, ex);
            }
        }

        // UC21: Interservice Communication — fetch history from DataRepository.API
        public async Task<List<QuantityMeasurementDTO>> GetHistoryByOperationAsync(string operationType)
        {
            var entities = await httpClient.GetFromJsonAsync<List<QuantityMeasurementEntity>>(
                $"api/data/measurements/operation/{operationType}") ?? new List<QuantityMeasurementEntity>();
            return QuantityMeasurementDTO.FromEntityList(entities);
        }

        public async Task<List<QuantityMeasurementDTO>> GetHistoryByUsernameAsync(string username)
        {
            var entities = await httpClient.GetFromJsonAsync<List<QuantityMeasurementEntity>>(
                $"api/data/measurements/username/{username}") ?? new List<QuantityMeasurementEntity>();
            return QuantityMeasurementDTO.FromEntityList(entities);
        }

        public async Task<List<QuantityMeasurementDTO>> GetHistoryByTypeAsync(string measurementType)
        {
            var entities = await httpClient.GetFromJsonAsync<List<QuantityMeasurementEntity>>(
                $"api/data/measurements/type/{measurementType}") ?? new List<QuantityMeasurementEntity>();
            return QuantityMeasurementDTO.FromEntityList(entities);
        }

        public async Task<int> GetCountByOperationAsync(string operationType)
        {
            return await httpClient.GetFromJsonAsync<int>($"api/data/measurements/count/{operationType}");
        }

        public async Task<List<QuantityMeasurementDTO>> GetErrorHistoryAsync()
        {
            var entities = await httpClient.GetFromJsonAsync<List<QuantityMeasurementEntity>>(
                $"api/data/measurements/errored") ?? new List<QuantityMeasurementEntity>();
            return QuantityMeasurementDTO.FromEntityList(entities);
        }

        // UC21: Save entity to DataRepository.API via HTTP POST
        private async Task SaveEntityAsync(QuantityMeasurementEntity entity)
        {
            entity.Username = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Guest";
            await httpClient.PostAsJsonAsync("api/data/measurements", entity);
        }

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
                unit1.ValidateOperationSupport(operationType);
                ValidateSameCategoryByUnit(unit1, targetUnit);

                double baseValue1 = unit1.ConvertToBaseUnit(first.Value);
                double baseValue2 = unit2.ConvertToBaseUnit(second.Value);
                double baseResult = operation(baseValue1, baseValue2);
                double resultValue = Math.Round(targetUnit.ConvertFromBaseUnit(baseResult), 6);

                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(
                    operationType, first.ToString(), second.ToString(),
                    targetUnitName, resultValue.ToString(), first.MeasurementType);
                SaveEntityAsync(entity).Wait();

                return new QuantityDTO(resultValue, targetUnitName, first.MeasurementType);
            }
            catch (QuantityMeasurementException) { throw; }
            catch (Exception ex)
            {
                QuantityMeasurementEntity errorEntity = new QuantityMeasurementEntity(
                    operationType, ex.Message, first?.MeasurementType ?? "N/A");
                SaveEntityAsync(errorEntity).Wait();
                throw new QuantityMeasurementException($"{operationType} failed: " + ex.Message, ex);
            }
        }

        private static void ValidateNotNull(QuantityDTO dto, string name)
        {
            if (dto is null) throw new QuantityMeasurementException($"{name} cannot be null.");
        }

        private static void ValidateSameCategory(QuantityDTO first, QuantityDTO second)
        {
            if (!first.MeasurementType.Equals(second.MeasurementType, StringComparison.OrdinalIgnoreCase))
                throw new QuantityMeasurementException(
                    $"Cannot operate across different measurement categories: {first.MeasurementType} vs {second.MeasurementType}");
        }

        private static void ValidateSameCategoryByUnit(IMeasurable unit1, IMeasurable unit2)
        {
            if (!unit1.GetMeasurementType().Equals(unit2.GetMeasurementType(), StringComparison.OrdinalIgnoreCase))
                throw new QuantityMeasurementException(
                    $"Cannot operate across different measurement categories: {unit1.GetMeasurementType()} vs {unit2.GetMeasurementType()}");
        }

        private static IMeasurable ResolveUnit(string unitName)
        {
            IMeasurable? unit = IMeasurable.GetUnitByName(unitName);
            if (unit is null) throw new QuantityMeasurementException($"Unknown unit: {unitName}");
            return unit;
        }
    }
}
