using System;
using System.Collections.Generic;

namespace FootballCoach.Model
{
    public class Match
    {
        public int MatchId { get; set; }
        public DateTime Date { get; set; }
        public Team Opponent { get; set; }
        public bool IsHomeMatch { get; set; }

        public ICollection<MatchEvent> Events { get; set; }
    }
}
