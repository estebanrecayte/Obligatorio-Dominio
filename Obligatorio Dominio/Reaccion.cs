using System;
using System.Collections.Generic;

namespace Obligatorio_Dominio
{
    public class Reaccion
    {
        public string Tipo { get; set; }
        public Miembro Autor { get; set; }

        public Reaccion() { }

        public Reaccion(string tipo, Miembro autor)
        {
            Tipo = tipo;
            Autor = autor;
        }
    }
}
