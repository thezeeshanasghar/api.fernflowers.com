namespace api.fernflowers.com.Data.Entities;

public class PatientSchedule
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public long DoseId { get; set; }
    public long DoctorId { get; set; }
    public long ChildId { get; set; }
    public bool IsSkip { get; set; }
    public bool IsDone { get; set; }
}
