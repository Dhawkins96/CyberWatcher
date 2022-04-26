using CyberWatcher.Helper;
using CyberWatcher.Model.Password_Manager;
using CyberWatcher.View;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace CyberWatcher.ViewModel
{
    public class StartupViewModel : INotifyPropertyChanged
    {
        DbConnection con = new DbConnection();
        public string _txtUserLog;
        public string _txtPassLog;
        public string _txtUserReg;
        public string _txtPassReg;

        private ICommand _btnLogin;
        private ICommand _btnReg;

        public StartupViewModel()
        {
            
        }
        public ICommand BtnReg
        {
            get
            {
                return _btnReg ?? (_btnReg = new RelayCommand(p => Registrator()));
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

        public void Login(string username, string password)
        {
            try
            {
                DbConnection.DataSource();
                con.connOpen();
                SqlCommand command = new SqlCommand();
                command.CommandText = "Select PK_UserID from [dbo].[UserDB] WHERE Username=@Username AND UserPassword=@Password";
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", Encrypt.HashString(password));
                command.Connection = DbConnection.connectionString;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    StaticUtilities.UserID = Convert.ToInt32(reader[0]);
                    Debug.WriteLine(StaticUtilities.UserID);
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
                con.connClose();
            }

        }
        public void Registrator()
        {
            try
            {
                DbConnection.DataSource(); ;
                con.connOpen();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO [dbo].[UserDB] (Username, UserPassword) values (@username, @password)";
                command.Parameters.AddWithValue("@username", TxtUserReg);
                command.Parameters.AddWithValue("@password", Encrypt.HashString(TxtPassReg));
                command.Connection = DbConnection.connectionString;
                command.ExecuteNonQuery();
                MessageBox.Show("Account created", "Information");
                con.connClose();
                Login(TxtUserReg, TxtPassReg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information");
                
            }
            finally
            {
                con.connClose();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
