using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        //encriptar de Texto a SHA256  -> Para la clave

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            //usando las referencias de "system.security.cryptography
            using(SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach(byte b in result)
                    Sb.Append(b.ToString("x2")); //la cadena sea x2 más grande
            }
            return Sb.ToString();
        }
    }
}
