using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace RegistroEstudiantes.Middlewares
{
    public class GlobalExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionsMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionsMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionsMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext contexto)
        {
            try
            {
                await _next(contexto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada");

                contexto.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Ocurrió un error en el servidor",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Detail = _env.IsDevelopment() ? ex.Message : "Error inesperado. Intente más tarde.",
                    Instance = contexto.Request.Path
                };

                contexto.Response.StatusCode = problem.Status.Value;

                var opcionesJson = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(problem, opcionesJson);
                await contexto.Response.WriteAsync(json);
            }
        }
    }
}
