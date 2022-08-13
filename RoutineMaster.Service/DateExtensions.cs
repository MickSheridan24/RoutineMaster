namespace RoutineMaster.Service
{
    public static class DateExtensions
    {
        public static bool IsToday(this DateTime date){
            var today = DateTime.UtcNow;
            return date.Year == today.Year
            && date.Month == today.Month
            && date.Day == today.Day;
        }

        public static bool IsThisMonth(this DateTime date){
            var today = DateTime.UtcNow;
            return date.Year == today.Year
            && date.Month == today.Month;
        }
    }
}