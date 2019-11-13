using System;
using System.Linq;

namespace _Model.Resultados
{
    [Serializable]
    public class Resultado_ApiRest<Entity>
    {
        public Entity Return { get; set; }

        public string Error { get; set; }

        public bool Ok
        {
            get
            {
                return string.IsNullOrEmpty(Error);
            }
        }
    }
}