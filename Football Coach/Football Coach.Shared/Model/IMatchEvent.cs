using System;

namespace Football_Coach.Model
{
    public interface IMatchEvent
    {
        TimeSpan Minute { get; set; }
        string Remark { get; set; }
    }
}