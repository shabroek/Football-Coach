using System.Collections.Generic;

namespace FootballCoach.Model
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}