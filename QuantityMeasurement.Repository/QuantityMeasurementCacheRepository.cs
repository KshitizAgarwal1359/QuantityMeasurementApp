using System.Text.Json;
using QuantityMeasurement.Models;

namespace QuantityMeasurement.Repository
{
    // UC15: QuantityMeasurementCacheRepository — Singleton in-memory cache repository.
    // Implements IQuantityMeasurementRepository for storing QuantityMeasurementEntity objects.
    //
    // Singleton Design Pattern ensures:
    //   - Single instance throughout the application lifecycle
    //   - Centralized data storage — all components access the same data
    //   - Resource efficiency — no duplicate instances
    //   - Thread-safe via static readonly initialization (lazy by CLR)
    //
    // Persistence: Serializes entities to a JSON file on disk using System.Text.Json.
    //   - SaveToDisk: appends new entities to the file
    //   - LoadFromDisk: reads existing entities when the repository is initialized
    //   This ensures data is not lost across application restarts.
    //
    // The List<QuantityMeasurementEntity> serves as the in-memory cache,
    // providing fast access without database overhead.
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

        // Saves a QuantityMeasurementEntity to the in-memory cache and persists to disk.
        // Returns the saved entity for confirmation.
        public QuantityMeasurementEntity Save(QuantityMeasurementEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }
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
