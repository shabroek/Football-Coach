using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FootballCoach.Model;
using GalaSoft.MvvmLight;

namespace FootballCoach.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Match> _matches;
        private ObservableCollection<Player> _players;

        public MainViewModel()
        {
            //if (IsInDesignMode)
            //{
                Matches = new ObservableCollection<Match>
                {
                    new Match
                    {
                        Date = DateTime.Today,
                        Events = new ObservableCollection<IMatchEvent>(),
                        Opponent = new Team {Name = "Sarto 10"},
                        IsHomeMatch = false
                    },
                    new Match
                    {
                        Date = DateTime.Today.AddDays(-7),
                        Events = new ObservableCollection<IMatchEvent>(),
                        Opponent = new Team {Name = "SVSSS 4"},
                        IsHomeMatch = true
                    },
                    new Match
                    {
                        Date = DateTime.Today.AddDays(-14),
                        Events = new ObservableCollection<IMatchEvent>(),
                        Opponent = new Team {Name = "Wilhelmina Boys 8"},
                        IsHomeMatch = false
                    },
                    new Match
                    {
                        Date = DateTime.Today.AddDays(-21),
                        Events = new ObservableCollection<IMatchEvent>(),
                        Opponent = new Team {Name = "Spoordonkse Boys 3"},
                        IsHomeMatch = true
                    },
                    new Match
                    {
                        Date = DateTime.Today.AddDays(-28),
                        Events = new ObservableCollection<IMatchEvent>(),
                        Opponent = new Team {Name = "Oisterwijk 2"},
                        IsHomeMatch = false
                    }
                };
                Players = new ObservableCollection<Player>
                {
                    new Player
                    {
                        FirstName = "Sander",
                        LastName = "van Broekhoven",
                        DateOfBirth = new DateTime(1984, 5, 11)
                    },
                    new Player
                    {
                        FirstName = "Erik",
                        LastName = "van Duppen",
                        DateOfBirth = new DateTime(1981, 8, 9)
                    },
                    new Player
                    {
                        FirstName = "Marcel",
                        LastName = "van Broekhoven",
                        DateOfBirth = new DateTime(1960, 8, 4)
                    },
                    new Player
                    {
                        FirstName = "Bart",
                        LastName = "de Groof",
                        DateOfBirth = new DateTime(1984, 8, 14)
                    },
                    new Player
                    {
                        FirstName = "Max",
                        LastName = "Haen",
                        DateOfBirth = new DateTime(1984, 9, 1)
                    },
                    new Player
                    {
                        FirstName = "Mark",
                        LastName = "Didden",
                        DateOfBirth = new DateTime(1984, 7, 11)
                    },
                };

            //}
            //else
            //{
            //    Matches = new ObservableCollection<Match>();
            //    Players = new ObservableCollection<Player>();
            //}
        }

        public async Task LoadMatches()
        {
            await Task.FromResult(1);
        }

        public async Task LoadPlayers()
        {
            await Task.FromResult(1);

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
    }
}
