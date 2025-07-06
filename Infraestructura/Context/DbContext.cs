using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Infraestructura.Context
{
    public class DbContext
    {
        private readonly string _cadenaConexion;

        public DbContext(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(_cadenaConexion);
        }
    }
}
