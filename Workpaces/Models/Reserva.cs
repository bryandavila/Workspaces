using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Workpaces.Models;

namespace Workpaces.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public TimeSpan HoraInicio { get; set; }
        [Required]
        public TimeSpan HoraFin { get; set; }

        public int SalaId { get; set; }
        [ForeignKey("SalaId")]
        public Sala Sala { get; set; }

        public string UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; }
    }
}