using Football_Coach.Model;

namespace Football_Coach.ViewModel
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