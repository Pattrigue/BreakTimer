using System;

namespace BreakTimer.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToTimestamp(this double time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);

            return $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds:000}";
        }

        public static string ToTimestamp(this double time, string zeroSymbol)
        {
            return time == 0 ? zeroSymbol : time.ToTimestamp();
        }
    }
}
