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
        public ActionResult VerReservas(string filtroFecha = null, string filtroSala = null, string filtroUsuario = null)
        {
            var reservas = context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.Usuario)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtroFecha) && DateTime.TryParse(filtroFecha, out DateTime fecha))
            {
                reservas = reservas.Where(r => DbFunctions.TruncateTime(r.Fecha) == fecha);
            }

            if (!string.IsNullOrEmpty(filtroSala))
            {
                reservas = reservas.Where(r => r.Sala.Nombre.Contains(filtroSala));
            }

            if (!string.IsNullOrEmpty(filtroUsuario))
            {
                reservas = reservas.Where(r => r.Usuario.UserName.Contains(filtroUsuario));
            }

            return View(reservas.ToList());
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
