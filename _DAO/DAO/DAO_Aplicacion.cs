using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Model.Entities;
using _Model;

namespace _DAO.DAO
{
    public class DAO_Aplicacion : BaseDAO<Aplicacion>
    {
        private static DAO_Aplicacion instance;

        public static DAO_Aplicacion Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAO_Aplicacion();
                }
                return instance;
            }
        }

        public Resultado<Aplicacion> GetByNombre(string nombre)
        {
            var resultado = new Resultado<Aplicacion>();

            try
            {
                var entity = GetSession().QueryOver<Aplicacion>().Where(x => x.Nombre.Equals(nombre) && x.FechaBaja == null).SingleOrDefault();
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
