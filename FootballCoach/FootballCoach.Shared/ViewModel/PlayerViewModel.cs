using FootballCoach.Model;

namespace FootballCoach.ViewModel
{
    public class PlayerViewModel : FootballViewModelBase
    {
        private Player _player;

        public PlayerViewModel()
        {
            Title = "Speler";
        }

        public Player Player
        {
            get { return _player; }
            set
            {
                _player = value;
                RaisePropertyChanged();
            }
        }
    }
}