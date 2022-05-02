using CyberWatcher.Helper;
using CyberWatcher.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CyberWatcher.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        HomeViewModel MPVM = new HomeViewModel();

        
        public static bool LocalNetworkCheck = false;
        public static bool PortScanCheck = false;
        public static bool ScanRunning = false;

        public HomeView()
        {
            InitializeComponent();
            
            if (ScanRunning == true)
            {
                lblScanOutput.Content = "SCAN STILL RUNNING";
                btnScan.IsEnabled = false;
                pbStatus.Visibility = Visibility.Visible;
                pbStatus.IsIndeterminate = true;
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
                btnScan.IsEnabled = false;
                ScanRunning = true;
                // show status bar when scan is running 
                pbStatus.Visibility = Visibility.Visible;
            }

            MPVM.ScanComplete += ScanCompleteHandler;

            worker.RunWorkerAsync();

        }

        //This is the click button to run the scan 
        private void BtnScan_Click(object sender, RoutedEventArgs e)
        {
            if(LocalNetwork.IsChecked == true || PortScan.IsChecked == true)
            {
                ScanprocessReturn();
            }
            else
            {
                MessageBox.Show("Please select the scan type!");
            }
            
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
                btnScan.IsEnabled = true;
                ScanRunning = false;
               
                lblScanOutput.Content = MPVM.FinishedScan();
            });

        }

        //this is the checkbox for conducting a local scan that will blank out the IP Address textbox
        private void LocalNetwork_Checked(object sender, RoutedEventArgs e)
        {

            if (LocalNetwork.IsChecked == true)
            {
                
                LocalNetworkCheck = true;
            }
            
        }

        private void PortScan_Checked(object sender, RoutedEventArgs e)
        {
            if (PortScan.IsChecked == true)
            {

                PortScanCheck = true;
            }
        }

        
    }
}
