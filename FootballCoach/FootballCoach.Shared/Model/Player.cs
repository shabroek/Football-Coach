﻿using System;
using System.Collections.Generic;

namespace FootballCoach.Model
{
    public class Player
    {
        public override string ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Position> PreferredPosition { get; set; }
    }
}