﻿using _Rules;
using _Rules.Rules;
using System;
using System.Linq;
using System.Web.Http;

namespace WS_Intranet.v0.Controllers
{
    public class _Control : ApiController
    {
        protected string GetToken()
        {
            return Request.Headers.GetValues("--Token").First();
        }

        public Result<UsuarioLogueado> GetUsuarioLogeado()
        {
            var token = GetToken();
            var resultado = new Result<UsuarioLogueado>();

            try
            {
                var resultadoIdByToken = new Rules_MuniOnlineUsuario(null).GetIdByToken(token);
                if (!resultadoIdByToken.Ok)
                {
                    resultado.Error = resultadoIdByToken.Error;
                    return resultado;
                }

                var usuarioLogeado = new UsuarioLogueado();

                usuarioLogeado.Id = resultadoIdByToken.Return;
                usuarioLogeado.Token = token;

                resultado.Return = usuarioLogeado;
            }
            catch (Exception)
            {
                resultado.SetError();
            }

            return resultado;
        }

        public Result<bool> ValidarToken()
        {
            var token = GetToken();
            var resultado = new Result<bool>();

            try
            {
                var resultadoValidarToken = new Rules_MuniOnlineUsuario(null).ValidarToken(token);
                if (!resultadoValidarToken.Ok)
                {
                    resultado.Error = resultadoValidarToken.Error;
                    return resultado;
                }

                resultado.Return = resultadoValidarToken.Return;
            }
            catch (Exception)
            {
                resultado.SetError();
            }

            return resultado;
        }
    }
}