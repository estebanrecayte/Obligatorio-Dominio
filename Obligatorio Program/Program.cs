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
                    case 6:
                        ListarMiembros();
                        break;
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

                Console.Write("¿Está bloqueado? (true/false): ");
                bool bloqueado = bool.Parse(Console.ReadLine());

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
            Console.WriteLine($"Publicaciones de {email}:");

            foreach (Publicacion publicacion in unSistema._publicaciones)
            {
                if (publicacion.Autor.Mail == email)
                {
                    if (publicacion is Post)
                    {
                        Console.WriteLine($"[Post] - Título: {publicacion.Titulo}, Contenido: {publicacion.Contenido}");
                    }
                    else if (publicacion is Comentario)
                    {
                        Console.WriteLine($"[Comentario] - Título: {publicacion.Titulo}, Contenido: {publicacion.Contenido}");
                    }
                }
            }
        }

        private static void ListarPostsPreCargados()
        {
            Console.WriteLine("Lista de Posts pre-cargados:");

            foreach (Publicacion publicacion in unSistema._publicaciones)
            {
                if (publicacion is Post)
                {
                    Post post = (Post)publicacion;
                    Console.WriteLine($"Título: {post.Titulo}");
                    Console.WriteLine($"Fecha: {post.Fecha.Date.ToShortDateString()}");
                    Console.WriteLine($"Contenido: {post.Contenido}");
                    Console.WriteLine($"Autor: {post.Autor.Nombre} {post.Autor.Apellido}");
                    Console.WriteLine($"Imagen: {post.Imagen}");
                    Console.WriteLine($"Tipo reaccion: {post.TipoReaccion}");
                    Console.WriteLine($"Público: {(post.Publico ? "Sí" : "No")}");
                    Console.WriteLine($"Censurado: {(post.Censurado ? "Sí" : "No")}");

                    Console.WriteLine("Comentarios:");
                    foreach (Comentario comentario in post.Comentarios)
                    {
                        Console.WriteLine($"  - {comentario.Titulo}: {comentario.Contenido}");
                        Console.WriteLine($"     Autor: {comentario.Autor.Nombre} {comentario.Autor.Apellido}");
                        Console.WriteLine($"     Es Privado: {(comentario.EsPrivado ? "Sí" : "No")}");
                    }

                    Console.WriteLine();
                }
            }
        }

        private static void ListarPostsConComentariosDeMiembro(string emailPostConComentario)
        {
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
                            break;
                        }
                    }
                }
            }
        }

        private static void ListarPostsEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            Console.WriteLine($"Posts realizados entre {fechaInicio.ToShortDateString()} y {fechaFin.ToShortDateString()}:\n");

            for (int i = unSistema._publicaciones.Count - 1; i >= 0; i--)
            {
                var publicacion = unSistema._publicaciones[i];

                if (publicacion is Post post &&
                    publicacion.Fecha >= fechaInicio &&
                    publicacion.Fecha <= fechaFin)
                {
                    Console.WriteLine($"ID: {post.Id}");
                    Console.WriteLine($"Fecha: {post.Fecha.ToShortDateString()}");
                    Console.WriteLine($"Título: {post.Titulo}");

                    string contenido = post.Contenido;
                    if (contenido.Length > 50)
                    {
                        contenido = contenido.Substring(0, 50) + "...";
                    }
                    Console.WriteLine($"Texto: {contenido}");

                    Console.WriteLine();
                }
            }
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
            foreach (Miembro miembro in miembrosConMasPublicaciones)
            {
                Console.WriteLine($"Miembro con más publicaciones:");
                Console.WriteLine($"Nombre: {miembro.Nombre}");
                Console.WriteLine($"Apellido: {miembro.Apellido}");
                Console.WriteLine($"Correo Electrónico: {miembro.Mail}");
                Console.WriteLine("--------------");
            }
        }

        private static bool ValidarFecha(DateTime fecha)
        {
            DateTime fechaMinima = new DateTime(1990, 1, 1);
            DateTime fechaMaxima = DateTime.Now;

            return (fecha >= fechaMinima && fecha <= fechaMaxima);
        }

        private static void ListarPostsEntreFechasDesdeConsola()
        {
            Console.Write("Ingrese la fecha de inicio (YYYY-MM-DD): ");
            DateTime fechaInicio;
            if (!DateTime.TryParse(Console.ReadLine(), out fechaInicio) || !ValidarFecha(fechaInicio))
            {
                Console.WriteLine("Fecha de inicio no válida. Debe estar entre 1990 y la fecha actual.");
                return;
            }

            Console.Write("Ingrese la fecha de fin (YYYY-MM-DD): ");
            DateTime fechaFin;
            if (!DateTime.TryParse(Console.ReadLine(), out fechaFin) || !ValidarFecha(fechaFin))
            {
                Console.WriteLine("Fecha de fin no válida. Debe estar entre 1990 y la fecha actual.");
                return;
            }

            ListarPostsEntreFechas(fechaInicio, fechaFin);
        }

        private static void ListarMiembros()
        {
            List<Miembro> listaMiembro = unSistema._miembros;
            if (listaMiembro.Count == 0)
            {
                Console.WriteLine("No hay miembros.");
            }
            else
            {
                foreach (Miembro unMiembro in listaMiembro)
                {
                    Console.WriteLine(unMiembro);
                }
            }
            Console.ReadKey();
        }
    }
}
