using CyberWatcher.Helper;
using CyberWatcher.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    //sb.Append("-Pn ");
                    //smb enumeration as this port has a poor security track record.
                    //Brute force SSH, Telnet, FTP
                    //sb.Append("--script ssh-brute,telnet-brute,ftp-brute,smb-os-discovery, ");

                    // Test popular ports 
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
                    //myProcess.StartInfo.RedirectStandardOutput = true;
                    //myProcess.StartInfo.RedirectStandardError = true;
                    myProcess.Exited += new EventHandler(myProcess_Exited);
                    await Task.Run(() => myProcess.Start());

                    ////outputs the result as a string to the results page. 
                    //var stdOutSb = new StringBuilder();
                    while (!myProcess.HasExited)
                    {
                        ScanFinished = "Loading";
                        //stdOutSb.Append(myProcess.StandardOutput.ReadToEnd());
                        //stdOutSb.Append(myProcess.StandardError.ReadToEnd());
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

        private void myProcess_Exited(object sender, System.EventArgs e)
        {
            ScanFinished = "Scan Finished";
        }

        

    }
}
