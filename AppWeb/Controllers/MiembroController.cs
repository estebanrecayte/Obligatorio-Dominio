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

            if (miembroActual.Bloqueado)
            {
                return View("UserBloqueado");
            }

            try
            {
                // Lógica para validar y agregar la publicación al sistema
                post.EstablecerAutor(miembroActual);
                post.Fecha=DateTime.Now;
                post.TipoReaccion = TipoReaccion.SinReaccion;
                _sistema.AltaPublicacion(post);

                List<Publicacion> todosLosPosts = _sistema.ListarPublicacionesHabilitadasParaMiembro(miembroActual);
                ViewBag.Posts = _sistema.ListarPublicacionesPropias(miembroActual, todosLosPosts);

                // Tirale en la linea 254 un breakpoint y hacele debug. Agregue en la linea 253 esa funcion para que tambien tenga la fecha que se hace

                return View("MisPublicaciones"); // Redirige a la acción MisPublicaciones después de una publicación exitosa
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }
        }


        public IActionResult MisPublicaciones()
        {
            Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

            if (miembroActual == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            if (miembroActual.Bloqueado) //cambie el simbolo ! para que verifique si esta bloqueado
            {
                return View("UserBloqueado");
            }

            try
            {
                List<Publicacion> todosLosPosts = _sistema.ListarPublicacionesHabilitadasParaMiembro(miembroActual);
                ViewBag.Posts = _sistema.ListarPublicacionesPropias(miembroActual, todosLosPosts);
                
               
                return View();
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult SeleccionarReaccion(int publicacionId, string reaccion)
        {
            // Obtener la publicación
            Publicacion publicacion = _sistema.ObtenerPublicacionPorId(publicacionId);

            // Validar que la publicación existe y tiene TipoReaccion.SinReaccion
            if (publicacion != null && publicacion.TipoReaccion == TipoReaccion.SinReaccion)
            {
                // Obtener el miembro actual
                Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

                // Validar que el miembro existe
                if (miembroActual != null)
                {
                    // Asignar la reacción a la publicación
                    publicacion.TipoReaccion = reaccion == "Like" ? TipoReaccion.Like : TipoReaccion.Dislike;
                    // Puedes almacenar el miembro que ha reaccionado en la lista de autores de la reacción si es necesario.

                    // Redireccionar a la vista de publicaciones habilitadas
                    return RedirectToAction("PostHabilitadosMiembro");
                }
            }

            // Manejar el caso de error (puedes redirigir a una página de error)
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CambiarReaccion(int publicacionId, string reaccion)
        {
            // Obtener la publicación
            Publicacion publicacion = _sistema.ObtenerPublicacionPorId(publicacionId);

            // Validar que la publicación existe y tiene TipoReaccion diferente de SinReaccion
            if (publicacion != null && publicacion.TipoReaccion != TipoReaccion.SinReaccion)
            {
                // Obtener el miembro actual
                Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

                // Validar que el miembro existe
                if (miembroActual != null)
                {
                    // Cambiar la reacción de la publicación
                    publicacion.TipoReaccion = reaccion == "Like" ? TipoReaccion.Like : TipoReaccion.Dislike;
                    // Puedes actualizar la lista de autores de la reacción si es necesario.

                    // Redireccionar a la vista de publicaciones habilitadas
                    return RedirectToAction("PostHabilitadosMiembro");
                }
            }

            // Manejar el caso de error (puedes redirigir a una página de error)
            return RedirectToAction("Index");
        }
    }
}
