using System.Diagnostics.CodeAnalysis;
// ReSharper disable once CheckNamespace


namespace System
{
    public static class DateTimeExtensions
    {
        [ExcludeFromCodeCoverage]
        public static DateTime IgnoreSeconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }
    }
}