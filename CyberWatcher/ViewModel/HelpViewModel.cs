using CyberWatcher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CyberWatcher.ViewModel
{
    public class HelpViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _helpNav = new ObservableCollection<string>();
        private string _selectedHelp;

        public HelpViewModel()
        {
            HelpNav.Add("How to use application?");
            HelpNav.Add("What is NMAP?");
            HelpNav.Add("How to improve your cyberawareness?");
            HelpNav.Add("10 basic tips for cyber security");
        }

        public ObservableCollection<string> HelpNav
        {
            get { return _helpNav; }
            set
            {
                _helpNav = value;
            }
        }
        public string SelectedHelp
        {
            get { return _selectedHelp; }
            set
            {
                if (_selectedHelp != value)
                {
                    _selectedHelp = value;


                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
