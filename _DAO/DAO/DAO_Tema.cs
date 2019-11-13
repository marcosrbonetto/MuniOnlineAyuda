using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Model.Entities;
using _Model;

namespace _DAO.DAO
{
    public class DAO_Tema : BaseDAO<Tema>
    {
        private static DAO_Tema instance;

        public static DAO_Tema Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAO_Tema();
                }
                return instance;
            }
        }

        public Resultado<Tema> GetByNombre(string nombre)
        {
            var resultado = new Resultado<Tema>();

            try
            {
                var entity = GetSession().QueryOver<Tema>().Where(x => x.Nombre.Equals(nombre) && x.FechaBaja == null).SingleOrDefault();
                resultado.Return = entity;
            }
            catch (Exception e)
            {
                resultado.SetError(e);
            }
            return resultado;

        }
    }
}
