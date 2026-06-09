using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.CompilerServices;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly RecipeService service;

        public RecipeController(KursovayaContext db)
        {
            this.db = db;
            service = new RecipeService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetById(int id)
        {
            var recipe = service.Get(id).Result;
            return recipe == null ? NotFound(new { message = "рецептура не найдена" }) : Ok(recipe);
        }
        [HttpPost]
        public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipe)
        {
            if (service.Create(recipe))
                return CreatedAtAction(nameof(GetById), new { id = recipe.RecipeId }, recipe);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> Update(int id, [FromBody] Recipe recipe)
        {
            if (recipe.RecipeId != id) return BadRequest();
            if (service.Update(id, recipe))
                return Ok(recipe);
            return NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> Delite(int id)
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
