using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Workpaces.Models;
using Microsoft.AspNet.Identity;

namespace Workpaces.Controllers
{
    public class ReservaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin,Usuario")]
        // GET: Reserva
        public ActionResult Index()
        {
            // Obtener el ID del usuario actual
            string currentUserId = User.Identity.GetUserId();

            // Filtrar reservas según el rol del usuario
            var reservas = User.IsInRole("Admin")
                ? db.Reservas.Include(r => r.Sala).Include(r => r.Usuario) // Admin ve todas las reservas
                : db.Reservas.Include(r => r.Sala).Include(r => r.Usuario).Where(r => r.UsuarioId == currentUserId); // Usuarios ven solo sus reservas

            return View(reservas.ToList());
        }

        // GET: Reserva/Create
        [Authorize(Roles = "Admin,Usuario")]
        public ActionResult Create()
        {
            ViewBag.SalaId = new SelectList(db.Sala, "IdSala", "Nombre");
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email");
            ViewBag.Id = User.Identity.GetUserId();
            return View();
        }

        // POST: Reserva/Create
        [Authorize(Roles = "Admin,Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdReserva,Fecha,HoraInicio,HoraFin,SalaId,UsuarioId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                var existingReservations = db.Reservas
                    .Where(r => r.SalaId == reserva.SalaId && r.Fecha == reserva.Fecha &&
                                ((reserva.HoraInicio >= r.HoraInicio && reserva.HoraInicio < r.HoraFin) ||
                                 (reserva.HoraFin > r.HoraInicio && reserva.HoraFin <= r.HoraFin)))
                    .ToList();

                if (existingReservations.Any())
                {
                    ModelState.AddModelError("", "La sala no esta disponible en la fecha o horario seleccionado.");
                    ViewBag.Id = User.Identity.GetUserId();
                    ViewBag.SalaId = new SelectList(db.Sala, "IdSala", "Nombre", reserva.SalaId);
                    ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", reserva.UsuarioId);
                    return View(reserva);
                }

                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = User.Identity.GetUserId();
            ViewBag.SalaId = new SelectList(db.Sala, "IdSala", "Nombre", reserva.SalaId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", reserva.UsuarioId);
            return View(reserva);
        }
        [Authorize(Roles = "Admin")]
        // GET: Reserva/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalaId = new SelectList(db.Sala, "IdSala", "Nombre", reserva.SalaId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", reserva.UsuarioId);
            return View(reserva);
        }

        // POST: Reserva/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdReserva,Fecha,HoraInicio,HoraFin,SalaId,UsuarioId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                var existingReservations = db.Reservas
                    .Where(r => r.SalaId == reserva.SalaId && r.Fecha == reserva.Fecha && r.IdReserva != reserva.IdReserva &&
                                ((reserva.HoraInicio >= r.HoraInicio && reserva.HoraInicio < r.HoraFin) ||
                                 (reserva.HoraFin > r.HoraInicio && reserva.HoraFin <= r.HoraFin)))
                    .ToList();

                if (existingReservations.Any())
                {
                    ModelState.AddModelError("", "La sala no esta disponible en la fecha o horario seleccionado.");
                    ViewBag.SalaId = new SelectList(db.Sala, "IdSala", "Nombre", reserva.SalaId);
                    ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", reserva.UsuarioId);
                    return View(reserva);
                }

                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalaId = new SelectList(db.Sala, "IdSala", "Nombre", reserva.SalaId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", reserva.UsuarioId);
            return View(reserva);
        }

        // GET: Reserva/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }
        [Authorize(Roles = "Admin")]
        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}