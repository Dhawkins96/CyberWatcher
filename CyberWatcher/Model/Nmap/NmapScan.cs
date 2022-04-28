using CyberWatcher.Helper;
using CyberWatcher.Model.Password_Manager;
using CyberWatcher.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace CyberWatcher.Model.Nmap
{
    public class NMapScan
    {
        public static string ScanFinished;

        public async Task<string> RunScan(string IpAddress)
        {
            try
            {
                //LocalAddress localAddress = new LocalAddress();

                using (Process myProcess = new Process())
                {
                    
                    myProcess.StartInfo.UseShellExecute = false;
                    //This will use the nmap external tool that is stored in the External Tools folder
                    //Running the nmap tool 
                    myProcess.StartInfo.FileName = @"C:\Users\Daisy\source\repos\WPF_CyberWatcher\CyberWatcher\ExternalTools\nmap.exe";
                    //Running the options
                    var sb = new StringBuilder();
                    // Fast scan mode 
                    sb.Append("-T4 ");

                    //full enumeration scan 
                    //sb.Append("-p- ");
                    sb.Append("-A ");
                    sb.Append("-v ");
                    
                    // Saves to XML 
                    sb.Append("-oX " + @"C:\Users\Daisy\source\repos\WPF_CyberWatcher\CyberWatcher\Model\Nmap\NmapOutput\Output-%T%D.xml ");

                    //local network scan takes local ip from dns and scans class subnet
                    if (HomeView.LocalNetworkCheck == true)
                    {
                        sb.Append(IpAddress + "/24");
                    }
                    
                    //add the arguments to the end of the nmap scan
                    myProcess.StartInfo.Arguments = sb.ToString();
                    //hide the window to avoid a popup
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Exited += new EventHandler(myProcess_Exited);
                    await Task.Run(() => myProcess.Start());

                    while (!myProcess.HasExited)
                    {
                        ScanFinished = "Loading";
                    }

                    return ScanFinished;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        private void Insert()
        {
            StaticUtilities.LastScan = new DirectoryInfo(@"C:\Users\Daisy\source\repos\WPF_CyberWatcher\CyberWatcher\Model\Nmap\NmapOutput").GetFiles().OrderByDescending(o => o.LastWriteTime).FirstOrDefault();
            using (SqlConnection cn = new SqlConnection(DbConnection.GetConnection()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into [dbo].[XMLNmapDB] (FK_UserID, NmapName) values (@UserID, @NmapName)", cn);

                    cmd.Parameters.AddWithValue("@UserID", DbType.Int32).Value = StaticUtilities.UserID;
                    cmd.Parameters.AddWithValue("@NmapName", DbType.String).Value = StaticUtilities.LastScan.Name;
                    cn.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SQL Database Error");

                }
                finally
                {
                    cn.Close();
                }
            }
            Debug.WriteLine("inserted");
        }

        private void myProcess_Exited(object sender, EventArgs e)
        {
            Insert();
            ScanFinished = "Scan Finished";
        }

        

    }
}
