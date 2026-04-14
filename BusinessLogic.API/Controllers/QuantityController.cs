using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.API.Models;
using BusinessLogic.API.Services;

namespace BusinessLogic.API.Controllers
{
    // UC21: Quantity Measurement API Controller — requires JWT authentication.
    [Authorize]
    [ApiController]
    [Route("api/v1/quantities")]
    [Produces("application/json")]
    public class QuantityController : ControllerBase
    {
        private readonly IQuantityMeasurementService service;

        public QuantityController(IQuantityMeasurementService service)
        {
            this.service = service;
        }

        // POST /api/v1/quantities/compare
        [HttpPost("compare")]
        public async Task<IActionResult> CompareQuantities([FromBody] QuantityInputDTO input)
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

        // POST /api/v1/quantities/convert
        [HttpPost("convert")]
        public async Task<IActionResult> ConvertQuantity([FromBody] QuantityInputDTO input)
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

        // POST /api/v1/quantities/add
        [HttpPost("add")]
        public async Task<IActionResult> AddQuantities([FromBody] QuantityInputDTO input)
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

        // POST /api/v1/quantities/subtract
        [HttpPost("subtract")]
        public async Task<IActionResult> SubtractQuantities([FromBody] QuantityInputDTO input)
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

        // POST /api/v1/quantities/divide
        [HttpPost("divide")]
        public async Task<IActionResult> DivideQuantities([FromBody] QuantityInputDTO input)
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

        // GET /api/v1/quantities/history/operation/{operation}
        [HttpGet("history/operation/{operation}")]
        public async Task<IActionResult> GetOperationHistory(string operation)
        {
            List<QuantityMeasurementDTO> history = await service.GetHistoryByOperationAsync(operation.ToUpper());
            return Ok(history);
        }

        // GET /api/v1/quantities/history/type/{type}
        [HttpGet("history/type/{type}")]
        public async Task<IActionResult> GetTypeHistory(string type)
        {
            List<QuantityMeasurementDTO> history = await service.GetHistoryByTypeAsync(type);
            return Ok(history);
        }

        // GET /api/v1/quantities/count/{operation}
        [HttpGet("count/{operation}")]
        public async Task<IActionResult> GetOperationCount(string operation)
        {
            int count = await service.GetCountByOperationAsync(operation.ToUpper());
            return Ok(count);
        }

        // GET /api/v1/quantities/history/errored
        [HttpGet("history/errored")]
        public async Task<IActionResult> GetErrorHistory()
        {
            List<QuantityMeasurementDTO> errors = await service.GetErrorHistoryAsync();
            return Ok(errors);
        }

        // GET /api/v1/quantities/history/my
        [HttpGet("history/my")]
        public async Task<IActionResult> GetMyHistory()
        {
            string username = HttpContext.User.Identity?.Name ?? "Guest";
            List<QuantityMeasurementDTO> history = await service.GetHistoryByUsernameAsync(username);
            return Ok(history);
        }
    }
}
