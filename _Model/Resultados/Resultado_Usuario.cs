using System;
using System.Collections.Generic;
using System.Linq;
using _Model.Entities;

namespace _Model.Resultados
{
    [Serializable]
    public class Resultado_Usuario
    {
        public int Id { get; set; }
        public string Token { get; set; }

        //SiSi
        public int? CantidadHijos { get; set; }
        public bool RecibeNotificaciones { get; set; }
        public bool CargoExperienciaLaboral { get; set; }
        public bool TieneTrabajoActualmente { get; set; }
        public int? EstudioAlcanzadoId { get; set; }
        public string estudioAlcanzadoNombre { get; set; }
        public int? ocupacionId { get; set; }
        public string ocupacionNombre { get; set; }

        // Datos basicos
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }

        // Datos personales
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public string Cuil { get; set; }
        public bool SexoMasculino { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string IdentificadorFotoPersonal { get; set; }
        public string IdentificadorFotoRegistroCivil { get; set; }
        public string DomicilioLegal { get; set; }
        public int? EstadoCivilId { get; set; }
        public string EstadoCivilNombre { get; set; }
        public string DomicilioDireccion { get; set; }
        public string DomicilioAltura { get; set; }
        public string DomicilioPiso { get; set; }
        public string DomicilioDepto { get; set; }
        public string DomicilioTorre { get; set; }
        public string DomicilioCodigoPostal { get; set; }
        public string DomicilioBarrioNombre { get; set; }
        public int? DomicilioBarrioId { get; set; }
        public string DomicilioCiudadNombre { get; set; }
        public int? DomicilioCiudadId { get; set; }
        public string DomicilioProvinciaNombre { get; set; }
        public int? DomicilioProvinciaId { get; set; }

        // Datos de acceso
        public string Username { get; set; }

        // Datos de contacto
        public string Email { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoCelular { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }

        // Datos empleado
        public bool Empleado { get; set; }
        public string Funcion { get; set; }
        public string Cargo { get; set; }
        public DateTime? FechaJubilacion { get; set; }

        // Datos de validacion
        public bool ValidacionEmail { get; set; }
        public bool ValidacionDNI { get; set; }
        public bool ValidacionPersonal { get; set; }
        public bool ValidacionDomicilio { get; set; }
        public bool ValidacionNumeroTramite { get; set; }

        //Validaciones
        public bool ValidadoEmail { get; set; }
        public bool ValidadoRenaper { get; set; }
        public bool ValidadoPersonal { get; set; }
        public bool ValidadoDomicilio { get; set; }


        public Resultado_Usuario()
        {
        }

    }
}