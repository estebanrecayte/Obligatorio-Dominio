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

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
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
                Console.WriteLine("Miembro Agregado:");
                Console.WriteLine($"Nombre: {miembro.Mail}, {miembro.Contrasena}, {miembro.Nombre}, Edad: {miembro.Apellido}, Otros detalles: {miembro.FechaNacimiento}");
                return RedirectToAction("CreateMiembro");
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View();
        }
    }
}

