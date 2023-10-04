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

        public Sistema()
        {
            Precargas();
        }

        public void Precargas()
        {
            PrecargaMiembros();
            PrecargaAdm();
            PrecargaInvitaciones();
            PrecargaPosts();
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
                        break; // Aca rompemos el bucle interno cuando encontramos el miembro
                    }
                }
            }

            if (miembrosDeseados.Count == correosDeseados.Count)
            {
                Random random = new Random();
                List<Miembro> miembrosProcesados = new List<Miembro>();

                foreach (Miembro miembro1 in miembrosDeseados)
                {
                    miembrosProcesados.Add(miembro1);

                    foreach (Miembro miembro2 in miembrosDeseados)
                    {
                        if (miembro1 != miembro2 && !miembrosProcesados.Contains(miembro2))
                        {
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

        public void AltaInvitacion(Invitacion invitacion)
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

            if (invitacion.Estado == Estado.Aprobada)
            {
                invitacion.MiembroSolicitante.ListaAmigos.Add(invitacion.MiembroSolicito);
                invitacion.MiembroSolicito.ListaAmigos.Add(invitacion.MiembroSolicitante);
            }
        }

        public void PrecargaAdm()
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

        public void PrecargaMiembros()
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
        }


        public void PrecargaPosts()
        {
            Random random = new Random();

            foreach (Miembro autor in _miembros)
            {
                DateTime fechaEspecifica = new DateTime(2023, 1, 1);

                for (int i = 0; i < 5; i++)
                {
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

                    bool esPublico = i % 2 == 0; // Determina si el post es público o privado

                    Post post = new Post($"Título del Post {i + 1}", $"Contenido del Post {i + 1}", fechaPublicacion, reaccionAleatoria, autor, $"imagen{i + 1}.jpg", esPublico, false);

                    for (int j = 0; j < 3; j++)
                    {
                        int comentarioRandom = random.Next(0, _miembros.Count);
                        Miembro comentarista = _miembros[comentarioRandom];
                        Comentario comentario = new Comentario($"Comentario {j + 1}", $"Contenido del comentario {j + 1}", fechaPublicacion, comentarista, reaccionAleatoria, esPublico);
                        post.AgregarComentario(comentario);
                    }

                    AltaPublicacion(post);
                }
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

            if (publicacion is Post post)
            {
                foreach (var comentario in post.Comentarios)
                {
                    _publicaciones.Add(comentario);
                }
            }
        }


        public List<string> MostrarPostYComentariosDeMiembro(string email)
        {
            bool correoEncontrado = false;
            List<string> resultados = new List<string>
            {
                $"Publicaciones de {email}:\n"
            };

            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion.Autor.Mail == email)
                {
                    correoEncontrado = true;

                    if (publicacion is Post)
                    {
                        resultados.Add($"[Post] - Id: {publicacion.Id}");
                    }
                    else if (publicacion is Comentario)
                    {
                        resultados.Add($"[Comentario] - Id: {publicacion.Id}");
                    }
                }
            }

            if (!correoEncontrado)
            {
                resultados.Add("Error: El correo electrónico no existe en los datos o no tiene publicaciones.");
            }

            return resultados;
        }


        public List<string> MostrarPostConComentariosMiembro(string emailPostConComentario)
        {
            bool correoEncontrado = false;
            bool registrosEncontrados = false;
            List<string> resultados = new List<string>();

            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion is Post post)
                {
                    foreach (Comentario comentario in post.Comentarios)
                    {
                        if (comentario.Autor.Mail == emailPostConComentario)
                        {
                            resultados.Add($"El post: {post.Titulo} tiene comentarios de {comentario.Autor.Nombre}");
                            correoEncontrado = true;
                            registrosEncontrados = true;
                            break;
                        }
                    }
                }
            }

            if (!correoEncontrado)
            {
                resultados.Add($"Error: El correo electrónico {emailPostConComentario} no existe en los datos.");
            }

            if (!registrosEncontrados)
            {
                resultados.Add("No se encontraron registros para el correo electrónico proporcionado.");
            }

            return resultados;
        }

        public List<Miembro> MostrarMiembrosConMasPublic()
        {
            int mayor = 0;
            List<Miembro> aux = new List<Miembro>();
            foreach (Miembro mie in _miembros)
            {
                int cantidad = 0;

                foreach (Publicacion publicacion in _publicaciones)
                {
                    if (publicacion.Autor == mie)
                    {
                        cantidad++;
                    }
                }

                if (cantidad > mayor)
                {
                    mayor = cantidad;
                    aux.Clear();
                    aux.Add(mie);
                }
                else if (cantidad == mayor)
                {
                    aux.Add(mie);
                }
            }
            return aux;
        }

        public void MostrarMiembros(List<Miembro> miembros)
        {
            foreach (Miembro miembro in miembros)
            {
                Console.WriteLine($"{miembro}");
                Console.WriteLine("--------------");
            }
        }
    }
}

