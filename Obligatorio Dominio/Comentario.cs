using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Comentario: Publicacion
    {
        public bool Estado { get; set; }
        public Comentario(string texto, string contenido, DateTime fecha, Miembro miembro, bool estado):
            base (texto, contenido, fecha, miembro)
        {
            Estado = estado;
        }
    }
}
