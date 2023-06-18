namespace api.fernflowers.com.Data.Entities;

public class DoctorSchedule
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public long DoseId { get; set; }
    public long DoctorId { get; set; }
}
