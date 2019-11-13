using System;
using System.Linq;
using _Model.Entities;

namespace _DAO.Maps
{
    class PreguntaMap : BaseEntityMap<Pregunta>
    {
        public PreguntaMap()
        {
            Table("Pregunta");

            Map(x => x.Titulo, "Titulo");
            Map(x => x.Descripcion, "Descripcion");
            Map(x => x.Contador, "Contador");
            Map(x => x.Tags, "Tags");
            
            References(x => x.Aplicacion, "IdAplicacion").Not.Nullable();
            References(x => x.Tema, "IdTema").Not.Nullable();
        }
    }
}