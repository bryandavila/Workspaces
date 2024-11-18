using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Workpaces.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        public bool Estado { get; set; }

        // Relación con Usuario
        public ICollection<Usuario> Usuarios { get; set; }
    }
}