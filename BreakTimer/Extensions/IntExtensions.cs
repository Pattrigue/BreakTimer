namespace BreakTimer.Extensions
{
    public static class IntExtensions
    {
        public static string ToTimestamp(this int totalSeconds)
        {
            int seconds = totalSeconds % 60;
            int minutes = totalSeconds / 60;

            return $"{minutes:00}:{seconds:00}";
        }

        public static string ToTimestamp(this int totalSeconds, string zeroSymbol)
        {
            return totalSeconds == 0 ? zeroSymbol : totalSeconds.ToTimestamp();
        }
    }
}
