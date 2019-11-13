using System;
using System.Linq;
using _DAO.DAO;
using _Model;
using _Model.Entities;
using System.Collections.Generic;

namespace _Rules.Rules
{
    public class Rules_Pregunta : BaseRules<Pregunta>
    {
        private readonly DAO_Pregunta dao;

        public Rules_Pregunta(UsuarioLogueado data)
            : base(data)
        {
            dao = DAO_Pregunta.Instance;
        }

        public Resultado<_Model.Resultados.Resultado_Paginador<Pregunta>> GetPaginado(_Model.Consultas.Consulta_PreguntaPaginada consulta)
        {
            return dao.GetPaginado(consulta);
        }

        public Resultado<List<Pregunta>> Get(_Model.Consultas.Consulta_Pregunta consulta)
        {
            return dao.Get(consulta);
        }        

        public Resultado<List<Pregunta>> GetPublico(string busqueda)
        {
            var resultado = new Resultado<List<Pregunta>>();

            var resultadoQuery = GetAll();
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            var items = new List<Busqueda>();
            var palabras = busqueda.Split(' ').Select(x => TransformarPalaba(x)).Where(x => x != "").ToList();
            foreach (var item in resultadoQuery.Return)
            {
                int contador = 0;

                //Titulo
                if (item.Titulo != null)
                {
                    var palabrasTitulo = item.Titulo.Split(' ').Select(x => TransformarPalaba(x)).Where(x => x != "").ToList();
                    contador += palabrasTitulo.Intersect(palabras).Count();
                }

                //Tags
                if (item.Tags != null)
                {
                    var palabrasTags = item.Tags.Split(' ').Select(x => TransformarPalaba(x)).Where(x => x != "").ToList();
                    contador += palabrasTags.Intersect(palabras).Count();
                }

                if (contador > 0)
                {
                    items.Add(new Busqueda() { Contador=contador, Pregunta= item});
                }
            }

            resultado.Return = items.OrderByDescending(x => x.Contador).ThenBy(x => x.Pregunta.Titulo).Select(x => x.Pregunta).ToList();
            return resultado;
        }

        public Resultado<int> GetCantidad(_Model.Consultas.Consulta_Pregunta consulta)
        {
            return dao.GetCantidad(consulta);
        }

        public Resultado<Pregunta> Insertar(_Model.Comandos.Comando_PreguntaNueva comando)
        {
            var resultado = new Resultado<Pregunta>();

            var resultadoTransaccion = dao.Transaction(() =>
            {
                try
                {
                    var validarComando = ValidarComandoInsertar(comando);
                    if (!validarComando.Ok)
                    {
                        resultado.Error = validarComando.Error;
                        return false;
                    }

                    //Busco la Aplicacion
                    var resultadoAplicacion = new Rules_Aplicacion(getUsuarioLogueado()).GetById(comando.Aplicacion.Value);
                    if (!resultadoAplicacion.Ok)
                    {
                        resultado.Error = resultadoAplicacion.Error;
                        return false;
                    }

                    var aplicacion = resultadoAplicacion.Return;
                    if (aplicacion == null || aplicacion.FechaBaja != null)
                    {
                        resultado.Error = "La Aplicacion no existe o esta dada de baja";
                        return false;
                    }

                    //Busco el tema
                    var resultadoTema = new Rules_Tema(getUsuarioLogueado()).GetById(comando.Tema.Value);
                    if (!resultadoTema.Ok)
                    {
                        resultado.Error = resultadoTema.Error;
                        return false;
                    }

                    var tema = resultadoTema.Return;
                    if (tema == null || tema.FechaBaja != null)
                    {
                        resultado.Error = "El tema no existe o esta dado de baja";
                        return false;
                    }
                    
                    //Creo la entidad
                    var entity = new Pregunta()
                    {
                        Aplicacion=aplicacion,
                        Tema = tema,
                        IdUsuarioAlta = comando.IdUsuario.Value,
                        Titulo=comando.Titulo,
                        Descripcion=comando.Descripcion,
                        Tags=comando.Tags,
                    };

                    //Inserto
                    var resultadoInsertar = base.Insert(entity);
                    if (!resultadoInsertar.Ok)
                    {
                        resultado.Error = resultadoInsertar.Error;
                        return false;
                    }

                    resultado.Return = resultadoInsertar.Return;
                    return true;
                }
                catch (Exception e)
                {
                    resultado.Error = "Error procesando la solicitud";
                    return false;
                }
            });

            if (resultado.Ok && !resultadoTransaccion)
            {
                resultado.Error = "Error procesando la solicitud";
                return resultado;
            }

            return resultado;
        }

