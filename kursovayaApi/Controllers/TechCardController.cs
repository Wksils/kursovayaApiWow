using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TechCardController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly TechCardService service;

        public TechCardController(KursovayaContext db)
        {
            this.db = db;
            service = new TechCardService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechCard>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TechCard>> GetById(int id)
        {
            var card = service.Get(id).Result;
            return card == null? NotFound(new {message = "тех.карта не найдена"}) : Ok(card);
        }
        [HttpPost]
        public async Task<ActionResult<TechCard>> Create([FromBody] TechCard card)
        {
            if (service.Create(card))
                return CreatedAtAction(nameof(GetById), new { id = card.CardId }, card);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TechCard>> Update(int id, [FromBody] TechCard card)
        {
            if(card.CardId != id) return BadRequest();
            if (service.Update(id, card))
                return Ok(card);
            return NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (service.Delete(id))
                return NoContent();
            return NotFound();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Archive(int id)
        {
            if (service.Archive(id)) return Ok();
            return BadRequest();
        }
    }
}
