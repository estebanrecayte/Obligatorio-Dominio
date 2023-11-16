using System;
using System.Collections.Generic;

namespace Obligatorio_Dominio
{
    public class Reaccion
    {
        public string Tipo { get; set; }
        private List<Miembro> ListaAutor { get; set; }

        public Reaccion() { }
        public Reaccion(string tipo, List<Miembro> listaAutor)
        {
            Tipo = tipo;
            ListaAutor = listaAutor;
        }
    }
}