        public Resultado<Pregunta> Actualizar(_Model.Comandos.Comando_PreguntaActualizar comando)
        {
            var resultado = new Resultado<Pregunta>();

            var resultadoTransaccion = dao.Transaction(() =>
            {
                try
                {
                    //Busco la entidad
                    var resultadoEntity = GetByIdObligatorio(comando.Id.Value);
                    if (!resultadoEntity.Ok)
                    {
                        resultado.Error = resultadoEntity.Error;
                        return false;
                    }
                    var entity = resultadoEntity.Return;
                    if (entity == null || entity.FechaBaja != null)
                    {
                        resultado.Error = "La pregunta no existe o esta dada de baja";
                        return false;
                    }

                    //Busco la Aplicacion
                    var resultadoAplicacion = new Rules_Aplicacion(getUsuarioLogueado()).GetById(comando.Aplicacion.Value);
                    if (!resultadoAplicacion.Ok)
                    {
                        resultado.Error = resultadoAplicacion.Error;
                        return false;
                    }

                    var aplicacion = resultadoAplicacion.Return;
                    if (aplicacion == null || aplicacion.FechaBaja != null)
                    {
                        resultado.Error = "La Aplicacion no existe o esta dada de baja";
                        return false;
                    }

                    //Busco el tema
                    var resultadoTema = new Rules_Tema(getUsuarioLogueado()).GetById(comando.Tema.Value);
                    if (!resultadoTema.Ok)
                    {
                        resultado.Error = resultadoTema.Error;
                        return false;
                    }

                    var tema = resultadoTema.Return;
                    if (tema == null || tema.FechaBaja != null)
                    {
                        resultado.Error = "El tema no existe o esta dado de baja";
                        return false;
                    }

                    //Actualizo
                    entity.Id = comando.Id.Value;
                    entity.Titulo = comando.Titulo;
                    entity.Descripcion=comando.Descripcion;
                    entity.Tags=comando.Tags;
                    entity.Aplicacion=aplicacion;
                    entity.Tema = tema;

                    //Actualizo
                    var resultadoUpdate = base.Update(entity);
                    if (!resultadoUpdate.Ok)
                    {
                        resultado.Error = resultadoUpdate.Error;
                        return false;
                    }

                    resultado.Return = resultadoUpdate.Return;
                    return true;

                }
                catch (Exception e)
                {
                    resultado.Error = "Error procesando la solicitud";
                    return false;
                }
            });

            if (resultado.Ok && !resultadoTransaccion)
            {
                resultado.Error = "Error procesando la solicitud";
                return resultado;
            }

            return resultado;
        }

        public Resultado<bool> Borrar(int id)
        {
            var resultado = new Resultado<bool>();

            var resultadoQuery = GetById(id);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            var entity = resultadoQuery.Return;
            if (entity == null || entity.FechaBaja != null)
            {
                resultado.Error = "La pregunta no existe o esta dada de baja";
                return resultado;
            }

            var resultadoDelete = base.Delete(entity);
            if (!resultadoDelete.Ok)
            {
                resultado.Error = resultadoDelete.Error;
                return resultado;
            }

            resultado.Return = true;
            return resultado;
        }

