using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace WS_Internet.v0
{
    public class RestCall
    {
        public static Result<T> Call<T>(HttpRequestMessage httpRequestMessage, object body = null)
        {
            try
            {
                var client = ApiClient(httpRequestMessage.RequestUri.PathAndQuery);
                var request = ApiRequest(httpRequestMessage.Method.Method);

                if (client == null || request == null)
                {
                    return ErrorDefault<T>();
                }

                // Headers
                if (httpRequestMessage.Headers != null)
                {
                    if (httpRequestMessage.Headers.Contains("Token"))
                    {
                        request.AddHeader("Token", httpRequestMessage.Headers.GetValues("Token").First());
                    }
                }

                // Body
                if (body != null)
                {
                    request.AddBody(body);
                }

                IRestResponse response = client.Execute(request).Result;
                var json = JObject.Parse(response.Content);
                return json.ToObject<Result<T>>();
            }
            catch (Exception e)
            {
                return ErrorDefault<T>();
            }
        }

        private static RestClient ApiClient(string url)
        {
            var directorios = ConfigurationManager.AppSettings["DIRECTORIO_PUBLICACION"].Split(';');

            foreach (var d in directorios)
            {
                if (url.StartsWith("/" + d))
                {
                    url = url.Replace(d + "/", "");
                }
            }

            return new RestClient(ConfigurationManager.AppSettings["URL_SERVER"] + url);
        }

        private static RestRequest ApiRequest(string method)
        {
            Method methodPortable;
            Enum.TryParse(method, true, out methodPortable);

            var request = new RestRequest(methodPortable);

            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            return request;
        }

        private static Result<T> ErrorDefault<T>()
        {
            var resultado = new Result<T>();
            resultado.Error = "Error procesando la solicitud";
            return resultado;
        }
    }
}