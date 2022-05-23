using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace dYdX.Net
{
    public class EthPrivate
    {
        public string baseUrl { get; private set; }
        public int networkId { get; private set; }
        private string ethereumPrivateKey = "";

        public EthPrivate(string baseUrl, int networkId)
        {
            this.baseUrl = baseUrl;
            this.networkId = networkId;
        }

        public void SetEthereumPrivateKey(string ethereumPrivateKey)
        {
            this.ethereumPrivateKey = ethereumPrivateKey;
        }

        public async Task<JObject> CreateApiKey()
        {
            try
            {
                var account = new Account(ethereumPrivateKey);
                string ethereumAddress = account.Address;
                string requestPath = "/v3/api-keys";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                var sign = Utils.SignTypedDataV4(new EthECKey(ethereumPrivateKey), "POST", requestPath, "{}", timeStamp);
                return HttpHelper.HttpClient.PostEthPrivate(url, "", sign, timeStamp, ethereumAddress);
            }
            catch (Exception exception)
            {
                throw new Exception("DeleteApiKey request failed.", exception);
            }
        }

        public async Task<JObject> Recovery()
        {
            try
            {
                var account = new Account(ethereumPrivateKey);
                string ethereumAddress = account.Address;
                string requestPath = "/v3/recovery";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                var sign = Utils.SignTypedDataV4(new EthECKey(ethereumPrivateKey), "GET", requestPath,"{}", timeStamp);
                return HttpHelper.HttpClient.GetEthPrivate(url, sign, timeStamp, ethereumAddress);
            }
            catch (Exception exception)
            {
                throw new Exception("Recovery request failed.", exception);
            }
        }

        public async Task<JObject> DeleteApiKey(string apiKey)
        {
            try
            {
                var account = new Account(ethereumPrivateKey);
                string ethereumAddress = account.Address;
                string requestPath = "/v3/api-keys?" + apiKey;
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                var sign = Utils.SignTypedDataV4(new EthECKey(ethereumPrivateKey), "DELETE", requestPath, "{}", timeStamp);
                return HttpHelper.HttpClient.DeleteEthPrivate(url, sign, timeStamp, ethereumAddress);
            }
            catch (Exception exception)
            {
                throw new Exception("DeleteApiKey request failed.", exception);
            }
        }
    }
}
