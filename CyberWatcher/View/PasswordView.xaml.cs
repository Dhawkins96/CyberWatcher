using CyberWatcher.Model.Password_Manager;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

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

    }
}
