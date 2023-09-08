using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Invitacion
    {
        public int Id {get; set;}
        public Miembro MiembroSolicito { get; set;}
        public Miembro MiembroSolicitante { get; set; }

        public DateTime Fecha { get; set; }

        public Estado Estado { get; set; }


        public Invitacion(int id, Miembro miembroSolicito, Miembro miembroSolicitante, DateTime fecha, Estado estado) {

            Id = id;
            MiembroSolicito = miembroSolicito;
            MiembroSolicitante = miembroSolicitante;
            Fecha = fecha;
            Estado = estado;
        }
    }
}
