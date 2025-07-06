using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Estudiante
    {
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; } = null!;
        public string ClaveAcceso { get; set; } = null!;
    }
}
