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
        public DateTime? FechaNacimiento { get; set; }
        public bool Bloqueado { get; set; }
        //private List<Miembro> ListaAmigos { get; set; }
        //private List<Invitacion> ListaInvitaciones { get; set; }

        public Miembro(string mail, string contrasena, string nombre, string apellido, DateTime fechaNacimiento, bool bloqueado)
            : base(mail, contrasena)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento.Date;
            Bloqueado = bloqueado;
            //ListaAmigos = listaAmigos;
            //ListaInvitaciones = listaInvitaciones;
        }

        public override void Validar()
        {
            ValidarMail();
            ValidarContrasena();
            ValidarNombre();
            ValidarApellido();
            ValidarFechaNacimiento();
            //ValidarBloqueado();
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

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede ser vacío");
            }
        }

        private void ValidarApellido()
        {
            if (string.IsNullOrEmpty(Apellido))
            {
                throw new Exception("El apellido no puede ser vacío");
            }
        }

        private void ValidarFechaNacimiento()
        {
            if (FechaNacimiento == null)
            {
                throw new Exception("La fecha de nacimiento no puede ser vacía");
            }

            DateTime fechaMinima = new DateTime(1910, 1, 1);
            DateTime fechaMaxima = DateTime.Now; // La fecha máxima es la fecha actual

            if (FechaNacimiento < fechaMinima || FechaNacimiento > fechaMaxima)
            {
                throw new Exception("La fecha de nacimiento debe estar entre 1900 y el año actual");
            }
        }


        //private void ValidarBloqueado()
        //{
        //    // Aquí debes agregar tu lógica para validar si el usuario está bloqueado o no.
        //    // Puedes lanzar una excepción si el usuario está bloqueado según tu lógica.
        //}


        //public override string ToString()
        //{
        //    string respuesta = string.Empty;
        //    respuesta += $"Mail: {Mail} \n";
        //    respuesta += $"Contrasena: **** \n";
        //    respuesta += $"Nombre: {Nombre} \n";
        //    respuesta += $"Apellido: {Apellido} \n";
        //    respuesta += $"Fecha de nacimiento: {FechaNacimiento} \n";
        //    respuesta += $"Bloqueado?: {Bloqueado} \n";
        //    return respuesta;
        //}

        public override bool Equals(object? obj)
        {
            Miembro? unM = obj as Miembro;
            return unM != null && Mail == unM.Mail;
        }
    }
}
