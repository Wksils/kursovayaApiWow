using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UomController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly UomService service;

        public UomController(KursovayaContext db)
        {
            this.db = db;
            service = new UomService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitsOfMeasure>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitsOfMeasure>> GetById(int id)
        {
            var uom = service.Get(id).Result;
            return uom == null? NotFound(new {message = "не найдено"}) : Ok(uom);
        }
        [HttpPost]
        public async Task<ActionResult<UnitsOfMeasure>> Create([FromBody] UnitsOfMeasure uom)
        {
            if (service.Create(uom))
                return CreatedAtAction(nameof(GetById), new { id = uom.UomId }, uom);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UnitsOfMeasure>> Update(int id, [FromBody] UnitsOfMeasure uom)
        {
            if (uom.UomId != id) return BadRequest();
            if (service.Update(id, uom))
                return Ok(uom);
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
