using System;

namespace FootballCoach.Model
{
    public class Goal : IMatchEvent
    {
        public TimeSpan Minute { get; set; }
        public string Remark { get; set; }
        public Player ScoredBy { get; set; }
        public Player AssistedBy { get; set; }
        public GoalType Type { get; set; }
    }
}