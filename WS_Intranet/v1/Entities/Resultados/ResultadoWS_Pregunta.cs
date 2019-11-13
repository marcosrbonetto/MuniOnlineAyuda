using System;
using System.Linq;
using _Model.Entities;
using System.Collections.Generic;

namespace WS_Intranet.v1.Entities.Resultados
{
    [Serializable]
    public class ResultadoWS_Pregunta
    {
        public int Aplicacion { get; set; }
        public int Tema { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Contador { get; set; }
        public string Tags { get; set; }
        public int Id { get; set; }

        public ResultadoWS_Pregunta(Pregunta entity)
        {
            if (entity == null)
            {
                return;
            }

            Aplicacion=entity.Aplicacion.Id;
            Tema = entity.Tema.Id;
            Titulo=entity.Titulo;
            Descripcion=entity.Descripcion;
            Contador = entity.Contador;
            Tags = entity.Tags;
            Id = entity.Id;

        }

        public static List<ResultadoWS_Pregunta> ToList(IList<Pregunta> list)
        {
            return list.Select(x => new ResultadoWS_Pregunta(x)).ToList();
        }
    }
}