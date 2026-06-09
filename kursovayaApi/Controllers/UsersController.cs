using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly UsersService service;

        public UsersController(KursovayaContext db)
        {
            this.db = db;
            service = new UsersService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var person = service.Get(id).Result;
            return person == null ? NotFound(new { message = "пользователь не найден" }) : Ok(person);
        }
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            if (service.Create(user))
                return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody] User user)
        {
            if (user.UserId != id) return BadRequest();
            if (service.Update(id, user))
                return Ok(user);
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
