using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeComponentController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly RecipeComponentService service;

        public RecipeComponentController(KursovayaContext db)
        {
            this.db = db;
            service = new RecipeComponentService(db);
        }
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<RecipeComponent>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeComponent>> GetById(int id)
        {
            var component = service.Get(id).Result;
            return component == null? NotFound(new {message = "компонент рецептуры не найден"}) : Ok(component);
        }
        [HttpPost]
        public async Task<ActionResult<RecipeComponent>> Create([FromBody] RecipeComponent component)
        {
            if (service.Create(component))
                return CreatedAtAction(nameof(GetById), new { id = component.ComponentId }, component);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeComponent>> Update(int id, [FromBody] RecipeComponent component)
        {
            if (component.ComponentId != id) return BadRequest();
            if (service.Update(id, component))
                return Ok(component);
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
