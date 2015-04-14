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
            var matches = await _footballService.GetAllMatches();
            Matches = new ObservableCollection<Match>(matches);
        }

        private async Task LoadPlayers()
        {
            var players = await _footballService.GetAllPlayers();
            Players = new ObservableCollection<Player>(players);
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
