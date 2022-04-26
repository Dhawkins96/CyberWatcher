using CyberWatcher.Model.User;
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
        }

        private void RichTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
            }
        }

    }
}
