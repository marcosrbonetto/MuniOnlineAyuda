using _Model.Entities;
using _Model.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WS_Intranet.v1.Entities.Resultados
{
    [Serializable]
    public class ResultadoWS_RolMuniOnline
    {
       public int Id { get; set; }

        public string Nombre { get; set; }

         public ResultadoWS_RolMuniOnline()
        {
        }

         public ResultadoWS_RolMuniOnline(Resultado_RolMuniOnline rol)
        {
            Nombre = rol.Nombre;
            Id = rol.Id;
        }

         public static List<ResultadoWS_RolMuniOnline> ToList(IList<Resultado_RolMuniOnline> list)
        {
            return list.Select(x => new ResultadoWS_RolMuniOnline(x)).ToList();
        }
    }
}