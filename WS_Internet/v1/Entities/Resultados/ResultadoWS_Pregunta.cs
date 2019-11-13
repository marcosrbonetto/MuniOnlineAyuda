using System;
using System.Linq;
using System.Collections.Generic;

namespace WS_Internet.v1.Entities.Resultados
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

        
    }
}