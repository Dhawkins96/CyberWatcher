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
    public class UserAccountViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource UserAccountItemsCollection;
        public ICollectionView UserAccountSourceCollection => UserAccountItemsCollection.View;

        public UserAccountViewModel()
        {
            ObservableCollection<UserAccountItems> UserAccountItems = new ObservableCollection<UserAccountItems>
            {
                new UserAccountItems { UserAccountName = "Visual Studio 2019", UserAccountImage = "/Assets/vs_icon.png" },
                new UserAccountItems { UserAccountName = "Android Studio", UserAccountImage = "/Assets/android_icon.png" },
                new UserAccountItems { UserAccountName = "Python", UserAccountImage = "/Assets/python_icon.png" },
                new UserAccountItems { UserAccountName = "Swift", UserAccountImage = "/Assets/swift_icon.png" },
                new UserAccountItems { UserAccountName = "Visual Studio Code", UserAccountImage = "/Assets/vsc_icon.png" },
                new UserAccountItems { UserAccountName = "Javascript", UserAccountImage = "/Assets/javascript_icon.png" },
                new UserAccountItems { UserAccountName = "HTML 5", UserAccountImage = "/Assets/html_icon.png" },
                new UserAccountItems { UserAccountName = "Angular", UserAccountImage = "/Assets/angular_icon.png" },
                new UserAccountItems { UserAccountName = "Flutter", UserAccountImage = "/Assets/flutter_icon.png" }
            };

            UserAccountItemsCollection = new CollectionViewSource { Source = UserAccountItems };
            UserAccountItemsCollection.Filter += MenuItems_Filter;

        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                UserAccountItemsCollection.View.Refresh();
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

            UserAccountItems _item = e.Item as UserAccountItems;
            if (_item.UserAccountName.ToUpper().Contains(FilterText.ToUpper()))
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
