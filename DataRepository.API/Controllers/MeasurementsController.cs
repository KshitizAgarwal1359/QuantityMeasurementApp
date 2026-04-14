using Microsoft.AspNetCore.Mvc;
using DataRepository.API.Models;

namespace DataRepository.API.Controllers
{
    // UC21: Internal CRUD controller for QuantityMeasurement entities.
    // Called by BusinessLogic.API via HttpClient (Interservice Communication).
    [ApiController]
    [Route("api/data/measurements")]
    [Produces("application/json")]
    public class MeasurementsController : ControllerBase
    {
        private readonly IQuantityMeasurementRepository repository;

        public MeasurementsController(IQuantityMeasurementRepository repository)
        {
            this.repository = repository;
        }

        // POST /api/data/measurements — Save a measurement record
        [HttpPost]
        public IActionResult Save([FromBody] QuantityMeasurementEntity entity)
        {
            QuantityMeasurementEntity saved = repository.Save(entity);
            return Ok(saved);
        }

        // GET /api/data/measurements — Get all measurements
        [HttpGet]
        public IActionResult GetAll()
        {
            List<QuantityMeasurementEntity> all = repository.GetAllMeasurements();
            return Ok(all);
        }

        // GET /api/data/measurements/operation/{operation}
        [HttpGet("operation/{operation}")]
        public IActionResult GetByOperation(string operation)
        {
            List<QuantityMeasurementEntity> results = repository.GetMeasurementsByOperation(operation);
            return Ok(results);
        }

        // GET /api/data/measurements/type/{type}
        [HttpGet("type/{type}")]
        public IActionResult GetByType(string type)
        {
            List<QuantityMeasurementEntity> results = repository.GetMeasurementsByType(type);
            return Ok(results);
        }

        // GET /api/data/measurements/username/{username}
        [HttpGet("username/{username}")]
        public IActionResult GetByUsername(string username)
        {
            List<QuantityMeasurementEntity> results = repository.GetMeasurementsByUsername(username);
            return Ok(results);
        }

        // GET /api/data/measurements/count/{operation}
        [HttpGet("count/{operation}")]
        public IActionResult GetCount(string operation)
        {
            int count = repository.GetCountByOperation(operation);
            return Ok(count);
        }

        // GET /api/data/measurements/errored
        [HttpGet("errored")]
        public IActionResult GetErrored()
        {
            List<QuantityMeasurementEntity> results = repository.GetErroredMeasurements();
            return Ok(results);
        }
    }
}
