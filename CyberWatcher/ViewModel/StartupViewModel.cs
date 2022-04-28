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
    public class StartupViewModel : INotifyPropertyChanged
    {
        public string _txtUserLog;
        public string _txtPassLog;
        public string _txtUserReg;
        public string _txtPassReg;
        public string _txtEmailReg;

        private ICommand _btnLogin;
        private ICommand _btnReg;

        public StartupViewModel()
        {
            
        }

        public void Login(string username, string password)
        {
            using (SqlConnection cn = new SqlConnection(DbConnection.GetConnection()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select PK_UserID from [dbo].[UserDB] WHERE Username=@Username AND UserPassword=@Password", cn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", Encrypt.HashString(password));

                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        StaticUtilities.UserID = Convert.ToInt32(reader[0]);
                        
                        MainWindow main = new MainWindow();
                        main.Show();
                        Application.Current.MainWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error", "Information");
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
        public void RegUser()
        {
            using (SqlConnection cn = new SqlConnection(DbConnection.GetConnection()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[UserDB](Username, UserPassword, UserEmail) values (@Username, @Password, @Email)", cn);
                    cmd.Parameters.AddWithValue("@Username", TxtUserReg);
                    cmd.Parameters.AddWithValue("@Password", Encrypt.HashString(TxtPassReg));

                    SqlParameter email = new SqlParameter("@Email", SqlDbType.VarChar);
                    cmd.Parameters.Add(email).Value = TxtEmailReg;
                    cn.Open();

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account created", "Information");

                    Login(TxtUserReg, TxtPassReg);
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
        public ICommand BtnReg
        {
            get
            {
                return _btnReg ?? (_btnReg = new RelayCommand(p => RegUser()));
            }
        }
        public ICommand BtnLogin
        {
            get
            {
                return _btnLogin ?? (_btnLogin = new RelayCommand(p => Login(TxtUserLog, TxtPassLog)));
            }
        }
        public string TxtUserLog
        {
            get { return _txtUserLog; }
            set
            {
                _txtUserLog = value;
                OnPropertyChanged(TxtUserLog);
            }
        }
        public string TxtPassLog
        {
            get { return _txtPassLog; }
            set
            {
                _txtPassLog = value;
                OnPropertyChanged(TxtPassLog);
            }
        }
        public string TxtUserReg
        {
            get { return _txtUserReg; }
            set
            {
                _txtUserReg = value;
                OnPropertyChanged(TxtUserReg);
            }
        }
        public string TxtPassReg
        {
            get { return _txtPassReg; }
            set
            {
                _txtPassReg = value;
                OnPropertyChanged(TxtPassReg);
            }
        }
        public string TxtEmailReg
        {
            get { return _txtEmailReg; }
            set
            {
                _txtEmailReg = value;
                OnPropertyChanged(TxtEmailReg);
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
