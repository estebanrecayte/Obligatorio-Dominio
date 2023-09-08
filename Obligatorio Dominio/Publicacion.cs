using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Publicacion
    {
        private static int contadorId;
        public int Id { get; private set; }
        
        public string Titulo { get; private set; }
        public string Contenido { get; private set; }
        public DateTime Fecha { get; private set; }

        public Miembro Miembro { get; private set; }

        public Publicacion (int id,string titulo,string contenido,DateTime fecha,Miembro miembro) {

            Id= id;
            Titulo= titulo;
            Contenido= contenido;
            Fecha= fecha;
            Miembro= miembro;
            contadorId++;
        }    
    }
}
