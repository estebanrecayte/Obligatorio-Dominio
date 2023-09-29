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
        public string Titulo { get; private set; }
        public string Contenido { get; private set; }
        public DateTime Fecha { get; private set; }
        public Miembro Autor { get; private set; }
        public List<Reaccion> Reacciones { get; set; }
        public TipoReaccion TipoReaccion { get; set; }

        public Publicacion (string titulo,string contenido,DateTime fecha,Miembro autor, TipoReaccion tipoReaccion) {
            Id = ultId++; // Asignamos el valor de ultId y luego lo incrementamos
            Titulo = titulo;
            Autor = autor;
            Fecha = fecha;
            Contenido = contenido;
            Reacciones = new List<Reaccion>();
            TipoReaccion = tipoReaccion;
        }
    }
}
