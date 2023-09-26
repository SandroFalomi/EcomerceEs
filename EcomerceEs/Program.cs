using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomerceEs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //GestioneOrdini go = new GestioneOrdini();

            //foreach(var value in go.SingoloOrdine("1"))
            //{
            //    Console.WriteLine(value);
            //}

            InterfacciaUtente.VisualizzaMenu();



            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\n[System]Fine del programma");
            Console.ResetColor();
            Console.ReadKey();  
        }
    }
}
