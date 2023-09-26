using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomerceEs
{
    public static class pooo
    {
        public static void metodo()
        {
            Console.WriteLine("HELLO");
            //            string connStr = "data source=.\\SQLEXPRESS; " +
            string connStr = @"data source=.; initial catalog=orders; User ID=sa; Password=951478632;";
            SqlConnection con = new SqlConnection(connStr);
            using (con)
            {
                Console.WriteLine($"connessione = {con}");
                con.Open();
                Console.WriteLine("connessione aperta");
                /////////////////// QUERY SCALARE
                string q = "select count(*) from orders";
                var cmd = new SqlCommand(q, con);
                var n = cmd.ExecuteScalar();
                Console.WriteLine($"ci sono {n} ordini");
                /////////////////// QUERY READER
                cmd = new SqlCommand("select * from orders", con);
                using (var orders = cmd.ExecuteReader())
                {
                    while (orders.Read())
                        Console.WriteLine("{0} {1}", orders["orderid"], orders["customer"]);
                }
                /////////////////// QUERY READER
                string user = "Jack";
                cmd = new SqlCommand($"select * from orders where customer = '{user}'", con);
                using (var orders = cmd.ExecuteReader())
                {
                    while (orders.Read())
                        Console.WriteLine("->{0} {1}", orders["orderid"], orders["customer"]);
                }
                /////////////////// PARAMETRI
                Console.WriteLine("con parametro");
                cmd = new SqlCommand("select * from orders where customer = @user", con);
                cmd.Parameters.Add(new SqlParameter("@user", user));
                using (var orders = cmd.ExecuteReader())
                {
                    while (orders.Read())
                        Console.WriteLine("->{0} {1}", orders["orderid"], orders["customer"]);
                }
                /////////////////// DML
                SqlTransaction tr = null;
                try
                {
                    tr = con.BeginTransaction();
                    Console.WriteLine("DML UPDATE");
                    cmd = new SqlCommand("update orderitems set price=price+100 where orderid=@order", con, tr);
                    cmd.Parameters.Add(new SqlParameter("@order", 1));
                    Console.WriteLine($"ho modificato {cmd.ExecuteNonQuery()} righe");
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    tr.Rollback();
                }
                /////////////////// ADAPTER
                SqlDataAdapter a = new SqlDataAdapter("select * from customers", con);
                DataSet model = new DataSet();
                a.Fill(model, "customers");
                Console.WriteLine("\ncarico il DataSet dei customer");
                foreach (DataRow c in model.Tables["customers"].Rows)
                    Console.WriteLine("{0} {1}", c["customer"], c["country"]);
                foreach (var col in model.Tables["customers"].Columns)
                    Console.WriteLine("colonna:" + col);

            }
            Console.WriteLine("connessione chiusa");
            Console.ReadLine();
        }
    }
}
