using GestionEstudiantesDB.Models;
using GestionEstudiantesDB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionEstudiantesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly IGenericRepository<Materia> _repository;

        public MateriasController(IGenericRepository<Materia> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> GetMaterias()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> GetMateria(int id)
        {
            var materia = await _repository.GetByIdAsync(id);
            if (materia == null)
                return NotFound();

            return Ok(materia);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMateria([FromBody] Materia materia)
        {
            await _repository.AddAsync(materia);
            return CreatedAtAction(nameof(GetMateria), new { id = materia.Id }, materia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMateria(int id, [FromBody] Materia materia)
        {
            if (id != materia.Id)
                return BadRequest();

            await _repository.UpdateAsync(materia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
