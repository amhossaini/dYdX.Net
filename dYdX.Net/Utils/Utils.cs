using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace dYdX.Net
{
    public static class Utils
    {
        public static string GetParamsString(JObject param)
        {
            string ret = "";
            foreach (var prop in param.Properties())
                if (prop.Value.Type != JTokenType.Null)
                    ret += (ret == "") ? ("?" + prop.Name + "=" + prop.Value) : ("&" + prop.Name + "=" + prop.Value);
            return ret;
        }

        public static string Sign(string requestPath, string method, string isoTimestamp, string data, string secret)
        {
            string messageStr = isoTimestamp + method + requestPath + data;
            string scrt = secret;
            scrt = scrt.Replace('-', '+').Replace('_', '/');
            int paddings = scrt.Length % 4;
            if (paddings > 0)
                scrt += new string('=', 4 - paddings);
            byte[] encodedDataAsBytes = Convert.FromBase64String(scrt);
            var hash = new HMACSHA256(encodedDataAsBytes);
            var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(messageStr));
            var sign = Convert.ToBase64String(bytes);
            return sign.Replace('+', '-').Replace('/', '_');
        }

        public static string GenerateRandomClientId()
        {
            Random r = new Random();
            int n = r.Next();
            return n.ToString();
        }

    }
}
