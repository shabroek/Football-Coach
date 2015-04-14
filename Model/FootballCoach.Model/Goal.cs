namespace FootballCoach.Model
{
    public class Goal : MatchEvent
    {
        public Player ScoredBy { get; set; }
        public Player AssistedBy { get; set; }
        public GoalType Type { get; set; }
    }
}