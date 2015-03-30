using System;

namespace FootballCoach.Model
{
    public interface IMatchEvent
    {
        TimeSpan Minute { get; set; }
        string Remark { get; set; }
    }
}