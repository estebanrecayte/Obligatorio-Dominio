using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Obligatorio_Dominio
{
    public class Comentario : Publicacion
    {
        public bool EsPrivado { get; set; }

        public Comentario(string titulo, string contenido, DateTime fecha, Miembro autor, TipoReaccion tipoReaccion, bool esPrivado)
            : base(titulo, contenido, fecha, autor, tipoReaccion)
        {
            EsPrivado = esPrivado;
        }

        public Comentario()
        {

        }

        public override int CalcularValorAceptacion()
        {
            int likes = ObtenerLikes() * 5;
            int dislikes = ObtenerDislikes() * -2;

            int va = likes + dislikes;

            return va;
        }

        public override string ToString()
        {
            string respuesta = string.Empty;
            respuesta += $"Id del comentario: {Id} \n";
            respuesta += $"Titulo del comentario: {Titulo} \n";
            respuesta += $"Autor del comentario: {Autor.Nombre} \n";
            respuesta += $"Fecha del comentario: {Fecha.Date.ToShortDateString()} \n";
            respuesta += $"Tipo de reaccion? {TipoReaccion} \n";
            respuesta += $"El comentario es publico? : {EsPrivado} \n";
            return respuesta;
        }
    }
}
