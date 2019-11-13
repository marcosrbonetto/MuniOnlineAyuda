using System;
using System.Linq;
using System.Collections.Generic;

namespace WS_Internet.v1.Entities.Resultados
{
    [Serializable]
    public class ResultadoWS_Aplicacion
    {
        public int Id { get; set; }
        public string Identificador { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }

        public List<ResultadoWS_Tema> Temas { get; set; }

        
    }
}