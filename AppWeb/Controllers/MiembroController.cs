using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Obligatorio_Dominio;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWeb.Controllers
{
    public class MiembroController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult Index()
        {
            string nombreMiembro = HttpContext.Session.GetString("nombreMiembro");

            if (nombreMiembro != null)
            {
                ViewBag.MensajeBienvenida = $"Bienvenido, {nombreMiembro}!";
                return View();
            }
            else
            {
                // Manejar el caso en el que no se encuentra el nombre en la sesión
                return RedirectToAction("Login", "Inicio");
            }
        }

        public IActionResult CreateMiembro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMiembro(Miembro miembro)
        {
            try
            {
                _sistema.AltaMiembro(miembro);
                return RedirectToAction("CreateMiembro");
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View();
        }

        public IActionResult PostHabilitadosMiembro(Miembro miembro)
        {

            List<Publicacion> todosLosPosts = _sistema.ListarPublicacionesHabilitadasParaMiembro(miembro);

            ViewBag.Posts = todosLosPosts;

            return View();
        }

        public IActionResult EnviarInvitacion(string mail)
        {
            Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

            if (miembroActual == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            try
            {
                Miembro miembroDestino = _sistema.BuscarMiembro(mail);

                if (miembroDestino == null)
                {
                    ViewBag.error = "El miembro no existe";
                    return View();
                }

                miembroActual.EnviarSolicitudAmistad(miembroDestino);
                Invitacion nuevaInvitacion = new Invitacion(miembroActual, miembroDestino, DateTime.Now, Estado.PendienteAprobacion);
                _sistema.AltaInvitacion(nuevaInvitacion);

                return View();

            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }

            return View();
        }


        public IActionResult MiembrosEnviarAmistad()
        {
            Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

            if (miembroActual == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            List<Miembro> miembrosDisponibles = _sistema.ObtenerMiembrosDisponiblesParaSolicitud(miembroActual.Mail);

            ViewBag.MiembrosDisponibles = miembrosDisponibles;

            return View();
        }


        public Miembro ObtenerMiembroActualDesdeSesion()
        {
            string mailMiembroActual = HttpContext.Session.GetString("mail");
            return _sistema.BuscarMiembro(mailMiembroActual);
        }

    }
}

