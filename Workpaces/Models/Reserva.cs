using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Workpaces.Models;

namespace Workspaces.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        public int SalaId { get; set; }
        public Sala Sala { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public TimeSpan HoraInicio { get; set; }
        [Required]
        public TimeSpan HoraFin { get; set; }
    }
}