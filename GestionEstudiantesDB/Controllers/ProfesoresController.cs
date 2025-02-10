using GestionEstudiantesDB.Models;
using GestionEstudiantesDB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionEstudiantesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly IGenericRepository<Profesor> _repository;

        public ProfesoresController(IGenericRepository<Profesor> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesores()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesor(int id)
        {
            var profesor = await _repository.GetByIdAsync(id);
            if (profesor == null)
                return NotFound();

            return Ok(profesor);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProfesor([FromBody] Profesor profesor)
        {
            await _repository.AddAsync(profesor);
            return CreatedAtAction(nameof(GetProfesor), new { id = profesor.Id }, profesor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesor(int id, [FromBody] Profesor profesor)
        {
            if (id != profesor.Id)
                return BadRequest();

            await _repository.UpdateAsync(profesor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
