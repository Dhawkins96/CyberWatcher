using CyberWatcher.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace CyberWatcher.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource SettingsItemsCollection;
        public ICollectionView SettingsSourceCollection => SettingsItemsCollection.View;

        public SettingsViewModel()
        {
            ObservableCollection<SettingsItems> SettingsItems = new ObservableCollection<SettingsItems>
            {

                new SettingsItems { SettingsName = "Books", SettingsImage = "/Assets/book_icon.png" },
                new SettingsItems { SettingsName = "Studio", SettingsImage = "/Assets/studio_icon.png" },
                new SettingsItems { SettingsName = "Export", SettingsImage = "/Assets/export_icon.png" },
                new SettingsItems { SettingsName = "Print", SettingsImage = "/Assets/print_icon.png" },
                new SettingsItems { SettingsName = "Orders", SettingsImage = "/Assets/order_icon.png" },
                new SettingsItems { SettingsName = "Stocks", SettingsImage = "/Assets/stock_icon.png" },
                new SettingsItems { SettingsName = "Invoice", SettingsImage = "/Assets/invoice_icon.png" }

            };

            SettingsItemsCollection = new CollectionViewSource { Source = SettingsItems };
            SettingsItemsCollection.Filter += MenuItems_Filter;

        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                SettingsItemsCollection.View.Refresh();
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

            SettingsItems _item = e.Item as SettingsItems;
            if (_item.SettingsName.ToUpper().Contains(FilterText.ToUpper()))
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
