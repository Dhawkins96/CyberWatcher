using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberWatcher.Model.Nmap
{

    public class HostDetails
    {
        public string HostState { get; set; }
        public string HostMac { get; set; }
        public string HostVendor { get; set; }
        
    }

    public class ListOsMatches
    {
        public string HostOsName { get; set; }
        public string HostOsAccuracy { get; set; }
        public string HostOsVendor { get; set; }
        public string HostOsFamily { get; set; }
        public string HostOsGen { get; set; }
    }
    
    public class ListViewPorts
    {
        public string PortID { get; set; }
        public string PortProtocol { get; set; }
        public States states { get; set; }
    }

    public enum States
    {
        Open,
        Close,
        Filtered,
    }

    public class PortService
    {
        
        public string ServiceName { get; set; }
        public string Product { get; set; }
        public string Verison { get; set; }

    }

    public class ScanInfo
    {
        public string ScanTime { get; set; }
        public string Summary { get; set; }
    }
}
