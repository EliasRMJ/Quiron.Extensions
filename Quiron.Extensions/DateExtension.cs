namespace Quiron.Extensions
{
    public static class DateExtension
    {
        public static bool EqualToday(this DateOnly date)
        {
            return date == DateOnly.FromDateTime(DateTime.Now);
        }

        public static bool EqualTodayOrMinus(this DateOnly date)
        {
            return date <= DateOnly.FromDateTime(DateTime.Now);
        }

        public static bool EqualTodayOrBigger(this DateOnly date)
        {
            return date >= DateOnly.FromDateTime(DateTime.Now);
        }

        public static bool IsDateBigger(this DateOnly date)
        {
            return date > DateOnly.FromDateTime(DateTime.Now);
        }

        public static bool IsDateMinus(this DateOnly date)
        {
            return date < DateOnly.FromDateTime(DateTime.Now);
        }

        public static int Weekly(this DateOnly date)
        {
            var weekly = date.Day / 7;
            if (date.Day % 7 > 0) weekly++;
            return weekly;
        }
    }
}