using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomerceEs
{
    public static class InterfacciaUtente
    {
        public static void VisualizzaMenu()
        {

            GestioneOrdini go = new GestioneOrdini();

            //go.CheckIsEmpty();
            //Console.ReadKey();
            char sceltaUtente;

            do
            {
                Console.Clear();
                Console.WriteLine("Buongiorno, ha già un account?\n1)Accedi\n2)Registrati");
                ConsoleKeyInfo po = Console.ReadKey();
                sceltaUtente = po.KeyChar;
            } while (sceltaUtente.Equals('1') == false && sceltaUtente.Equals('2') == false);

            Console.Clear();

            bool risultato = true;
            
            do
            {
                string[] credentials = askCredentials().Split(';');
                if (sceltaUtente.Equals('1'))
                {
                    risultato = !go.Login(credentials[0], credentials[1]);
                }
                else
                {
                    risultato = !go.CreateNewAccount(credentials[0], credentials[1]);
                }
                Console.Clear();
                Console.WriteLine(risultato == false ? "Login avvenuto con successo" : "Qualcosa è andato storto");
                Console.WriteLine("\nPremere un tasto per andare avanti...");
                Console.ReadKey();
            } while (risultato);

            

            bool exit = true;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(  "Che cosa desideri fare?" +
                                        "\n1) Lista degli ordini" +
                                        "\n2) Dettaglio Ordine" +
                                        "\n3) Creazione di un nuovo ordine" +
                                        "\n4) Exit");
                    ConsoleKeyInfo po = Console.ReadKey();
                    sceltaUtente = po.KeyChar;
                } while (sceltaUtente.Equals('1') == false && sceltaUtente.Equals('2') == false && sceltaUtente.Equals('3') == false && sceltaUtente.Equals('4') == false);

                switch (sceltaUtente)
                {
                    case '1':
                        List<string> ritorno = go.ListaOrdini();
                        int scelta;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Inserire a quale ordine vuoi accedere");
                            foreach (string item in ritorno)
                            {
                                string[] itemdivisa = item.Split(';');
                                Console.WriteLine(itemdivisa[0] + " " + itemdivisa[1] + " " + itemdivisa[2]);
                            }
                            Console.WriteLine("0 Exit");
                            ConsoleKeyInfo input = Console.ReadKey();
                            scelta = int.Parse(input.KeyChar + "");
                        } while (scelta > ritorno.Count || scelta < 0);
                        Console.Clear();
                        foreach (var value in go.SingoloOrdine(scelta+""))
                        {
                            Console.WriteLine(value);
                        }
                            break;
                    case '2':
                        go.OrdineRecap();
                        break;
                    case '3':
                        go.NuovoOrdine();
                        break;
                    case '4':
                        exit = false;
                        break;
                }


                Console.WriteLine("\nPremere un tasto per continuare");
                Console.ReadKey();
            } while (exit);
            Console.Clear();
        }

        private static string askCredentials()
        {
            Console.Clear();
            Console.WriteLine("Inserire User: ");
            string user = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Inserire Password: ");
            string password = Console.ReadLine();
            return user + ";" + password;
        }
    }
}
