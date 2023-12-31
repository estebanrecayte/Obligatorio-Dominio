﻿using System;
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
        public List<Miembro> ListaAmigos { get; set; }
        public List<Invitacion> ListaInvitaciones { get; set; }

        public Miembro(string mail, string contrasena, string nombre, string apellido, DateTime fechaNacimiento, bool bloqueado)
            : base(mail, contrasena)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Bloqueado = bloqueado;
            ListaAmigos = new List<Miembro>();
            ListaInvitaciones = new List<Invitacion>();
        }

        public Miembro()
        {

        }

        public override void Validar()
        {
            ValidarMail();
            ValidarNombre();
            ValidarContrasena();
            ValidarApellido();
            ValidarFechaNacimiento();

            if (ListaAmigos == null)
            {
                ListaAmigos = new List<Miembro>();
            }
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede ser vacío");
            }
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

        // POSIBLE FUTURO DESARROLLO PARA LA SEGUNDA ENTREGA
        // agreegar amigo

        public void EnviarSolicitudAmistad(Miembro miembroSolicitado)
        {
            if (!Bloqueado)
            {
                if (!EsAmigo(miembroSolicitado) && !HaEnviadoSolicitud(miembroSolicitado))
                {
                    bool solicitudExistente = false;

                    foreach (var invitacion in ListaInvitaciones)
                    {
                        if (invitacion.MiembroSolicitante == this && invitacion.MiembroSolicito == miembroSolicitado && invitacion.Estado == Estado.PendienteAprobacion)
                        {
                            solicitudExistente = true;
                            break; // Salir del bucle si se encuentra una solicitud existente
                        }
                    }

                    if (!solicitudExistente)
                    {
                        Invitacion invitacion = new Invitacion(this, miembroSolicitado, DateTime.Now, Estado.PendienteAprobacion);
                        ListaInvitaciones.Add(invitacion);
                        miembroSolicitado.RecibirSolicitudAmistad(invitacion);
                    }
                }
            }
        }

        public void AceptarSolicitudAmistad(Invitacion invitacion)
        {
            if (!Bloqueado && invitacion.Estado == Estado.PendienteAprobacion)
            {
                invitacion.Estado = Estado.Aprobada;
                Miembro amigo = invitacion.MiembroSolicitante;
                ListaAmigos.Add(amigo);
                amigo.ListaAmigos.Add(this);
            }
        }

        public void RechazarSolicitudAmistad(Invitacion invitacion)
        {
            if (!Bloqueado && invitacion.Estado == Estado.PendienteAprobacion)
            {
                invitacion.Estado = Estado.Rechazada;
            }
        }

        public bool EsAmigo(Miembro miembro)
        {
            return miembro != null && ListaAmigos?.Contains(miembro) == true;
        }

        public bool HaEnviadoSolicitud(Miembro miembro)
        {
            if (ListaInvitaciones == null)
            {
                return false; // O manejar el caso de ListaInvitaciones siendo nula de alguna manera
            }

            foreach (Invitacion invitacion in ListaInvitaciones)
            {
                if (invitacion.MiembroSolicitante == miembro)
                {
                    return true;
                }
            }
            return false;
        }

        public void RecibirSolicitudAmistad(Invitacion invitacion)
        {
            if (!Bloqueado)
            {
                ListaInvitaciones.Add(invitacion);
            }
        }

        // aca terminan los posibles codigos a utilizar a futuro
        public override string ToString()
        {
            string respuesta = string.Empty;
            respuesta += $"Mail: {Mail} \n";
            respuesta += $"Contrasena: **** \n";
            respuesta += $"Nombre: {Nombre} \n";
            respuesta += $"Apellido: {Apellido} \n";
            respuesta += $"Fecha de nacimiento: {FechaNacimiento?.ToShortDateString()} \n";
            respuesta += $"Bloqueado?: {Bloqueado} \n";
            return respuesta;
        }



        public int CompareTo(Miembro otroM)
        {
            if (otroM == null)
            {
                return 1; // Si otroM es nulo, el objeto actual es mayor.
            }

            // Compara los apellidos primero.
            int comparacionApellidos = this.Apellido.CompareTo(otroM.Apellido);

            if (comparacionApellidos != 0)
            {
                return comparacionApellidos; // Si los apellidos son diferentes, regresa la comparación de apellidos.
            }
            else
            {
                // Si los apellidos son iguales, compara los nombres.
                return this.Nombre.CompareTo(otroM.Nombre);
            }
        }
    }
}

