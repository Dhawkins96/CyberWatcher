using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CyberWatcher.Model.Bandwidth_Speed
{
    public class UploadMonitor : BandwidthMonitorBase
    {
        public UploadMonitor()
        {
            Icon = new MonitorIcon("↑", "Upload", Brushes.Blue);
        }

        protected override long GetTotalBytes() =>
             NetworkInterface
             .GetAllNetworkInterfaces()
             .Select(x => x.GetIPStatistics().BytesSent)
             .Sum();
    }
}
