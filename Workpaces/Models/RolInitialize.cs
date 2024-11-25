using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using Workpaces.Models;

namespace Sem09.Models
{
    public class RolInitialize
    {
        public static void Inicializar()
        {
            var rolManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));


            //roles

            List<string> roles = new List<string>();
            roles.Add("Admin");
            roles.Add("Usuario");

            foreach (var rol in roles)
            {
                if (!rolManager.RoleExists(rol))
                {
                    rolManager.Create(new IdentityRole(rol));
                }
            }

            //uSUARIO POR DEFECTO

            var adminUser = new ApplicationUser { UserName = "admin@gmail.com", Email= "admin@gmail.com" };
            string Contrsena = "Admin123";

            if (userManager.FindByEmail(adminUser.Email) == null)
            {
                var creacion = userManager.Create(adminUser, Contrsena);
                if (creacion.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }          
        }
    }
}