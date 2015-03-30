using System.Collections.ObjectModel;
using FootballCoach.Model;

namespace FootballCoach.ViewModel
{
    public interface IApplicationState
    {
        ObservableCollection<Player> Players { get; }
        ObservableCollection<Match> Matches { get; }
    }
}