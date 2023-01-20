namespace api.fernflowers.com.Data.Entities
{
    public class Clinictiming
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public string Session { get; set; }
        public bool IsOpen { get; set; }
        public int ClinicId { get; set; }
        //         public virtual Clinic Clinic { get; set; }
    }
}
