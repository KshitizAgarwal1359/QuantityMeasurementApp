using System.Text.Json;
using QuantityMeasurement.Models;

namespace QuantityMeasurement.Repository
{
    // Singleton in-memory cache repository.
    public sealed class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        // Singleton instance — thread-safe via static readonly initialization
        private static readonly QuantityMeasurementCacheRepository instance =
            new QuantityMeasurementCacheRepository();

        // File path for disk persistence
        private static readonly string DATA_FILE = "quantity_measurements.json";

        // In-memory cache — ArrayList equivalent in C#
        private readonly List<QuantityMeasurementEntity> cache;

        // Private constructor — prevents external instantiation (Singleton pattern)
        private QuantityMeasurementCacheRepository()
        {
            this.cache = LoadFromDisk();
        }

        // Singleton accessor — global point of access
        public static QuantityMeasurementCacheRepository GetInstance()
        {
            return instance;
        }

        public QuantityMeasurementEntity Save(QuantityMeasurementEntity entity)
        {
            cache.Add(entity);
            SaveToDisk(entity);
            return entity;
        }

        // Returns all measurements from the in-memory cache.
        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return new List<QuantityMeasurementEntity>(cache);
        }

        // Returns the count of cached measurements.
        public int GetCount()
        {
            return cache.Count;
        }

        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            List<QuantityMeasurementEntity> result = new List<QuantityMeasurementEntity>();
            foreach (QuantityMeasurementEntity e in cache)
            {
                if (e.OperationType.Equals(operationType, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(e);
                }
            }
            return result;
        }

        public int GetTotalCount()
        {
            return cache.Count;
        }

        // Persists a single entity to disk by rewriting the entire cache to JSON file.
        // Uses System.Text.Json for serialization (modern C# equivalent of Java ObjectOutputStream).
        private void SaveToDisk(QuantityMeasurementEntity entity)
        {
            try
            {
                string json = JsonSerializer.Serialize(cache, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(DATA_FILE, json);
            }
            catch (Exception)
            {
                // Silently handle disk persistence failures — cache still works in-memory
            }
        }

        // Loads entities from disk during initialization.
        // Returns empty list if file doesn't exist or is corrupted.
        private static List<QuantityMeasurementEntity> LoadFromDisk()
        {
            try
            {
                if (File.Exists(DATA_FILE))
                {
                    string json = File.ReadAllText(DATA_FILE);
                    return JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(json)
                           ?? new List<QuantityMeasurementEntity>();
                }
            }
            catch (Exception)
            {
                // If load fails, start with empty cache
            }
            return new List<QuantityMeasurementEntity>();
        }
    }
}
