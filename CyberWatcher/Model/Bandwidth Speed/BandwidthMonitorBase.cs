using CyberWatcher.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberWatcher.Model.Bandwidth_Speed    
{
    public abstract class BandwidthMonitorBase : ObservableObject, IMonitor
    {
        private readonly char[] ByteSuffixes = new char[] { 'B', 'K', 'M', 'G' };
        private string _displayValue;
        private long _lastBytes;

        public string DisplayValue
        {
            get => _displayValue;
            private set => Set(ref _displayValue, value);
        }

        public MonitorIcon Icon { get; protected set; }

        public Task UpdateAsync()
        {
            string GetUpdatedValue()
            {
                var bytes = GetBytesDiffAndUpdateLast();

                return Properties.Settings.Default.Bits
                    ? GetReadableByteString(bytes * 8).ToLower()
                    : GetReadableByteString(bytes);
            }

            DisplayValue = GetUpdatedValue();
            return Task.CompletedTask;
        }

        protected abstract long GetTotalBytes();

        private long GetBytesDiffAndUpdateLast()
        {
            var bytes = GetTotalBytes();
            try
            {
                // If the last value hasn't been set or is naturally less than zero (due to interface disconnect, etc), just return 0.
                if (_lastBytes <= 0)
                {
                    return 0;
                }

                // Return the difference in bytes since the last call.
                return bytes - _lastBytes;
            }
            finally
            {
                _lastBytes = bytes;
            }
        }

        /// <summary>
        /// Returns a short user-friendly representation of a number of bytes.
        /// </summary>
        private string GetReadableByteString(double bytes)
        {
            // Not the fastest implementation, but it's only called twice a second at most.

            if (bytes < 0)
                return "<0B";

            var suffixIndex = 0;
            while (bytes >= 1000) // Keep at 3 or less digits.
            {
                bytes /= 1024;
                suffixIndex++;
            }

            var readableBytesString = bytes.ToString("0.0");

            if (readableBytesString.Length > 3)
                readableBytesString = bytes.ToString("0");
            
            return readableBytesString + ByteSuffixes[suffixIndex];
        }
    }
}
