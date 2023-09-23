using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Miembro : Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Bloqueado { get; set; }
        private List<Miembro> ListaAmigos { get; set; }
        private List<Invitacion> ListaInvitaciones { get; set; }

        public Miembro(string mail, string contrasena, string nombre, string apellido, DateTime fechaNacimiento, bool bloqueado, List<Miembro> listaAmigos, List<Invitacion> listaInvitaciones)
            : base(mail, contrasena)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Bloqueado = bloqueado;
            ListaAmigos = listaAmigos;
            ListaInvitaciones = listaInvitaciones;
        }
    }
}