        public Resultado<bool> ValidarComandoInsertar(_Model.Comandos.Comando_PreguntaNueva comando)
        {
            var resultado = new Resultado<bool>();

            try
            {

                //Si es actualizar, valido el id
                if (comando is _Model.Comandos.Comando_PreguntaActualizar)
                {
                    _Model.Comandos.Comando_PreguntaActualizar c = (_Model.Comandos.Comando_PreguntaActualizar)comando;
                    if (!c.Id.HasValue || c.Id.Value <= 0)
                    {
                        resultado.Error = "El id de la pregunta es requerido";
                        return resultado;
                    }
                }

                //Titulo requerdio
                if (string.IsNullOrEmpty(comando.Titulo))
                {
                    resultado.Error = "El titulo es requerido";
                    return resultado;
                }

                //Descripcion requerdia
                if (string.IsNullOrEmpty(comando.Descripcion))
                {
                    resultado.Error = "La descripcion es requerida";
                    return resultado;
                }

                //Tags requeridos
                if (string.IsNullOrEmpty(comando.Tags))
                {
                    resultado.Error = "Tags requeridos";
                    return resultado;
                }

                //Aplicacion requerida
                if (!comando.Aplicacion.HasValue || comando.Aplicacion<=0)
                {
                    resultado.Error = "La aplicacion a la cual pertenece la pregunta es requerida";
                    return resultado;
                }

                //Tema requerido
                if (!comando.Tema.HasValue || comando.Tema<=0)
                {
                    resultado.Error = "El tema al cual pertenece la pregunta es requerido";
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

        public Resultado<List<Pregunta>> Top(int? cantidad, string aplicacion)
        {
            var resultado = new Resultado<List<Pregunta>>();

            //if (string.IsNullOrEmpty(aplicacion))
            //{
            //    resultado.Error = "El parametro Aplicacion es obligatorio";
            //    return resultado;
            //}

            var resultadoApp =  dao.Get(new _Model.Consultas.Consulta_Pregunta() { AplicacionIdentificador = aplicacion});
            if (!resultadoApp.Ok)
            {
                resultado.SetError(resultadoApp.Error);
                return resultado;
            }

            if (resultadoApp.Return == null || resultadoApp.Return.Count == 0)
            {
                resultado.SetError("La Aplicacion no existe");
                return resultado;
            }

            var preguntas = resultadoApp.Return;

            resultado.Return = preguntas.OrderByDescending(x => x.Contador).ThenBy(x => x.Titulo).ToList();
            if (cantidad.HasValue)
            {
                resultado.Return = resultado.Return.Take(cantidad.Value).ToList();
            }
            return resultado;
        }

        public Resultado<bool> SumarContador(int id)
        {
            var resultado = new Resultado<bool>();

            var resultadoQuery = dao.GetById(id);
            if (!resultadoQuery.Ok)
            {
                resultado.Error = resultadoQuery.Error;
                return resultado;
            }

            var entidad = resultadoQuery.Return;
            if (entidad == null)
            {
                resultado.Error = "La ayuda no existe";
                return resultado;
            }

            entidad.Contador += 1;
            var resultadoUpdate = Update(entidad);
            if (!resultadoUpdate.Ok)
            {
                resultado.Error = resultadoUpdate.Error;
                return resultado;
            }

            resultado.Return = true;
            return resultado;
        }

        private string TransformarPalaba(string palabra)
        {
            palabra = palabra.ToLower().Trim();
            palabra = palabra.Replace('?', ' ');
            palabra = palabra.Replace('¿', ' ');
            palabra = palabra.Replace('!', ' ');
            palabra = palabra.Replace('¡', ' ');
            palabra = palabra.Replace('á', 'a');
            palabra = palabra.Replace('é', 'e');
            palabra = palabra.Replace('í', 'i');
            palabra = palabra.Replace('ó', 'o');
            palabra = palabra.Replace('ú', 'u');
            palabra = palabra.Trim();

            List<string> palabrasIgnoradas = new List<string> { "mi", "yo", "mis", "sus", "su", "me", "la", "las", "le", "les", "lo", "los", "a", "ante", "bajo", "con", "contra", "de", "desde", "en", "entre", "de", "desde", "durante", "mientras", "excepto", "hacia", "hasta", "mediante", "para", "salvo", "segun", "sin", "sobre", "tras" };
            if (palabrasIgnoradas.Contains(palabra)) return "";

            palabra = palabra.Replace('v', 'b');
            palabra = palabra.Replace('s', 'c');
            return palabra;
        }

        class Busqueda
        {
            public int Contador { get; set; }
            public Pregunta Pregunta { get; set; }
        }


    }
}