using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workpaces.Models;

namespace Workpaces.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Room
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    var Sala = context.Sala.ToList();
        //    return View(Sala);
        //}

        [HttpGet]
        public ActionResult Index()
        {
            var isAdmin = User.IsInRole("Admin");  // Verifica si el usuario es un administrador
            List<Sala> salas;

            if (isAdmin)
            {
                // Si es administrador, mostrar todas las salas
                salas = context.Sala.ToList();
            }
            else
            {
                // Si es usuario, mostrar solo las salas disponibles
                salas = context.Sala.Where(s => s.Estado == true).ToList();
            }

            return View(salas);
        }


        [HttpGet]
        public ActionResult Editar(int? id)
        {

            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var sala = context.Sala.Find(id);

            if (sala == null)
                return HttpNotFound();
            
            return View(sala);
        }
        [HttpPost]
        public ActionResult Editar(Sala sala)
        {
            if (ModelState.IsValid)
            {
                context.Entry(sala).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sala);
        }

        [HttpGet]

        public ActionResult Crear()
        {
           
            return View();
        }
        [HttpPost]

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
        public ActionResult Eliminar(int? id)
        {
            var sala = context.Sala.Find(id);

            if (sala == null)
            {
                return HttpNotFound();
            }

            return View(sala);
        }
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarConfirmado(int id)
        {
            var sala = context.Sala.Find(id);

            if (sala == null)
            {
                return HttpNotFound();
            }

            context.Sala.Remove(sala);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}