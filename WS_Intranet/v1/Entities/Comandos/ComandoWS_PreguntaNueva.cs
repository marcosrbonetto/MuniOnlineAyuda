using System;
using System.Linq;

namespace WS_Intranet.v1.Entities.Comandos
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

        public _Model.Comandos.Comando_PreguntaNueva Convertir()
        {
            return new _Model.Comandos.Comando_PreguntaNueva()
            {
                IdUsuario = IdUsuario,
                Aplicacion=Aplicacion,
                Tema=Tema,
                Titulo=Titulo,
                Descripcion=Descripcion,
                Tags=Tags,
            };
        }
    }
}