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

        public IActionResult Index()
        {
            ViewBag.Miembros = _sistema.OrdenarMiembrosPorNombre();
            return View();
        }
    }
}

