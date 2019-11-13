using System;
using System.Linq;

namespace WS_Internet.v1.Entities.Comandos
{
    [Serializable]
    public class ComandoWS_IniciarSesion
    {
        public string Username { get; set; }

        public string Password { get; set; }

        
    }
}