using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;
using WS_Intranet.v0.Controllers.FilterAttributes;

namespace WS_Intranet.v1.Controllers.FilterAttributes
{
    public class EsOperador : _Autorizacion
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            if (!actionContext.Request.Headers.Contains("--Token"))
            {
                actionContext.Response = Error(HttpStatusCode.OK, "Debe mandar su token de acceso");
                return;
            }

            var token = actionContext.Request.Headers.GetValues("--Token").FirstOrDefault();
            var resultadoId = new _Rules.Rules.Rules_MuniOnlineUsuario(null).GetIdByToken(token);
            if (!resultadoId.Ok)
            {
                actionContext.Response = Error(HttpStatusCode.OK, resultadoId.Error);
                return;
            }

            var resultadoOperador = new _Rules.Rules.Rules_MuniOnlineUsuario(null).EsOperador(token);
            if (!resultadoOperador.Ok)
            {
                actionContext.Response = Error(HttpStatusCode.OK, resultadoOperador.Error);
                return;
            }

            if (!resultadoOperador.Return)
            {
                actionContext.Response = Error(HttpStatusCode.OK, "No tiene el permiso necesario para realizar esta acción");
                return;
            }

        }
    }
}