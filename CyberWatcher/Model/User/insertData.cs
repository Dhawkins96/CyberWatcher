using CyberWatcher.Helper;
using CyberWatcher.Model.Password_Manager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CyberWatcher.Model.User
{
    public class insertData
    {
        DbConnection con = new DbConnection();
        Encrypt en = new Encrypt();

        public string InsertData(string userInsert, string passInsert)
        {
            try
            {
                DbConnection.DataSource(); ;
                con.connOpen();
                SqlCommand command = new SqlCommand(); // "UPDATE [dbo].[UserDB] SET Password=@Password WHERE Username=@Username";
                command.CommandText = "INSERT INTO [dbo].[UserDB] (Username, UserPassword) values (@username, @password)";
                command.Parameters.AddWithValue("@username", userInsert);
                command.Parameters.AddWithValue("@password", Encrypt.HashString(passInsert));
                command.Connection = DbConnection.connectionString;
                command.ExecuteNonQuery();
                MessageBox.Show("Account created", "Information");
                con.connClose();

                return userInsert + passInsert;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information");
                return null;
            }
            finally
            {
                con.connClose();
            }
        }
    }
}
