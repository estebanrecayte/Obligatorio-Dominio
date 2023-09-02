using System;

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
                    // Implementar meto2
                    break;
                case 2:
                    // Implementar meto2
                    break;
                case 3:
                    // Implementar meto2
                    break;
                case 4:
                    // Implementar meto2
                    break;
                case 5:
                    // Implementar meto2
                    break;
            }
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
            catch (Exception)
            {
                Console.WriteLine("Ingrese un número válido - PRUEBA.");
            }
        }
    }

}