using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballCoach.ExtensionMethods
{
    public static class EnumExtensions
    {
        public static string ToEnumString<T>(this IEnumerable<T> array) where T : struct
        {
            return array.Aggregate(String.Empty, (a, b) => String.IsNullOrEmpty(a) ? Enum.GetName(typeof(T), b) : a + ',' + Enum.GetName(typeof(T), b));
        }
    }
}
