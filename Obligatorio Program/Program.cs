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
                        AltaMiembro();// Recordar hacer clase abstracta en usuario
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
                        Console.Write("Ingrese la fecha de inicio (YYYY-MM-DD): ");
                        DateTime fechaInicio;
                        DateTime.TryParse(Console.ReadLine(), out fechaInicio);

                        Console.Write("Ingrese la fecha de fin (YYYY-MM-DD): ");
                        DateTime fechaFin;
                        DateTime.TryParse(Console.ReadLine(), out fechaFin);

                        ListarPostsEntreFechas(fechaInicio, fechaFin);
                        break;
                    case 5:
                        ListarPostsPreCargados();
                        // Implementar obtener miembros con más publicaciones
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

                // Crear una instancia de Miembro con los datos ingresados.
                Miembro nuevoMiembro = new Miembro(mail, contrasena, nombre, apellido, fechaNacimiento, bloqueado);

                // Llamar al método Validar() para realizar validaciones adicionales.
                nuevoMiembro.Validar();

                // Si llegamos aquí sin excepciones, agregar el nuevo miembro al sistema.
                unSistema.AltaMiembro(nuevoMiembro); // Debes implementar este método en tu clase Sistema.

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
                    Post post = (Post)publicacion; // Convierte la publicación en un Post
                    Console.WriteLine($"Título: {post.Titulo}");
                    Console.WriteLine($"Fecha: {post.Fecha.Date.ToShortDateString()}"); // para pasar solo la fecha
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
                            break; // Sal del bucle interno una vez que encuentres un comentario del miembro
                        }
                    }
                }
            }
        }


        private static void ListarPostsEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            Console.WriteLine($"Posts realizados entre {fechaInicio.ToShortDateString()} y {fechaFin.ToShortDateString()}:\n");

            // Recorre la lista en sentido inverso
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

    }
}