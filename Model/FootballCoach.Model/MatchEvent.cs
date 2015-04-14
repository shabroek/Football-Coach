using System;

namespace FootballCoach.Model
{
    public class MatchEvent
    {
        public int MatchEventId { get; set; }
        public TimeSpan Minute { get; set; }
        public string Remark { get; set; }
    }
}