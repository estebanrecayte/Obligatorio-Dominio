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

                // Verificar si el miembro actual está bloqueado
                if (miembroActual.Bloqueado)
                {
                    return View("UserBloqueado");
                }

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

            if (miembro.Bloqueado)
            {
                return View("UserBloqueado");
            }

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

            if (miembro.Bloqueado)
            {
                return View("UserBloqueado");
            }

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
                post.Reacciones = new List<Reaccion>();

                // Agregar un comentario a la publicación
                post.Comentarios = new List<Comentario>(); // Inicializar la lista si no está inicializada
                _sistema.AltaPublicacion(post);

                List<Publicacion> todosLosPosts = _sistema.ListarPublicacionesHabilitadasParaMiembro(miembroActual);
                ViewBag.Posts = _sistema.ListarPublicacionesPropias(miembroActual, todosLosPosts);

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

            if (miembroActual.Bloqueado)
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


            if (publicacion != null && publicacion.TipoReaccion == TipoReaccion.SinReaccion)
            {
                Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

                if (miembroActual.Bloqueado)
                {
                    return View("UserBloqueado");
                }

                if (miembroActual != null)
                {
                    Reaccion nuevaReaccion = new Reaccion(reaccion, miembroActual);
                    publicacion.AgregarReaccion(nuevaReaccion);

                    publicacion.TipoReaccion = reaccion == "Like" ? TipoReaccion.Like : TipoReaccion.Dislike;
                    return RedirectToAction("PostHabilitadosMiembro");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CambiarReaccion(int publicacionId, string reaccion)
        {
            Publicacion publicacion = _sistema.ObtenerPublicacionPorId(publicacionId);

            if (publicacion != null && publicacion.TipoReaccion != TipoReaccion.SinReaccion)
            {
                Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

                if (miembroActual.Bloqueado)
                {
                    return View("UserBloqueado");
                }

                if (miembroActual != null)
                {
                    publicacion.TipoReaccion = reaccion == "Like" ? TipoReaccion.Like : TipoReaccion.Dislike;
                    Reaccion nuevaReaccion = new Reaccion(reaccion, miembroActual);
                    publicacion.Reacciones.Clear(); // Eliminar todas las reacciones existentes
                    publicacion.AgregarReaccion(nuevaReaccion);

                    return RedirectToAction("PostHabilitadosMiembro");
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult ComentarPublicacion(int publicacionId, string contenidoComentario, bool esPrivado)
        {

            try
            {

                Publicacion publicacion = _sistema.ObtenerPublicacionPorId(publicacionId);

                if (publicacion != null)
                {
                    Miembro miembroActual = ObtenerMiembroActualDesdeSesion();

                    if (miembroActual.Bloqueado)
                    {
                        return View("UserBloqueado");
                    }

                    if (miembroActual != null)
                    {
                        // Crear un nuevo comentario
                        Comentario nuevoComentario = new Comentario
                        {
                            Titulo = "Titulo de comentario",
                            Contenido = contenidoComentario,
                            Autor = miembroActual,
                            Fecha = DateTime.Now,
                            TipoReaccion = TipoReaccion.SinReaccion,
                            EsPrivado = esPrivado
                        };

                        // Verificar si la publicación es un Post
                        if (publicacion is Post post)
                        {
                            // Agregar el comentario al Post
                            post.AgregarComentario(nuevoComentario);

                            // Utilizar AltaPublicacion para actualizar el Post en el sistema
                            _sistema.AltaPublicacion(post);
                        }

                        // Redirigir a la vista de publicaciones habilitadas
                        return View("PostHabilitadosMiembro");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }

            return RedirectToAction("PostHabilitadosMiembro");
        }



        public IActionResult BuscarPublicaciones(string textoBusqueda)
        {
            return View("BuscarPublicacion");
        }


        public IActionResult FiltrarPublicaciones(string textoBusqueda, int valorAceptacion)
        {
            try
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

                List<Publicacion> publicacionesFiltradas = _sistema.BuscarPublicacionesPorTexto(textoBusqueda, valorAceptacion);

                if (publicacionesFiltradas == null)
                {
                    publicacionesFiltradas = new List<Publicacion>();
                }

                return View("PublicacionesFiltradas", publicacionesFiltradas);
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }
        }
    }
}
