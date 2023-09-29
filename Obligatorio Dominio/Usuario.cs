using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public abstract class Usuario
    {
        public string Mail { get; private set; }
        public string Contrasena { get; private set; }

        public Usuario(string mail, string contrasena) { 
            Mail = mail;
            Contrasena = contrasena;

        }


        public abstract void Validar();

    }

}
