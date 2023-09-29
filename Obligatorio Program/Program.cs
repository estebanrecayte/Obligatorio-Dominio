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
                        MostrarMiembros();
                        // Implementar listar publicaciones por email de miembro
                        break;
                    case 3:
                        // Implementar listar posts en los que ha realizado comentarios por email de miembro
                        break;
                    case 4:
                        // Implementar listar posts entre dos fechas
                        break;
                    case 5:
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


        private static void MostrarMiembros()
        {
            Console.WriteLine("Lista de Administrador:");

            foreach (Administrador administrador in unSistema._administradores)
            {
                //Console.WriteLine($"Nombre: {miembro.Nombre}");
                //Console.WriteLine($"Apellido: {miembro.Apellido}");
                Console.WriteLine($"Correo electrónico: {administrador.Mail}");
                //Console.WriteLine($"Fecha de Nacimiento: {miembro.FechaNacimiento}");
                //Console.WriteLine($"Bloqueado: {miembro.Bloqueado}");
                //Console.WriteLine();
            }
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

    }
}
