using System;

namespace FootballCoach.Model
{
    public class Player
    {
        public override string ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }

        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Position PreferredPosition { get; set; }
    }
}