using CyberWatcher.Model.Password_Manager;
using CyberWatcher.ViewModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberWatcher.View
{
    /// <summary>
    /// Interaction logic for PasswordView.xaml
    /// </summary>
    public partial class PasswordView : UserControl
    {
        public PasswordView()
        {
            InitializeComponent();
            
        }
        private void MinusSliderValue(object sender, RoutedEventArgs e)
        {
            
            this.Slider_Lenght_Password.Value--;

        }

       
        private void PlusSliderValue(object sender, RoutedEventArgs e)
        {
           
            this.Slider_Lenght_Password.Value++;
        }

        public void Generate(object sender, RoutedEventArgs e)
        {
            
            PasswordGenerate passwordGenerate = new PasswordGenerate();
            
            RandomPassword.Text = passwordGenerate.GivePassword(int.Parse(RandomPasswordLength.Text), (bool)Numbers.IsChecked, (bool)Low.IsChecked, (bool)Uper.IsChecked, (bool)Special.IsChecked);

            
            Clipboard.SetDataObject(RandomPassword.Text);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public bool ShowHide = true;
        private void btnShowHide_Click(object sender, RoutedEventArgs e)
        {
            if(ShowHide == true)
            {
                dgPass.FontFamily = new FontFamily("Arial");
                btnShowHide.Content = "Hide Passwords";
                ShowHide = false;
            }
            else
            {
                dgPass.FontFamily = new FontFamily("file:///c:/Users/Daisy/source/repos/WPF_CyberWatcher/CyberWatcher/Fonts/#password");
                btnShowHide.Content = "Show Passwords";
                ShowHide = true;

            }
                
           
        }
    }
}
