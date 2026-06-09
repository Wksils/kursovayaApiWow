using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductionBatchController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly ProductionBatchService service;

        public ProductionBatchController(KursovayaContext db)
        {
            this.db = db;
            service = new ProductionBatchService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionBatch>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionBatch>> GetById(int id)
        {
            var production = service.Get(id).Result;
            return production==null?NotFound(new {message = "Партия не найдена"}) : Ok(production);
        }
        [HttpPost]
        public async Task<ActionResult<ProductionBatch>> Create([FromBody] ProductionBatch production)
        {
            if (service.Create(production))
                return CreatedAtAction(nameof(GetById), new { id = production.BatchId }, production);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductionBatch>> Update(int id, [FromBody] ProductionBatch production)
        {
            if (production.BatchId != id) return BadRequest();
            if (service.Update(id, production)) return Ok(production);
            return NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (service.Delete(id)) return NoContent();
            return NotFound();
        }
    }
}
