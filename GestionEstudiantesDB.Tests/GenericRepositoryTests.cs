using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionEstudiantesDB.Data;
using GestionEstudiantesDB.Models;
using GestionEstudiantesDB.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GestionEstudiantesDB.Tests
{
    public class GenericRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly GenericRepository<Estudiante> _repository;

        public GenericRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GestionEstudiantesDB")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new GenericRepository<Estudiante>(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEstudiante()
        {           
            var estudiante = new Estudiante { Nombre = "Andrés", Apellido = "Basto", FechaNacimiento = new DateTime(2000, 05, 17), Programa = "Ingeniería de Sistemas" };

            await _repository.AddAsync(estudiante);

            var result = await _repository.GetByIdAsync(estudiante.Id);
            Assert.NotNull(result);
            Assert.Equal("Andrés", result.Nombre);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEstudiantes()
        {
            _context.Estudiantes.Add(new Estudiante { Nombre = "Heidy", Apellido = "González", FechaNacimiento = new DateTime(1999, 05, 20), Programa = "Ingeniería de Sistemas" });
            _context.Estudiantes.Add(new Estudiante { Nombre = "Daniel", Apellido = "González", FechaNacimiento = new DateTime(1999, 10, 30), Programa = "Ingeniería Civil" });
            await _context.SaveChangesAsync();

            var estudiantes = await _repository.GetAllAsync();

            Assert.Equal(2, estudiantes.Count());
        }
    }
}
