using System;
using System.Linq;
using _DAO.DAO;
using _Model;
using _Model.Entities;
using System.Collections.Generic;

namespace _Rules.Rules
{
    public class Rules_Tema : BaseRules<Tema>
    {
        private readonly DAO_Tema dao;

        public Rules_Tema(UsuarioLogueado data)
            : base(data)
        {
            dao = DAO_Tema.Instance;
        }

        public Resultado<Tema> GetById(int id)
        {
            return dao.GetById(id);
        }

       
    }
}