using CyberWatcher.Helper;
using CyberWatcher.Model.Password_Manager;
using CyberWatcher.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CyberWatcher.Model.User
{
    public class SelectItem
    {
        DbConnection con = new DbConnection();
        
        public string SelectData(string userInsert, string passInsert)
        {
            try
            {
                DbConnection.DataSource();
                con.connOpen();
                SqlCommand command = new SqlCommand();
                command.CommandText = "Select PK_UserID from [dbo].[UserDB] WHERE Username=@Username AND UserPassword=@Password";
                command.Parameters.AddWithValue("@Username", userInsert);
                command.Parameters.AddWithValue("@Password", Encrypt.HashString(passInsert));
                command.Connection = DbConnection.connectionString;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    StaticUtilities.UserID = Convert.ToInt32(reader[0]);
                    Debug.WriteLine(StaticUtilities.UserID);
                    MainWindow main = new MainWindow();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Error", "Information");
                }

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
