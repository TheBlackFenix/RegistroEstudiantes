using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Helpers
{
    public class BCryptHelper
    {
        public static string Encriptar(string textoPlano)
        {
            return BCrypt.Net.BCrypt.HashPassword(textoPlano);
        }

        public static bool Verificar(string textoPlano, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(textoPlano, hash);
        }
    }
}
