using System.Drawing;

namespace Calculadora
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var matematicas = new Matematicas();
            Console.WriteLine("Hello, World!");
            Console.WriteLine($"Suma: {matematicas.Sumar(5, 7)}");

        }
    }
}
