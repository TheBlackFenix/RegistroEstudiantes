using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistroEstudiantes.DTO;

namespace RegistroEstudiantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IServicioAutenticacion _servicio;

        public AutenticacionController(IServicioAutenticacion servicio)
        {
            _servicio = servicio;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _servicio.IniciarSesion(dto.IdEstudiante, dto.Clave);
            return token is null ? Unauthorized(new { message = "Credenciales inválidas" }) : Ok(new { token });
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDto dto)
        {
            var exito = await _servicio.Registrar(dto.IdEstudiante, dto.NombreEstudiante, dto.ClaveAcceso);
            return exito ? Ok(new { message = "Estudiante registrado" }) : BadRequest(new { message = "Error al registrar" });
        }
    }
}
