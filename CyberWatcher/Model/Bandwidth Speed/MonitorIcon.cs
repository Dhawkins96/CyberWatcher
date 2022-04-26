using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CyberWatcher.Model.Bandwidth_Speed
{
    public class MonitorIcon
    {
        public MonitorIcon(string text, string name, SolidColorBrush brush)
        {
            Text = text;
            Name = name;
            Brush = brush;
        }

        /// <summary>
        /// String-based icon.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Name to use in tooltip.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Icon text brush.
        /// </summary>
        public SolidColorBrush Brush { get; }
    }
}
