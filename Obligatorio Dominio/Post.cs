using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Post
    {
        public string Imagen { get; set; }

        public bool Publico {get; set; }

        public bool Censurado { get; set; }

        private List<Comentario> Comentarios { get; set; } //no pasar comentarios crear metodo

        public Post(string imagen,bool publico, bool censurado, List<Comentario> comentarios)
        {
            Imagen = imagen;
            Publico = publico;
            Censurado = censurado;
            Comentarios = comentarios;
        }
    }
}
