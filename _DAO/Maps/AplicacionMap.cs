using _Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DAO.Maps
{
    class AplicacionMap : BaseEntityMap<Aplicacion>
    {
        public AplicacionMap()
        {
            ReadOnly();
            Table("Aplicacion");

            Map(x => x.Id, "Id");
            Map(x => x.Nombre, "Nombre");
            Map(x => x.Identificador, "Identificador");
            Map(x => x.Icono, "Icono");
        }
    }
}
