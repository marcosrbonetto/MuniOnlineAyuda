using _Model;
using _Model.Entities;
using _Model.Resultados;
using System;
using System.Configuration;
using System.Linq;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using Newtonsoft.Json.Linq;
using _Model.Resultados;
using _Model.Comandos;
using System.Collections.Generic;

namespace _Rules.Rules.WSs
{
    public class BaseWSRules
    {
        private readonly UsuarioLogueado data;

        public BaseWSRules(UsuarioLogueado data)
        {
            this.data = data;
        }

        protected UsuarioLogueado GetUsuarioLogueado()
        {
            return data;
        }
    }
    public class ApiRestCall
    {
        public static RestClient ApiClient(string url)
        {
            var client = new RestClient(url);
            return client;
        }

        public static RestRequest ApiRequest(Method method)
        {
            var request = new RestRequest(method);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            return request;
        }

        public static Resultado_ApiRest<T> CallGet<T>(string url, Dictionary<string, string> header = null)
        {
            return Call<T>(url, Method.GET, null, header);
        }

        public static Resultado_ApiRest<T> CallPost<T>(string url, object body = null, Dictionary<string, string> header = null)
        {
            return Call<T>(url, Method.POST, body, header);
        }

        public static Resultado_ApiRest<T> CallPut<T>(string url, object body = null, Dictionary<string, string> header = null)
        {
            return Call<T>(url, Method.PUT, body, header);
        }

        public static Resultado_ApiRest<T> Call<T>(string url, Method metodo, object body = null, Dictionary<string, string> header = null)
        {
            try
            {
                var client = ApiClient(url);
                var request = ApiRequest(metodo);

                if (client == null || request == null)
                {
                    return ErrorDefault<T>();
                }

                // Header
                if (header != null)
                {
                    foreach (var dicc in header)
                    {
                        request.AddHeader(dicc.Key, dicc.Value);
                    }
                }

                // Body
                if (body != null)
                {
                    request.AddBody(body);
                }

                IRestResponse response = client.Execute(request).Result;
                var json = JObject.Parse(response.Content);
                return json.ToObject<Resultado_ApiRest<T>>();

            }
            catch (Exception e)
            {
                return ErrorDefault<T>();
            }
        }

        private static Resultado_ApiRest<T> ErrorDefault<T>()
        {
            var resultado = new Resultado_ApiRest<T>();
            resultado.Error = Mensajes.ERROR_PROCESANDO_SOLICITUD;
            return resultado;
        }
    }
}