using System.Collections.ObjectModel;
using Football_Coach.Model;

namespace Football_Coach.ViewModel
{
    public interface IApplicationState
    {
        ObservableCollection<Player> Players { get; }
        ObservableCollection<Match> Matches { get; }
    }
}