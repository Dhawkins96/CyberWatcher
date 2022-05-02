using CyberWatcher.Model.User;
using System.Collections.Generic;
using System.ComponentModel;

namespace CyberWatcher.ViewModel
{
    public class HelpDetailsViewModel : INotifyPropertyChanged
    {

        public HelpDetailsViewModel(string title)
        {
            List<HelpModel> helps = new List<HelpModel>(HelpChoices.ListHelpValues);

            for(int i = 0; i < helps.Count; i++)
            {
                if(title == helps[i].Title)
                {
                    TxtDescription = helps[i].Description;
                }                
            }
            

            TxtTitle = title;
            
        }


        private string _txtDescription;
        public string TxtDescription
        {
            get { return _txtDescription; }
            set
            {
                if (_txtDescription != value)
                {
                    _txtDescription = value;
                    OnPropertyChanged("TxtDescription");
                }
            }
        }

        private string _txtTitle;
        public string TxtTitle
        {
            get { return _txtTitle; }
            set
            {
                if(_txtTitle != value)
                {
                    _txtTitle = value;
                    OnPropertyChanged("TxtTitle");
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
