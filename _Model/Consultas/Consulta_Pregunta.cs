using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model.Consultas
{
    public class Consulta_Pregunta
    {
        public string Titulo { get; set; }
        public string Aplicacion { get; set; }
        public int? AplicacionId { get; set; }
        public string AplicacionIdentificador { get; set; }
        public string Tema { get; set; }
        public bool? DadosDeBaja { get; set; }
        public string Busqueda { get; set; }

        public Consulta_Pregunta()
        {
            DadosDeBaja = false;
        }
    }
}
