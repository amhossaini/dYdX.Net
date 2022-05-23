using Nethereum.ABI;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Util;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace dYdX.Net
{
    public static class Utils
    {
        private static readonly ABIEncode _abiEncode = new ABIEncode();

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

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private static byte[] GetDomainHash()
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new BinaryWriter(memoryStream))
            {
                var encodedType = "EIP712Domain(string name,string version,uint256 chainId)";
                var typeHash = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(encodedType));
                writer.Write(typeHash);

                var value = Encoding.UTF8.GetBytes("dYdX");
                var abiValueEncoded = Sha3Keccack.Current.CalculateHash(value);
                writer.Write(abiValueEncoded);

                value = Encoding.UTF8.GetBytes("1.0");
                abiValueEncoded = Sha3Keccack.Current.CalculateHash(value);
                writer.Write(abiValueEncoded);

                var abiValue = new ABIValue("uint256", 1);
                abiValueEncoded = _abiEncode.GetABIEncoded(abiValue);
                writer.Write(abiValueEncoded);

                writer.Flush();
                return Sha3Keccack.Current.CalculateHash(memoryStream.ToArray());
            }
        }

        private static byte[] GetMessageHash(string action)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new BinaryWriter(memoryStream))
            {
                var encodedType = "dYdX(string action,string onlySignOn)";
                var typeHash = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(encodedType));
                writer.Write(typeHash);

                var value = Encoding.UTF8.GetBytes(action);
                var abiValueEncoded = Sha3Keccack.Current.CalculateHash(value);
                writer.Write(abiValueEncoded);

                value = Encoding.UTF8.GetBytes("https://trade.dydx.exchange");
                abiValueEncoded = Sha3Keccack.Current.CalculateHash(value);
                writer.Write(abiValueEncoded);

                writer.Flush();
                return Sha3Keccack.Current.CalculateHash(memoryStream.ToArray());
            }
        }

        private static byte[] GetMessageHash(string method, string requestPath, string data, string isoTimestamp)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new BinaryWriter(memoryStream))
            {
                var encodedType = "dYdX(string method,string requestPath,string body,string timestamp)";
                var typeHash = Sha3Keccack.Current.CalculateHash(Encoding.UTF8.GetBytes(encodedType));
                writer.Write(typeHash);

                var value = Encoding.UTF8.GetBytes(method);
                var abiValueEncoded = Sha3Keccack.Current.CalculateHash(value);
                writer.Write(abiValueEncoded);

                value = Encoding.UTF8.GetBytes(requestPath);
                abiValueEncoded = Sha3Keccack.Current.CalculateHash(value);
                writer.Write(abiValueEncoded);

                value = Encoding.UTF8.GetBytes(data);
                abiValueEncoded = Sha3Keccack.Current.CalculateHash(value);
                writer.Write(abiValueEncoded);

                value = Encoding.UTF8.GetBytes(isoTimestamp);
                abiValueEncoded = Sha3Keccack.Current.CalculateHash(value);
                writer.Write(abiValueEncoded);

                writer.Flush();
                return Sha3Keccack.Current.CalculateHash(memoryStream.ToArray());
            }
        }

        public static string SignTypedDataV4(string action, EthECKey key)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("1901".HexToByteArray());

                var h1 = GetDomainHash();
                var h2 = GetMessageHash(action);

                writer.Write(h1);
                writer.Write(h2);

                writer.Flush();
                var result = memoryStream.ToArray();
                var signature = key.SignAndCalculateV(Sha3Keccack.Current.CalculateHash(result));
                return EthECDSASignature.CreateStringSignature(signature) + "00";
            }
        }

        public static string SignTypedDataV4(EthECKey key, string method, string requestPath, string data, string isoTimestamp)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("1901".HexToByteArray());

                var h1 = GetDomainHash();
                var h2 = GetMessageHash(method, requestPath, data, isoTimestamp);

                writer.Write(h1);
                writer.Write(h2);

                writer.Flush();
                var result = memoryStream.ToArray();
                var signature = key.SignAndCalculateV(Sha3Keccack.Current.CalculateHash(result));
                return EthECDSASignature.CreateStringSignature(signature) + "00";
            }
        }

    }
}
