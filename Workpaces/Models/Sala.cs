using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Workspaces.Models;

namespace Workpaces.Models
{
    public class Sala
    {
    
        [Key]
        public int IdSala { get; set; }
        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }
        [Required]
        public int Capacidad { get; set; }
        [Required]
        [MaxLength(200)]
        public string Ubicacion { get; set; }
        [Required]
        [MaxLength(500)]
        public string DisponibilidadEquipo { get; set; }
        [Required]
        public bool Estado { get; set; }

       
        public ICollection<Reserva> Reserva { get; set; }
    }
}