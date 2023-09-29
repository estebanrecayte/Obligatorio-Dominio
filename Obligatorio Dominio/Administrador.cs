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


        public override bool Equals(object? obj)
        {
            Administrador? unA = obj as Administrador;
            return unA != null && Mail == unA.Mail;
        }
    }
}
