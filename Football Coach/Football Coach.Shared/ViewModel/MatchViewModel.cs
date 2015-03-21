using System.Collections.ObjectModel;
using Football_Coach.Model;

namespace Football_Coach.ViewModel
{
    public class MatchViewModel : FootballViewModelBase
    {
        private ObservableCollection<IMatchEvent> _matchEvents;

        public MatchViewModel()
        {
            Title = "Wedstrijden";
        }

        public ObservableCollection<IMatchEvent> MatchEvents
        {
            get { return _matchEvents; }
            set
            {
                _matchEvents = value;
                RaisePropertyChanged();
            }
        }

        


        public void AddMatchEvent(IMatchEvent matchEvent)
        {
            MatchEvents.Add(matchEvent);
        }

        public void AddMatchEventAt(int index, IMatchEvent matchEvent)
        {
            MatchEvents.Insert(index, matchEvent);
        }

        public void RemoveMatchEvent(IMatchEvent matchEvent)
        {
            MatchEvents.Remove(matchEvent);
        }
    }
}
