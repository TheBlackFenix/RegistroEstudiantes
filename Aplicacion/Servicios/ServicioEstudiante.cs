using Aplicacion.Interfaces;
using Dapper;
using Dominio.Entidades;
using Infraestructura.Context;
using System.Data;
using System.Text.Json;

namespace Aplicacion.Servicios
{
    public class ServicioEstudiante : IServicioEstudiante
    {
        private readonly DbContext _conexion;

        public ServicioEstudiante(DbContext conexion)
        {
            _conexion = conexion;
        }

        public async Task<bool> RegistrarMaterias(int idEstudiante, List<int> materias)
        {
            using var conn = _conexion.CrearConexion();

            var jsonMaterias = JsonSerializer.Serialize(materias);
            var parametros = new DynamicParameters();
            parametros.Add("p_IdEstudiante", idEstudiante);
            parametros.Add("p_MateriasJSON", jsonMaterias);

            try
            {
                await conn.ExecuteAsync("sp_RegistrarMateriasEstudiante", parametros, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<MateriaProfesor>> ObtenerMateriasDisponibles(int idEstudiante)
        {
            using var conn = _conexion.CrearConexion();

            var resultado = await conn.QueryAsync<MateriaProfesor>(
                "sp_ObtenerMateriasDisponibles",
                new { p_IdEstudiante = idEstudiante },
                commandType: CommandType.StoredProcedure);

            return resultado.ToList();
        }

        public async Task<List<EstudianteMateria>> VerCompaneros(int idEstudiante)
        {
            using var conn = _conexion.CrearConexion();

            var resultado = await conn.QueryAsync<EstudianteMateria>(
                "sp_VerEstudiantesPorMateria",
                new { p_IdEstudiante = idEstudiante },
                commandType: CommandType.StoredProcedure);

            return resultado.ToList();
        }

        public async Task<List<Materia>> ObtenerMateriasEstudiante(int idEstudiante)
        {
            using var conn = _conexion.CrearConexion();

            var resultado = await conn.QueryAsync<Materia>(
                "sp_ObtenerMateriasRegistradas",
                new { p_IdEstudiante = idEstudiante },
                commandType: CommandType.StoredProcedure);

            return resultado.ToList();
        }
    }
}
