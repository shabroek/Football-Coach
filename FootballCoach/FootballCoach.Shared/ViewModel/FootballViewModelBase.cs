using GalaSoft.MvvmLight;

namespace FootballCoach.ViewModel
{
    public class FootballViewModelBase : ViewModelBase
    {
        private string _title;

        public string ApplicationTitle
        {
            get { return "Voetbal"; }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
    }
}