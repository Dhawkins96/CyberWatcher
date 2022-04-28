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
        public static string GetConnection()
        {
            string cs = @"Server = daisy; DataBase = CyberWatcher; Integrated Security = true";
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cs);
            builder.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
            return builder.ConnectionString;
           
        }

    }
}
