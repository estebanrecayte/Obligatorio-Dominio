using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Administrador : Usuario
    {
        public int Telefono { get; set; }


        public Administrador(string mail, string contrasena, int telefono): base(mail,contrasena)
        {
            Telefono= telefono;
        }
    }
}
