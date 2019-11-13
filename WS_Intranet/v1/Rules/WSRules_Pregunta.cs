using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using WS_Intranet.v1.Entities.Resultados;
using _Model.Entities;
using _Rules;
using WS_Intranet.v0.Rules;
using WS_Intranet.v0;
using System.Collections.Generic;

namespace WS_Intranet.v1.Rules
{
    public class WSRules_Pregunta : WSRules_Base
    {
        private readonly WSRules_Pregunta rulesBase;

        public WSRules_Pregunta(UsuarioLogueado data)
            : base(data)
        {
        }

        public Result<v1.Entities.Resultados.ResultadoWS_Paginador<v1.Entities.Resultados.ResultadoWS_Pregunta>> BuscarPaginado(v1.Entities.Consultas.Consulta_PreguntaPaginada consulta)
        {
            var resultado = new Result<v1.Entities.Resultados.ResultadoWS_Paginador<v1.Entities.Resultados.ResultadoWS_Pregunta>>();

            //Busco la info
            var resultadoData = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).GetPaginado(consulta.Convertir());
            if (!resultadoData.Ok)
            {
                resultado.Error = resultadoData.Error;
                return resultado;
            }

            //Resultado
            resultado.Return = new ResultadoWS_Paginador<ResultadoWS_Pregunta>();
            resultado.Return.Count = resultadoData.Return.Count;
            resultado.Return.CantidadPaginas = resultadoData.Return.CantidadPaginas;
            resultado.Return.OrderBy = resultadoData.Return.OrderBy;
            resultado.Return.PaginaActual = resultadoData.Return.PaginaActual;
            resultado.Return.TamañoPagina = resultadoData.Return.TamañoPagina;
            resultado.Return.Data = v1.Entities.Resultados.ResultadoWS_Pregunta.ToList(resultadoData.Return.Data);
            return resultado;
        }

        public Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>> Buscar(v1.Entities.Consultas.Consulta_PreguntaPaginada consulta)
        {
            var resultado = new Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>>();

            //Busco la info
            var resultadoData = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).Get(consulta.Convertir());
            if (!resultadoData.Ok)
            {
                resultado.Error = resultadoData.Error;
                return resultado;
            }

            resultado.Return= v1.Entities.Resultados.ResultadoWS_Pregunta.ToList(resultadoData.Return);
            return resultado;
        }

        public Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>> BuscarPublico(string busqueda)
        {
            var resultado = new Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>>();

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).GetPublico(busqueda);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            //Convierto
            resultado.Return = ResultadoWS_Pregunta.ToList(resultadoQuery.Return);
            return resultado;
        }
        public Result<v1.Entities.Resultados.ResultadoWS_Pregunta> GetDetalle(int id)
        {
            var resultado = new Result<v1.Entities.Resultados.ResultadoWS_Pregunta>();

            //Sumo el contador
            var resultadpUpdate = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).SumarContador(id);
            if (!resultadpUpdate.Ok)
            {
                resultado.Error = resultadpUpdate.Error;
                return resultado;
            }

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).GetById(id);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            //Valido
            var entity = resultadoQuery.Return;
            if (entity == null || entity.FechaBaja != null)
            {
                resultado.Error = "La inscripción no existe o está dada de baja";
                return resultado;
            }


            //Convierto
            resultado.Return = new ResultadoWS_Pregunta(resultadoQuery.Return);
            return resultado;
        }
        public Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>> GetAll()
        {
            var resultado = new Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>>();

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).GetAll(false);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            //Convierto
            resultado.Return = ResultadoWS_Pregunta.ToList(resultadoQuery.Return);
            return resultado;
        }
        public Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>> Top(int? cantidad, string app)
        {
            var resultado = new Result<List<v1.Entities.Resultados.ResultadoWS_Pregunta>>();

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).Top(cantidad,app);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            //Convierto
            resultado.Return = ResultadoWS_Pregunta.ToList(resultadoQuery.Return);
            return resultado;
        }
        public Result<v1.Entities.Resultados.ResultadoWS_Pregunta> Insertar(v1.Entities.Comandos.ComandoWS_PreguntaNueva comando)
        {
            var resultado = new Result<v1.Entities.Resultados.ResultadoWS_Pregunta>();

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).Insertar(comando.Convertir());
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            //Convierto
            resultado.Return = new ResultadoWS_Pregunta(resultadoQuery.Return);
            return resultado;
        }
        public Result<v1.Entities.Resultados.ResultadoWS_Pregunta> Actualizar(v1.Entities.Comandos.ComandoWS_PreguntaActualizar comando)
        {
            var resultado = new Result<v1.Entities.Resultados.ResultadoWS_Pregunta>();

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).Actualizar(comando.Convertir());
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            //Convierto
            resultado.Return = new ResultadoWS_Pregunta(resultadoQuery.Return);
            return resultado;
        }
        public Result<bool> Borrar(int id)
        {
            var resultado = new Result<bool>();

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Pregunta(getUsuarioLogueado()).Borrar(id);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            //Convierto
            resultado.Return = true;
            return resultado;
        }
        public Result<List<v1.Entities.Resultados.ResultadoWS_Aplicacion>> GetAplicaciones()
        {
            var resultado = new Result<List<v1.Entities.Resultados.ResultadoWS_Aplicacion>>();

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Aplicacion(getUsuarioLogueado()).GetAll(false);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            //Convierto
            resultado.Return = ResultadoWS_Aplicacion.ToList(resultadoQuery.Return);
            return resultado;
        }
        public Result<List<v1.Entities.Resultados.ResultadoWS_Tema>> GetTemas(int? idAplicacion)
        {
            var resultado = new Result<List<v1.Entities.Resultados.ResultadoWS_Tema>>();

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Tema(getUsuarioLogueado()).GetAll(false);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }
            //Filtro 
            if (idAplicacion.HasValue)
            {
                resultadoQuery.Return = resultadoQuery.Return.Where(x => x.Aplicacion!=null && x.Aplicacion.Id == idAplicacion).ToList();
            }

            //Convierto
            resultado.Return = ResultadoWS_Tema.ToList(resultadoQuery.Return);
            return resultado;
        }

        public Result<List<v1.Entities.Resultados.ResultadoWS_Aplicacion>> GetAplicacionesEnCascada()
        {
            var resultado = new Result<List<v1.Entities.Resultados.ResultadoWS_Aplicacion>>();

            //Busco 
            var resultadoQuery = new _Rules.Rules.Rules_Aplicacion(getUsuarioLogueado()).GetAll(false);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }
            //Convierto
            var aplicaciones = ResultadoWS_Aplicacion.ToList(resultadoQuery.Return);

            //Busco sus Temas
            foreach (var app in aplicaciones)
            {
                var consultaTemas = GetTemas(app.Id);
                if (!consultaTemas.Ok)
                {
                    resultado.Error = consultaTemas.Error;
                    return resultado;
                }
                var temas = consultaTemas.Return;
                
                foreach(var tema in temas)
                {
                    //busco sus preguntas
                    var consultaPreguntas = Buscar(new v1.Entities.Consultas.Consulta_PreguntaPaginada() 
                    { 
                        AplicacionId = tema.IdAplicacion, 
                        Tema = tema.Id.ToString() 
                    });
                    if (!consultaPreguntas.Ok)
                    {
                        resultado.Error = consultaPreguntas.Error;
                        return resultado;
                    }
                    var preguntas = consultaPreguntas.Return;
                    tema.Items = preguntas;
                }
                app.Temas = temas;
            }

            resultado.Return = aplicaciones.Where(x=>x.Temas.Count>0).ToList();
            return resultado;
        }
    }
}