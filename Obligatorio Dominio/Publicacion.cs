using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Publicacion
    {
        public int Id { get; private set; }
        private static int ultId = 1; // Inicializamos en 1
        public string Texto { get; private set; }
        public string Contenido { get; private set; }
        public DateTime Fecha { get; private set; }

        public Miembro Miembro { get; private set; }

        public Publicacion (string texto,string contenido,DateTime fecha,Miembro miembro) {


            Id = ultId++; // Asignamos el valor de ultId y luego lo incrementamos
            Texto = texto;
            Contenido= contenido;
            Fecha= fecha;
            Miembro= miembro;
            
        }    
    }
}
