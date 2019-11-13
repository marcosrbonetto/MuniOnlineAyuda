using _Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DAO.Maps
{
    class TemaMap : BaseEntityMap<Tema>
    {
        public TemaMap()
        {
            Table("Tema");

            Map(x => x.Nombre, "Nombre");
            References(x => x.Aplicacion, "IdAplicacion").Not.Nullable();
        }
    }
}
