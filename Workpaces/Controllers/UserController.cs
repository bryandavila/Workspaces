using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using Workpaces.Models;

namespace Workpaces.Controllers
{
    [Authorize(Roles = "Usuario")] // Solo el usuario puede accesar

    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public ActionResult Reservar(int IdSala, DateTime FechaReserva, TimeSpan HoraInicio, TimeSpan HoraFin)
        {
            // Verifica si la sala está disponible en el horario indicado
            var sala = _context.Sala.FirstOrDefault(s => s.IdSala == IdSala);

            if (sala == null)
            {
                TempData["ErrorMessage"] = "La sala no existe.";
                return RedirectToAction("Reservar");
            }

            // Verifica si ya existe una reserva en el horario indicado
            bool isAvailable = !_context.Reservas.Any(r => r.SalaId == IdSala
                                                           && r.Fecha == FechaReserva
                                                           && r.HoraInicio < HoraFin
                                                           && r.HoraFin > HoraInicio);

            if (!isAvailable)
            {
                TempData["ErrorMessage"] = "El horario seleccionado no está disponible.";
            }
            else
            {
                // Crear la reserva
                var reserva = new Reserva
                {
                    SalaId = IdSala,
                    Fecha = FechaReserva,
                    HoraInicio = HoraInicio,
                    HoraFin = HoraFin,
                    UsuarioId = User.Identity.GetUserId()  // Asumiendo que tienes autenticación de usuarios
                };

                _context.Reservas.Add(reserva);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Reserva realizada con éxito.";
            }

            return RedirectToAction("Reservar");
        }

        public ActionResult Reservar()
        {
            var salas = _context.Sala.ToList();
            return View(salas);
        }
    }

}
