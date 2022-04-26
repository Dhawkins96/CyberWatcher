using CyberWatcher.Helper;
using CyberWatcher.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CyberWatcher.Model.Bandwidth_Speed
{
    public class LatencyMonitor : ObservableObject, IMonitor
    {
        private readonly Ping _ping = new Ping();
        private string _displayValue;

        public string DisplayValue
        {
            get => _displayValue;
            private set => Set(ref _displayValue, value);
        }

        public MonitorIcon Icon { get; } = new MonitorIcon("⟳", "Latency", Brushes.DarkOrange);

        public async Task UpdateAsync()
        {
            async Task<string> GetUpdatedValue()
            {
                var reply = await _ping.SendPingAsync(Settings.Default.PingHost, (int)Settings.Default.Timeout.TotalMilliseconds);
                var latency = reply.RoundtripTime;
                var status = reply.Status;

                return status == IPStatus.Success ? latency.ToString() : "Fail";
            }
            DisplayValue = await GetUpdatedValue();
        }
    }
}
