using System;
using System.Linq;
using _Model.Entities;
using System.Collections.Generic;

namespace WS_Intranet.v1.Entities.Resultados
{
    [Serializable]
    public class ResultadoWS_Aplicacion
    {
        public int Id { get; set; }
        public string Identificador { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }

        public List<ResultadoWS_Tema> Temas { get; set; }

        public ResultadoWS_Aplicacion(Aplicacion entity)
        {
            if (entity == null)
            {
                return;
            }

            Identificador = entity.Identificador;
            Nombre = entity.Nombre;
            Id = entity.Id;
            Icono = entity.Icono;

        }

        public static List<ResultadoWS_Aplicacion> ToList(IList<Aplicacion> list)
        {
            return list.Select(x => new ResultadoWS_Aplicacion(x)).ToList();
        }
    }
}