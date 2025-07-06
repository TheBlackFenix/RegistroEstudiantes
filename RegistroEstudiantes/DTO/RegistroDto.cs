namespace RegistroEstudiantes.DTO
{
    public class RegistroDto
    {
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; } = null!;
        public string ClaveAcceso { get; set; } = null!;
    }
}
