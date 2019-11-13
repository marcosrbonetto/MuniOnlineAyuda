using System;
using System.Linq;
using _Model.Entities;
using System.Collections.Generic;

namespace WS_Intranet.v1.Entities.Resultados
{
    [Serializable]
    public class ResultadoWS_Tema
    {
       public int Id { get; set; }
       public int IdAplicacion { get; set; }
        public string Nombre { get; set; }
        public List<ResultadoWS_Pregunta> Items { get; set; }

        public ResultadoWS_Tema(Tema entity)
        {
            if (entity == null)
            {
                return;
            }

            Nombre = entity.Nombre;
            Id = entity.Id;
            if (entity.Aplicacion != null)
            {
                IdAplicacion = entity.Aplicacion.Id;
            }

        }

        public static List<ResultadoWS_Tema> ToList(IList<Tema> list)
        {
            return list.Select(x => new ResultadoWS_Tema(x)).ToList();
        }
    }
}