using System;
using System.Linq;
using _Model;
using _Model.Entities;
using NHibernate;
using System.Collections.Generic;
using _Model.Resultados;
using NHibernate.Criterion.Lambda;
using NHibernate.Criterion;
using _Model.Consultas;

namespace _DAO.DAO
{
    public class DAO_Pregunta : BaseDAO<Pregunta>
    {
        private static DAO_Pregunta instance;

        public static DAO_Pregunta Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAO_Pregunta();
                }
                return instance;
            }
        }

        Dictionary<string, bool> joins = new Dictionary<string, bool>();
        Aplicacion joinAplicacion;
        Tema joinTema;

        private void ClearJoins()
        {
            joinAplicacion = null;
            joinTema = null;
            joins = new Dictionary<string, bool>();
        }

        private void JoinAplicacion(IQueryOver<Pregunta, Pregunta> query)
        {
            if (!joins.ContainsKey("aplicacion"))
            {
                query.Left.JoinAlias(x => x.Aplicacion, () => joinAplicacion);
                joins["aplicacion"] = true;
            }
        }

        private void JoinTema(IQueryOver<Pregunta, Pregunta> query)
        {
            if (!joins.ContainsKey("tema"))
            {
                query.Left.JoinAlias(x => x.Tema, () => joinTema);
                joins["tema"] = true;
            }
        }

        public IQueryOver<Pregunta, Pregunta> GetQuery(_Model.Consultas.Consulta_Pregunta consulta)
        {
            ClearJoins();

            var query = GetSession().QueryOver<Pregunta>();


                //Aplicacion
                if (!string.IsNullOrEmpty(consulta.Aplicacion))
                {
                    JoinAplicacion(query);
                    foreach (var palabra in consulta.Aplicacion.Split(' '))
                    {
                        var p = palabra.Trim();
                        query.Where(() => ((joinAplicacion.Nombre != null && joinAplicacion.Nombre.IsLike(p, MatchMode.Anywhere))));
                    }
                }

                //Id Aplicacion 
                if (consulta.AplicacionId.HasValue)
                {
                    JoinAplicacion(query);
                    query.Where(() => joinAplicacion.Id != null && joinAplicacion.Id == consulta.AplicacionId.Value);
                }

                //Identificador Aplicacion 
                if (!string.IsNullOrEmpty(consulta.AplicacionIdentificador))
                {
                    JoinAplicacion(query);
                    foreach (var palabra in consulta.AplicacionIdentificador.Split(' '))
                    {
                        var p = palabra.Trim();
                        query.Where(() => ((joinAplicacion.Identificador != null && joinAplicacion.Identificador.IsLike(p, MatchMode.Anywhere))));
                    }
                }

                //Tema
                if (!string.IsNullOrEmpty(consulta.Tema))
                {
                    JoinTema(query);
                    foreach (var palabra in consulta.Tema.Split(' '))
                    {
                        var p = Convert.ToInt32(palabra.Trim());
                        query.Where(() => (joinTema.Id != null && joinTema.Id==p));
                    }
                }

                //Titulo
                if (!string.IsNullOrEmpty(consulta.Titulo))
                {
                    foreach (var palabra in consulta.Titulo.Split(' '))
                    {
                        var p = palabra.Trim();
                        query.Where(x => x.Titulo.IsLike(p, MatchMode.Anywhere));
                    }
                }

                //Dados de baja
                if (consulta.DadosDeBaja.HasValue)
                {
                    if (consulta.DadosDeBaja.Value)
                    {
                        query.Where(x => x.FechaBaja != null);
                    }
                    else
                    {
                        query.Where(x => x.FechaBaja == null);
                    }
                }
              
            return query;
        }

        public Resultado<List<Pregunta>> Get(_Model.Consultas.Consulta_Pregunta consulta)
        {
            var resultado = new Resultado<List<Pregunta>>();

            try
            {
                var query = GetQuery(consulta);
                resultado.Return = query.List().ToList();
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }
            return resultado;
        }

        public Resultado<Resultado_Paginador<Pregunta>> GetPaginado(_Model.Consultas.Consulta_PreguntaPaginada consulta)
        {
            var resultado = new Resultado<Resultado_Paginador<Pregunta>>();

            try
            {
                int tamaño = consulta.TamañoPagina;
                if (tamaño == 0)
                {
                    resultado.Error = "Tamaño de página inválido";
                    return resultado;
                }

                if (!Enum.IsDefined(typeof(_Model.Enums.PreguntaOrderBy), consulta.OrderBy))
                {
                    resultado.Error = "Debe enviar el ordenamiento deseado";
                    return resultado;
                }

                int pagina = consulta.Pagina;

                int count = GetQuery(consulta).RowCount();
                int cantidadPaginas = (int)Math.Ceiling((double)count / consulta.TamañoPagina);

                if (count != 0)
                {
                    if (pagina > cantidadPaginas || pagina < 0)
                    {
                        resultado.Error = "Página indicada inválida";
                        return resultado;
                    }
                }

                var query = GetQuery(consulta);

                switch (consulta.OrderBy)
                {
                    case Enums.PreguntaOrderBy.Id:
                        {
                            if (consulta.OrderByAsc)
                            {
                                query = query
                                    .OrderBy(x => x.Id).Asc;
                            }
                            else
                            {
                                query = query
                                    .OrderBy(x => x.Id).Desc;
                            }

                        } break;

                    case Enums.PreguntaOrderBy.AplicacionNombre:
                        {
                            JoinAplicacion(query);
                            if (consulta.OrderByAsc)
                            {
                                query = query
                                    .OrderBy(() => joinAplicacion.Nombre).Asc
                                    .ThenBy(x => x.Id).Asc;
                            }
                            else
                            {
                                query = query
                                    .OrderBy(() => joinAplicacion.Nombre).Desc
                                    .ThenBy(x => x.Id).Desc;
                            }

                        } break;

                    case Enums.PreguntaOrderBy.TemaNombre:
                        {
                            JoinTema(query);
                            if (consulta.OrderByAsc)
                            {
                                query = query
                                    .OrderBy(() => joinTema.Nombre).Asc
                                    .ThenBy(x => x.Id).Asc;
                            }
                            else
                            {
                                query = query
                                    .OrderBy(() => joinTema.Nombre).Desc
                                    .ThenBy(x => x.Id).Desc;
                            }
                        } break;
                    case Enums.PreguntaOrderBy.Titulo:
                        {
                            if (consulta.OrderByAsc)
                            {
                                query = query
                                    .OrderBy((x) => x.Titulo).Asc
                                    .ThenBy(x => x.Id).Asc;

                            }
                            else
                            {
                                query = query
                                    .OrderBy((x) => x.Titulo).Desc
                                    .ThenBy(x => x.Id).Desc;
                            }
                        } break;
                    case Enums.PreguntaOrderBy.Contador:
                        {
                            if (consulta.OrderByAsc)
                            {
                                query = query
                                    .OrderBy((x) => x.Contador).Asc
                                    .ThenBy(x => x.Id).Asc;
                            }
                            else
                            {
                                query = query
                                    .OrderBy((x) => x.Contador).Desc
                                    .ThenBy(x => x.Id).Desc;
                            }
                        } break;
                }
                List<Pregunta> items = new List<Pregunta>();

                items = query.Take(tamaño).Skip(pagina * tamaño).List().ToList();

                resultado.Return = new Resultado_Paginador<Pregunta>();
                resultado.Return.Count = count;
                resultado.Return.Data = items;
                resultado.Return.PaginaActual = consulta.Pagina;
                resultado.Return.TamañoPagina = consulta.TamañoPagina;
                resultado.Return.CantidadPaginas = cantidadPaginas;

                return resultado;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }
            return resultado;

        }

        public Resultado<int> GetCantidad(_Model.Consultas.Consulta_Pregunta consulta)
        {
            var resultado = new Resultado<int>();

            try
            {
                var query = GetQuery(consulta);
                resultado.Return = query.RowCount();
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }
            return resultado;

        }
        
    }
}