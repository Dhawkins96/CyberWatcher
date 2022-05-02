using CyberWatcher.Model;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CyberWatcher.View
{
    /// <summary>
    /// Interaction logic for StartupView.xaml
    /// </summary>
    public partial class StartupView : UserControl
    {
        
        public StartupView()
        {
            InitializeComponent();
            btnRegUser.IsEnabled = false;
        }

         

        private void txtPassReg_TextChanged(object sender, TextChangedEventArgs e)
        {
            string characters = txtPassReg.Text;
           
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{7,20}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"); ;

            int score = 0;

            if (hasUpperChar.IsMatch(characters))
            {
                score = score + 1;
                ckCaptial.IsChecked = true;
            }
            else
            {
                score = score--;
                ckCaptial.IsChecked = false;
            }
            if (hasNumber.IsMatch(characters))
            {
                score = score + 1;
                ckNumber.IsChecked = true;
            }
            else
            {
                score = score--;
                ckNumber.IsChecked = false;
            }

            if (hasSymbols.IsMatch(characters))
            {
                score = score + 1;
                ckSpecial.IsChecked = true;
            }
            else
            {
                score = score--;
                ckSpecial.IsChecked = false;
            }

            if (hasMiniMaxChars.IsMatch(characters))
            {
                score = score + 1;
                ckLength.IsChecked = true;
            }
            else
            {
                score = score--;
                ckLength.IsChecked = false;
            }

            

            if(score >= 4)
            {
                btnRegUser.IsEnabled = true;
            }
            else
            {
                btnRegUser.IsEnabled = false;
            }


        }

    }
}
