using System;
using System.Linq;
using System.Collections.Generic;

namespace WS_Internet.v1.Entities.Resultados
{
    [Serializable]
    public class ResultadoWS_Tema
    {
       public int Id { get; set; }
       public int IdAplicacion { get; set; }
        public string Nombre { get; set; }
        public List<ResultadoWS_Pregunta> Items { get; set; }

       
    }
}