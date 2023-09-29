using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Comentario : Publicacion
    {
        public bool EsPrivado { get; private set; }

        public Comentario(string titulo, string contenido, DateTime fecha, Miembro autor, TipoReaccion tipoReaccion, bool esPrivado)
            : base(titulo, contenido, fecha, autor, tipoReaccion)
        {
            EsPrivado = esPrivado;
        }
    }
}
