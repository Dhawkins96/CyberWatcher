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
        private CollectionViewSource HelpItemsCollection;
        public ICollectionView HelpSourceCollection => HelpItemsCollection.View;

        public HelpViewModel()
        {
            ObservableCollection<HelpItems> HelpItems = new ObservableCollection<HelpItems>
            {

                new HelpItems { HelpName = "Bass", HelpImage = "/Assets/note_icon.png" },
                new HelpItems { HelpName = "Beats", HelpImage = "/Assets/note_icon.png" },
                new HelpItems { HelpName = "Electronic", HelpImage = "/Assets/note_icon.png" },
                new HelpItems { HelpName = "Hip hop", HelpImage = "/Assets/note_icon.png" },
                new HelpItems { HelpName = "Deep House", HelpImage = "/Assets/note_icon.png" },
                new HelpItems { HelpName = "Instrumental", HelpImage = "/Assets/note_icon.png" }

            };

            HelpItemsCollection = new CollectionViewSource { Source = HelpItems };
            HelpItemsCollection.Filter += MenuItems_Filter;
        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                HelpItemsCollection.View.Refresh();
                OnPropertyChanged("FilterText");
            }
        }

        private void MenuItems_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            HelpItems _item = e.Item as HelpItems;
            if (_item.HelpName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
