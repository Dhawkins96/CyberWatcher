using CyberWatcher.Helper;
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
                    new MenuItems { MenuName = "Home", MenuImage = "/Assets/IconHome.png" },
                    new MenuItems { MenuName = "Password", MenuImage = "/Assets/IconLock.png" },
                    new MenuItems { MenuName = "Help", MenuImage = "/Assets/IconHelp.png" }, 
                    new MenuItems { MenuName = "User Account", MenuImage = "/Assets/IconUser.png" }
                };
                MenuItemsCollection = new CollectionViewSource { Source = menuItems };
                SelectedViewModel = new HomeViewModel();
            }
        }

        public NavigationViewModel()
        {
            
            LoadWindow();
            
        }

        // Implement interface member for INotifyPropertyChanged.
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
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

        private ICommand _helpCommand;
        public ICommand HelpCommand
        {
            get
            {
                if (_helpCommand == null)
                {
                    _helpCommand = new RelayCommand(param => GetHelpDetails((string)param));
                }
                return _helpCommand;
            }
        }

       

        public void GetHelpDetails(string para)
        {
            SelectedViewModel = new HelpDetailsViewModel(para);
        }

        // Show Home View
        private void ShowHome()
        {
            if(StaticUtilities.UserID != 0)
            {
                SelectedViewModel = new HomeViewModel();
            }
        }

        // Show Help View
        private void BackHelp()
        {
            if (StaticUtilities.UserID != 0)
            {
                SelectedViewModel = new HelpViewModel();
            }
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
        private ICommand _backHelpCommand;
        public ICommand BackHelpCommand
        {
            get
            {
                if (_backHelpCommand == null)
                {
                    _backHelpCommand = new RelayCommand(p => BackHelp());
                }
                return _backHelpCommand;
            }
        }

        // Close App
        public void CloseApp(object obj)
        {
            MainWindow win = obj as MainWindow;
            win.Close();
        }

        public void MaxApp(object obj)
        {
            MainWindow win = obj as MainWindow;
            if (win.WindowState != System.Windows.WindowState.Maximized)
            {
                win.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                win.WindowState = System.Windows.WindowState.Normal;
            }
               
                
        }

        public void MinApp(object obj)
        {
            MainWindow win = obj as MainWindow;
            win.WindowState = System.Windows.WindowState.Minimized;
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

        private ICommand _maxCommand;
        public ICommand MaxAppCommand
        {
            get
            {
                if (_maxCommand == null)
                {
                    _maxCommand = new RelayCommand(p => MaxApp(p));
                }
                return _maxCommand;
            }
        }
        private ICommand _minCommand;
        public ICommand MinAppCommand
        {
            get
            {
                if (_minCommand == null)
                {
                    _minCommand = new RelayCommand(p => MinApp(p));
                }
                return _minCommand;
            }
        }
    }
}
