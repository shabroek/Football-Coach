using System.Collections.ObjectModel;
using FootballCoach.Model;

namespace FootballCoach.ViewModel
{
    public sealed class ApplicationState : IApplicationState
    {
        public ObservableCollection<Player> Players { get; private set; }
        public ObservableCollection<Match> Matches { get; private set; }
    }
}