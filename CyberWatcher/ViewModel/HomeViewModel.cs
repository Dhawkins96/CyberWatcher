using CyberWatcher.Helper;
using CyberWatcher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Windows.Devices.Enumeration;
using static CyberWatcher.Model.DeviceInformationDisplay;
using CyberWatcher.Model.Nmap;
using System.IO;
using System.Linq;
using System.Xml;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Windows;
using CyberWatcher.Model.Password_Manager;
using System.Data;
using System.Windows.Data;

namespace CyberWatcher.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DeviceInfo> _collection = new ObservableCollection<DeviceInfo>(); //change name later
        private ObservableCollection<DeviceInformationDisplay> _resultCollection = new ObservableCollection<DeviceInformationDisplay>();
        private List<DeviceInfo> devices = new List<DeviceInfo>();
        private DeviceInfo _selectedDevice;

        private ObservableCollection<string> _host = new ObservableCollection<string>();
        private string _selectedHost;
        private ListViewPorts _selectedPort;

        private ObservableCollection<HostDetails> _ListHostDetails = new ObservableCollection<HostDetails>();
        private ObservableCollection<PortService> _ListhostPortDetails = new ObservableCollection<PortService>();
        private ObservableCollection<ScanInfo> _ListScanDetails = new ObservableCollection<ScanInfo>();

        public string xmlFile;
        public event EventHandler ScanComplete;
        public  XmlDocument doc = new XmlDocument();
        private ObservableCollection<ListViewPorts> _items = new ObservableCollection<ListViewPorts>();

        public ObservableCollection<ListViewPorts> Items
        {
            get { return _items; }
            set 
            { 
                _items = value;
                
            }
        }

        public static string NmapScanResults;

        public HomeViewModel()
        {
            GetScanInfo();
            GetUserNmap();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Items);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("states");
            view.GroupDescriptions.Add(groupDescription);



            devices = DeviceSelectorChoices.DevicePickerSelectors;
            foreach (DeviceInfo info in devices)
            {
                Collection.Add(info);
            }
            SelectedDevice = Collection.First();
        }

        public void GetUserNmap()
        {
            
            using (SqlConnection cn = new SqlConnection(DbConnection.GetConnection()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select NmapName From [dbo].[XMLNmapDB] WHERE " +
                        "PK_NmapID =(Select Max(PK_NmapID) FROM [dbo].[XMLNmapDB] WHERE FK_UserId=@UserID)", cn);

                    cmd.Parameters.AddWithValue("@UserID", DbType.Int32).Value = StaticUtilities.UserID;
                    
                    cn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "UserDB");
                    cmd.ExecuteNonQuery();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            xmlFile = dr[0].ToString();
                        }
                    if(xmlFile == null)
                    {
                        Debug.WriteLine("No Scans found");
                    }
                    else
                    {
                        
                        doc.Load(@"C:\Users\Daisy\source\repos\WPF_CyberWatcher\CyberWatcher\Model\Nmap\NmapOutput\" + xmlFile);
                        xmlNmap();
                        GetScanInfo();
                        Debug.WriteLine(xmlFile);
                    }

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
           
        }
        

       


        public string LoadNmapScanInBackground()
        {
            if (PingState.SuccessfulPing)
            {
                var DisplayNmap = new NMapScan();
                NmapScanResults = DisplayNmap.RunScan(PingState.IPAddress).Result;
                

                ScanComplete.Invoke(this, null);
                return NmapScanResults.ToString();
            }

            else
            {
                return "Ping unsuccessful";

            }
        }
        public string FinishedScan()
        {
            StaticUtilities.LastScan = new DirectoryInfo(@"C:\Users\Daisy\source\repos\WPF_CyberWatcher\CyberWatcher\Model\Nmap\NmapOutput").GetFiles().OrderByDescending(o => o.LastWriteTime).FirstOrDefault();

            return NmapScanResults;

        }


        //change name of mathod!!
        public void xmlNmap()
        {
            XmlNodeList addresses = doc.SelectNodes("/nmaprun/host[status/@state='up']/address");

            for (int i = 0; i < addresses.Count; i++)
            {
                string addr = addresses[i].Attributes["addr"].Value;
                string addrType = addresses[i].Attributes["addrtype"].Value;
                if(addrType == "ipv4")
                {
                    Host.Add(addr);
                }
                
            }
            SelectedHost = Host.First();
        }
       

        public void GetHostInfo()
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ns", "file:///C:/Users/Daisy/source/repos/WPF_CyberWatcher/CyberWatcher/ExternalTools/nmap.xsl");

            ListHostDetails.Clear();
            HostDetails hd = new HostDetails();
            XmlNodeList chosenIP = doc.SelectNodes("descendant::host[address/@addr='" + SelectedHost + "']", nsmgr);
            foreach (XmlNode host in chosenIP)
            {
                XmlNodeList status = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/status", nsmgr);
                for(int i = 0; i < status.Count; i++)
                {
                    string state = status[i].Attributes["state"].Value;

                    hd.HostState = state;
                    
                }

                XmlNodeList address = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/address", nsmgr);
                for (int i = 0; i < address.Count; i++)
                {
                    string addr = address[i].Attributes["addr"].Value;
                    string addrType = address[i].Attributes["addrtype"].Value;
                    string addVendor = address[i].Attributes["vendor"]?.Value;
                    if (addrType == "mac")
                    {
                        hd.HostMac = addr;
                        hd.HostVendor = addVendor;
                                                
                    }

                }

                XmlNodeList osMatch = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/os/osmatch", nsmgr);
                for (int i = 0; i < osMatch.Count; i++)
                {
                    string osName = osMatch[i].Attributes["name"].Value;
                    string osAccuracy = osMatch[i].Attributes["accuracy"].Value;
                    hd.HostOsName = osName;
                    hd.HostOsAccuracy = osAccuracy;
                   
                }
                XmlNodeList osClass = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/os/osmatch/osclass", nsmgr);
                for (int i = 0; i < osClass.Count; i++)
                {
                    string osVendor = osClass[i].Attributes["vendor"]?.Value;
                    string osFamily = osClass[i].Attributes["osfamily"]?.Value;
                    string osType = osClass[i].Attributes["ostype"]?.Value;
                    string osGen = osClass[i].Attributes["osgen"]?.Value;
                    hd.HostOsVendor = osVendor;
                    hd.HostOsFamily = osFamily;
                    hd.HostOsType = osType;
                    hd.HostOsGen = osGen;
                    
                }
                ListHostDetails.Add(hd);
                
                
            }

        }
       
        public void GetPortInfo()
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ns", "file:///C:/Users/Daisy/source/repos/WPF_CyberWatcher/CyberWatcher/ExternalTools/nmap.xsl");

            Items.Clear();
            
            XmlNodeList chosenIP = doc.SelectNodes("descendant::host[address/@addr='" + SelectedHost + "']", nsmgr);
            foreach (XmlNode host in chosenIP)
            {
                XmlNodeList openPort = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/ports/port[state/@state = 'open']", nsmgr);
                for (int i = 0; i < openPort.Count; i++) 
                {
                    Items.Add(new ListViewPorts()
                    {
                        PortID = openPort[i].Attributes["portid"].Value,
                        PortProtocol = openPort[i].Attributes["protocol"].Value,
                        states = States.Open
                        
                    });
                    
                }
                XmlNodeList closePort = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/ports/port[state/@state = 'closed']", nsmgr);
                for (int i = 0; i < closePort.Count; i++)
                {
                    Items.Add(new ListViewPorts()
                    {
                        PortID = closePort[i].Attributes["portid"].Value,
                        PortProtocol = closePort[i].Attributes["protocol"].Value,
                        states = States.Close

                    });

                }
                XmlNodeList filteredPort = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/ports/port[state/@state = 'filtered']", nsmgr);
                for (int i = 0; i < filteredPort.Count; i++)
                {
                    Items.Add(new ListViewPorts()
                    {
                        PortID = filteredPort[i].Attributes["portid"].Value,
                        PortProtocol = filteredPort[i].Attributes["protocol"].Value,
                        states = States.Filtered

                    });

                }
            }
            ListHostPortDetails.Clear();
        }

        public void GetScanInfo()
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ns", "file:///C:/Users/Daisy/source/repos/WPF_CyberWatcher/CyberWatcher/ExternalTools/nmap.xsl");
            ScanInfo sc = new ScanInfo();
            XmlNodeList scan = doc.SelectNodes("/nmaprun/runstats/finished", nsmgr);

            ListScanDetails.Clear();

            for (int i = 0; i < scan.Count; i++)
            {
                string time = scan[i].Attributes["timestr"].Value;
                string sum = scan[i].Attributes["summary"].Value;
                sc.ScanTime = time;
                sc.Summary = sum;

            }
            ListScanDetails.Add(sc);
        }
       
        public void GetServiceInfo()
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ns", "file:///C:/Users/Daisy/source/repos/WPF_CyberWatcher/CyberWatcher/ExternalTools/nmap.xsl");

            PortService ps = new PortService();
            ListHostPortDetails.Clear();
            XmlNodeList chosenIP = doc.SelectNodes("descendant::host[address/@addr='" + SelectedHost + "']", nsmgr);
            foreach (XmlNode host in chosenIP)
            {
               
                XmlNodeList service = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/ports/port[@portid='" + SelectedPort.PortID + "']/service", nsmgr);
                for (int i = 0; i < service.Count; i++)
                {
                    string serName = service[i].Attributes["name"].Value;
                    string serProduct = service[i].Attributes["product"]?.Value;
                    string serVersion = service[i].Attributes["version"]?.Value;

                    ps.ServiceName = serName;
                    ps.Product = serProduct;
                    ps.Verison = serVersion;
                }
                ListHostPortDetails.Add(ps);
            }

        }

        public async void GetDeviceInfo()
        {
            DeviceInfo deviceSelectorInfo;
            DeviceInformationCollection deviceInfoCollection;
            ResultCollection.Clear();
            deviceSelectorInfo = (DeviceInfo)SelectedDevice;

            if (null == deviceSelectorInfo.Selector)
            {
                // If the a pre-canned device class selector was chosen, call the DeviceClass overload
                deviceInfoCollection = await DeviceInformation.FindAllAsync(deviceSelectorInfo.DeviceClassSelector);
            }
            else
            {
                // Use AQS string selector from dynamic call to a device api's GetDeviceSelector call
                deviceInfoCollection = await DeviceInformation.FindAllAsync(
                    deviceSelectorInfo.Selector,
                    null // don't request additional properties for this sample
                    );
            }


            foreach (DeviceInformation deviceInfo in deviceInfoCollection)
            {
                ResultCollection.Add(new DeviceInformationDisplay(deviceInfo));
            }
            if (ResultCollection == null)
            {

            }

        }

        public ObservableCollection<string> Host
        {
            get { return _host; }
            set
            {
                _host = value;
            }
        }
        

        public ObservableCollection<HostDetails> ListHostDetails
        {
            get { return _ListHostDetails; }
            set
            {
                _ListHostDetails = value;
            }
        }

        public ObservableCollection<PortService> ListHostPortDetails
        {
            get { return _ListhostPortDetails; }
            set
            {
                _ListhostPortDetails = value;
            }
        }
        public ObservableCollection<ScanInfo> ListScanDetails
        {
            get { return _ListScanDetails; }
            set
            {
                _ListScanDetails = value;
            }
        }

        public string SelectedHost
        {
            get { return _selectedHost; }
            set
            {
                if(_selectedHost != value)
                {
                    _selectedHost = value;
                    
                    GetHostInfo();
                    GetPortInfo();
                    
                }
            }
        }
        public ListViewPorts SelectedPort
        {
            get 
            { 
               
                return _selectedPort; 
            }
            set
            {
                if (_selectedPort != value)
                {
                    _selectedPort = value;
                    if(SelectedPort != null)
                    {
                        
                        GetServiceInfo();
                    }
                }
            }
        }

        public DeviceInfo SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                if (_selectedDevice != value)
                {
                    _selectedDevice = value;
                    GetDeviceInfo();
                }
            }
        }

        public ObservableCollection<DeviceInfo> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
            }
        }

        public ObservableCollection<DeviceInformationDisplay> ResultCollection
        {
            get { return _resultCollection; }
            set
            {
                _resultCollection = value;
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    
}
