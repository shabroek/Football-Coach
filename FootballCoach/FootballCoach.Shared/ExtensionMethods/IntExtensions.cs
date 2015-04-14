// ReSharper disable once CheckNamespace
namespace System
{
    public static class IntExtensions
    {
        public static int GetNextTenth(this int value)
        {
            return Convert.ToInt32(Math.Ceiling(((value + 0.5) / 10.0d)) * 10);
        }

        public static short GetNextTenth(this short value)
        {
            return Convert.ToInt16(Math.Ceiling(((value + 0.5) / 10.0d)) * 10);
        }

        public static long GetNextTenth(this long value)
        {
            return Convert.ToInt64(Math.Ceiling(((value + 0.5) / 10.0d)) * 10);
        }
    }
}
