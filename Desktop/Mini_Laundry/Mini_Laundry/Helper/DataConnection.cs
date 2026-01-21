using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Laundry.Helper
{
    class DataConnection
    {
        private static string connStr = "Data Source=DESKTOP-HQTH6PB\\SQLEXPRESS;Initial Catalog=laundryApp;Integrated Security=True;TrustServerCertificate=True;";
        public static SqlConnection getConn()
        {
            return new SqlConnection(connStr);
        }
        public static DataTable getData(string query)
        {
            using (SqlConnection conn = getConn())
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
                return dt;
            }
        }
        public static void execQuery(string query)
        {
            using (SqlConnection conn = getConn())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
