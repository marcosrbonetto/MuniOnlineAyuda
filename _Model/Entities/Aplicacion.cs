using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model.Entities
{
    public class Aplicacion : BaseEntity
    {
        public virtual string Nombre { get; set; }
        public virtual string Identificador { get; set; }
        public virtual string Icono { get; set; }

        

    }
}
