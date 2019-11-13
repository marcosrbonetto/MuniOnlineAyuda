using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model.Resultados
{
    public class Resultado_MuniOnline<Entity>
    {
        public Entity Return { get; set; }

        public string Error { get; set; }

        public bool Ok
        {
            get
            {
                return string.IsNullOrEmpty(Error);
            }
        }
    }
}
