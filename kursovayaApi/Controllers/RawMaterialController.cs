using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RawMaterialController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly RawMaterialService service;

        public RawMaterialController(KursovayaContext db)
        {
            this.db = db;
            service = new RawMaterialService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RawMaterial>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RawMaterial>> GetById(int id)
        {
            var material = service.Get(id).Result;
            return material == null? NotFound(new {message = "сырье не найдено"}) : Ok(material);
        }
        [HttpPost]
        public async Task<ActionResult<RawMaterial>> Create([FromBody] RawMaterial material)
        {
            if (service.Create(material))
                return CreatedAtAction(nameof(GetById), new { id = material.MaterialId }, material);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<RawMaterial>> Update(int id, [FromBody] RawMaterial material)
        {
            if (material.MaterialId != id) return BadRequest();
            if (service.Update(id, material))
                return Ok(material);
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
