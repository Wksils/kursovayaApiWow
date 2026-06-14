using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExtruderEventController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly ExtruderEventService service;

        public ExtruderEventController(KursovayaContext db)
        {
            this.db = db;
            service = new ExtruderEventService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtruderEvent>>> Getall()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtruderEvent>> GetById(int id)
        {
            var extruder = service.Get(id).Result;
            return extruder == null ? NotFound(new {message = "событие не найдено"}) : Ok(extruder);
        }
        [HttpPost]
        public async Task<ActionResult<ExtruderEvent>> Create([FromBody] ExtruderEvent extruder)
        {
            if (service.Create(extruder))
                return CreatedAtAction(nameof(GetById), new { id = extruder.EventId }, extruder);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ExtruderEvent>> Update(int id, [FromBody]ExtruderEvent extruder)
        {
            if (extruder.EventId != id) return BadRequest();
            if (service.Update(id, extruder)) return Ok(extruder);
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
