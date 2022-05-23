using Nethereum.Signer;
using Nethereum.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

namespace dYdX.Net
{
    public class Onboarding
    {
        public string baseUrl { get; private set; }
        public int networkId { get; private set; }
        private string ethereumPrivateKey = "";
        private const string ONBOARDING = "dYdX Onboarding";
        private const string KEY_DERIVATION = "dYdX STARK Key";

        public Onboarding(string baseUrl, int networkId)
        {
            this.baseUrl = baseUrl;
            this.networkId = networkId;
        }

        public void SetEthereumPrivateKey(string ethereumPrivateKey)
        {
            this.ethereumPrivateKey = ethereumPrivateKey;
        }

        public async Task<JObject> CreateUser(string starkPublicKey, string starkPublicKeyYCoordinate, string ethereumAddress, string referredByAffiliateLink = null, string country = null)
        {
            try
            {
                JObject dataObjet = new JObject();
                dataObjet.Add("starkKey", starkPublicKey);
                dataObjet.Add("starkKeyYCoordinate", starkPublicKeyYCoordinate);
                if (referredByAffiliateLink != null)
                    dataObjet.Add("referredByAffiliateLink", referredByAffiliateLink);
                if (country != null)
                    dataObjet.Add("country", country);

                string requestPath = "/v3/onboarding";
                string url = baseUrl + requestPath;
                
                var sign = Utils.SignTypedDataV4(ONBOARDING, new EthECKey(ethereumPrivateKey));

                return HttpHelper.HttpClient.PostOnboarding(url, JsonConvert.SerializeObject(dataObjet), sign, ethereumAddress);
            }
            catch (Exception exception)
            {
                throw new Exception("CreateUser request failed.", exception);
            }
        }

        public async Task<JObject> DeriveStarkKey()
        {
            try
            {
                var sign = Utils.SignTypedDataV4(KEY_DERIVATION, new EthECKey(ethereumPrivateKey));
                var hash = Sha3Keccack.Current.CalculateHashFromHex(sign);
                BigInteger starkPrivateKeyInt = BigInteger.Parse("0" + hash, NumberStyles.HexNumber) >> 5;
                
                string starkPrivateKey = "0" + starkPrivateKeyInt.ToString("x");

                JObject result = new JObject();
                result.Add("starkPrivateKey", starkPrivateKey);
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = @"python.exe";
                start.Arguments = string.Format("\"{0}\" \"{1}\""
                    , @"StarkexHelper\getKeyPair.py"
                    , starkPrivateKey);
                start.UseShellExecute = false;
                start.CreateNoWindow = true; 
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true; 
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd(); 
                        string res = reader.ReadToEnd().Replace("\n", "").Replace("\r", ""); 
                        if (res == null || res.Trim() == "")
                            throw new Exception("Create Starkex signature failed.");
                        var ary = res.Split('&');
                        result.Add("starkPublicKey", ary[0]);
                        result.Add("starkPublicKeyCoordinate", ary[1]);
                    }
                }
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception("DeriveStarkKey request failed.", exception);
            }
        }

        public async Task<JObject> RecoverDefaultApiCredentials()
        {
            try
            {
                var sign = Utils.SignTypedDataV4(ONBOARDING, new EthECKey(ethereumPrivateKey));

                var rHex = sign.Substring(2, 64);
                var hashedRBytes = Utils.HexStringToByteArray(Sha3Keccack.Current.CalculateHashFromHex(rHex));
                var secretBytes = hashedRBytes.Slice(0, 30);

                var sHex = sign.Substring(66, 64);
                var hashedSBytes = Utils.HexStringToByteArray(Sha3Keccack.Current.CalculateHashFromHex(sHex));
                var keyBytes = hashedSBytes.Slice(0, 16);
                var passphraseBytes = hashedSBytes.Slice(16, 31);

                var keyHex = BitConverter.ToString(keyBytes).Replace("-", "").ToLower();
                string keyUUID = keyHex.Substring(0, 8) + "-" + keyHex.Substring(8, 4) + "-" + keyHex.Substring(12, 4) + "-" + keyHex.Substring(16, 4) + "-" + keyHex.Substring(20);

                JObject result = new JObject();
                result.Add("secret", Convert.ToBase64String(secretBytes).Replace('+', '-').Replace('/', '_'));
                result.Add("key", keyUUID);
                result.Add("passphrase", Convert.ToBase64String(passphraseBytes).Replace('/', '_'));

                return result;
            }
            catch (Exception exception)
            {
                throw new Exception("RecoverDefaultApiCredentials request failed.", exception);
            }
        }
    }
}
