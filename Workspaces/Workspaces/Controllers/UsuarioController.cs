using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Workspaces;

namespace Workspaces.Controllers
{
    public class UsuarioController : Controller
    {
        private WorkspacesDBEntities db = new WorkspacesDBEntities();

        //GET: Usuario/Registro
        public ActionResult Registro()
        {
            return View();
        }

        //POST: Usuario/Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro([Bind(Include = "nombre,contrasena,correo")] Usuario usuario, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                //Valida que la contraseña y la confirmación coincidan
                if (usuario.contrasena != confirmPassword)
                {
                    ViewBag.ConfirmPasswordError = "Las contraseñas no coinciden.";
                    return View(usuario);
                }

                //Verifica si el correo ya existe en la base de datos
                var existingUser = db.Usuario.FirstOrDefault(u => u.correo == usuario.correo);
                if (existingUser != null)
                {
                    ModelState.AddModelError("correo", "Este correo ya está registrado.");
                    return View(usuario);
                }

                //Establece el estado del usuario a activo y rol predeterminado a 2
                usuario.estado = true;
                usuario.rol = 2;

                //Guarda al usuario en la base de datos
                db.Usuario.Add(usuario);
                db.SaveChanges();

                //Se muestra un mensaje de éxito si el usuario se pudo registrar
                TempData["Message"] = "Registro exitoso";

                return RedirectToAction("Registro");
            }

            return View(usuario);
        }

        //GET: Usuario/Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Usuario/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string correo, string contrasena)
        {
            if (ModelState.IsValid)
            {
                //Verifica si el usuario existe y la contraseña es correcta
                var usuario = db.Usuario.FirstOrDefault(u => u.correo == correo && u.contrasena == contrasena);

                if (usuario != null)
                {
                    //Se guarda la información del usuario en la sesión
                    Session["UsuarioID"] = usuario.id_usuario;
                    Session["UsuarioNombre"] = usuario.nombre;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Correo o contraseña incorrectos. Intente de nuevo.";
                }
            }

            return View();
        }

        // GET:Usuario/Logout
        public ActionResult Logout()
        {
            Session.Clear(); //Limpia la sesión
            return RedirectToAction("Login");
        }
    }
}
