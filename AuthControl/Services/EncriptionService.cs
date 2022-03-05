using System.Security.Cryptography;
using System.Text;

namespace AuthControl.Services
{
    public class EncriptionService
    {
        public string Encript(string password)
        {
            MD5 md5Hasher = MD5.Create();

            var md5PasswordString = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < md5PasswordString.Length; i++)

            {
                strBuilder.Append(md5PasswordString[i].ToString("x2"));

            }

            var passEncripted = strBuilder.ToString();

            return EncodeToBase64(passEncripted);
        }


        static public string EncodeToBase64(string texto)
        {
            try
            {
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //converte de base64 para texto
        static public string DecodeFrom64(string dados)
        {
            try
            {
                byte[] dadosAsBytes = System.Convert.FromBase64String(dados);
                string resultado = System.Text.ASCIIEncoding.ASCII.GetString(dadosAsBytes);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
