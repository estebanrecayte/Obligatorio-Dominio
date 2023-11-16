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
        private static int ultId = 1; 
        public string Titulo { get;  set; }
        public string Contenido { get;  set; }
        public DateTime Fecha { get;  set; }
        public Miembro Autor { get;  set; }
        public List<Reaccion> Reacciones { get; set; }
        public TipoReaccion TipoReaccion { get; set; }

        public Publicacion(string titulo, string contenido, DateTime fecha, Miembro autor, TipoReaccion tipoReaccion)
        {
            Id = ultId++; 
            Titulo = titulo;
            Autor = autor;
            Fecha = fecha;
            Contenido = contenido;
            Reacciones = new List<Reaccion>();
            TipoReaccion = tipoReaccion;
        }
        public Publicacion() { }

        public void Validar()
        {
            ValidarContenido();
            ValidarTitulo();
        }

        public void ValidarTitulo()
        {
            if (string.IsNullOrEmpty(Titulo) || Titulo.Length < 3)
            {
                throw new Exception("El título no puede ser vacío y debe tener al menos 3 caracteres");
            }
        }

        public void EstablecerAutor(Miembro autor)
        {
            Autor = autor;
        }

        public void ValidarContenido()
        {
            if (string.IsNullOrEmpty(Contenido))
            {
                throw new Exception("El contenido no puede ser vacío");
            }
        }

        public override bool Equals(object? obj)
        {
            Publicacion? unP = obj as Publicacion;
            return unP != null && Id == unP.Id;
        }


        public override string ToString()
        {
            string respuesta = string.Empty;
            return respuesta;
        }
    }
}
