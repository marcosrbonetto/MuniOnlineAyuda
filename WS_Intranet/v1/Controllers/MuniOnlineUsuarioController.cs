using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WS_Intranet.v0;
using WS_Intranet.v0.Controllers;
using WS_Intranet.v0.Controllers.FilterAttributes;
using WS_Intranet.v1.Entities.Resultados;
using WS_Intranet.v1.Rules;

namespace WS_Intranet.v1.Controllers
{
    [RoutePrefix("v1/MuniOnlineUsuario")]
    public class MuniOnlineUsuario_v1Controller : _Control
    {

        [HttpPut]
        [Route("IniciarSesion")]
        public Result<string> IniciarSesion(v1.Entities.Comandos.ComandoWS_IniciarSesion comando)
        {
            return new v1.Rules.WSRules_MuniOnlineUsuario(null).IniciarSesion(comando);
        }

        [HttpPut]
        [ConToken]
        [Route("CerrarSesion")]
        public Result<bool> CerrarSesion()
        {
            var resultado = new Result<bool>();

            var usuarioLogeado = GetUsuarioLogeado();
            if (!usuarioLogeado.Ok)
            {
                resultado.Error = usuarioLogeado.Error;
                return resultado;
            }

            return new WSRules_MuniOnlineUsuario(usuarioLogeado.Return).CerrarSesion();
        }

        [HttpGet]
        [ConToken]
        [Route("ValidarToken")]
        public Result<bool> ValidarToken()
        {
            return base.ValidarToken();
        }

        [HttpGet]
        [ConToken]
        [Route("ValidadoRenaper")]
        public Result<bool> ValidadoRenaper()
        {
            var resultado = new Result<bool>();

            var usuarioLogeado = GetUsuarioLogeado();
            if (!usuarioLogeado.Ok)
            {
                resultado.Error = usuarioLogeado.Error;
                return resultado;
            }

            return new WSRules_MuniOnlineUsuario(usuarioLogeado.Return).ValidadoRenaper();
        }

        [HttpGet]
        [ConToken]
        [Route("EsOperador")]
        public Result<bool> EsOperador()
        {
            var resultado = new Result<bool>();

            var usuarioLogeado = GetUsuarioLogeado();
            if (!usuarioLogeado.Ok)
            {
                resultado.Error = usuarioLogeado.Error;
                return resultado;
            }

            return new WSRules_MuniOnlineUsuario(usuarioLogeado.Return).EsOperador();
        }

        [HttpGet]
        [ConToken]
        [Route("Usuario")]
        public Result<ResultadoWS_MuniOnlineUsuario> GetUsuario()
        {
            var resultado = new Result<ResultadoWS_MuniOnlineUsuario>();

            var usuarioLogeado = GetUsuarioLogeado();
            if (!usuarioLogeado.Ok)
            {
                resultado.Error = usuarioLogeado.Error;
                return resultado;
            }

            return new WSRules_MuniOnlineUsuario(usuarioLogeado.Return).GetUsuario();
        }

    }
}