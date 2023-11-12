using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Obligatorio_Dominio;


namespace AppWeb.Controllers
{
    public class AdministradorController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        private Administrador _administrador = new Administrador("admin@admin.com", "admin123");

        public IActionResult Index()
        {
            ViewBag.Miembros = _sistema.OrdenarMiembrosPorNombre();
            return View();
        }


        //[HttpPost]
        public IActionResult BloquearUsuario(string nombre)
        {
            Miembro miembro = null;

            foreach (Miembro m in _sistema._miembros)
            {
                if (m.Nombre == nombre)
                {
                    miembro = m;
                    break;
                }
            }

            if (miembro != null)
            {
                _administrador.BloquearMiembro(miembro);
            }

            ViewBag.Miembros = _sistema.OrdenarMiembrosPorNombre();
            return View("Bloqueo");
        }


        //[HttpPost]
        public IActionResult BanearPost(int postId)
        {
            Post post = null;

            foreach (Publicacion publicacion in _sistema._publicaciones)
            {
                if (publicacion.Id == postId && publicacion is Post)
                {
                    post = (Post)publicacion;
                    break;
                }
            }

            if (post != null)
            {
                _administrador.BanearPost(post);
            }

            ViewBag.Posts = _sistema.ListarTodosLosPosts();
            return View("BanearPost");
        }

    }
}

