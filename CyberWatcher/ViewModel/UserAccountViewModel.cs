using CyberWatcher.Helper;
using CyberWatcher.Model.Password_Manager;
using CyberWatcher.View;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace CyberWatcher.ViewModel
{
    public class UserAccountViewModel : INotifyPropertyChanged
    {
        private int UserID;

        protected string _lblUsername;
        protected string _lblUserEmail;
        private ICommand _btnLogout;
        private ICommand _btnDeleteUser;

        public UserAccountViewModel()
        {
            GetUserInfo();
            UserID = StaticUtilities.UserID;

        }

        public void GetUserInfo()
        {
            using (SqlConnection cn = new SqlConnection(DbConnection.GetConnection()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Username, UserEmail FROM [dbo].[UserDB]" +
                    " WHERE PK_UserID=@UserId", cn);
                    cmd.Parameters.AddWithValue("@UserId", StaticUtilities.UserID);
                    cn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "UserDB");
                    cmd.ExecuteNonQuery();

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        LblUsername = dr[0].ToString();
                        LblUserEmail = dr[1].ToString();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information");

                }
                finally
                {
                    cn.Close();
                }
            }
        }

        private void DeleteUser()
        {
            using (SqlConnection cn = new SqlConnection(DbConnection.GetConnection()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[UserDB] WHERE PK_UserID=@UserID", cn);
                    cmd.Parameters.AddWithValue("@UserID", DbType.Int32).Value = StaticUtilities.UserID;
                    
                    cn.Open();
                    cmd.ExecuteNonQuery();

                    Logout();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information");

                }
                finally
                {
                    cn.Close();
                }
            }
            Debug.WriteLine("Deleted");
        }

        public void Logout()
        {
            if (UserID != 0)
            {
                StaticUtilities.UserID = 0;
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();

            }
            else
            {
                MessageBox.Show("Error", "User Not Logged in");
            }
            
        }

        public ICommand BtnDeleteUser
        {
            get
            {
                return _btnDeleteUser ?? (_btnDeleteUser = new RelayCommand(p => DeleteUser()));
            }
        }

        public ICommand BtnLogout
        {
            get
            {
                return _btnLogout ?? (_btnLogout = new RelayCommand(p => Logout()));
            }
        }

        public string LblUsername
        {
            get { return _lblUsername; }
            set
            {
                _lblUsername = value;
                OnPropertyChanged(LblUsername);
            }
        }
        public string LblUserEmail
        {
            get { return _lblUserEmail; }
            set
            {
                _lblUserEmail = value;
                OnPropertyChanged(LblUserEmail);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
