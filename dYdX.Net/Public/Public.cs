using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using dYdX.Net.Types;

namespace dYdX.Net
{
    public class Public
    {
        public string baseUrl { get; private set; }

        public Public(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<JObject> GetMarkets(string market = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if(market != null)
                    paramObjet.Add("market", market);
                string requestPath = "/v3/markets" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetMarkets request failed.", exception);
            }
        }

        public async Task<JObject> GetOrderbook(string market)
        {
            try
            {
                string url = baseUrl + "/v3/orderbook/" + market;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetOrderbook request failed.", exception);
            }
        }

        public async Task<JObject> GetTrades(string market, DateTime? startingBeforeOrAt = null, int? limit = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if (startingBeforeOrAt != null)
                    paramObjet.Add("startingBeforeOrAt", startingBeforeOrAt.Value.ToUniversalTime().ToString("o"));
                paramObjet.Add("limit", limit);
                string requestPath = "/v3/trades/" + market + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetTrades request failed.", exception);
            }
        }

        public async Task<JObject> GetFastWithdrawals(int? creditAmount =null, int? debitAmount = null, string creditAsset = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("creditAmount", creditAmount);
                paramObjet.Add("debitAmount", debitAmount);
                paramObjet.Add("creditAsset", creditAsset);
                string requestPath = "/v3/fast-withdrawals" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetFastWithdrawals request failed.", exception);
            }
        }

        public async Task<JObject> GetMarketStats(string market, int days = 1)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("days", days);
                string requestPath = "/v3/stats/" + market + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetMarketStats request failed.", exception);
            }
        }

        public async Task<JObject> GetHistoricalFunding(string market, DateTime? effectiveBeforeOrAt = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if (effectiveBeforeOrAt != null)
                    paramObjet.Add("effectiveBeforeOrAt", effectiveBeforeOrAt.Value.ToUniversalTime().ToString("o"));
                string requestPath = "/v3/historical-funding/" + market + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                return HttpHelper.HttpClient.Get(url);

            }
            catch (Exception exception)
            {
                throw new Exception("GetHistoricalFunding request failed.", exception);
            }
        }

        public async Task<JObject> GetCandlesForMarket(string market, DateTime? fromISO = null, DateTime? toISO = null, int? limit = null, Enums.CandleResolution? resolution = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                if (fromISO != null)
                    paramObjet.Add("fromISO", fromISO.Value.ToUniversalTime().ToString("o"));
                if (toISO != null)
                    paramObjet.Add("toISO", toISO.Value.ToUniversalTime().ToString("o"));
                paramObjet.Add("limit", limit);
                if(resolution != null)
                       paramObjet.Add("effectiveBeforeOrAt", Enums.CandleResolutionArray[(int)resolution]);
                string requestPath = "/v3/candles/" + market + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetCandlesForMarket request failed.", exception);
            }
        }

        public async Task<JObject> GetGlobalConfiguration()
        {
            try
            {
                string url = baseUrl + "/v3/config";
                return HttpHelper.HttpClient.Get(url);

            }
            catch (Exception exception)
            {
                throw new Exception("GetGlobalConfiguration request failed.", exception);
            }
        }

        public async Task<bool> CheckUserExists(string ethereumAddress)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("ethereumAddress", ethereumAddress);
                string requestPath = "/v3/users/exists" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                var exists = HttpHelper.HttpClient.Get(url);
                return exists["exists"].Value<bool>();
            }
            catch (Exception exception)
            {
                throw new Exception("CheckUserExists request failed.", exception);
            }
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("username", username);
                string requestPath = "/v3/usernames" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                var exists = HttpHelper.HttpClient.Get(url);
                return exists["exists"].Value<bool>();
            }
            catch (Exception exception)
            {
                throw new Exception("CheckUsernameExists request failed.", exception);
            }
        }

        public async Task<JObject> GetAPIServerTime()
        {
            try
            {
                string url = baseUrl + "/v3/time";
                return HttpHelper.HttpClient.Get(url);

            }
            catch (Exception exception)
            {
                throw new Exception("GetAPIServerTime request failed.", exception);
            }
        }

        public async Task<JObject> GetPublicLeaderboardPNLs(Enums.LeaderboardPnlPeriod period, DateTime startingBeforeOrAt, Enums.PNL sortBy, int? limit = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("period", Enums.LeaderboardPnlPeriodArray[(int)period]);
                if (startingBeforeOrAt != null)
                    paramObjet.Add("startingBeforeOrAt", startingBeforeOrAt.ToUniversalTime().ToString("o"));
                paramObjet.Add("sortBy", Enums.PNLArray[(int)sortBy]);
                paramObjet.Add("limit", limit);
                string requestPath = "/v3/leaderboard-pnl" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetPublicLeaderboardPNLs request failed.", exception);
            }
        }

        public async Task<JObject> GetPublicRetroactiveMiningRewards(string ethereumAddress)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("ethereumAddress", ethereumAddress);
                string requestPath = "/v3/public-retroactive-mining" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetPublicRetroactiveMiningRewards request failed.", exception);
            }
        }

        public async Task<JObject> VerifyEmail(string token)
        {
            try
            {
                string url = baseUrl + "/v3/emails/verify-email";
                var data = @"{""token"": """ + token + @""" }";
                return HttpHelper.HttpClient.Put(url, data);

            }
            catch (Exception exception)
            {
                throw new Exception("VerifyEmail request failed.", exception);
            }
        }

        public async Task<JObject> GetCurrentlyRevealedHedgies()
        {
            try
            {
                string url = baseUrl + "/v3/hedgies/current";
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetCurrentlyRevealedHedgies request failed.", exception);
            }
        }

        public async Task<JObject> GetHistoricallyRevealedHedgies(Enums.NftRevealType nftRevealType, int? start = null, int? end = null)
        {
            try
            {
                JObject paramObjet = new JObject();
                paramObjet.Add("nftRevealType", Enums.NftRevealTypeArray[(int)nftRevealType]);
                paramObjet.Add("start", start);
                paramObjet.Add("end", end);
                string requestPath = "/v3/lhedgies/history" + Utils.GetParamsString(paramObjet);
                string url = baseUrl + requestPath;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetPublicLeaderboardPNLs request failed.", exception);
            }
        }

        public async Task<JObject> GetProfile(string publicId)
        {
            try
            {
                string url = baseUrl + "/v3/profile/" + publicId;
                return HttpHelper.HttpClient.Get(url);
            }
            catch (Exception exception)
            {
                throw new Exception("GetProfile request failed.", exception);
            }
        }
    }
}
