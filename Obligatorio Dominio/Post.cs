using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Post: Publicacion
    {
        public string Imagen { get; set; }
        public bool Publico {get; set; }
        public bool Censurado { get; set; }

        private List<Comentario> Comentarios { get; set; } //no pasar comentarios crear meto2 porque el profe nos dijo esto? no nos acordamos. 

        public Post (string texto, string contenido, DateTime fecha, Miembro miembro, string imagen,bool publico, bool censurado, List<Comentario> comentarios)
            : base(texto,contenido, fecha,miembro)
        {
            Imagen = imagen;
            Publico = publico;
            Censurado = censurado;
            Comentarios = comentarios;
        }
    }
}
