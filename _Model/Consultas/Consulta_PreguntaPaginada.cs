﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model.Consultas
{
    public class Consulta_PreguntaPaginada : Consulta_Pregunta
    {
        public Enums.PreguntaOrderBy OrderBy { get; set; }
        public bool OrderByAsc { get; set; }
        public int Pagina { get; set; }
        public int TamañoPagina { get; set; }
    }
}
