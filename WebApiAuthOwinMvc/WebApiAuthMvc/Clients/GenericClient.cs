using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.IO; 
using System.Net; 
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace WebApiAuthMvc.Clients
{
    public class GenericClient
    {
        public static string GetEvent(string uRL, string param = "", string token = "")
        {
            string responseResult = string.Empty;
            var urlWithParam = string.Format("{0}{1}", uRL, param); //"http://mysite.com?ver=1&var=2"; 

            try
            {
                var httpRequest = WebRequest.CreateHttp(urlWithParam);
                var cacheCredentials = new CredentialCache();
                cacheCredentials.Add(new Uri(uRL), "Negotiate", CredentialCache.DefaultNetworkCredentials);
                //cacheCredentials.Add(new Uri(uRL), "Basic", new NetworkCredential("user", "pass"));

                httpRequest.Method = "GET";
                httpRequest.Accept = "application/json";
                httpRequest.Credentials = cacheCredentials;
                httpRequest.PreAuthenticate = true;
                httpRequest.Headers.Add("Authorization", token);
                //httpRequest.Headers.Add("Authorization", "Basic dchZ2VudDM6cGFdGVzC5zc3dvmQ=");

                using (var httpResponse = httpRequest.GetResponse() as HttpWebResponse)
                {
                    var responseStream = httpResponse.GetResponseStream();
                    var httpCode = httpResponse.StatusCode;

                    if (responseStream != null)
                    {
                        using (var strReader = new StreamReader(responseStream))
                        {
                            responseResult = strReader.ReadToEnd();
                            //var bb = JArray.Parse(responseResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
            }

            return responseResult;
        }

        public static string PostEvent(string apiUrl, string inputJson, string method = "POST", string token = "")
        {
            string responseResult = string.Empty;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiUrl));
                var cacheCredentials = new CredentialCache();
                cacheCredentials.Add(new Uri(apiUrl), "Negotiate", CredentialCache.DefaultNetworkCredentials);
                 
                httpRequest.Accept = "application/json";
                httpRequest.Credentials = cacheCredentials;
                httpRequest.PreAuthenticate = true;

                httpRequest.ContentType = "application/json";
                httpRequest.Method = method;

                byte[] bytes = Encoding.UTF8.GetBytes(inputJson);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }

                using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    using (Stream stream = httpResponse.GetResponseStream())
                    {
                        responseResult = (new StreamReader(stream)).ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
            }

            return responseResult;
        }

        public static string PostEvent1(string uRL, string postData, string token = "")
        {
            string responseResult = string.Empty;
            //uRL="http://mysite.com"; 

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var httpRequest = WebRequest.CreateHttp(uRL);
                var cacheCredentials = new CredentialCache();
                cacheCredentials.Add(new Uri(uRL), "Negotiate", CredentialCache.DefaultNetworkCredentials);
                //cacheCredentials.Add(new Uri(uRL), "Basic", new NetworkCredential("user", "pass"));

                httpRequest.Method = "POST";
                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.Credentials = cacheCredentials;
                httpRequest.PreAuthenticate = true;
                //httpRequest.Headers.Add("Authorization", token);
                //httpRequest.Headers.Add("Authorization", "Basic dchZ2VudDM6cGFdGVzC5zc3dvmQ=");

                var serializer = new JavaScriptSerializer();
                var data = new
                {
                    Name = "testing"
                };
                var json = serializer.Serialize(data);

                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                httpRequest.ContentLength = byteArray.Length;

                //Stream dataStream = httpRequest.GetRequestStream();
                //dataStream.Write(byteArray, 0, byteArray.Length);
                //dataStream.Close();

                using (Stream sendStream = httpRequest.GetRequestStream())
                {
                    sendStream.Write(byteArray, 0, byteArray.Length);
                }

                using (var httpResponse = httpRequest.GetResponse() as HttpWebResponse)
                {
                    var responseStream = httpResponse.GetResponseStream();
                    var httpCode = httpResponse.StatusCode;

                    if (responseStream != null)
                    {
                        using (var strReader = new StreamReader(responseStream))
                        {
                            responseResult = strReader.ReadToEnd();
                            var bb = JArray.Parse(responseResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
            }

            return responseResult;
        }

    }
} 