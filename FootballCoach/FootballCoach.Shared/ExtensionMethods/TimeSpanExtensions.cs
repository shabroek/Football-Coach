using System.Diagnostics.CodeAnalysis;
using System.Globalization;
// ReSharper disable once CheckNamespace


namespace System
{
    [ExcludeFromCodeCoverage]
    public static class TimeSpanExtensions
    {
        public static TimeSpan IgnoreSeconds(this TimeSpan timeSpan)
        {
            return new TimeSpan(timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, 0);
        }

        public static string ToTotalHoursString(this TimeSpan timeSpan, CultureInfo culture)
        {
            return String.Format("{0:N0}:{1:00}", timeSpan.TotalHours, timeSpan.Minutes);
        }
    }
}


