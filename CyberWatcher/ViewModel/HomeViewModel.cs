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
        
        private ObservableCollection<string> _hostDetails = new ObservableCollection<string>();
        private ObservableCollection<string> _hostPortDetails = new ObservableCollection<string>();

        
        public event EventHandler ScanComplete;
        public  XmlDocument doc = new XmlDocument();

        public static string NmapScanResults;

        private ICommand _btnScan;
        public ICommand BtnScan
        {
            get
            {
                return _btnScan ?? (_btnScan = new RelayCommand(p => DoScan()));
            }
        }

        public string xmlFile = StaticUtilities.LastScan.Name;

        public HomeViewModel()
        { 
            xmlNmap();
            doc.Load(@"C:\Users\Daisy\source\repos\WPF_CyberWatcher\CyberWatcher\Model\Nmap\NmapOutput\" + xmlFile);
            
            devices = DeviceSelectorChoices.DevicePickerSelectors;
            foreach (DeviceInfo info in devices)
            {
                Collection.Add(info);
            }

        }

        public void DoScan()
        {
            //input in here for command button to run nmap scan!!

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
            doc.Load(@"C:\Users\Daisy\source\repos\WPF_CyberWatcher\CyberWatcher\Model\Nmap\NmapOutput\" + xmlFile);
            Debug.WriteLine(xmlFile);
            XmlNodeList addresses = doc.SelectNodes("/nmaprun/host[status/@state='up']/address");

            for (int i = 0; i < addresses.Count; i++)
            {
                string attrVal = addresses[i].Attributes["addr"].Value;
                string attrVal1 = addresses[i].Attributes["addrtype"].Value;
                if(attrVal1 == "ipv4")
                {
                    Host.Add(attrVal);
                }
                
            }
            
        }

        public void GetHostInfo()
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ns", "file:///C:/Users/Daisy/source/repos/WPF_CyberWatcher/CyberWatcher/ExternalTools/nmap.xsl");

            HostDetails.Clear();
            
            XmlNodeList chosenIP = doc.SelectNodes("descendant::host[address/@addr='" + SelectedHost + "']", nsmgr);
            foreach (XmlNode host in chosenIP)
            {
                XmlNodeList status = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/status", nsmgr);
                for(int i = 0; i < status.Count; i++)
                {
                    string state = status[i].Attributes["state"].Value;
                    HostDetails.Add(state);
                }

                XmlNodeList address = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/address", nsmgr);
                for (int i = 0; i < address.Count; i++)
                {
                    string addr = address[i].Attributes["addr"].Value;
                    string addrType = address[i].Attributes["addrtype"].Value;
                    string addVendor = address[i].Attributes["vendor"]?.Value;
                    if (addrType == "mac")
                    {
                        HostDetails.Add(addr);
                        HostDetails.Add(addVendor);
                    }

                }

                XmlNodeList osMatch = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/os/osmatch", nsmgr);
                for (int i = 0; i < osMatch.Count; i++)
                {
                    string osName = osMatch[i].Attributes["name"].Value;
                    string osAccuracy = osMatch[i].Attributes["accuracy"].Value;
                    HostDetails.Add(osName);
                    HostDetails.Add(osAccuracy);
                }
                XmlNodeList osClass = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/os/osmatch/osclass", nsmgr);
                for (int i = 0; i < osClass.Count; i++)
                {
                    string osVendor = osClass[i].Attributes["vendor"]?.Value;
                    string osFamily = osClass[i].Attributes["osfamily"]?.Value;
                    string osType = osClass[i].Attributes["ostype"]?.Value;
                    string osGen = osClass[i].Attributes["osgen"]?.Value;
                    HostDetails.Add(osVendor);
                    HostDetails.Add(osFamily);
                    HostDetails.Add(osType);
                    HostDetails.Add(osGen);
                }
            }

        }

        public void GetPortInfo()
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ns", "file:///C:/Users/Daisy/source/repos/WPF_CyberWatcher/CyberWatcher/ExternalTools/nmap.xsl");

            HostPortDetails.Clear();

            XmlNodeList chosenIP = doc.SelectNodes("descendant::host[address/@addr='" + SelectedHost + "']", nsmgr);
            foreach (XmlNode host in chosenIP)
            {
                XmlNodeList port = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/ports/port", nsmgr);
                for (int i = 0; i < port.Count; i++) 
                {
                    string portId = port[i].Attributes["portid"].Value;
                    string portProto = port[i].Attributes["protocol"].Value;

                    HostPortDetails.Add(portId);
                    HostPortDetails.Add(portProto);
                }
                XmlNodeList state = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/ports/port/state", nsmgr);
                for(int i = 0; i < state.Count; i++)
                {
                    string portState = state[i].Attributes["state"].Value;

                    HostPortDetails.Add(portState);
                }

                XmlNodeList service = host.SelectNodes("/nmaprun/host[address/@addr='" + SelectedHost + "']/ports/port/service", nsmgr);
                for (int i = 0; i < service.Count; i++)
                {
                    string serName = service[i].Attributes["name"].Value;
                    string serProduct = service[i].Attributes["product"]?.Value;
                    string serVersion = service[i].Attributes["version"]?.Value;

                    HostPortDetails.Add(serName);
                    HostPortDetails.Add(serProduct);
                    HostPortDetails.Add(serVersion);
                }


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

        public ObservableCollection<string> HostDetails
        {
            get { return _hostDetails; }
            set
            {
                _hostDetails = value;
            }
        }

        public ObservableCollection<string> HostPortDetails
        {
            get { return _hostPortDetails; }
            set
            {
                _hostPortDetails = value;
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
