using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        }

        public void BanearPost(Post post)
        {
            post.Censurado = true;
        }

        public override void Validar()
        {
            ValidarMail();
            ValidarContrasena();
        }

        public void ValidarMail()
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

        public override string ToString()
        {
            string respuesta = string.Empty;
            respuesta += $"Mail: {Mail} \n";
            respuesta += $"Contrasena: **** \n";
            return respuesta;
        }

    }
}
