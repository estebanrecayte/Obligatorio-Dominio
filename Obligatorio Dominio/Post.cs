﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Post : Publicacion
    {
        public string Imagen { get; set; }
        public bool Publico { get;  set; }
        public bool Censurado { get;  set; }
        public List<Comentario> Comentarios { get; set; } // Lista de comentarios


        public Post() {}
        
        public Post(string titulo, string contenido, DateTime fecha, TipoReaccion tipoReaccion, Miembro autor, string imagen, bool publico, bool censurado)
            : base(titulo, contenido, fecha, autor, tipoReaccion)
        {
            Imagen = imagen;
            Publico = publico;
            Censurado = censurado;
            Comentarios = new List<Comentario>(); // Inicializar la lista de comentarios
        }

        public override int CalcularValorAceptacion()
        {
            int likes = ObtenerLikes() * 5;
            int dislikes = ObtenerDislikes() * -2;

            int va = likes + dislikes;

            if (Publico)
            {
                va += 10;
            }

            return va;
        }

        public void AgregarComentario(Comentario comentario)
        {
            if (comentario != null)
            {
                Comentarios.Add(comentario);
            }
            else
            {
                throw new ArgumentNullException(nameof(comentario), "El comentario no puede ser nulo.");
            }
        }

        public void EliminarComentario(Comentario comentario)
        {
            Comentarios.Remove(comentario);
        }

        public override string ToString()
        {
            string respuesta = string.Empty;
            respuesta += $"Id del post: {Id} \n";
            respuesta += $"Titulo del post: {Titulo} \n";
            respuesta += $"Fecha del post: {Fecha.Date.ToShortDateString()} \n";
            return respuesta;
        }
    }
}
