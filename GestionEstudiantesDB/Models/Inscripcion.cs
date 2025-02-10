﻿namespace GestionEstudiantesDB.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }
        public Estudiante Estudiante { get; set; }
        public Materia Materia { get; set; }
    }
}
