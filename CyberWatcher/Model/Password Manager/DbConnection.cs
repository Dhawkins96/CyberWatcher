using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberWatcher.Model.Password_Manager
{
    public class DbConnection
    {
        public static SqlConnection connectionString;

        public static SqlConnection DataSource()
        {
            connectionString = new SqlConnection(@"Server = daisy; DataBase = CyberWatcher; Integrated Security = true");
            return connectionString;
        }

        public void connOpen()
        {
            DataSource();
            connectionString.Open();
        }

        public void connClose()
        {
            DataSource();
            connectionString.Close();
        }

       
    }
}
