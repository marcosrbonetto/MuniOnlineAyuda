using System;
using System.Linq;
using WS_Intranet.v1.Entities.Resultados;
using _Rules;
using WS_Intranet.v0.Rules;
using WS_Intranet.v0;
using _Rules.Rules;
using System.Collections.Generic;
using WS_Intranet.v1.Entities.Comandos;

namespace WS_Intranet.v1.Rules
{
    public class WSRules_MuniOnlineUsuario : WSRules_Base
    {
        private readonly Rules_MuniOnlineUsuario rules;

        public WSRules_MuniOnlineUsuario(UsuarioLogueado data)
            : base(data)
        {
            rules = new Rules_MuniOnlineUsuario(data);
        }


        public Result<string> IniciarSesion(ComandoWS_IniciarSesion comando)
        {
            var resultado = new Result<string>();

            var resultadoIniciarSesion = rules.IniciarSesion(comando.ToRules());
            if (!resultadoIniciarSesion.Ok)
            {
                resultado.Error = resultadoIniciarSesion.Error;
                return resultado;
            }

            resultado.Return = resultadoIniciarSesion.Return;
            return resultado;
        }

        public Result<bool> EsOperador()
        {
            var resultado = new Result<bool>();

            var resultadoIniciarSesion = rules.EsOperador(getUsuarioLogueado().Token);
            if (!resultadoIniciarSesion.Ok)
            {
                resultado.Error = resultadoIniciarSesion.Error;
                return resultado;
            }

            resultado.Return = resultadoIniciarSesion.Return;
            return resultado;
        }

        public Result<List<ResultadoWS_RolMuniOnline>> GetRol()
        {
            var resultado = new Result<List<ResultadoWS_RolMuniOnline>>();

            var resultadoIniciarSesion = rules.GetRol(getUsuarioLogueado().Token);
            if (!resultadoIniciarSesion.Ok)
            {
                resultado.Error = resultadoIniciarSesion.Error;
                return resultado;
            }

            resultado.Return = ResultadoWS_RolMuniOnline.ToList(resultadoIniciarSesion.Return);
            return resultado;
        }

        public Result<bool> CerrarSesion()
        {
            var resultado = new Result<bool>();

            var resultadoCerrarSesion = rules.CerrarSesion();
            if (!resultadoCerrarSesion.Ok)
            {
                resultado.Error = resultadoCerrarSesion.Error;
                return resultado;
            }

            resultado.Return = resultadoCerrarSesion.Return;
            return resultado;
        }

        public Result<int> GetIdUsuario()
        {
            var resultado = new Result<int>();

            var resultadoIdByToken = rules.GetIdByToken();
            if (!resultadoIdByToken.Ok)
            {
                resultado.Error = resultadoIdByToken.Error;
                return resultado;
            }

            resultado.Return = resultadoIdByToken.Return;
            return resultado;
        }

        public Result<ResultadoWS_MuniOnlineUsuario> GetUsuario()
        {
            var resultado = new Result<ResultadoWS_MuniOnlineUsuario>();

            var resultadoByToken = rules.GetByToken();
            if (!resultadoByToken.Ok)
            {
                resultado.Error = resultadoByToken.Error;
                return resultado;
            }

            resultado.Return = new ResultadoWS_MuniOnlineUsuario(resultadoByToken.Return);
            return resultado;
        }

        public Result<bool> ValidarToken()
        {
            var resultado = new Result<bool>();

            var resultadoValidarToken = rules.ValidarToken();
            if (!resultadoValidarToken.Ok)
            {
                resultado.Return = false;
                return resultado;
            }

            resultado.Return = resultadoValidarToken.Return;
            return resultado;
        }

        public Result<bool> ValidadoRenaper()
        {
            var resultado = new Result<bool>();

            var resultadoValidadoRenaper = rules.ValidadoRenaper();
            if (!resultadoValidadoRenaper.Ok)
            {
                resultado.Error = resultadoValidadoRenaper.Error;
                return resultado;
            }

            resultado.Return = resultadoValidadoRenaper.Return;
            return resultado;
        }

        public Result<bool> AplicacionBloqueada()
        {
            var resultado = new Result<bool>();

            var resultadoAplicacionBloqueada = rules.AplicacionBloqueada();
            if (!resultadoAplicacionBloqueada.Ok)
            {
                resultado.Error = resultadoAplicacionBloqueada.Error;
                return resultado;
            }

            resultado.Return = resultadoAplicacionBloqueada.Return;
            return resultado;
        }
    }
}