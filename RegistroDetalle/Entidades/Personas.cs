using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroDetalle.Entidades
{
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
       
        [ForeignKey("PersonaId")]
        public virtual List<TelefonoDetalle> Telefonos { get; set; } = new List<TelefonoDetalle>();
        public Personas() {}     
        public Personas(int personaId, string nombre, string cedula, string direccion, DateTime fechaNacimiento,  List<TelefonoDetalle> telefonos)
        {
            PersonaId = personaId;
            Nombre = nombre;
            Cedula = cedula;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;          
            Telefonos = new List<TelefonoDetalle>();
        }
    }
}
