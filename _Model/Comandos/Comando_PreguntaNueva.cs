using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model.Comandos
{
    public class Comando_PreguntaNueva
    {
        public string Titulo { get; set; }
        public int? Aplicacion { get; set; }
        public int? Tema { get; set; }
        public string Descripcion { get; set; }
        public string Tags { get; set; }
        public int? IdUsuario { get; set; }
    }
}
