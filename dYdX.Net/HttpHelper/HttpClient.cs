using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace dYdX.Net.HttpHelper
{
    class HttpClient
    {
        public static JObject Get(string url)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "GET";
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }

        public static JObject Put(string url, string data)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "PUT";
                webrequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(webrequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }

        public static JObject GetPrivate(string url, string sign, string timeStamp, string apiKey, string passphrase)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "GET";
                webrequest.Headers.Add("DYDX-SIGNATURE", sign);
                webrequest.Headers.Add("DYDX-API-KEY", apiKey);
                webrequest.Headers.Add("DYDX-TIMESTAMP", timeStamp);
                webrequest.Headers.Add("DYDX-PASSPHRASE", passphrase);
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }

        public static JObject PutPrivate(string url, string data, string sign, string timeStamp, string apiKey, string passphrase)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "PUT";
                webrequest.Headers.Add("DYDX-SIGNATURE", sign);
                webrequest.Headers.Add("DYDX-API-KEY", apiKey);
                webrequest.Headers.Add("DYDX-TIMESTAMP", timeStamp);
                webrequest.Headers.Add("DYDX-PASSPHRASE", passphrase);
                webrequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(webrequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }

        public static JObject PostPrivate(string url, string data, string sign, string timeStamp, string apiKey, string passphrase)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest;
                webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "POST";
                webrequest.Headers.Add("DYDX-SIGNATURE", sign);
                webrequest.Headers.Add("DYDX-API-KEY", apiKey);
                webrequest.Headers.Add("DYDX-TIMESTAMP", timeStamp);
                webrequest.Headers.Add("DYDX-PASSPHRASE", passphrase);
                webrequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(webrequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }

        public static JObject DeletePrivate(string url, string sign, string timeStamp, string apiKey, string passphrase)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "DELETE";
                webrequest.Headers.Add("DYDX-SIGNATURE", sign);
                webrequest.Headers.Add("DYDX-API-KEY", apiKey);
                webrequest.Headers.Add("DYDX-TIMESTAMP", timeStamp);
                webrequest.Headers.Add("DYDX-PASSPHRASE", passphrase);
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }

        public static JObject PostOnboarding(string url, string data, string sign, string ethereumAddress)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest;
                webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "POST";
                webrequest.Headers.Add("DYDX-SIGNATURE", sign);
                webrequest.Headers.Add("DYDX-ETHEREUM-ADDRESS", ethereumAddress);
                webrequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(webrequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }

        public static JObject GetEthPrivate(string url, string sign, string timeStamp, string ethereumAddress)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "GET";
                webrequest.Headers.Add("DYDX-SIGNATURE", sign);
                webrequest.Headers.Add("DYDX-TIMESTAMP", timeStamp);
                webrequest.Headers.Add("DYDX-ETHEREUM-ADDRESS", ethereumAddress);
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }

        public static JObject DeleteEthPrivate(string url, string sign, string timeStamp, string ethereumAddress)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "DELETE";
                webrequest.Headers.Add("DYDX-SIGNATURE", sign);
                webrequest.Headers.Add("DYDX-TIMESTAMP", timeStamp);
                webrequest.Headers.Add("DYDX-ETHEREUM-ADDRESS", ethereumAddress);
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }

        public static JObject PostEthPrivate(string url, string data, string sign, string timeStamp, string ethereumAddress)
        {
            JObject jsonResult = new JObject();
            HttpWebResponse webresponse;
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "POST";
                webrequest.Headers.Add("DYDX-SIGNATURE", sign);
                webrequest.Headers.Add("DYDX-TIMESTAMP", timeStamp);
                webrequest.Headers.Add("DYDX-ETHEREUM-ADDRESS", ethereumAddress);
                webrequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(webrequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (((int)webresponse.StatusCode).ToString().StartsWith("2"))
                {
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = string.Empty;
                    result = responseStream.ReadToEnd();
                    jsonResult = JObject.Parse(result);
                    webresponse.Close();
                    return jsonResult;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    string text;
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream resData = response.GetResponseStream())
                    using (var reader = new StreamReader(resData))
                    {
                        text = reader.ReadToEnd();
                    }
                    jsonResult = JObject.Parse(text);
                    jsonResult.Add("status", (int)httpResponse.StatusCode);
                }
            }
            return jsonResult;
        }
    }
}
