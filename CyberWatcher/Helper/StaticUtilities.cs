using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CyberWatcher.Helper
{
    public static class LocalAddress
    {
        //Get local Ipv4 adress from local machine
        public static string GetLocalIPAddress()
        {

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    PingState.SuccessfulPing = true;
                    PingState.IPAddress = ip.ToString();
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }

    public static class PingState
    {
        public static bool SuccessfulPing { get; set; } = false; //Change name
        public static string IPAddress { get; set; } = string.Empty;
    }
    
    public static class StaticUtilities
    {
        public static int UserID { get; set; }
    }
}
