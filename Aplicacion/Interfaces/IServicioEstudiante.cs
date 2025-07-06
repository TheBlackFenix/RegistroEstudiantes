using Dominio.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IServicioEstudiante
    {
        Task<bool> RegistrarMaterias(int idEstudiante, List<int> materias);
        Task<List<MateriaProfesor>> ObtenerMateriasDisponibles(int idEstudiante);
        Task<List<EstudianteMateria>> VerCompaneros(int idEstudiante);
        Task<List<Materia>> ObtenerMateriasEstudiante(int idEstudiante);
    }
}
