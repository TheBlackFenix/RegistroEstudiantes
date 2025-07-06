using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RegistroEstudiantes.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController: ControllerBase
    {
        private readonly IServicioEstudiante _servicio;

        public EstudiantesController(IServicioEstudiante servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("materias-disponibles")]
        public async Task<IActionResult> ObtenerMateriasDisponibles()
        {
            int.TryParse(User.FindFirst("id").Value, out int claimId);
            if(claimId <= 0)
                return Unauthorized("ID de estudiante no válido.");
            var materias = await _servicio.ObtenerMateriasDisponibles(claimId);
            return Ok(materias);
        }

        [HttpPost("registrar-materias")]
        public async Task<IActionResult> RegistrarMaterias([FromBody] List<int> materias)
        {
            int.TryParse(User.FindFirst("id").Value, out int claimId);
            if (claimId <= 0)
                return Unauthorized("ID de estudiante no válido.");
            var resultado = await _servicio.RegistrarMaterias(claimId, materias);
            if (resultado == false)
                return BadRequest(new { message = "Error al registrar materias. Verifique los datos." });
            return Ok(new { message = "Materias registradas" });
        }

        [HttpGet("companeros")]
        public async Task<IActionResult> VerCompaneros()
        {
            int.TryParse(User.FindFirst("id").Value,out int claimId);
            if (claimId <= 0)
                return Unauthorized("ID de estudiante no válido.");
            var compañeros = await _servicio.VerCompaneros(claimId);
            return Ok(compañeros);
        }

        [HttpGet("materias-inscritas")]
        public async Task<IActionResult> MateriasInscritas()
        {
            int.TryParse(User.FindFirst("id").Value, out int claimId);
            if (claimId <= 0)
                return Unauthorized("ID de estudiante no válido.");
            var materias = await _servicio.ObtenerMateriasEstudiante(claimId);
            return Ok(materias);
        }
    }

}
