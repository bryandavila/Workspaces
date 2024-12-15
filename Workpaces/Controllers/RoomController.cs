using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Workpaces.Models;

namespace Workpaces.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoomController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            var salas = context.Sala.ToList();
            return View(salas);
        }


        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Sala sala)
        {
            if (ModelState.IsValid)
            {
                context.Sala.Add(sala);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sala);
        }


        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var sala = context.Sala.Find(id);
            if (sala == null) return HttpNotFound();

            return View(sala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Sala sala)
        {
            if (ModelState.IsValid)
            {
                var salaExistente = context.Sala.Find(sala.IdSala);
                if (salaExistente == null)
                {
                    return HttpNotFound();
                }

                salaExistente.Nombre = sala.Nombre;
                salaExistente.Capacidad = sala.Capacidad;
                salaExistente.Estado = sala.Estado; // Actualizar estado
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sala);
        }


        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var sala = context.Sala.Find(id);
            if (sala == null) return HttpNotFound();

            return View(sala);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarConfirmado(int id)
        {
            var sala = context.Sala.Find(id);
            if (sala != null)
            {
                context.Sala.Remove(sala);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
