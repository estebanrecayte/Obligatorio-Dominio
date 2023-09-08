using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Comentario
    {
        public bool Estado { get; set; }
        public Comentario(bool estado)
        {
            Estado = estado;
        }
    }
}
