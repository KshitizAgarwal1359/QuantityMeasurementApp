using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.Models;
using QuantityMeasurement.Service;

namespace QuantityMeasurement.Controllers
{
    // UC17: REST API Controller for quantity measurement operations.
    [ApiController]
    [Route("api/v1/quantities")]
    [Produces("application/json")]
    public class QuantityMeasurementApiController : ControllerBase
    {
        private readonly IQuantityMeasurementService service;

        public QuantityMeasurementApiController(IQuantityMeasurementService service)
        {
            this.service = service;
        }

        // POST /api/v1/quantities/compare — Compare two quantities
        [HttpPost("compare")]
        public IActionResult CompareQuantities([FromBody] QuantityInputDTO input)
        {
            QuantityDTO result = service.Compare(input.ThisQuantityDTO, input.ThatQuantityDTO!);
            QuantityMeasurementDTO response = new QuantityMeasurementDTO
            {
                ThisValue = input.ThisQuantityDTO.Value,
                ThisUnit = input.ThisQuantityDTO.UnitName,
                ThisMeasurementType = input.ThisQuantityDTO.MeasurementType,
                ThatValue = input.ThatQuantityDTO!.Value,
                ThatUnit = input.ThatQuantityDTO.UnitName,
                ThatMeasurementType = input.ThatQuantityDTO.MeasurementType,
                Operation = "COMPARE",
                ResultString = (result.Value == 1.0).ToString().ToLower(),
                ResultValue = result.Value,
                IsError = false
            };
            return Ok(response);
        }

        // POST /api/v1/quantities/convert — Convert a quantity to target unit
        [HttpPost("convert")]
        public IActionResult ConvertQuantity([FromBody] QuantityInputDTO input)
        {
            string targetUnit = input.ThatQuantityDTO?.UnitName ?? input.TargetUnit ?? "";
            QuantityDTO result = service.Convert(input.ThisQuantityDTO, targetUnit);
            QuantityMeasurementDTO response = new QuantityMeasurementDTO
            {
                ThisValue = input.ThisQuantityDTO.Value,
                ThisUnit = input.ThisQuantityDTO.UnitName,
                ThisMeasurementType = input.ThisQuantityDTO.MeasurementType,
                ThatValue = input.ThatQuantityDTO?.Value ?? 0.0,
                ThatUnit = targetUnit,
                ThatMeasurementType = input.ThisQuantityDTO.MeasurementType,
                Operation = "CONVERT",
                ResultValue = result.Value,
                ResultUnit = result.UnitName,
                IsError = false
            };
            return Ok(response);
        }

        // POST /api/v1/quantities/add — Add two quantities
        [HttpPost("add")]
        public IActionResult AddQuantities([FromBody] QuantityInputDTO input)
        {
            string targetUnit = input.TargetUnit ?? input.ThisQuantityDTO.UnitName;
            QuantityDTO result = service.Add(input.ThisQuantityDTO, input.ThatQuantityDTO!, targetUnit);
            QuantityMeasurementDTO response = new QuantityMeasurementDTO
            {
                ThisValue = input.ThisQuantityDTO.Value,
                ThisUnit = input.ThisQuantityDTO.UnitName,
                ThisMeasurementType = input.ThisQuantityDTO.MeasurementType,
                ThatValue = input.ThatQuantityDTO!.Value,
                ThatUnit = input.ThatQuantityDTO.UnitName,
                ThatMeasurementType = input.ThatQuantityDTO.MeasurementType,
                Operation = "ADD",
                ResultValue = result.Value,
                ResultUnit = result.UnitName,
                ResultMeasurementType = result.MeasurementType,
                IsError = false
            };
            return Ok(response);
        }

        // POST /api/v1/quantities/subtract — Subtract two quantities
        [HttpPost("subtract")]
        public IActionResult SubtractQuantities([FromBody] QuantityInputDTO input)
        {
            string targetUnit = input.TargetUnit ?? input.ThisQuantityDTO.UnitName;
            QuantityDTO result = service.Subtract(input.ThisQuantityDTO, input.ThatQuantityDTO!, targetUnit);
            QuantityMeasurementDTO response = new QuantityMeasurementDTO
            {
                ThisValue = input.ThisQuantityDTO.Value,
                ThisUnit = input.ThisQuantityDTO.UnitName,
                ThisMeasurementType = input.ThisQuantityDTO.MeasurementType,
                ThatValue = input.ThatQuantityDTO!.Value,
                ThatUnit = input.ThatQuantityDTO.UnitName,
                ThatMeasurementType = input.ThatQuantityDTO.MeasurementType,
                Operation = "SUBTRACT",
                ResultValue = result.Value,
                ResultUnit = result.UnitName,
                ResultMeasurementType = result.MeasurementType,
                IsError = false
            };
            return Ok(response);
        }

        // POST /api/v1/quantities/divide — Divide two quantities
        [HttpPost("divide")]
        public IActionResult DivideQuantities([FromBody] QuantityInputDTO input)
        {
            QuantityDTO result = service.Divide(input.ThisQuantityDTO, input.ThatQuantityDTO!);
            QuantityMeasurementDTO response = new QuantityMeasurementDTO
            {
                ThisValue = input.ThisQuantityDTO.Value,
                ThisUnit = input.ThisQuantityDTO.UnitName,
                ThisMeasurementType = input.ThisQuantityDTO.MeasurementType,
                ThatValue = input.ThatQuantityDTO!.Value,
                ThatUnit = input.ThatQuantityDTO.UnitName,
                ThatMeasurementType = input.ThatQuantityDTO.MeasurementType,
                Operation = "DIVIDE",
                ResultValue = result.Value,
                IsError = false
            };
            return Ok(response);
        }

        // GET /api/v1/quantities/history/operation/{operation} — Get history by operation
        [HttpGet("history/operation/{operation}")]
        public IActionResult GetOperationHistory(string operation)
        {
            List<QuantityMeasurementDTO> history = service.GetHistoryByOperation(operation.ToUpper());
            return Ok(history);
        }

        // GET /api/v1/quantities/history/type/{type} — Get history by measurement type
        [HttpGet("history/type/{type}")]
        public IActionResult GetTypeHistory(string type)
        {
            List<QuantityMeasurementDTO> history = service.GetHistoryByType(type);
            return Ok(history);
        }

        // GET /api/v1/quantities/count/{operation} — Get operation count
        [HttpGet("count/{operation}")]
        public IActionResult GetOperationCount(string operation)
        {
            int count = service.GetCountByOperation(operation.ToUpper());
            return Ok(count);
        }

        // GET /api/v1/quantities/history/errored — Get all errored measurements
        [HttpGet("history/errored")]
        public IActionResult GetErrorHistory()
        {
            List<QuantityMeasurementDTO> errors = service.GetErrorHistory();
            return Ok(errors);
        }
    }
}
