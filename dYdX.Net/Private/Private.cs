using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using dYdX.Net.Types;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace dYdX.Net
{
    public class Private
    {
        public string baseUrl { get; private set; }
        public int networkId { get; private set; }

        private Structs.ApiKeyCredentials apiKeyCredentials = new Structs.ApiKeyCredentials();
        private string starkPrivateKey = "";

        public Private(string baseUrl, int networkId)
        {
            this.baseUrl = baseUrl;
            this.networkId = networkId;
        }

        public void SetApiKeyCredentials(Structs.ApiKeyCredentials apiKeyCredentials)
        {
            this.apiKeyCredentials = new Structs.ApiKeyCredentials(apiKeyCredentials.key, apiKeyCredentials.secret, apiKeyCredentials.passphrase);
        }

        public void SetStarkPrivateKey(string starkPrivateKey)
        {
            this.starkPrivateKey = starkPrivateKey;
        }

        public async Task<JObject> GetRegistration()
        {
            try
            {
                string requestPath = "/v3/registration";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetRegistration request failed.", exception);
            }
        }

        public async Task<JObject> GetAPIKeys()
        {
            try
            {
                string requestPath = "/v3/api-keys";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetAPIKeys request failed.", exception);
            }
        }

        public async Task<JObject> GetUser()
        {
            try
            {
                string requestPath = "/v3/users";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetMarkets request failed.", exception);
            }
        }

        public async Task<JObject> GetAccounts()
        {
            try
            {
                string requestPath = "/v3/accounts";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetAccounts request failed.", exception);
            }
        }

        public async Task<JObject> GetAccount(string ethereumAddress)
        {
            string accountId = "";
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = @"python.exe";
                start.Arguments = string.Format("\"{0}\" \"{1}\""
                    , @"StarkexHelper\db.py"
                    , ethereumAddress );
                start.UseShellExecute = false;
                start.CreateNoWindow = true; 
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true; 
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd(); 
                        string result = reader.ReadToEnd().Replace("\n", "").Replace("\r", ""); 
                        if (result == null || result.Trim() == "")
                            throw new Exception("Create Starkex signature failed.");
                        accountId = result;
                    }
                }
                string requestPath = "/v3/accounts/" + accountId;
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetAccount request failed.", exception);
            }
        }

        public async Task<JObject> GetAccountLeaderboardPnl(Enums.LeaderboardPnlPeriod period, DateTime? startingBeforeOrAt = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if (startingBeforeOrAt != null)
                    paramObjet.Add("startingBeforeOrAt", startingBeforeOrAt.Value.ToUniversalTime().ToString("o"));

                string requestPath = "/v3/accounts/leaderboard-pnl/" + Enums.LeaderboardPnlPeriodArray[(int)period] + Utils.GetParamsString(paramObjet); ;
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetAccountLeaderboardPnl request failed.", exception);
            }
        }

        public async Task<JObject> GetPositions(string market = null, Enums.PositionStatus? status = null, int? limit = null, DateTime? createdBeforeOrAt = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if(market != null)
                    paramObjet.Add("market", market);
                if(status != null)
                    paramObjet.Add("status", Enums.PositionStatusArray[(int)status]);
                paramObjet.Add("limit", limit);
                if(createdBeforeOrAt != null)
                    paramObjet.Add("createdBeforeOrAt", createdBeforeOrAt.Value.ToUniversalTime().ToString("o"));
                string requestPath = "/v3/positions" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetPositions request failed.", exception);
            }
        }

        public async Task<JObject> GetTransfers(Enums.TransferType? transferType = null, int? limit = null, DateTime? createdBeforeOrAt = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if (transferType != null)
                    paramObjet.Add("transferType", Enums.TransferTypeArray[(int)transferType]);
                if(limit != null)
                    paramObjet.Add("limit", limit);
                if (createdBeforeOrAt != null)
                    paramObjet.Add("createdBeforeOrAt", createdBeforeOrAt.Value.ToUniversalTime().ToString("o"));
                string requestPath = "/v3/transfers" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetTransfers request failed.", exception);
            }
        }

        public async Task<JObject> GetOrders(string market = null, Enums.OrderStatus? status = null, Enums.Side? side = null, Enums.OrderType? type = null, int? limit = null, DateTime? createdBeforeOrAt = null, bool? returnLatestOrders = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if(market != null)
                    paramObjet.Add("market", market);
                if (status != null)
                    paramObjet.Add("status", Enums.OrderStatusArray[(int)status]);
                if (side != null)
                    paramObjet.Add("side", Enums.SideArray[(int)side]);
                if (type != null)
                    paramObjet.Add("type", Enums.OrderTypeArray[(int)type]);
                if(limit != null)
                    paramObjet.Add("limit", limit);
                if (createdBeforeOrAt != null)
                    paramObjet.Add("createdBeforeOrAt", createdBeforeOrAt.Value.ToUniversalTime().ToString("o"));
                if(returnLatestOrders != null)
                    paramObjet.Add("returnLatestOrders", returnLatestOrders);
                string requestPath = "/v3/orders" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetOrders request failed.", exception);
            }
        }

        public async Task<JObject> GetActiveOrders(string market, Enums.Side? side = null, string id = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("market", market);
                if (side != null)
                    paramObjet.Add("side", Enums.SideArray[(int)side]);
                if(id != null)
                    paramObjet.Add("id", id);
                string requestPath = "/v3/active-orders" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetActiveOrders request failed.", exception);
            }
        }

        public async Task<JObject> GetOrderById(string id)
        {
            try
            {
                string requestPath = "/v3/orders/" + id;
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetOrderById request failed.", exception);
            }
        }

        public async Task<JObject> GetOrderByClientId(string id)
        {
            try
            {
                string requestPath = "/v3/orders/client/" + id;
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetOrderByClientId request failed.", exception);
            }
        }

        public async Task<JObject> GetFills(string market = null, string orderId = null, int? limit = null, DateTime? createdBeforeOrAt = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if (market != null)
                    paramObjet.Add("market", market);
                if(orderId != null)
                    paramObjet.Add("orderId", orderId);
                if(limit != null)
                    paramObjet.Add("limit", limit);
                if (createdBeforeOrAt != null)
                    paramObjet.Add("createdBeforeOrAt", createdBeforeOrAt.Value.ToUniversalTime().ToString("o"));
                string requestPath = "/v3/fills" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetFills request failed.", exception);
            }
        }

        public async Task<JObject> GetFundingPayments(string market = null, int? limit = null, DateTime? effectiveBeforeOrAt = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if (market != null)
                    paramObjet.Add("market", market);
                if(limit != null)
                    paramObjet.Add("limit", limit);
                if (effectiveBeforeOrAt != null)
                    paramObjet.Add("effectiveBeforeOrAt", effectiveBeforeOrAt.Value.ToUniversalTime().ToString("o"));
                string requestPath = "/v3/funding" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetFundingPayments request failed.", exception);
            }
        }

        public async Task<JObject> GetHistoricalPNLTicks(DateTime? effectiveBeforeOrAt = null, DateTime? effectiveAtOrAfter = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if (effectiveBeforeOrAt != null)
                    paramObjet.Add("effectiveBeforeOrAt", effectiveBeforeOrAt.Value.ToUniversalTime().ToString("o"));
                if (effectiveAtOrAfter != null)
                    paramObjet.Add("effectiveAtOrAfter", effectiveAtOrAfter.Value.ToUniversalTime().ToString("o"));
                string requestPath = "/v3/historical-pnl" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetHistoricalPNLTicks request failed.", exception);
            }
        }

        public async Task<JObject> GetTradingRewards(int? epoch = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("epoch", epoch);
                string requestPath = "/v3/rewards/weight" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetTradingRewards request failed.", exception);
            }
        }

        public async Task<JObject> GetLiquidityProviderRewards(int? epoch = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("epoch", epoch);
                string requestPath = "/v3/rewards/liquidity" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetLiquidityProviderRewards request failed.", exception);
            }
        }

        public async Task<JObject> GetRetroactiveMiningRewards()
        {
            try
            {
                string requestPath = "/v3/rewards/retroactive-mining";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetRetroactiveMiningRewards request failed.", exception);
            }
        }

        public async Task<JObject> GetProfile()
        {
            try
            {
                string requestPath = "/v3/profile/private";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "GET", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.GetPrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("GetProfile request failed.", exception);
            }
        }

        public async Task<JObject> UpdateUser(JObject userData, string email = null, string username = null, bool? isSharingUsername = null, bool? isSharingAddress = null, string country = null)
        {
            try
            {
                string strUserData = JsonConvert.SerializeObject(userData);
                JObject dataObjet = new JObject();
                if(email != null)
                    dataObjet.Add("email", email);
                if(username != null)
                    dataObjet.Add("username", username);
                if(isSharingUsername != null)
                    dataObjet.Add("isSharingUsername", isSharingUsername);
                if(isSharingAddress != null)
                    dataObjet.Add("isSharingAddress", isSharingAddress);
                dataObjet.Add("userData", strUserData);
                if(country != null)
                    dataObjet.Add("country", country);

                string requestPath = "/v3/users";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "PUT", timeStamp, JsonConvert.SerializeObject(dataObjet), apiKeyCredentials.secret);
                return HttpHelper.HttpClient.PutPrivate(url, strUserData, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("UpdateUser request failed.", exception);
            }
        }

        public async Task<JObject> CreateAccount(string starkPublicKey, string starkPublicKeyYCoordinate)
        {
            try
            {
                JObject dataObjet = new JObject();
                dataObjet.Add("starkKey", starkPublicKey);
                dataObjet.Add("starkKeyYCoordinate", starkPublicKeyYCoordinate);

                string requestPath = "/v3/accounts";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "POST", timeStamp, JsonConvert.SerializeObject(dataObjet), apiKeyCredentials.secret);
                return HttpHelper.HttpClient.PostPrivate(url, JsonConvert.SerializeObject(dataObjet), sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("CreateAccount request failed.", exception);
            }
        }

        public async Task<JObject> CreateOrder(Structs.ApiOrder apiOrder, int positionId)
        {
            try
            {
                if (apiOrder.clientId == null)
                    apiOrder.clientId = Utils.GenerateRandomClientId();
                if (apiOrder.signature == null)
                {
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.FileName = @"python.exe";
                    start.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\" \"{6}\" \"{7}\" \"{8}\" \"{9}\" \"{10}\""
                        , @"StarkexHelper\sign.py"
                        , networkId
                        , positionId
                        , apiOrder.clientId
                        , apiOrder.market
                        , Enums.SideArray[(int)apiOrder.side]
                        , apiOrder.size
                        , apiOrder.price
                        , apiOrder.limitFee
                        , apiOrder.expiration.ToUniversalTime().ToString("o")
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
                            string result = reader.ReadToEnd().Replace("\n", "").Replace("\r", ""); 
                            if(result == null || result.Trim() == "")
                                throw new Exception("Create Starkex signature failed.");
                            apiOrder.signature = result;
                        }
                    }
                }
                JObject dataObjet = new JObject();
                if (apiOrder.market != null)
                    dataObjet.Add("market", apiOrder.market);
                if (apiOrder.side != 0)
                    dataObjet.Add("side", Enums.SideArray[(int)apiOrder.side]);
                if (apiOrder.type != 0)
                    dataObjet.Add("type", Enums.OrderTypeArray[(int)apiOrder.type]);
                dataObjet.Add("timeInForce", Enums.TimeInForceArray[(int)apiOrder.timeInForce]);
                dataObjet.Add("size", apiOrder.size.ToString());
                dataObjet.Add("price", apiOrder.price.ToString());
                dataObjet.Add("limitFee", apiOrder.limitFee.ToString());
                dataObjet.Add("expiration", apiOrder.expiration.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                if(apiOrder.cancelId != null)
                    dataObjet.Add("cancelId", apiOrder.cancelId);
                if (apiOrder.triggerPrice != null)
                    dataObjet.Add("triggerPrice", apiOrder.triggerPrice);
                if (apiOrder.trailingPercent != null)
                    dataObjet.Add("trailingPercent", apiOrder.trailingPercent);
                dataObjet.Add("postOnly", apiOrder.postOnly);
                dataObjet.Add("clientId", apiOrder.clientId);
                dataObjet.Add("signature", apiOrder.signature);

                string requestPath = "/v3/orders";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "POST", timeStamp, JsonConvert.SerializeObject(dataObjet), apiKeyCredentials.secret);
                return HttpHelper.HttpClient.PostPrivate(url, JsonConvert.SerializeObject(dataObjet), sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("CreateOrder request failed.", exception);
            }
        }

        public async Task<JObject> CreateTransfer(Structs.ApiTransfer apiTransfer)
        {
            try
            {
                if (apiTransfer.clientId == null)
                    apiTransfer.clientId = Utils.GenerateRandomClientId();
                if (apiTransfer.signature == null)
                {
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.FileName = @"python.exe";
                    start.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\" \"{6}\" \"{7}\" \"{8}\""
                        , @"StarkexHelper\signTransfer.py"
                        , networkId
                        , apiTransfer.senderPositionId
                        , apiTransfer.receiverPositionId
                        , apiTransfer.receiverPublicKey
                        , apiTransfer.amount
                        , apiTransfer.clientId
                        , apiTransfer.expiration.ToUniversalTime().ToString("o")
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
                            string result = reader.ReadToEnd().Replace("\n", "").Replace("\r", ""); 
                            if (result == null || result.Trim() == "")
                                throw new Exception("Create Starkex signature failed.");
                            apiTransfer.signature = result;
                        }
                    }
                }
                JObject dataObjet = new JObject();
                dataObjet.Add("amount", apiTransfer.amount.ToString());
                dataObjet.Add("receiverAccountId", apiTransfer.receiverAccountId);
                dataObjet.Add("clientId", apiTransfer.clientId);
                dataObjet.Add("signature", apiTransfer.signature);
                dataObjet.Add("expiration", apiTransfer.expiration);

                string requestPath = "/v3/transfers";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "POST", timeStamp, JsonConvert.SerializeObject(dataObjet), apiKeyCredentials.secret);
                return HttpHelper.HttpClient.PostPrivate(url, JsonConvert.SerializeObject(dataObjet), sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("CreateTransfer request failed.", exception);
            }
        }

        public async Task<JObject> CancelOrder(string orderId)
        {
            try
            {
                string requestPath = "/v3/orders/" + orderId;
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "DELETE", timeStamp, "", apiKeyCredentials.secret);
                
                return HttpHelper.HttpClient.DeletePrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("CancelOrder request failed.", exception);
            }
        }

        public async Task<JObject> CancelAllOrder(string market=null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if(market != null)
                    paramObjet.Add("market", market);
                string requestPath = "/v3/orders" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "DELETE", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.DeletePrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("CancelAllOrder request failed.", exception);
            }
        }

        public async Task<JObject> CancelActiveOrder(string market, Enums.Side? side = null, string id = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("market", market);
                if (side != null)
                    paramObjet.Add("side", Enums.SideArray[(int)side]);
                if(id != null)
                    paramObjet.Add("id", id);
                string requestPath = "/v3/active-orders" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "DELETE", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.DeletePrivate(url, sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("CancelActiveOrder request failed.", exception);
            }
        }

        public async Task<JObject> CreateWithdrawal(Structs.ApiWithdrawal apiWithdrawal, int positionId)
        {
            try
            {
                if (apiWithdrawal.clientId == null)
                    apiWithdrawal.clientId = Utils.GenerateRandomClientId();
                apiWithdrawal.clientId = "902014659";
                if (apiWithdrawal.signature == null)
                {
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.FileName = @"python.exe";
                    start.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\" \"{6}\""
                        , @"StarkexHelper\signWithdrawal.py"
                        , networkId
                        , positionId
                        , apiWithdrawal.clientId
                        , apiWithdrawal.amount
                        , apiWithdrawal.expiration.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
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
                            string result = reader.ReadToEnd().Replace("\n", "").Replace("\r", ""); 
                            if (result == null || result.Trim() == "")
                                throw new Exception("Create Starkex signature failed.");
                            apiWithdrawal.signature = result;
                        }
                    }
                }
                
                JObject dataObjet = new JObject();
                dataObjet.Add("amount", apiWithdrawal.amount.ToString());
                if (apiWithdrawal.asset != null)
                    dataObjet.Add("asset", apiWithdrawal.asset);
                else
                    dataObjet.Add("asset", "USDC");
                dataObjet.Add("expiration", apiWithdrawal.expiration.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                dataObjet.Add("clientId", apiWithdrawal.clientId);
                dataObjet.Add("signature", apiWithdrawal.signature);

                string requestPath = "/v3/withdrawals";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                string sign = Utils.Sign(requestPath, "POST", timeStamp, JsonConvert.SerializeObject(dataObjet), apiKeyCredentials.secret);
                return HttpHelper.HttpClient.PostPrivate(url, JsonConvert.SerializeObject(dataObjet), sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("CreateWithdrawal request failed.", exception);
            }
        }

        public async Task<JObject> CreateFastWithdrawal(Structs.ApiFastWithdrawal apiFastWithdrawal, int positionId)
        {
            try
            {
                if (apiFastWithdrawal.clientId == null)
                    apiFastWithdrawal.clientId = Utils.GenerateRandomClientId();
                if (apiFastWithdrawal.signature == null)
                {
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.FileName = @"python.exe";
                    start.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\" \"{6}\" \"{7}\" \"{8}\" \"{9}\" \"{10}\""
                        , @"StarkexHelper\signFastWithdrawal.py"
                        , networkId
                        , positionId
                        , apiFastWithdrawal.clientId
                        , apiFastWithdrawal.creditAmount
                        , apiFastWithdrawal.debitAmount
                        , apiFastWithdrawal.toAddress
                        , apiFastWithdrawal.lpPositionId
                        , apiFastWithdrawal.lpStarkPublicKey
                        , apiFastWithdrawal.expiration.ToUniversalTime().ToString("o")
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
                            string result = reader.ReadToEnd().Replace("\n", "").Replace("\r", ""); 
                            if (result == null || result.Trim() == "")
                                throw new Exception("Create Starkex signature failed.");
                            apiFastWithdrawal.signature = result;
                        }
                    }
                }
                JObject dataObjet = new JObject();
                dataObjet.Add("creditAsset", apiFastWithdrawal.creditAsset);
                dataObjet.Add("creditAmount", apiFastWithdrawal.creditAmount.ToString());
                dataObjet.Add("debitAmount", apiFastWithdrawal.debitAmount.ToString());
                dataObjet.Add("toAddress", apiFastWithdrawal.toAddress);
                dataObjet.Add("lpPositionId", apiFastWithdrawal.lpPositionId);
                dataObjet.Add("expiration", apiFastWithdrawal.expiration.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                dataObjet.Add("clientId", apiFastWithdrawal.clientId);
                dataObjet.Add("signature", apiFastWithdrawal.signature);

                string requestPath = "/v3/fast-withdrawals";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "POST", timeStamp, JsonConvert.SerializeObject(dataObjet), apiKeyCredentials.secret);
                return HttpHelper.HttpClient.PostPrivate(url, JsonConvert.SerializeObject(dataObjet), sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("CreateFastWithdrawal request failed.", exception);
            }
        }

        public async Task<JObject> SendVerificationEmail()
        {
            try
            {
                string requestPath = "/v3/emails/send-verification-email";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "PUT", timeStamp, "", apiKeyCredentials.secret);
                return HttpHelper.HttpClient.PutPrivate(url, "", sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("SendVerificationEmail request failed.", exception);
            }
        }

        public async Task<JObject> RequestTestnetTokens()
        {
            try
            {
                JObject dataObjet = new JObject();
                string test = JsonConvert.SerializeObject(dataObjet);
                string requestPath = "/v3/testnet/tokens";
                string url = baseUrl + requestPath;
                string timeStamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                string sign = Utils.Sign(requestPath, "POST", timeStamp, JsonConvert.SerializeObject(dataObjet), apiKeyCredentials.secret);
                return HttpHelper.HttpClient.PostPrivate(url, "", sign, timeStamp, apiKeyCredentials.key, apiKeyCredentials.passphrase);
            }
            catch (Exception exception)
            {
                throw new Exception("RequestTestnetTokens request failed.", exception);
            }
        }

    }
}
