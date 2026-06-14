using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExtruderTelemetryController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly ExtruderTelemetryService service;

        public ExtruderTelemetryController(KursovayaContext db)
        {
            this.db = db;
            service = new ExtruderTelemetryService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtruderTelemetry>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtruderTelemetry>> GetById(int id)
        {
            var telemetry = service.Get(id).Result;
            return telemetry == null ? NotFound(new {massege = "не найдено"}) : Ok(telemetry);
        }
        [HttpPost]
        public async Task<ActionResult<ExtruderTelemetry>> Create([FromBody] ExtruderTelemetry telemetry)
        {
            if (service.Create(telemetry))
                return CreatedAtAction(nameof(GetById), new { id = telemetry.TelemetryId }, telemetry);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ExtruderTelemetry>> Update(int id, [FromBody] ExtruderTelemetry telemetry)
        {
            if (telemetry.TelemetryId != id) return BadRequest();
            if (service.Update(id, telemetry)) return Ok(telemetry);
            return NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (service.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
