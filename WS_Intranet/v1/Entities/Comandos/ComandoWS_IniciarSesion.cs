using _Model.Comandos;
using System;
using System.Linq;

namespace WS_Intranet.v1.Entities.Comandos
{
    [Serializable]
    public class ComandoWS_IniciarSesion
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public  Comando_IniciarSesion ToRules()
        {
            return new Comando_IniciarSesion
            {
                Username = Username,
                Password = Password
            };
        }
    }
}