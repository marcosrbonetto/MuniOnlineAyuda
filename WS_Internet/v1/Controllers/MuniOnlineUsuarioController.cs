using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WS_Internet.v0;
using WS_Internet.v0.Controllers;
using WS_Internet.v0.Controllers.FilterAttributes;
using WS_Internet.v1.Entities.Resultados;


namespace WS_Internet.v1.Controllers
{
    [RoutePrefix("v1/MuniOnlineUsuario")]
    public class MuniOnlineUsuario_v1Controller : ApiController
    {

        [HttpPut]
        [Route("IniciarSesion")]
        public Result<string> IniciarSesion(v1.Entities.Comandos.ComandoWS_IniciarSesion comando)
        {
            return RestCall.Call<string>(Request, comando);
        }

        [HttpPut]
        [ConToken]
        [Route("CerrarSesion")]
        public Result<bool> CerrarSesion()
        {
            return RestCall.Call<bool>(Request);
        }

        [HttpGet]
        [ConToken]
        [Route("ValidarToken")]
        public Result<bool> ValidarToken()
        {
            return RestCall.Call<bool>(Request);
        }

        [HttpGet]
        [ConToken]
        [Route("ValidadoRenaper")]
        public Result<bool> ValidadoRenaper()
        {
            return RestCall.Call<bool>(Request);
        }

        [HttpGet]
        [ConToken]
        [Route("EsOperador")]
        public Result<bool> EsOperador()
        {
            return RestCall.Call<bool>(Request);
        }

        [HttpGet]
        [ConToken]
        [Route("Usuario")]
        public Result<ResultadoWS_MuniOnlineUsuario> GetUsuario()
        {
            return RestCall.Call<ResultadoWS_MuniOnlineUsuario>(Request);
        }

    }
}