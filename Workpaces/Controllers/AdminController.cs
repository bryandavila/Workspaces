using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Workpaces.Models;
using System.Data.Entity.Infrastructure;


namespace Workpaces.Controllers
{
    [Authorize(Roles = "Admin")] // Solo Admin puede accesar
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();

        // Ver las reservas
        public ActionResult VerReservas()
        {
            var reservas = context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.Usuario)
                .ToList();
            return View(reservas);
        }


        // Editar una reserva
        public ActionResult EditarReserva(int idReserva)
        {
            var reserva = context.Reservas.Find(idReserva);
            if (reserva == null)
                return HttpNotFound();

            return View(reserva);
        }

        [HttpPost]
        public ActionResult EditarReserva(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                context.Entry(reserva).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("VerReservas");
            }
            return View(reserva);
        }

        // Confirmación para eliminar
        public ActionResult EliminarReserva(int idReserva)
        {
            var reserva = context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.Usuario)
                .FirstOrDefault(r => r.IdReserva == idReserva);

            if (reserva == null)
                return HttpNotFound();

            return View(reserva);
        }

        // Confirmar y eliminar la reserva
        [HttpPost, ActionName("EliminarReserva")]
        public ActionResult EliminarReservaConfirmado(int idReserva)
        {
            var reserva = context.Reservas.Find(idReserva);
            if (reserva == null)
                return HttpNotFound();

            context.Reservas.Remove(reserva); // Elimina la reserva
            context.SaveChanges(); // Guarda los cambios en la base de datos

            return RedirectToAction("VerReservas");
        }
    }
}
