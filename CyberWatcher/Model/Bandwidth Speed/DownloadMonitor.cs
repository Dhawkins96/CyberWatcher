using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CyberWatcher.Model.Bandwidth_Speed
{
    public class DownloadMonitor : BandwidthMonitorBase
    {
        public DownloadMonitor()
        {
            Icon = new MonitorIcon("↓", "Download", Brushes.Green);
        }

        protected override long GetTotalBytes() =>
             NetworkInterface
             .GetAllNetworkInterfaces()
             .Select(x => x.GetIPStatistics().BytesReceived)
             .Sum();
    }
}
