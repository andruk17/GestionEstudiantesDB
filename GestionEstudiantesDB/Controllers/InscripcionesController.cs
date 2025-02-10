using GestionEstudiantesDB.Models;
using GestionEstudiantesDB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionEstudiantesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionesController : ControllerBase
    {
        private readonly IGenericRepository<Inscripcion> _repository;

        public InscripcionesController(IGenericRepository<Inscripcion> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateInscripcion([FromBody] Inscripcion inscripcion)
        {
            //ESTUDIANTE NO DEBE TENER MÁS DE 3 MATERIAS
            var inscripcionesExistentes = await _repository.GetAllAsync();
            int cantidadInscrita = inscripcionesExistentes
                .Count(x => x.EstudianteId == inscripcion.EstudianteId);

            if (cantidadInscrita >= 3)
                return BadRequest("Un estudiante no puede inscribirse en más de 3 materias.");

            //NO SE PUEDE REPETIR PROFESOR
            var materiasDelEstudiante = inscripcionesExistentes
                .Where(x => x.EstudianteId == inscripcion.EstudianteId)
                .Select(x => x.Materia)
                .ToList();

            if (materiasDelEstudiante.Any(m => m.ProfesorId == inscripcion.Materia.ProfesorId))
                return BadRequest("El estudiante no puede tener al mismo profesor en más de una materia.");

            await _repository.AddAsync(inscripcion);
            return Ok("Registro de inscripción creado con éxito.");
        }
    }
}
