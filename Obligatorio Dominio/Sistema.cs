using System;
using System.Collections.Generic;

namespace Obligatorio_Dominio
{
    public class Sistema
    {
        public List<Miembro> _miembros = new List<Miembro>();
        public List<Administrador> _administradores = new List<Administrador>();
        public List<Invitacion> _invitaciones = new List<Invitacion>();
        public List<Publicacion> _publicaciones = new List<Publicacion>();
        private List<Reaccion> _reacciones = new List<Reaccion>();

        public Sistema()
        {
            PrecargaMiembros();
            PrecargaAdm();
            PrecargaInvitaciones();
            PrecargaPosts();
            //PrecargaReacciones();
        }

        public void Precargas()
        {
            PrecargaMiembros();
            PrecargaAdm();
            PrecargaInvitaciones();
            PrecargaPosts();
            //PrecargaReacciones();
        }

        public void PrecargaInvitaciones()
        {
            List<string> correosDeseados = new List<string>
            {
                "esteban@gmail.com", "mateo@gmail.com", "carlos@gmail.com",
                "laura@gmail.com", "maria@gmail.com", "pedro@gmail.com",
                "luis@gmail.com", "sara@gmail.com", "andres@gmail.com",
                "lucas@gmail.com"
            };
            List<Miembro> miembrosDeseados = new List<Miembro>();

            foreach (string correo in correosDeseados)
            {
                foreach (Miembro miembro in _miembros)
                {
                    if (miembro.Mail == correo)
                    {
                        miembrosDeseados.Add(miembro);
                        break; // Rompemos el bucle interno cuando encontramos el miembro
                    }
                }
            }

            if (miembrosDeseados.Count == correosDeseados.Count)
            {
                Random random = new Random();
                List<Miembro> miembrosProcesados = new List<Miembro>(); // Para rastrear los miembros que ya han enviado invitaciones

                foreach (Miembro miembro1 in miembrosDeseados)
                {
                    miembrosProcesados.Add(miembro1); // Agregamos miembro1 a la lista de miembros procesados

                    foreach (Miembro miembro2 in miembrosDeseados)
                    {
                        if (miembro1 != miembro2 && !miembrosProcesados.Contains(miembro2))
                        {
                            // Generar un estado aleatorio para la invitación
                            Estado estadoAleatorio = (Estado)random.Next(0, Enum.GetValues(typeof(Estado)).Length);

                            Invitacion invitacion = new Invitacion(miembro1, miembro2, DateTime.Now, estadoAleatorio);
                            AltaInvitacion(invitacion);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("No se pueden crear invitaciones porque no se encontraron suficientes miembros en el sistema.");
            }
        }

        private void AltaInvitacion(Invitacion invitacion)
        {
            if (invitacion == null)
            {
                throw new Exception("La invitación no es válida.");
            }
            if (_invitaciones.Contains(invitacion))
            {
                throw new Exception("La invitación ya existe.");
            }

            _invitaciones.Add(invitacion);
        }

        private void PrecargaAdm()
        {
            Administrador unAdministrador = new Administrador("esteban@gmail.com", "contrasena123");
            AltaAdministrador(unAdministrador);
        }

        public void AltaAdministrador(Administrador administrador)
        {
            if (administrador == null)
            {
                throw new Exception("El administrador no es válido.");
            }
            if (_administradores.Contains(administrador))
            {
                throw new Exception($"El administrador {administrador.Mail} ya existe.");
            }
            administrador.Validar();
            _administradores.Add(administrador);
        }

        private void PrecargaMiembros()
        {
            Miembro unMiembro1 = new Miembro("esteban@gmail.com", "contrasena123", "Esteban", "Recayte", new DateTime(1995, 05, 04), false);
            AltaMiembro(unMiembro1);

            Miembro unMiembro2 = new Miembro("mateo@gmail.com", "mateo1234", "Mateo", "Rodriguez", new DateTime(1997, 02, 01), true);
            AltaMiembro(unMiembro2);

            Miembro unMiembro3 = new Miembro("carlos@gmail.com", "carlos1234", "Carlos", "Lopez", new DateTime(1983, 07, 10), true);
            AltaMiembro(unMiembro3);

            Miembro unMiembro4 = new Miembro("laura@gmail.com", "laura1234", "Laura", "Martinez", new DateTime(1995, 12, 03), false);
            AltaMiembro(unMiembro4);

            Miembro unMiembro5 = new Miembro("maria@gmail.com", "maria1234", "Maria", "Sanchez", new DateTime(1987, 04, 18), false);
            AltaMiembro(unMiembro5);

            Miembro unMiembro6 = new Miembro("pedro@gmail.com", "pedro1234", "Pedro", "Gomez", new DateTime(1992, 06, 25), true);
            AltaMiembro(unMiembro6);

            Miembro unMiembro7 = new Miembro("luis@gmail.com", "luis1234", "Luis", "Fernandez", new DateTime(1988, 11, 07), false);
            AltaMiembro(unMiembro7);

            Miembro unMiembro8 = new Miembro("sara@gmail.com", "sara1234", "Sara", "Ramirez", new DateTime(1994, 03, 29), false);
            AltaMiembro(unMiembro8);

            Miembro unMiembro9 = new Miembro("andres@gmail.com", "andres1234", "Andres", "Perez", new DateTime(1996, 09, 14), false);
            AltaMiembro(unMiembro9);

            Miembro unMiembro10 = new Miembro("lucas@gmail.com", "lucas1234", "Lucas", "Lopez", new DateTime(1991, 01, 08), true);
            AltaMiembro(unMiembro10);
        }

        public void AltaMiembro(Miembro miembro)
        {
            if (miembro == null)
            {
                throw new Exception("El miembro no es válido.");
            }
            if (_miembros.Contains(miembro))
            {
                throw new Exception($"El miembro {miembro.Mail} ya existe.");
            }
            miembro.Validar();
            _miembros.Add(miembro);
            // validar con equals
        }

        public void PrecargaPosts()
        {
            Random random = new Random();

            Miembro[] autores = new Miembro[]
            {
            _miembros[0], // Esteban
            _miembros[1], // Mateo
            _miembros[2], // Carlos
            _miembros[3], // Laura
            _miembros[4]  // Maria
            };

            DateTime fechaEspecifica = new DateTime(2023, 1, 1);

            for (int i = 0; i < 5; i++)
            {
                Miembro autor = autores[i];
                TipoReaccion reaccionAleatoria = (TipoReaccion)random.Next(0, Enum.GetValues(typeof(TipoReaccion)).Length);

                DateTime fechaPublicacion;

                if (i % 2 == 0)
                {
                    fechaPublicacion = fechaEspecifica;
                }
                else
                {
                    fechaPublicacion = DateTime.Now;
                }

                Post post = new Post($"Título del Post {i + 1}", $"Contenido del Post {i + 1}", fechaPublicacion, reaccionAleatoria, autor, $"imagen{i + 1}.jpg", i % 2 == 0, i % 2 != 0);

                for (int j = 0; j < 3; j++)
                {
                    int comentarioRandom = random.Next(0, _miembros.Count);
                    Miembro comentarista = _miembros[comentarioRandom];
                    Comentario comentario = new Comentario($"Comentario {j + 1}", $"Contenido del comentario {j + 1}", fechaPublicacion, comentarista, reaccionAleatoria, false);
                    post.AgregarComentario(comentario);
                }

                AltaPublicacion(post);
            }
        }

        public void AltaPublicacion(Publicacion publicacion)
        {
            if (publicacion == null)
            {
                throw new Exception("La publicación no es válida.");
            }
            if (_publicaciones.Contains(publicacion))
            {
                throw new Exception("La publicación ya existe.");
            }

            _publicaciones.Add(publicacion);

            // Verificar si la publicación es de tipo Post y tiene comentarios
            if (publicacion is Post post)
            {
                foreach (var comentario in post.Comentarios)
                {
                    _publicaciones.Add(comentario); // Agregar cada comentario a la lista de publicaciones
                }
            }
        }
    }
}
