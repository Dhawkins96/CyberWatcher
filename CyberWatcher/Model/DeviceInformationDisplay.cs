using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace CyberWatcher.Model
{
    public class DeviceInformationDisplay : INotifyPropertyChanged
    {
        public class DeviceInfo
        {
            public string DisplayName { get; set; }
            public DeviceClass DeviceClassSelector { get; set; } = DeviceClass.All;
            public DeviceInformationKind Kind { get; set; } = DeviceInformationKind.Unknown;
            public string Selector { get; set; }
        }

        public class DeviceSelectorChoices
        {
            public static List<DeviceInfo> CommonDeviceSelectors => new List<DeviceInfo>()
            {
            // Pre-canned device class selectors
            new DeviceInfo() { DisplayName = "Audio Capture", DeviceClassSelector = DeviceClass.AudioCapture, Selector = null },
            new DeviceInfo() { DisplayName = "Audio Render", DeviceClassSelector = DeviceClass.AudioRender, Selector = null },
            new DeviceInfo() { DisplayName = "Portable Storage", DeviceClassSelector = DeviceClass.PortableStorageDevice, Selector = null },
            new DeviceInfo() { DisplayName = "Video Capture", DeviceClassSelector = DeviceClass.VideoCapture, Selector = null },

            };
            public static DeviceInfo BluetoothPairedOnly =>
                      new DeviceInfo() { DisplayName = "Bluetooth (paired)", Selector = BluetoothDevice.GetDeviceSelectorFromPairingState(true) };
            

            public static List<DeviceInfo> DevicePickerSelectors
            {
                get
                {
                    // Add all selectors that can be used with the DevicePicker
                    List<DeviceInfo> selectors = new List<DeviceInfo>(CommonDeviceSelectors);
                    selectors.Add(BluetoothPairedOnly);
                    

                    return selectors;
                }
            }
            public List<DeviceInfo> FindAllAsyncSelectors =>
                new List<DeviceInfo>(CommonDeviceSelectors)
                {
                BluetoothPairedOnly,
                };

        }
        public DeviceInformationDisplay(DeviceInformation deviceInfoIn)
        {
            DeviceInformation = deviceInfoIn;
        }

        public DeviceInformationKind Kind => DeviceInformation.Kind;
        public string Id => DeviceInformation.Id;
        public string Name => DeviceInformation.Name;
       

        public DeviceInformation DeviceInformation { get; private set; }

        public void Update(DeviceInformationUpdate deviceInfoUpdate)
        {
            DeviceInformation.Update(deviceInfoUpdate);

            OnPropertyChanged("Kind");
            OnPropertyChanged("Id");
            OnPropertyChanged("Name");
            OnPropertyChanged("DeviceInformation");
            

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
