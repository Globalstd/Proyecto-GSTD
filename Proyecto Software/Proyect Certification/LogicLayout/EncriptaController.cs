using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LogicLayout
{
    public class EncriptaController
    {
        public static string ObtieneEncriptacion(string cadena)
        {
            var md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(cadena));

            var sBuilder = new StringBuilder();

            for (Int16 i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString();
        }
    }
}
