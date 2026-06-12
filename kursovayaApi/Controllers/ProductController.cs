using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly ProductService service;

        public ProductController(KursovayaContext db)
        {
            this.db = db;
            service = new ProductService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = service.Get(id).Result;
            return product==null? NotFound(new { message = "продукт не найден"}) : Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromBody]Product product)
        {
            if (service.Create(product))
                return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, [FromBody] Product product)
        {
            if (product.ProductId != id) return BadRequest();
            if (service.Update(id, product)) return Ok(product);
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
