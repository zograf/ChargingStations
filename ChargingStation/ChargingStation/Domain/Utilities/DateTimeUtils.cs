namespace ChargingStation.Domain.Utilities
{
    public class DateTimeUtils
    {
        public static bool IsDateTimeOverlap(Tuple<DateTime, DateTime> first, Tuple<DateTime, DateTime> second)
        {
            return MaxDate(first.Item1, second.Item1) < MinDate(first.Item2, second.Item2);

        }

        public static bool IsDateTimeOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return MaxDate(start1, start2) < MinDate(end1, end2);

        }

        public static DateTime MaxDate(DateTime time1, DateTime time2)
        {
            return (time1 > time2 ? time1 : time2);
        }

        public static DateTime MinDate(DateTime time1, DateTime time2)
        {
            return (time1 < time2 ? time1 : time2);
        }

        public static bool IsDay(DateTime time)
        {
            return time.Hour < 20 && time.Hour >= 6  ? true: false;
        }
    }
}
