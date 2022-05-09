using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberWatcherTests.NotNullMethods
{
    [TestClass()]
    public class EncryptedTests
    {
        [TestMethod()]
        public void HashStringTest()
        {
            //Arrange
            var hashString = CyberWatcher.Helper.Encrypt.HashString("TestPassword");

            //Assert
            Assert.IsNotNull(hashString);
        }
        [TestMethod()]
        public void GetHashTest()
        {
            //Arrange
            var getHash = CyberWatcher.Helper.Encrypt.GetHash("TestPassword");

            //Assert
            Assert.IsNotNull(getHash);
        }
    }

    [TestClass()]
    public class StaticUtilitesTests
    {
        [TestMethod()]
        public void LocalAddressTest()
        {
            //Arrange
            string localAddress = CyberWatcher.Helper.LocalAddress.GetLocalIPAddress();
            
            //Assert
            Assert.IsNotNull(localAddress);
        }
    }

    [TestClass()]
    public class BandwidthTests
    {
        [TestMethod()]
        public void DownloadMonitorTest()
        {
            //Arrange
            var downloadMonitor = new CyberWatcher.Model.Bandwidth_Speed.DownloadMonitor();

            //Assert
            Assert.IsNotNull(downloadMonitor);
        }

        [TestMethod()]
        public void UploadMonitorTest()
        {
            //Arrange
            var uploadMonitor = new CyberWatcher.Model.Bandwidth_Speed.UploadMonitor();

            //Assert
            Assert.IsNotNull(uploadMonitor);
        }

    }

    [TestClass()]
    public class NmapTests
    {
        [TestMethod()]
        public void NmapScanInfoTest()
        {
            //Arrange
            var scanInfo = new CyberWatcher.Model.Nmap.ScanInfo();

            //Assert
            Assert.IsNotNull(scanInfo);
        }

        [TestMethod()]
        public void NmapHostDetailsTest()
        {
            //Arrange
            var hostDetails = new CyberWatcher.Model.Nmap.HostDetails();

            //Assert
            Assert.IsNotNull(hostDetails);
        }
        
        [TestMethod()]
        public void NmapListPortTest()
        {
            //Arrange
            var listViewPorts = new CyberWatcher.Model.Nmap.ListViewPorts();

            //Assert
            Assert.IsNotNull(listViewPorts);
        }

        [TestMethod()]
        public void NmapPortServiceTest()
        {
            //Arrange
            var portService = new CyberWatcher.Model.Nmap.PortService();

            //Assert
            Assert.IsNotNull(portService);
        }

        [TestMethod()]
        public void NmapScanTest()
        {
            //Arrange
            var nMapScan = new CyberWatcher.Model.Nmap.NMapScan();

            //Assert
            Assert.IsNotNull(nMapScan);
        }

        [TestMethod()]
        public void NmapOSTest()
        {
            //Arrange
            var osMatches = new CyberWatcher.Model.Nmap.ListOsMatches();

            //Assert
            Assert.IsNotNull(osMatches);
        }
    }

    [TestClass()]
    public class PasswordManagerTests
    {
        [TestMethod()]
        public void DbConnectionTest()
        {
            //Arrange
            var dbConnection = new CyberWatcher.Model.Password_Manager.DbConnection();

            //Assert
            Assert.IsNotNull(dbConnection);
        }

        [TestMethod()]
        public void PasswordGeneratorTest()
        {
            //Arrange
            var passwordGenerate = new CyberWatcher.Model.Password_Manager.PasswordGenerate();

            //Assert
            Assert.IsNotNull(passwordGenerate);
        }

    }

    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void HelpModelTest()
        {
            //Arrange
            var helpModel = new CyberWatcher.Model.User.HelpModel();

            //Assert
            Assert.IsNotNull(helpModel);
        }

        [TestMethod()]
        public void HelpChoicesTest()
        {
            //Arrange
            var helpChoices = new CyberWatcher.Model.User.HelpChoices();

            //Assert
            Assert.IsNotNull(helpChoices);
        }

        [TestMethod()]
        public void DeviceInfoTest()
        {
            //Arrange
            var deviceInfo = new CyberWatcher.Model.DeviceInformationDisplay.DeviceInfo();

            //Assert
            Assert.IsNotNull(deviceInfo);
        }
        public void DeviceChoicesTest()
        {
            //Arrange
            var deviceSelectorChoices = new CyberWatcher.Model.DeviceInformationDisplay.DeviceSelectorChoices();

            //Assert
            Assert.IsNotNull(deviceSelectorChoices);
        }
    }

    [TestClass()]
    public class ViewModelTests
    {
        [TestMethod()]
        public void HomeViewModelTest()
        {
            //Arrange
            var homeViewModel = new CyberWatcher.ViewModel.HomeViewModel();

            //Assert
            Assert.IsNotNull(homeViewModel);
        }
        
        [TestMethod()]
        public void HelpViewModelTest()
        {
            //Arrange
            var helpViewModel = new CyberWatcher.ViewModel.HelpViewModel();

            //Assert
            Assert.IsNotNull(helpViewModel);
        }

        [TestMethod()]
        public void HelpDetailsViewModelTests()
        {
            //Arrange
            var helpDetailsViewModel = new CyberWatcher.ViewModel.HelpDetailsViewModel("Test Title");

            //Assert
            Assert.IsNotNull(helpDetailsViewModel);
        }

        [TestMethod()]
        public void PasswordViewModelTests()
        {
            //Arrange
            var passwordViewModel = new CyberWatcher.ViewModel.PasswordViewModel();

            //Assert
            Assert.IsNotNull(passwordViewModel);
        }

        [TestMethod()]
        public void StartupViewModelTests()
        {
            //Arrange
            var startupViewModel = new CyberWatcher.ViewModel.StartupViewModel();

            //Assert
            Assert.IsNotNull(startupViewModel);
        }

        [TestMethod()]
        public void UserAccountViewModelTests()
        {
            //Arrange
            var userAccountViewModel = new CyberWatcher.ViewModel.UserAccountViewModel();

            //Assert
            Assert.IsNotNull(userAccountViewModel);
        }
    }
}
