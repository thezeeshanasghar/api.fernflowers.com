namespace api.fernflowers.com.ModelDTO;

public class DoctorScheduleDTO
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public long DoseId { get; set; }
    public long DoctorId { get; set; }

    public DoseDTO Dose { get; set; }
}
