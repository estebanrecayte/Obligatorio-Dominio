using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Administrador : Usuario
    {
        public Administrador(string mail, string contrasena) : base(mail, contrasena)
        {

        }

        public void BloquearMiembro(Miembro miembro)
        {
            miembro.Bloqueado = true;
            // Aquí puedes agregar lógica adicional, como revocar amistades o desactivar invitaciones pendientes.
        }

        public override void Validar()
        {
            ValidarMail();
            ValidarContrasena();
        }

        private void ValidarMail()
        {
            if (string.IsNullOrEmpty(Mail))
            {
                throw new Exception("El correo no puede ser vacío");
            }
        }

        private void ValidarContrasena()
        {
            if (string.IsNullOrEmpty(Contrasena))
            {
                throw new Exception("La contraseña no puede ser vacía");
            }
        }

    }
}
