using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaterialBatchController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly MaterialBatchService service;

        public MaterialBatchController(KursovayaContext db)
        {
            this.db = db;
            service = new MaterialBatchService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialBatch>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialBatch>> GetById(int id)
        {
            var material = service.Get(id).Result;
            return material == null ? NotFound(new {message = "Запись о поставке сырья не найдена"}) : Ok(material);
        }
        [HttpPost]
        public async Task<ActionResult<MaterialBatch>> Create([FromBody] MaterialBatch material)
        {
            if (service.Create(material))
                return CreatedAtAction(nameof(GetById), new { id = material.BatchId }, material);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<MaterialBatch>> Update(int id, [FromBody]  MaterialBatch material)
        {
            if (material.BatchId != id) return NotFound();
            if (service.Update(id, material)) return Ok(material);
            return BadRequest();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (service.Delete(id)) return NoContent();
            return NotFound();
        }
    }
}
