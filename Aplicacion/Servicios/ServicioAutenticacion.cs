using Aplicacion.Interfaces;
using Dapper;
using Dominio.Entidades;
using Infraestructura.Context;
using Infraestructura.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aplicacion.Servicios
{
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        private readonly DbContext _conexion;
        private readonly IConfiguration _config;

        public ServicioAutenticacion(DbContext conexion, IConfiguration config)
        {
            _conexion = conexion;
            _config = config;
        }

        public async Task<bool> Registrar(int idEstudiante, string nombre, string clave)
        {
            using var conn = _conexion.CrearConexion();

            var claveHash = BCryptHelper.Encriptar(clave);

            var parametros = new DynamicParameters();
            parametros.Add("p_IdEstudiante", idEstudiante);
            parametros.Add("p_Nombre", nombre);
            parametros.Add("p_ClaveAcceso", claveHash);

            try
            {
                await conn.ExecuteAsync("sp_RegistrarEstudiante", parametros, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> IniciarSesion(int idEstudiante, string clave)
        {
            using var conn = _conexion.CrearConexion();

            var estudiante = await conn.QueryFirstOrDefaultAsync<Estudiante>(
                "sp_LoginEstudiante",
                new { p_IdEstudiante = idEstudiante },
                commandType: CommandType.StoredProcedure);

            if (estudiante is null)
                return null;

            if (!BCryptHelper.Verificar(clave, estudiante.ClaveAcceso))
                return null;

            // Generar JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var claveSecreta = Encoding.UTF8.GetBytes(_config["Jwt:ClaveSecreta"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("id", estudiante.IdEstudiante.ToString()),
                new Claim("nombre", estudiante.NombreEstudiante)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(claveSecreta),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
