using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TechStepController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly TechStepService service;
        public TechStepController(KursovayaContext db)
        {
            this.db = db;
            service = new TechStepService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechStep>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TechStep>> GetById(int id)
        {
            var step = service.Get(id).Result;
            return step == null ? NotFound(new {message = "шаг не найден"}) : Ok(step);
        }
        [HttpPost]
        public async Task<ActionResult<TechStep>> Create([FromBody] TechStep step)
        {
            if (service.Create(step))
                return CreatedAtAction(nameof(GetById), new { id = step.StepId }, step);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TechStep>> Update(int id, [FromBody] TechStep step)
        {
            if (step.StepId != id) return BadRequest();
            if (service.Update(id, step))
                return Ok(step);
            return NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (service.Delete(id)) return NoContent();
            return BadRequest();
        }
    }
}
