using System;
using System.Linq;
using _DAO.DAO;
using _Model;
using _Model.Entities;
using System.Collections.Generic;

namespace _Rules.Rules
{
    public class Rules_Aplicacion : BaseRules<Aplicacion>
    {
        private readonly DAO_Aplicacion dao;
        private readonly Rules_Pregunta _rulesPregunta;
        private readonly Rules_Tema _rulesTema;

        public Rules_Aplicacion(UsuarioLogueado data)
            : base(data)
        {
            dao = DAO_Aplicacion.Instance;
            _rulesPregunta = new Rules_Pregunta(data);
            _rulesTema = new Rules_Tema(data);
        }

        public Resultado<Aplicacion> GetById(int id)
        {
            return dao.GetById(id);
        }
        
       
    }
}