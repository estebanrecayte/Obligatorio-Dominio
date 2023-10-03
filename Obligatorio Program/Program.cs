using System;
using System.Collections.Generic;
using Obligatorio_Dominio;

namespace Obligatorio_Program
{
    internal class Program
    {
        private static Sistema unSistema = new Sistema();

        static void Main(string[] args)
        {
            Console.WriteLine("Datos pre-cargados exitosamente, presiona cualquier tecla para continuar\n");
            Console.ReadKey();

            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Menú de la Red Social\n");
                Console.WriteLine("1 - Alta de miembro");
                Console.WriteLine("2 - Listar publicaciones por email de miembro");
                Console.WriteLine("3 - Listar posts en los que ha realizado comentarios por email de miembro");
                Console.WriteLine("4 - Listar posts entre dos fechas");
                Console.WriteLine("5 - Obtener miembros con más publicaciones");
                Console.WriteLine("0 - Salir");

                opcion = PedirNumero();

                switch (opcion)
                {
                    case 1:
                        AltaMiembro();
                        break;
                    case 2:
                        Console.Write("Ingrese el correo electrónico del miembro: ");
                        string emailMiembro = Console.ReadLine();
                        ListarPublicacionesPorMiembro(emailMiembro);
                        break;
                    case 3:
                        Console.Write("Ingrese el correo electrónico del miembro: ");
                        string emailMiembroConComentarios = Console.ReadLine();
                        ListarPostsConComentariosDeMiembro(emailMiembroConComentarios);
                        break;
                    case 4:
                        ListarPostsEntreFechasDesdeConsola();
                        break;
                    case 5:
                        List<Miembro> miembrosConMasPublicaciones = ListarMiembrosConMasPublicaciones();
                        MostrarMiembrosConMasPublicaciones(miembrosConMasPublicaciones);
                        break;
                    // UTILIZAMOS ESTOS CASES PARA VERIFICAR FUNCIONAMIENTOS - SE DEJA PARA PROXIMA ENTREGA
                    //case 6:
                    //    //ListarInvitaciones();
                    //   break;
                    //case 7:
                    //    ListarPostsPreCargados();
                    //    break;
                    case 0:
                        Console.WriteLine("Saliendo del programa.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 0);
        }

        static int PedirNumero()
        {
            int numero;
            string numeroIngresado;

            while (true)
            {
                numeroIngresado = Console.ReadLine();
                try
                {
                    numero = int.Parse(numeroIngresado);
                    return numero;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ingrese un número válido.");
                }
            }
        }

        private static void AltaMiembro()
        {
            try
            {
                Console.WriteLine("Ingrese los datos del nuevo miembro:");

                Console.Write("Correo electrónico: ");
                string mail = Console.ReadLine();

                Console.Write("Contraseña: ");
                string contrasena = Console.ReadLine();

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();

                Console.Write("Fecha de nacimiento (YYYY-MM-DD): ");
                DateTime fechaNacimiento;
                DateTime.TryParse(Console.ReadLine(), out fechaNacimiento);

                bool bloqueado = false;

                Miembro nuevoMiembro = new Miembro(mail, contrasena, nombre, apellido, fechaNacimiento, bloqueado);

                nuevoMiembro.Validar();

                unSistema.AltaMiembro(nuevoMiembro);

                Console.WriteLine("Miembro agregado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el miembro: {ex.Message}");
            }
        }


        private static void ListarPublicacionesPorMiembro(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Error: El correo electrónico no puede estar vacío.");
                return;
            }

            bool correoEncontrado = false;

            foreach (Publicacion publicacion in unSistema._publicaciones)
            {
                if (publicacion.Autor.Mail == email)
                {

                    correoEncontrado = true;
                    Console.WriteLine($"Publicaciones de {email}:\n");

                    if (publicacion is Post)
                    {
                        Console.WriteLine($"[Post] - Id: {publicacion.Id}");
                    }
                    else if (publicacion is Comentario)
                    {
                        Console.WriteLine($"[Comentario] - Id: {publicacion.Id}");
                    }
                }
            }

            if (!correoEncontrado)
            {
                Console.WriteLine("Error: El correo electrónico no existe en los datos o no tiene publicaciones.");
            }
        }

 
        private static void ListarPostsConComentariosDeMiembro(string emailPostConComentario)
        {
            bool correoEncontrado = false;
            bool registrosEncontrados = false;

            Console.WriteLine($"Posts con comentarios realizados por {emailPostConComentario}:\n");

            foreach (Publicacion publicacion in unSistema._publicaciones)
            {
                if (publicacion is Post post)
                {
                    foreach (Comentario comentario in post.Comentarios)
                    {
                        if (comentario.Autor.Mail == emailPostConComentario)
                        {
                            Console.WriteLine($"El post: {post.Titulo} tiene comentarios de {comentario.Autor.Nombre}");
                            correoEncontrado = true;
                            registrosEncontrados = true;
                            break;
                        }
                    }
                }
            }

            if (!correoEncontrado)
            {
                Console.WriteLine($"Error: El correo electrónico {emailPostConComentario} no existe en los datos.");
            }

            if (!registrosEncontrados)
            {
                Console.WriteLine("No se encontraron registros para el correo electrónico proporcionado.");
            }
        }


        private static void ListarPostsEntreFechasYVerificarChar(DateTime fechaInicio, DateTime fechaFin)
        {
            bool registrosEncontrados = false;
            List<Post> postsFiltrados = new List<Post>();

            Console.WriteLine($"Posts realizados entre {fechaInicio.ToShortDateString()} y {fechaFin.ToShortDateString()}:\n");

            foreach (Publicacion publicacion in unSistema._publicaciones)
            {
                if (publicacion is Post post &&
                    publicacion.Fecha >= fechaInicio &&
                    publicacion.Fecha <= fechaFin)
                {
                    registrosEncontrados = true;
                    postsFiltrados.Add(post);
                }
            }

            // Ordenar la lista de posts por título de forma descendente.
            postsFiltrados.Sort((post1, post2) => string.Compare(post2.Titulo, post1.Titulo)); //ChatGPT

            foreach (Post post in postsFiltrados)
            {
                Console.WriteLine(post);

                string contenido = post.Contenido;
                if (contenido.Length > 50)
                {
                    contenido = contenido.Substring(0, 50) + "...";
                }
                Console.WriteLine($"Texto: {contenido}");

                Console.WriteLine();
            }

            if (!registrosEncontrados)
            {
                Console.WriteLine("No se encontraron registros para las fechas seleccionadas.");
            }
        }


        private static bool ValidarFecha(string fechaStr)
        {
            DateTime fecha;

            if (DateTime.TryParse(fechaStr, out fecha))
            {
                DateTime fechaMinima = new DateTime(2000, 1, 1);
                DateTime fechaMaxima = DateTime.Now;

                return (fecha >= fechaMinima && fecha <= fechaMaxima);
            }
            else
            {
                return false;
            }
        }

        private static void ListarPostsEntreFechasDesdeConsola()
        {
            Console.Write("Ingrese la fecha de inicio (YYYY-MM-DD): ");
            string fechaInicioStr = Console.ReadLine();

            Console.Write("Ingrese la fecha de fin (YYYY-MM-DD): ");
            string fechaFinStr = Console.ReadLine();

            DateTime fechaInicio, fechaFin;

            if (!ValidarFecha(fechaInicioStr) || !ValidarFecha(fechaFinStr))
            {
                Console.WriteLine("Fecha no válida o formato incorrecto. Debe estar entre 2000 y la fecha actual en el formato YYYY-MM-DD.");
                return;
            }

            if (!DateTime.TryParse(fechaInicioStr, out fechaInicio) || !DateTime.TryParse(fechaFinStr, out fechaFin))
            {
                Console.WriteLine("Formato de fecha incorrecto.");
                return;
            }

            ListarPostsEntreFechasYVerificarChar(fechaInicio, fechaFin);
        }


        private static List<Miembro> ListarMiembrosConMasPublicaciones()
        {
            int mayor = 0;
            List<Miembro> aux = new List<Miembro>();

            foreach (Miembro mie in unSistema._miembros)
            {
                int cantidad = 0;

                foreach (Publicacion publicacion in unSistema._publicaciones)
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

        private static void MostrarMiembrosConMasPublicaciones(List<Miembro> miembrosConMasPublicaciones)
        {
            if (miembrosConMasPublicaciones.Count > 0)
            {
                Console.WriteLine("Miembros con más publicaciones:");

                foreach (Miembro miembro in miembrosConMasPublicaciones)
                {
                    Console.WriteLine($"{miembro}");
                    Console.WriteLine("--------------");
                }
            }
            else
            {
                Console.WriteLine("No se encontraron miembros con más publicaciones.");
            }
        }


        public static void MostrarListaDeAmigos()
        {
            foreach (Miembro miembro in unSistema._miembros)
            {
                Console.WriteLine($"Amigos de {miembro.Mail}:");
                foreach (Miembro amigo in miembro.ListaAmigos)
                {
                    Console.WriteLine($" {amigo.Nombre} {amigo.Apellido}");
                }
                Console.WriteLine();
            }
        }

        // VERIFICAR FUNCIONAMIENTO PARA VER LOS POST LISTADOS

        //private static void ListarPost()
        //{
        //    List<Publicacion> listaPublicaciones = unSistema._publicaciones;
        //    if (listaPublicaciones.Count == 0)
        //    {
        //        Console.WriteLine("No hay publicaciones.");
        //    }
        //    else
        //    {
        //        foreach (Publicacion unaPubli in listaPublicaciones)
        //        {
        //            Console.WriteLine(unaPubli);
        //        }
        //    }
        //    Console.ReadKey();
        //}
    }
}
