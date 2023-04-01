using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RazorWeb.Others
{
    public class Util
    {
        public static string Md5(string sInput)
        {
            HashAlgorithm algorithmType = default;
            ASCIIEncoding enCoder = new ASCIIEncoding();
            byte[] valueByteArr = enCoder.GetBytes(sInput);
            byte[] hashArray = null;
            // Encrypt Input string 
            algorithmType = new MD5CryptoServiceProvider();
            hashArray = algorithmType.ComputeHash(valueByteArr);
            //Convert byte hash to HEX
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashArray)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
        public static string Sha256(string data)
        {
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                // Convert byte array to a string   
                var builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GetIpAddress(ActionExecutingContext context)
        {
            string ipAddress;
            try
            {
                ipAddress = context.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"].ToString();

                if (string.IsNullOrEmpty(ipAddress) || ipAddress.ToLower() == "unknown")
                    ipAddress = context.HttpContext.Request.Headers["REMOTE_ADDR"].ToString();
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP:" + ex.Message;
            }

            return ipAddress;
        }
        public static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }
}
