using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberWatcher.Model.Bandwidth_Speed
{
    public interface IMonitor
    {
        /// <summary>
        /// User-friendly text to show in UI.
        /// </summary>
        string DisplayValue { get; }

        /// <summary>
        /// Icon to show in UI.
        /// </summary>
        MonitorIcon Icon { get; }

        /// <summary>
        /// Updates <see cref="DisplayValue"/> with the latest value.
        /// </summary>
        Task UpdateAsync();
    }
}
