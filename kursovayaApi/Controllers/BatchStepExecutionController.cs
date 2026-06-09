using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BatchStepExecutionController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly BatchStepExecutionService service;

        public BatchStepExecutionController(KursovayaContext db)
        {
            this.db = db;
            service = new BatchStepExecutionService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchStepExecution>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BatchStepExecution>> GetById(int id)
        {
            var batch = service.Get(id).Result;
            return batch == null? NotFound(new {message = "шаг выполнения партии не найден"}): Ok(batch);
        }
        [HttpPost]
        public async Task<ActionResult<BatchStepExecution>> Create([FromBody] BatchStepExecution batch)
        {
            if (service.Create(batch))
                return CreatedAtAction(nameof(GetById), new { id = batch.ExecutionId }, batch);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<BatchStepExecution>> Update(int id, [FromBody] BatchStepExecution batch)
        {
            if(batch.ExecutionId != id) return BadRequest();
            if (service.Update(id, batch)) return Ok(batch);
            return BadRequest();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if(service.Delete(id)) return NoContent();
            return NotFound();
        }
    }
}
