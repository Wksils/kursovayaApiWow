using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EquipmentController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly EquipmentService service;

        public EquipmentController(KursovayaContext db)
        {
            this.db = db;
            service = new EquipmentService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetById(int id)
        {
            var equipment = service.Get(id).Result;
            return equipment == null? NotFound(new {message = "оборудование не найдено"}):Ok(equipment);
        }
        [HttpPost]
        public async Task<ActionResult<Equipment>> Create([FromBody] Equipment equipment)
        {
            if (service.Create(equipment))
                return CreatedAtAction(nameof(GetById), new { id = equipment.EquipmentId }, equipment);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Equipment>> Update(int id, [FromBody] Equipment equipment)
        {
            if (equipment.EquipmentId != id) return BadRequest();
            if (service.Update(id, equipment))
                return Ok(equipment);
            return NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> Delite(int id)
        {
            if (service.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
