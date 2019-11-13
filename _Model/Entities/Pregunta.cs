using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model.Entities
{
    [Serializable]
    public class Pregunta : BaseEntity
    {
        public virtual Aplicacion Aplicacion { get; set; }
        public virtual Tema Tema { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual int Contador { get; set; }
        public virtual string Tags { get; set; }

    }
}
