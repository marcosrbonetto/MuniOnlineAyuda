using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WS_Intranet.v0;
using WS_Intranet.v0.Controllers;
using WS_Intranet.v0.Controllers.FilterAttributes;
using WS_Intranet.v1.Controllers.FilterAttributes;
using WS_Intranet.v1.Entities.Resultados;
using WS_Intranet.v1.Rules;

namespace WS_Intranet.v1.Controllers
{
    [RoutePrefix("v1/Pregunta")]
    public class Pregunta_v1Controller : _Control
    {

        [HttpPut]
        [Route("BuscarPaginado")]
        public Result<v1.Entities.Resultados.ResultadoWS_Paginador<v1.Entities.Resultados.ResultadoWS_Pregunta>> BuscarPaginado(v1.Entities.Consultas.Consulta_PreguntaPaginada consulta)
        {
            return new WSRules_Pregunta(null).BuscarPaginado(consulta);
        }

        [HttpGet]
        [Route("BuscarPublico")]
        public Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>> BuscarPublico(string busqueda)
        {
            return new WSRules_Pregunta(null).BuscarPublico(busqueda);
        }

        [HttpGet]
        [Route("GetDetalle")]
        public Result<v1.Entities.Resultados.ResultadoWS_Pregunta> GetDetalle(int id)
        {
            return new WSRules_Pregunta(null).GetDetalle(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>> GetAll()
        {
            return new WSRules_Pregunta(null).GetAll();
        }
        [HttpPut]
        [Route("Buscar")]
        public Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>> Buscar(v1.Entities.Consultas.Consulta_PreguntaPaginada consulta)
        {
            return new WSRules_Pregunta(null).Buscar(consulta);
        }

        [HttpGet]
        [Route("Top")]
        public Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>> Top(int? cantidad = null, string app = null)
        {
            return new WSRules_Pregunta(null).Top(cantidad,app);
        }

        [HttpPost]
        [ConToken]
        [EsOperador]
        [Route("Insertar")]
        public Result<v1.Entities.Resultados.ResultadoWS_Pregunta> Insertar(v1.Entities.Comandos.ComandoWS_PreguntaNueva comando)
        {
            var usuarioLogeado = GetUsuarioLogeado();
            if (!usuarioLogeado.Ok)
            {
                var resultado = new Result<v1.Entities.Resultados.ResultadoWS_Pregunta>();
                resultado.Error = usuarioLogeado.Error;
                return resultado;
            }

            return new WSRules_Pregunta(usuarioLogeado.Return).Insertar(comando);
        }

        [HttpPut]
        [ConToken]
        [EsOperador]
        [Route("Actualizar")]
        public Result<v1.Entities.Resultados.ResultadoWS_Pregunta> Actualizar(v1.Entities.Comandos.ComandoWS_PreguntaActualizar comando)
        {
            var usuarioLogeado = GetUsuarioLogeado();
            if (!usuarioLogeado.Ok)
            {
                var resultado = new Result<v1.Entities.Resultados.ResultadoWS_Pregunta>();
                resultado.Error = usuarioLogeado.Error;
                return resultado;
            }

            return new WSRules_Pregunta(usuarioLogeado.Return).Actualizar(comando);
        }


        [HttpDelete]
        [ConToken]
        [EsOperador]
        [Route("Borrar")]
        public Result<bool> Borrar(int id)
        {
            var usuarioLogeado = GetUsuarioLogeado();
            if (!usuarioLogeado.Ok)
            {
                var resultado = new Result<bool>();
                resultado.Error = usuarioLogeado.Error;
                return resultado;
            }

            return new WSRules_Pregunta(usuarioLogeado.Return).Borrar(id);
        }

        [HttpGet]
        [Route("GetAplicaciones")]
        public Result<List<v1.Entities.Resultados.ResultadoWS_Aplicacion>> GetAplicaciones()
        {
            return new WSRules_Pregunta(null).GetAplicaciones();
        }

        [HttpGet]
        [Route("GetAplicacionesEnCascada")]
        public Result<List<v1.Entities.Resultados.ResultadoWS_Aplicacion>> GetAplicacionesEnCascada()
        {
            return new WSRules_Pregunta(null).GetAplicacionesEnCascada();
        }

        [HttpGet]
        [Route("GetTemas")]
        public Result<List<v1.Entities.Resultados.ResultadoWS_Tema>> GetTemas(int? idAplicacion = null)
        {
            return new WSRules_Pregunta(null).GetTemas(idAplicacion);
        }
    }
}