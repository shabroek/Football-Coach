using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FootballCoach.Http;
using FootballCoach.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FootballCoach.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFootballService _footballService;
        private ObservableCollection<Match> _matches;
        private ObservableCollection<Player> _players;

        private ICommand _loadPlayersCommand;
        private ICommand _loadMatchesCommand;

        public MainViewModel(IFootballService footballService)
        {
            _footballService = footballService;
            Matches = new ObservableCollection<Match>();
            Players = new ObservableCollection<Player>();
        }

        private async Task LoadMatches()
        {
            Matches = new ObservableCollection<Match>(await _footballService.GetAllMatches());
        }

        private async Task LoadPlayers()
        {
            Players = new ObservableCollection<Player>(await _footballService.GetAllPlayers());
        }

        public ObservableCollection<Match> Matches
        {
            get { return _matches; }
            set
            {
                _matches = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                RaisePropertyChanged();
            }
        }

        public ICommand LoadPlayersCommand
        {
            get { return _loadPlayersCommand ?? (_loadPlayersCommand = new RelayCommand(async () => await LoadPlayers())); }
        }

        public ICommand LoadMatchesCommand
        {
            get { return _loadMatchesCommand ?? (_loadMatchesCommand = new RelayCommand(async () => await LoadMatches())); }
        }
    }
}
