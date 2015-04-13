using System.Collections.ObjectModel;
using FootballCoach.Model;

namespace FootballCoach.ViewModel
{
    public class MatchViewModel : FootballViewModelBase
    {
        private ObservableCollection<MatchEvent> _matchEvents;

        public MatchViewModel()
        {
            Title = "Wedstrijden";
        }

        public ObservableCollection<MatchEvent> MatchEvents
        {
            get { return _matchEvents; }
            set
            {
                _matchEvents = value;
                RaisePropertyChanged();
            }
        }

        public void AddMatchEvent(MatchEvent matchEvent)
        {
            MatchEvents.Add(matchEvent);
        }

        public void AddMatchEventAt(int index, MatchEvent matchEvent)
        {
            MatchEvents.Insert(index, matchEvent);
        }

        public void RemoveMatchEvent(MatchEvent matchEvent)
        {
            MatchEvents.Remove(matchEvent);
        }
    }
}
