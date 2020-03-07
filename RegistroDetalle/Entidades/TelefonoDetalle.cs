using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroDetalle.Entidades
{
    public class TelefonoDetalle
    {
        [Key]
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public string TipoTelefono { get; set; }
        public string Telefono { get; set; }
        public TelefonoDetalle() { }       
        public TelefonoDetalle(int id, int personaId, string tipoTelefono, string telefono)
        {
            Id = id;
            PersonaId = personaId; 
            TipoTelefono = tipoTelefono;
            Telefono = telefono;
        }
    }
}
