using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Obligatorio_Dominio;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWeb.Controllers
{
    public class InicioController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login(string mail, string contrasena)
        {
            Administrador admin = _sistema.BuscarAdministrador(mail);

            if (!string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(contrasena))
            {
                if (admin != null && admin.Contrasena == contrasena)
                {
                    HttpContext.Session.SetString("rol", "administrador");
                    HttpContext.Session.SetString("mail", admin.Mail);
                    return Redirect("/administrador");
                }

                Miembro miembro = _sistema.BuscarMiembro(mail);

                if (miembro != null && miembro.Contrasena == contrasena)
                {
                    HttpContext.Session.SetString("rol", "miembro");
                    HttpContext.Session.SetString("mail", miembro.Mail);
                    HttpContext.Session.SetString("nombreMiembro", $"{miembro.Nombre} {miembro.Apellido}");
                    return RedirectToAction("Index", "Miembro");
                }

                ViewBag.error = "Credenciales inválidas";
            }

            return View("Login");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }

}

