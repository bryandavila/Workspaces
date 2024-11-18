using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Workspaces.Models;

namespace Workpaces.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]       
        [MinLength(6)]
        public string Contrasena { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        public bool Estado { get; set; }

        // Relación con Rol
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        // Relación con Reservas
        public ICollection<Reserva> Reservas { get; set; }
    }
}