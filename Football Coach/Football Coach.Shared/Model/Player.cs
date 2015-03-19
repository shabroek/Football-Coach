using System;
using System.Collections.Generic;

namespace Football_Coach.Model
{
    public class Player
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Position> PreferredPosition { get; set; }
    }
}