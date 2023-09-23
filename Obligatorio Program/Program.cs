using System;

namespace Obligatorio_Program
{
    class Program
    {
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
                         //Implementar alta de miembro
                        break;
                    case 2:
                        //Implementar listar publicaciones por email de miembro
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
                    //commit para arreglar errores
                }
            }
        }
    }
}