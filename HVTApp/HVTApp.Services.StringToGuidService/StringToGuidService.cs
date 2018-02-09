using System;
using System.Security.Cryptography;
using System.Text;

namespace HVTApp.Services.StringToGuidService
{
    public static class StringToGuidService
    {
        public static Guid GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            var csp = new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = csp.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += $"{b:x2}";

            return new Guid(hash);
        }
    }
}
