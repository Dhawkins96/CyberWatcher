using CyberWatcher.Helper;
using CyberWatcher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CyberWatcher.ViewModel
{
    public class PasswordViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SQLPass> SQLPass { get; set; }

        //change to private/protected for password??
        protected string _txtPassUser;
        protected string _txtPassPassword;
        protected string _txtPassSite;
        protected string _txtPassEmail;

        private SQLPass _selectedRow;

        private ICommand _btnClear;
        private ICommand _btnInsertPass;
        private ICommand _btnDeletePass;
        private ICommand _btnUpdatePass;

        public PasswordViewModel()
        {
            
            //Insert();
            Display();
        }
        public int user = StaticUtilities.UserID;
        private void Display()
        {
            
            using (SqlConnection cn = new SqlConnection(GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("SELECT PassUsername, PassPassword, PassSite, PassEmail FROM [dbo].[PassManDB]" +
                    " WHERE FK_UserID=@UserId", cn);
                cmd.Parameters.AddWithValue("@UserId", user);
                cn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "PassManDB");

                if (SQLPass == null)
                {
                    SQLPass = new ObservableCollection<SQLPass>();
                }
                
                SQLPass.Clear();  
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                     
                    SQLPass.Add(new SQLPass
                    {
                        PassUsername = dr[0].ToString(),
                        PassPassword = dr[1].ToString(),
                        PassSite = dr[2].ToString(),
                        PassEmail = dr[3].ToString(),
                    });
                }

            }
        }


        private void Insert()
        {
            using (SqlConnection cn = new SqlConnection(GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("insert into [dbo].[PassManDB] (FK_UserID, PassUsername, PassPassword, PassSite, PassEmail) values (@UserID, @PassUsername, @PassPassword, @PassSite, @PassEmail)", cn);
                SqlParameter pass = new SqlParameter("@PassPassword", SqlDbType.VarChar);
                cmd.Parameters.Add(pass).Value = TxtPassPassword;

                SqlParameter username = new SqlParameter("@PassUsername", SqlDbType.VarChar);
                cmd.Parameters.Add(username).Value = TxtPassUser;

                SqlParameter email = new SqlParameter("@PassEmail", SqlDbType.VarChar);
                cmd.Parameters.Add(email).Value = TxtPassEmail;

                cmd.Parameters.AddWithValue("@UserID", DbType.Int32).Value = user;
                cmd.Parameters.AddWithValue("@PassSite", DbType.String).Value = TxtPassSite;
                cn.Open();
                cmd.ExecuteNonQuery();

                TxtPassEmail = "";
                TxtPassPassword = "";
                TxtPassSite = "";
                TxtPassUser = "";
                Display();
            }
            Debug.WriteLine("inserted");
        }

        //ADD ARE YOU SURE BOX!!!!!
        private void Delete()
        {
            using (SqlConnection cn = new SqlConnection(GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[PassManDB] WHERE FK_UserID=@UserID AND PassUsername=@PassUsername AND PassPassword=@PassPassword" +
                    " AND PassSite=@PassSite AND PassEmail=@PassEmail", cn);
                SqlParameter pass = new SqlParameter("@PassPassword", SqlDbType.VarChar);
                cmd.Parameters.Add(pass).Value = TxtPassPassword;

                SqlParameter username = new SqlParameter("@PassUsername", SqlDbType.VarChar);
                cmd.Parameters.Add(username).Value = TxtPassUser;

                SqlParameter email = new SqlParameter("@PassEmail", SqlDbType.VarChar);
                cmd.Parameters.Add(email).Value = TxtPassEmail;

                cmd.Parameters.AddWithValue("@UserID", DbType.Int32).Value = user;
                cmd.Parameters.AddWithValue("@PassSite", DbType.String).Value = TxtPassSite;
                cn.Open();
                cmd.ExecuteNonQuery();

                Clear();
                Display();
            }
            Debug.WriteLine("Deleted");
        }

        //DOES NOT WORK USERID CHANGES ALL ENTRIES!! DONT WASTE TIME ON IT!!
        private void Update()
        {
            using (SqlConnection cn = new SqlConnection(GetConnection()))
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[PassManDB] SET PassUsername=@PassUsername, PassPassword=@PassPassword," +
                    " PassSite=@PassSite, PassEmail=@PassEmail WHERE FK_UserID=@UserID", cn);
                SqlParameter pass = new SqlParameter("@PassPassword", SqlDbType.VarChar);
                cmd.Parameters.Add(pass).Value = TxtPassPassword;

                SqlParameter username = new SqlParameter("@PassUsername", SqlDbType.VarChar);
                cmd.Parameters.Add(username).Value = TxtPassUser;

                SqlParameter email = new SqlParameter("@PassEmail", SqlDbType.VarChar);
                cmd.Parameters.Add(email).Value = TxtPassEmail;

                cmd.Parameters.AddWithValue("@UserID", DbType.Int32).Value = user;
                cmd.Parameters.AddWithValue("@PassSite", DbType.String).Value = TxtPassSite;
                cn.Open();
                cmd.ExecuteNonQuery();

                Clear();
                Display();
            }
            Debug.WriteLine("Updated");
        }

        public void Clear()
        {
            TxtPassEmail = "";
            TxtPassPassword = "";
            TxtPassSite = "";
            TxtPassUser = "";
        }

        private string GetConnection()
        {
            string cs = @"Server = daisy; DataBase = CyberWatcher; Integrated Security = true";
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cs);
            builder.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
            return builder.ConnectionString;

        }
        public void GetRowInfo()
        {
            //SQLPass rows = (SQLPass)SelectedRow;
            //TxtPassUser = rows.PassUsername;
            //TxtPassPassword = rows.PassPassword;
            //TxtPassSite = rows.PassSite;
            //TxtPassEmail = rows.PassEmail;
            //OnPropertyChanged(TxtPassUser);
            //OnPropertyChanged(TxtPassPassword);
            //OnPropertyChanged(TxtPassEmail);
            //OnPropertyChanged(TxtPassSite);
        }

        public SQLPass SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                if (_selectedRow != value)
                {
                    _selectedRow = value;
                    GetRowInfo();
                }
            }
        }
        public ICommand BtnInsertPass
        {
            get
            {
                return _btnInsertPass ?? (_btnInsertPass = new RelayCommand(p => Insert()));
            }
        }
        public ICommand BtnDeletePass
        {
            get
            {
                return _btnDeletePass ?? (_btnDeletePass = new RelayCommand(p => Delete()));
            }
        }
        public ICommand BtnUpdatePass
        {
            get
            {
                return _btnUpdatePass ?? (_btnUpdatePass = new RelayCommand(p => Update()));
            }
        }
        public ICommand BtnClear
        {
            get
            {
                return _btnClear ?? (_btnClear = new RelayCommand(p => Clear()));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string TxtPassUser
        {
            get { return _txtPassUser; }
            set
            {
                _txtPassUser = value;
                OnPropertyChanged(TxtPassUser);
            }
        }
        public string TxtPassPassword
        {
            get { return _txtPassPassword; }
            set
            {
                _txtPassPassword = value;
                OnPropertyChanged(TxtPassPassword);
            }
        }
        public string TxtPassSite
        {
            get { return _txtPassSite; }
            set
            {
                _txtPassSite = value;
                OnPropertyChanged(TxtPassSite);
            }
        }
        public string TxtPassEmail
        {
            get { return _txtPassEmail; }
            set
            {
                _txtPassEmail = value;
                OnPropertyChanged(TxtPassEmail);
            }
        }
    }

    public class SQLPass
    {
        public string PassUsername { get; set; }
        public string PassPassword { get; set; }
        public string PassSite { get; set; }
        public string PassEmail { get; set; }
    }
}
