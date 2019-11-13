using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model.Resultados
{
    [Serializable]
    public class Resultado_Paginador<T>
    {
        public int PaginaActual { get; set; }
        public int TamañoPagina { get; set; }
        public int CantidadPaginas { get; set; }
        public int Count { get; set; }
        public int OrderBy { get; set; }
        public List<T> Data { get; set; }
    }
}
