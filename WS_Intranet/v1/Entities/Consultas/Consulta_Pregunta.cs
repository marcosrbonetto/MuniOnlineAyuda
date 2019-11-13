using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_Intranet.v1.Entities.Consultas
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

        public _Model.Consultas.Consulta_Pregunta Convertir()
        {
            return new _Model.Consultas.Consulta_Pregunta()
            {
                Aplicacion = Aplicacion,
                AplicacionIdentificador = AplicacionIdentificador,
                Tema = Tema,
                Titulo = Titulo,
                DadosDeBaja = false
            };
        }
    }
}