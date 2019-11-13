using System;
using System.Linq;
using _DAO.DAO;
using _Model;
using _Model.Entities;
using System.Configuration;
using System.Collections.Generic;
using _Rules.Rules.WSs;
using _Model.Comandos;
using _Model.Resultados;

namespace _Rules.Rules
{
    public class Rules_MuniOnlineUsuario : BaseWSRules
    {
   

        private readonly string URL = ConfigurationManager.AppSettings["URL_WS_VECINO_VIRTUAL"];
        private readonly string APP = ConfigurationManager.AppSettings["APP_KEY_VECINO_VIRTUAL"];

        public Rules_MuniOnlineUsuario(UsuarioLogueado data)
            : base(data)
        {
        }

        public Resultado<string> IniciarSesion(Comando_IniciarSesion comando)
        {
            var resultado = new Resultado<string>();

            try
            {
                var diccionarioHeader = new Dictionary<string, string>();
                diccionarioHeader.Add("--Username", comando.Username);
                diccionarioHeader.Add("--Password", comando.Password);

                var url = URL + "/v2/Usuario/IniciarSesion";
                var resultadoIniciarSesion = ApiRestCall.CallGet<string>(url, diccionarioHeader);

                if (!resultadoIniciarSesion.Ok)
                {
                    resultado.Error = resultadoIniciarSesion.Error;
                    return resultado;
                }

                resultado.Return = resultadoIniciarSesion.Return;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }

            return resultado;
        }

        public Resultado<bool> CerrarSesion()
        {
            return CerrarSesion(GetUsuarioLogueado().Token);
        }

        public Resultado<bool> CerrarSesion(string token)
        {
            var resultado = new Resultado<bool>();

            try
            {
                var url = URL + "/v1/Usuario/CerrarSesion?token=" + token;
                var resultadoCerrarSesion = ApiRestCall.CallPut<bool?>(url);
                if (!resultadoCerrarSesion.Ok)
                {
                    resultado.Error = resultadoCerrarSesion.Error;
                    return resultado;
                }

                resultado.Return = true;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }

            return resultado;
        }

        public Resultado<bool> EsOperador(string token)
        {
            var resultado = new Resultado<bool>();

            var resultadoConsulta = GetRol(token);
            if (!resultadoConsulta.Ok)
            {
                resultado.Error = Mensajes.ERROR_PROCESANDO_SOLICITUD;
                return resultado;
            }

            resultado.Return = resultadoConsulta.Return.Any(x => x.Id == Constantes.OPERADOR);

            return resultado;
        }

        public Resultado<List<Resultado_RolMuniOnline>> GetRol(string token)
        {
            var resultado = new Resultado<List<Resultado_RolMuniOnline>>();

            try
            {
                var diccionarioHeader = new Dictionary<string, string>();
                diccionarioHeader.Add("--Token", token);
                diccionarioHeader.Add("--App", this.APP);

                var url = URL + "/v2/Usuario/Roles";
                var resultadoGetRoles = ApiRestCall.CallGet<List<Resultado_RolMuniOnline>>(url, diccionarioHeader);

                if (!resultadoGetRoles.Ok)
                {
                    resultado.Error = resultadoGetRoles.Error;
                    return resultado;
                }

                resultado.Return = resultadoGetRoles.Return;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }

            return resultado;
        }

        public Resultado<int> GetIdByToken()
        {
            return GetIdByToken(GetUsuarioLogueado().Token);
        }

        public Resultado<int> GetIdByToken(string token)
        {
            var resultado = new Resultado<int>();

            try
            {
                var url = URL + "/v1/Usuario/GetId?token=" + token;

                var resultadoId = ApiRestCall.CallGet<int?>(url);
                if (!resultadoId.Ok)
                {
                    resultado.Error = resultadoId.Error;
                    return resultado;
                }

                resultado.Return = resultadoId.Return.Value;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }

            return resultado;
        }

        public Resultado<Resultado_Usuario> GetByToken()
        {
            return GetByToken(GetUsuarioLogueado().Token);
        }

        public Resultado<Resultado_Usuario> GetByToken(string token)
        {
            var resultado = new Resultado<Resultado_Usuario>();

            try
            {
                var diccionarioHeader = new Dictionary<string, string>();
                diccionarioHeader.Add("--Token", token);

                var url = URL + "/v3/Usuario";
                var resultadoUsuario = ApiRestCall.CallGet<Resultado_Usuario>(url, diccionarioHeader);
                if (!resultadoUsuario.Ok)
                {
                    resultado.Error = resultadoUsuario.Error;
                    return resultado;
                }
                resultadoUsuario.Return.Token = token;
                resultado.Return = resultadoUsuario.Return;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }

            return resultado;
        }

        public Resultado<bool> ValidarToken()
        {
            return ValidarToken(GetUsuarioLogueado().Token);
        }

        public Resultado<bool> ValidarToken(string token)
        {
            var resultado = new Resultado<bool>();

            try
            {
                var url = URL + "/v1/Usuario/ValidarToken?token=" + token;

                var resultadoValidarToken = ApiRestCall.CallGet<bool?>(url);
                if (!resultadoValidarToken.Ok)
                {
                    resultado.Error = resultadoValidarToken.Error;
                    return resultado;
                }

                resultado.Return = resultadoValidarToken.Return.Value;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }

            return resultado;
        }

        public Resultado<bool> ValidadoRenaper()
        {
            return ValidadoRenaper(GetUsuarioLogueado().Token);
        }

        public Resultado<bool> ValidadoRenaper(string token)
        {
            var resultado = new Resultado<bool>();

            try
            {
                var url = URL + "/v1/Usuario/ValidadoRenaper?token=" + token;
                var resultadoValidadoRenaper = ApiRestCall.Call<bool?>(url, RestSharp.Portable.Method.GET);
                if (!resultadoValidadoRenaper.Ok)
                {
                    resultado.Error = resultadoValidadoRenaper.Error;
                    return resultado;
                }

                resultado.Return = resultadoValidadoRenaper.Return.Value;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }

            return resultado;
        }

        public Resultado<bool> AplicacionBloqueada()
        {
            return AplicacionBloqueada(GetUsuarioLogueado().Token);
        }

        public Resultado<bool> AplicacionBloqueada(string token)
        {
            var resultado = new Resultado<bool>();
            try
            {
                var url = URL + "/v1/Usuario/AplicacionBloqueada?token=" + token + "&app=" + APP;
                var resultadoAplicacionBloqueada = ApiRestCall.Call<bool>(url, RestSharp.Portable.Method.GET);
                if (!resultadoAplicacionBloqueada.Ok)
                {
                    resultado.Error = resultadoAplicacionBloqueada.Error;
                    return resultado;
                }

                resultado.Return = resultadoAplicacionBloqueada.Return;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }

            return resultado;
        }
    }
}