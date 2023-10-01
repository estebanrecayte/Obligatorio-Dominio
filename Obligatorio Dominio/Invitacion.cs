using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Invitacion
    {
        public int Id { get; private set; }
        private static int ultId = 1; // Inicializamos en 1
        public Miembro MiembroSolicito { get; set; }
        public Miembro MiembroSolicitante { get; set; }
        public DateTime Fecha { get; set; }
        public Estado Estado { get; set; }

        public Invitacion(Miembro miembroSolicito, Miembro miembroSolicitante, DateTime fecha, Estado estado)
        {
            Id = ultId++; // Asignamos el valor de ultId y luego lo incrementamos
            MiembroSolicito = miembroSolicito;
            MiembroSolicitante = miembroSolicitante;
            Fecha = fecha;
            Estado = estado;
        }

    public override bool Equals(object? obj)
    {
        Invitacion? unI = obj as Invitacion;
        return unI != null && Id == unI.Id;
    }



    }
}
