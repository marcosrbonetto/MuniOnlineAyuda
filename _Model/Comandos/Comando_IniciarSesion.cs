using System;
using System.Linq;

namespace _Model.Comandos
{
    public class Comando_IniciarSesion
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string App { get; set; }
        public string KeyTokenSinVencimiento { get; set; }
    }
}