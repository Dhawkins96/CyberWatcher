using CyberWatcher.Model.Bandwidth_Speed;
using CyberWatcher.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace CyberWatcher.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<IMonitor> Monitors { get; }

        public MainWindow()
        {
            InitializeComponent();

            //Starts running the monitors in the background 
            Monitors = new List<IMonitor> {
                new DownloadMonitor(),
                new UploadMonitor()
            };
        }
        public async Task StartMonitoring()
        {
            while (true)
            {
                foreach (var monitor in Monitors)
                {
                    try
                    {
                        await monitor.UpdateAsync();
                    }
                    catch
                    {
                    }
                }

                await Task.Delay(Settings.Default.Interval);
            }
        }
        private async void Window_SourceInitialized(object sender, EventArgs e)
        {
            await StartMonitoring();
        }


    }
}
