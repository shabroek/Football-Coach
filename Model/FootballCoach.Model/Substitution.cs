namespace FootballCoach.Model
{
    public class Substitution : MatchEvent
    {
        public Player PlayerOff { get; set; }
        public Player PlayerOn { get; set; }
        public SubstitutionReason Reason { get; set; }
    }
}