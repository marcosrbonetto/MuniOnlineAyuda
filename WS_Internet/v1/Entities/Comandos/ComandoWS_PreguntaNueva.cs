using System;
using System.Linq;

namespace WS_Internet.v1.Entities.Comandos
{
    [Serializable]
    public class ComandoWS_PreguntaNueva
    {
        public string Titulo { get; set; }
        public int? Aplicacion { get; set; }
        public int? Tema { get; set; }
        public string Descripcion { get; set; }
        public string Tags { get; set; }
        public int? IdUsuario { get; set; }

        
    }
}