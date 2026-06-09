using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabTestContrller : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly LabTestService service;

        public LabTestContrller(KursovayaContext db)
        {
            this.db = db;
            service = new LabTestService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTest>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LabTest>> GetById(int id)
        {
            var test = service.Get(id).Result;
            return test == null? NotFound(new {message = "запись испытания не найдена"}) : Ok(test);
        }
        [HttpPost]
        public async Task<ActionResult<LabTest>> Create([FromBody] LabTest test)
        {
            if (service.Create(test))
                return CreatedAtAction(nameof(GetById), new { id = test.TestId }, test);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<LabTest>> Update(int id, [FromBody] LabTest test)
        {
            if(test.TestId != id) return BadRequest();
            if (service.Update(id, test)) return Ok(test);
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
