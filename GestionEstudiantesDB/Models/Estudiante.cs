namespace GestionEstudiantesDB.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Programa { get; set; }
        public List<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}
