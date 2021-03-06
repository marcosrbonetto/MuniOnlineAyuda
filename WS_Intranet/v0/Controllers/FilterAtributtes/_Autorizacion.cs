﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace WS_Intranet.v0.Controllers.FilterAttributes
{
    public class _Autorizacion : AuthorizationFilterAttribute
    {
        protected HttpResponseMessage Error(HttpStatusCode code, string mensaje)
        {
            var resultado = new Result<string>();
            resultado.Error = mensaje;

            var requestMessage = JsonConvert.SerializeObject(resultado, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };

        }
    }
}