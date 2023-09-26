using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EcomerceEs
{
    public class GestioneOrdini
    {
        private string path = @"data source=.; initial catalog=orders; User ID=sa; Password=951478632;";
        SqlConnection Connesione = null;
        string Utente = null;

        public GestioneOrdini()
        {
            Connesione = new SqlConnection(path);
            Connesione.Close();
        }

        #region Gestione Account
        public void CheckIsEmpty()
        {
            int i;
            string comando =    "SELECT *" +
                                "FROM Utenti";
            this.Connesione.Open();
            using (var orders = new SqlCommand(comando, Connesione).ExecuteReader())
            {

                if (orders.Read() == false)
                {
                    Console.WriteLine("Vista la mancanza di un account ho creato un account Admin");
                    this.Connesione.Close();
                    this.CreateNewAccount("Admin", "Admin");
                }
                else
                {
                    Console.WriteLine("é stato rilevato almeno un account con nome " + orders["login"]);
                    this.Connesione.Close();
                }
            }
        }
            
        public bool Login(string user, string password)
        {
            string query = "SELECT *" +
                           "FROM Utenti";

            using (SqlCommand comando = new SqlCommand(query, this.Connesione))
            {
                this.Connesione.Open();
                var leggi = comando.ExecuteReader();
                while (leggi.Read())
                {
                    if (leggi["login"].Equals(user))
                    {
                        if (leggi["password"].Equals(password))
                        {
                            this.Utente = user;
                            this.Connesione.Close();
                            return true;
                        }
                    }
                }
                this.Connesione.Close();
                return false;
            }

        }

        public bool CreateNewAccount(string user, string password)
        {
            Exception verifica = null;

            string query = "INSERT INTO Utenti (Login, Password)" +
                      "VALUES (@user, @password)";

            SqlCommand command = new SqlCommand(query, Connesione);
            command.Parameters.Add(new SqlParameter("@user", user));
            command.Parameters.Add(new SqlParameter("@password", password));

            try
            {
                this.Connesione.Open();
                command.ExecuteNonQuery();
                //Console.WriteLine("Account creato con successo");
            }
            catch (Exception ex)
            {
                verifica = ex;
                //Console.BackgroundColor = ConsoleColor.Red;
                //Console.WriteLine("\n[System]\tImpossibile creare un nuovo account con queste credenziali\n");
                //Console.ResetColor();
            }
            finally
            {
                this.Connesione.Close();
            }
            return verifica == null ? true : false;
        }

        #endregion

        #region Visualizza e Gestisci Ordini
        
        public List<string> ListaOrdini()
        {
            string query = "SELECT *" +
                           "FROM Orders";

            List<string> ret = new List<string>();

            using (SqlCommand comando = new SqlCommand(query, this.Connesione))
            {
                this.Connesione.Open();
                var leggi = comando.ExecuteReader();
                while (leggi.Read())
                {
                    ret.Add(leggi["orderid"] + ";" + leggi["customer"] + ";" + leggi["orderdate"]);
                }
                this.Connesione.Close();
            }
            return ret;
        }

        public List<string> SingoloOrdine(string id)
        {
            string query = "SELECT customer, item, qty, price " +
                           "FROM Orders " +
                           "JOIN OrderItems ON Orders.orderid = OrderItems.orderid " +
                           "WHERE OrderItems.OrderId = @id"; ;

            List<string> ret = new List<string>();

            SqlCommand comando = new SqlCommand(query, this.Connesione);
            comando.Parameters.Add(new SqlParameter("@id", id));
            using (comando)
            {
                this.Connesione.Open();
                var leggi = comando.ExecuteReader();
                while (leggi.Read())
                {
                    ret.Add(leggi["customer"] + ";" + leggi["item"] + ";" + leggi["qty"] + ";" + leggi["price"]);
                }
                this.Connesione.Close();
            }
            return ret;
        }

        public string OrdineRecap()
        {
            throw new NotImplementedException() ;
        }

        public bool NuovoOrdine() 
        {  
            throw new NotImplementedException () ;
        }

        #endregion

        #region Get
        public string GetUtente()
        {
            return this.Utente;
        }
        #endregion
    }
}
