using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WS_Internet.v0;
using WS_Internet.v0.Controllers;
using WS_Internet.v0.Controllers.FilterAttributes;
using WS_Internet.v1.Controllers.FilterAttributes;
using WS_Internet.v1.Entities.Comandos;
using WS_Internet.v1.Entities.Consultas;
using WS_Internet.v1.Entities.Resultados;

namespace WS_Intranet.v1.Controllers
{
    [RoutePrefix("v1/Pregunta")]
    public class Pregunta_v1Controller : ApiController
    {

        [HttpPut]
        [Route("BuscarPaginado")]
        public Result<ResultadoWS_Paginador<ResultadoWS_Pregunta>> BuscarPaginado(Consulta_PreguntaPaginada consulta)
        {
            return RestCall.Call<ResultadoWS_Paginador<ResultadoWS_Pregunta>>(Request, consulta);
        }

        [HttpGet]
        [Route("BuscarPublico")]
        public Result<List<ResultadoWS_Pregunta>> BuscarPublico(string busqueda)
        {
            return RestCall.Call<List<ResultadoWS_Pregunta>>(Request);
        }

        [HttpGet]
        [Route("GetDetalle")]
        public Result<ResultadoWS_Pregunta> GetDetalle(int id)
        {
            return RestCall.Call<ResultadoWS_Pregunta>(Request);
        }

        [HttpGet]
        [Route("GetAll")]
        public Result<List<ResultadoWS_Pregunta>> GetAll()
        {
            return RestCall.Call<List<ResultadoWS_Pregunta>>(Request);
        }
        [HttpPut]
        [Route("Buscar")]
        public Result<List<ResultadoWS_Pregunta>> Buscar(Consulta_PreguntaPaginada consulta)
        {
            return RestCall.Call<List<ResultadoWS_Pregunta>>(Request, consulta);
        }

        [HttpGet]
        [Route("Top")]
        public Result<List<ResultadoWS_Pregunta>> Top(int? cantidad = null, string app = null)
        {
            return RestCall.Call<List<ResultadoWS_Pregunta>>(Request);
        }

        [HttpPost]
        [ConToken]
        [EsOperador]
        [Route("Insertar")]
        public Result<ResultadoWS_Pregunta> Insertar(ComandoWS_PreguntaNueva comando)
        {
            return RestCall.Call<ResultadoWS_Pregunta>(Request, comando);
        }

        [HttpPut]
        [ConToken]
        [EsOperador]
        [Route("Actualizar")]
        public Result<ResultadoWS_Pregunta> Actualizar(ComandoWS_PreguntaActualizar comando)
        {
            return RestCall.Call<ResultadoWS_Pregunta>(Request, comando);
        }


        [HttpDelete]
        [ConToken]
        [EsOperador]
        [Route("Borrar")]
        public Result<bool> Borrar(int id)
        {
            return RestCall.Call<bool>(Request);
        }

        [HttpGet]
        [Route("GetAplicaciones")]
        public Result<List<ResultadoWS_Aplicacion>> GetAplicaciones()
        {
            return RestCall.Call<List<ResultadoWS_Aplicacion>>(Request);
        }

        [HttpGet]
        [Route("GetAplicacionesEnCascada")]
        public Result<List<ResultadoWS_Aplicacion>> GetAplicacionesEnCascada()
        {
            return RestCall.Call<List<ResultadoWS_Aplicacion>>(Request);
        }

        [HttpGet]
        [Route("GetTemas")]
        public Result<List<ResultadoWS_Tema>> GetTemas(int? idAplicacion = null)
        {
            return RestCall.Call<List<ResultadoWS_Tema>>(Request);
        }
    }
}