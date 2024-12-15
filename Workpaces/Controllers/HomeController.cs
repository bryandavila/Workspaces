using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workpaces.Models;

namespace Workpaces.Controllers
{
   
    public class HomeController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private LoginViewModel user = new LoginViewModel();
        public ActionResult Index()
        {
            var usuario = User.Identity.Name;
            var reservas = context.Reservas.Where(i=>i.Usuario.Email==usuario).Include(r => r.Sala).Include(r => r.Usuario).ToList();
            return View(reservas);
        
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}