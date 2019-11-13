using System;
using System.Linq;
using System.Web.Http;
using WS_Internet.v0;
using WS_Internet.v1.Entities.Resultados;

namespace WS_Internet.v1.Controllers
{
    [RoutePrefix("v1/Usuario")]
    public class Usuario_v1Controller : ApiController
    {

        [HttpPut]
        [Route("IniciarSesion")]
        public Result<string> IniciarSesion(ComandoApp_IniciarSesion comando)
        {
            return RestCall.Call<string>(Request, comando);
        }

        [HttpPut]
        [Route("CerrarSesion")]
        public Result<bool> CerrarSesion()
        {
            return RestCall.Call<bool>(Request);
        }

        [HttpGet]
        [Route("IdUsuario")]
        public Result<int> GetIdUsuario()
        {
            return RestCall.Call<int>(Request);
        }

        [HttpGet]
        [Route("Usuario")]
        public Result<ResultadoApp_Usuario> GetUsuario()
        {
            return RestCall.Call<ResultadoApp_Usuario>(Request);
        }

        [HttpGet]
        [Route("ValidarToken")]
        public Result<bool> ValidarToken()
        {
            return RestCall.Call<bool>(Request);
        }

        [HttpGet]
        [Route("ValidadoRenaper")]
        public Result<bool> ValidadoRenaper()
        {
            return RestCall.Call<bool>(Request);
        }

        [HttpGet]
        [Route("AplicacionBloqueada")]
        public Result<bool> AplicacionBloqueada()
        {
            return RestCall.Call<bool>(Request);
        }
    }
}