using CyberWatcher.Helper;
using CyberWatcher.Model;
using CyberWatcher.Model.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CyberWatcher.ViewModel
{
    public class HelpViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource HelpItemsCollection;
        public ICollectionView HelpSourceCollection => HelpItemsCollection.View;

       
        

        public HelpViewModel()
        {
            ObservableCollection<HelpModel> HelpItems = new ObservableCollection<HelpModel>(HelpChoices.ListHelpValues);
            

            HelpItemsCollection = new CollectionViewSource { Source = HelpItems };

            
        }
 

       
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
