﻿using CyberWatcher.Helper;
using CyberWatcher.Model;
using CyberWatcher.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace CyberWatcher.ViewModel
{
    class NavigationViewModel : INotifyPropertyChanged
    {
        // CollectionViewSource enables XAML code to set the commonly used CollectionView properties,
        // passing these settings to the underlying view.
        private CollectionViewSource MenuItemsCollection;

        // ICollectionView enables collections to have the functionalities of current record management,
        // custom sorting, filtering, and grouping.
        public ICollectionView SourceCollection => MenuItemsCollection.View;

        public void LoadWindow()
        {
            if (StaticUtilities.UserID == 0)
            {
                ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
                {

                   
                    
                };
                MenuItemsCollection = new CollectionViewSource { Source = menuItems };
                SelectedViewModel = new StartupViewModel();
            }
            else
            {
                ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
                {
                    new MenuItems { MenuName = "Home", MenuImage = "/Assets/Home_Icon.png" },
                    new MenuItems { MenuName = "Password", MenuImage = "/Assets/Desktop_Icon.png" },
                    new MenuItems { MenuName = "Help", MenuImage = "/Assets/Music_Icon.png" },
                    new MenuItems { MenuName = "User Account", MenuImage = "/Assets/Download_Icon.png" }
                };
                MenuItemsCollection = new CollectionViewSource { Source = menuItems };
                SelectedViewModel = new HomeViewModel();
            }
        }

        public NavigationViewModel()
        {
            // ObservableCollection represents a dynamic data collection that provides notifications when items
            // get added, removed, or when the whole list is refreshed.


            LoadWindow();
            
            MenuItemsCollection.Filter += MenuItems_Filter;

            // Set Startup Page
            
        }

        // Implement interface member for INotifyPropertyChanged.
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Text Search Filter.
        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                MenuItemsCollection.View.Refresh();
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

            MenuItems _item = e.Item as MenuItems;
            if (_item.MenuName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        // Select ViewModel
        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set { _selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        // Switch Views
        public void SwitchViews(object parameter)
        {
            switch (parameter)
            {
                case "Home":
                    SelectedViewModel = new HomeViewModel();
                    break;
                case "Password":
                    SelectedViewModel = new PasswordViewModel();
                    break;
                case "User Account":
                    SelectedViewModel = new UserAccountViewModel();
                    break;
                case "Help":
                    SelectedViewModel = new HelpViewModel();
                    break;
                default:
                    SelectedViewModel = new HomeViewModel();
                    break;
            }
        }

        // Menu Button Command
        private ICommand _menucommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_menucommand == null)
                {
                    _menucommand = new RelayCommand(param => SwitchViews(param));
                }
                return _menucommand;
            }
        }

        

        // Show Home View
        private void ShowHome()
        {
            SelectedViewModel = new HomeViewModel();
        }

        // Back button Command
        private ICommand _backHomeCommand;
        public ICommand BackHomeCommand
        {
            get
            {
                if (_backHomeCommand == null)
                {
                    _backHomeCommand = new RelayCommand(p => ShowHome());
                }
                return _backHomeCommand;
            }
        }

        // Close App
        public void CloseApp(object obj)
        {
            MainWindow win = obj as MainWindow;
            win.Close();
        }

        // Close App Command
        private ICommand _closeCommand;
        public ICommand CloseAppCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(p => CloseApp(p));
                }
                return _closeCommand;
            }
        }

    }
}
