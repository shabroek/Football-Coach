using System.Collections.ObjectModel;
using Football_Coach.Model;

namespace Football_Coach.ViewModel
{
    public sealed class ApplicationState : IApplicationState
    {
        public ObservableCollection<Player> Players { get; private set; }
        public ObservableCollection<Match> Matches { get; private set; }
    }
}