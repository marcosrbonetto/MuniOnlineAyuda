using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model.Entities
{
    public class Tema : BaseEntity
    {
        public virtual Aplicacion Aplicacion {get; set;}
        public virtual string Nombre { get; set; }
    }
}
