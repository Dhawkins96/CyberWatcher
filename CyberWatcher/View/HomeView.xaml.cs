using CyberWatcher.Helper;
using CyberWatcher.Model.Bandwidth_Speed;
using CyberWatcher.Model.Nmap;
using CyberWatcher.Properties;
using CyberWatcher.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Threading;

namespace CyberWatcher.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        HomeViewModel MPVM = new HomeViewModel();

        //local scan defined in the nM
        public static bool LocalNetworkCheck = false;
        public static bool ScanRunning = false;

        public HomeView()
        {
            InitializeComponent();

            if(ScanRunning == true)
            {
                lblScanOutput.Content = "SCAN STILL RUNNING";
                Scan.IsEnabled = false;
                pbStatus.Visibility = Visibility.Visible;

            }
        }

        //This process runs the live host and the nmap scan if the ping was a success.
        private void ScanprocessReturn()
        {
            //this is set to true to show generic progress and not a percentage style
            pbStatus.IsIndeterminate = true;

            //start an async progress bar output
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;

            LocalAddress.GetLocalIPAddress(); 

            if (PingState.SuccessfulPing)
            { 
                lblScanOutput.Content += "FULL SCAN IN PROGRESS";
                Scan.IsEnabled = false;
                ScanRunning = true;
                // show status bar when scan is running 
                pbStatus.Visibility = Visibility.Visible;
            }

            MPVM.ScanComplete += ScanCompleteHandler;

            worker.RunWorkerAsync();

        }

        //This is the click button to run the scan 
        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            ScanprocessReturn();
        }

        //Automate the nmap scan to run in the background whilst the progress bar is working 
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MPVM.LoadNmapScanInBackground();
        }

        //Once the scan has succsessfully completed hide the progress bar and inform the user to check for the full results in the results page         
        private void ScanCompleteHandler(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() => {
                pbStatus.Visibility = Visibility.Hidden;
                Scan.IsEnabled = true;
                ScanRunning = false;
                //In case redirect is lagging an error come up informing the user to click results
                lblScanOutput.Content = MPVM.FinishedScan(string.Empty);
            });

        }

        //this is the checkbox for conducting a local scan that will blank out the IP Address textbox
        private void LocalNetwork_Checked_1(object sender, RoutedEventArgs e)
        {

            if (LocalNetwork.IsChecked == true)
            {
                
                LocalNetworkCheck = true;
            }
            else
            {
                //Make the field visable for use.
                //IpAddress.Visibility = System.Windows.Visibility.Visible;
            }
        }

    }
}
