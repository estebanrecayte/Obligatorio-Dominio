﻿using System;
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
        private Miembro _miembro;

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


        public IActionResult VerSolicitudesAmistad()
        {
            Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

            if (miembroActual == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            try
            {
                List<Invitacion> solicitudesPendientes = _sistema.ObtenerInvitacionesPendientes(miembroActual.Mail);

                ViewBag.SolicitudesPendientes = solicitudesPendientes;
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }

            return View("VerSolicitudes");
        }

        [HttpPost]
        public IActionResult AceptarSolicitudAmistad(int invitacionId)
        {
            string mailMiembroActual = HttpContext.Session.GetString("mail");
            Miembro miembro = _sistema.BuscarMiembro(mailMiembroActual);

            try
            {
                // Obtener invitaciones pendientes del miembro actual
                List<Invitacion> invitacionesPendientes = _sistema.ObtenerInvitacionesPendientes(mailMiembroActual);

                // Buscar la invitación por el ID
                Invitacion invitacion = invitacionesPendientes.FirstOrDefault(i => i.Id == invitacionId);

                if (invitacion != null)
                {
                    // Aceptar la solicitud en el miembro
                    miembro.AceptarSolicitudAmistad(invitacion);

                    // Actualizar la lista de invitaciones pendientes
                    invitacionesPendientes = _sistema.ObtenerInvitacionesPendientes(mailMiembroActual);
                    ViewBag.SolicitudesPendientes = invitacionesPendientes;

                    ViewBag.AccionRealizada = "Aceptar";
                }
                else
                {
                    ViewBag.error = "No se encontró la invitación con el ID proporcionado.";
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }

            return View("EstadoSolicitud");
        }



        [HttpPost]
        public IActionResult RechazarSolicitudAmistad(int invitacionId)
        {
            string mailMiembroActual = HttpContext.Session.GetString("mail");
            Miembro miembro = _sistema.BuscarMiembro(mailMiembroActual);

            try
            {
                // Obtener invitaciones pendientes del miembro actual
                List<Invitacion> invitacionesPendientes = _sistema.ObtenerInvitacionesPendientes(mailMiembroActual);

                // Inicializar la invitación como null
                Invitacion invitacion = null;

                // Buscar la invitación por el ID utilizando un bucle foreach
                foreach (var invitacionPendiente in invitacionesPendientes)
                {
                    if (invitacionPendiente.Id == invitacionId)
                    {
                        invitacion = invitacionPendiente;
                        break; // Salir del bucle una vez encontrada la invitación
                    }
                }

                if (invitacion != null)
                {
                    // Rechazar la solicitud en el miembro
                    miembro.RechazarSolicitudAmistad(invitacion);

                    // Actualizar la lista de invitaciones pendientes
                    invitacionesPendientes = _sistema.ObtenerInvitacionesPendientes(mailMiembroActual);
                    ViewBag.SolicitudesPendientes = invitacionesPendientes;

                    ViewBag.AccionRealizada = "Rechazar";
                }
                else
                {
                    ViewBag.error = "No se encontró la invitación con el ID proporcionado.";
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }

            return View("EstadoSolicitud");
        }


        public IActionResult CrearPublicacion(Post post)
        {
            Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

            if (miembroActual == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            if (!miembroActual.Bloqueado)
            {
                ViewBag.error = "No puedes realizar publicaciones porque estás bloqueado por el administrador.";
                return View("Error");
            }

            try
            {
                // Lógica para validar y agregar la publicación al sistema
                post.EstablecerAutor(miembroActual);
                _sistema.AltaPublicacion(post);

                ViewBag.Mensaje = "Publicación realizada con éxito.";
                return View("EstadoPublicacion");
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View("CreatePost");
            }
        }

    }
}
