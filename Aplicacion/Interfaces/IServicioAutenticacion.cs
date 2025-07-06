using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IServicioAutenticacion
    {
        Task<string?> IniciarSesion(int idEstudiante, string clave);
        Task<bool> Registrar(int idEstudiante, string nombre, string clave);
    }
}
