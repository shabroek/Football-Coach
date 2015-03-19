using System;

namespace Football_Coach.Model
{
    public class Substitution : IMatchEvent
    {
        public TimeSpan Minute { get; set; }
        public string Remark { get; set; }
        public Player PlayerOff { get; set; }
        public Player PlayerOn { get; set; }
        public SubstitutionReason Reason { get; set; }
    }
}