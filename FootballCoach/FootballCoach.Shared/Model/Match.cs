using System;
using System.Collections.Generic;

namespace FootballCoach.Model
{
    public class Match
    {
        public DateTime Date { get; set; }
        public Team Opponent { get; set; }
        public IEnumerable<IMatchEvent> Events { get; set; }
        public bool IsHomeMatch { get; set; }
    }
}
