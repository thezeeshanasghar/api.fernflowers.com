namespace api.fernflowers.com.ModelDTO;
public class PatientDoseScheduleDTO
{
    public long ScheduleId { get; set; }
    public string DoseName { get; set; }
    // public DateOnly Date { get; set; }
    public bool IsSkip { get; set; }
    public bool IsDone { get; set; }
    public string BrandName {get;set;}
}