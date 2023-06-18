namespace api.fernflowers.com.Data.Entities
{
    public class ClinicTiming
    {
        public long Id { get; set; }
        public DayOfWeek Day { get; set; }
        public Session Session { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public long ClinicId { get; set; }
    }
}

public enum DayOfWeek
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

public enum Session
{
    Morning,
    Evening
}