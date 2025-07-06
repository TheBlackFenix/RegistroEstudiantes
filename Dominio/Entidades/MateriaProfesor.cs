using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class MateriaProfesor
    {
        public int IdMateria { get; set; }
        public string NombreMateria { get; set; } = null!;
        public int Creditos { get; set; }
        public int IdProfesor { get; set; }
        public string NombreProfesor { get; set; }
    }
}
